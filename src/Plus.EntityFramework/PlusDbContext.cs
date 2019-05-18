using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Plus.Dependency;
using Plus.Domain.Entities;
using Plus.Domain.Entities.Auditing;
using Plus.Domain.Repositories;
using Plus.Domain.Uow;
using Plus.EntityFramework.Extensions;
using Plus.Event.Bus;
using Plus.Event.Bus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Plus.EntityFramework
{
    /// <summary>
    /// PlusDbContext
    /// </summary>
    public abstract class PlusDbContext : DbContext, ITransientDependency
    {
        private class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;

            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                {
                    return _newValue;
                }
                return base.Visit(node);
            }
        }

        public IEntityChangeEventHelper EntityChangeEventHelper { get; set; }

        public ILogger Logger { get; set; }

        public IEventBus EventBus { get; set; }

        public IGuidGenerator GuidGenerator { get; set; }

        public ICurrentUnitOfWorkProvider CurrentUnitOfWorkProvider { get; set; }

        protected virtual bool IsSoftDeleteFilterEnabled => CurrentUnitOfWorkProvider?.Current?.IsFilterEnabled(PlusDataFilters.SoftDelete) == true;

        protected PlusDbContext(DbContextOptions options)
            : base(options)
        {
            InitializeDbContext();
        }

        private void InitializeDbContext()
        {
            SetNullsForInjectedProperties();
        }

        private void SetNullsForInjectedProperties()
        {
            Logger = NullLogger.Instance;
            EntityChangeEventHelper = NullEntityChangeEventHelper.Instance;
            GuidGenerator = SequentialGuidGenerator.Instance;
            EventBus = NullEventBus.Instance;
        }

        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType) where TEntity : class
        {
            if (entityType.BaseType != null || !ShouldFilterEntity<TEntity>(entityType))
            {
                return;
            }
            Expression<Func<TEntity, bool>> expression = CreateFilterExpression<TEntity>();
            if (expression != null)
            {
                if (entityType.IsQueryType)
                {
                    modelBuilder.Query<TEntity>().HasQueryFilter(expression);
                }
                else
                {
                    modelBuilder.Entity<TEntity>().HasQueryFilter(expression);
                }
            }
        }

        protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }
            return false;
        }

        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>() where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> softDeleteFilter = e => !((ISoftDelete)e).IsDeleted || ((ISoftDelete)e).IsDeleted != IsSoftDeleteFilterEnabled;
                expression = expression == null ? softDeleteFilter : CombineExpressions(expression, softDeleteFilter);
            }

            return expression;
        }

        public override int SaveChanges()
        {
            try
            {
                var changeReport = ApplyAbpConcepts();
                var result = base.SaveChanges();
                EntityChangeEventHelper.TriggerEvents(changeReport);
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new PlusDbConcurrencyException(ex.Message, ex);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var changeReport = ApplyAbpConcepts();
                var result = await base.SaveChangesAsync(cancellationToken);
                await EntityChangeEventHelper.TriggerEventsAsync(changeReport);
                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new PlusDbConcurrencyException(ex.Message, ex);
            }
        }

        protected virtual EntityChangeReport ApplyAbpConcepts()
        {
            var changeReport = new EntityChangeReport();

            var userId = GetAuditUserId();

            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                if (entry.State != EntityState.Modified && entry.CheckOwnedEntityChange())
                {
                    Entry(entry.Entity).State = EntityState.Modified;
                }

                ApplyAbpConcepts(entry, userId, changeReport);
            }

            return changeReport;
        }

        protected virtual void ApplyAbpConcepts(EntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyPlusConceptsForAddedEntity(entry, userId, changeReport);
                    break;
                case EntityState.Modified:
                    ApplyPlusConceptsForAddedEntity(entry, userId, changeReport);
                    break;
                case EntityState.Deleted:
                    ApplyPlusConceptsForAddedEntity(entry, userId, changeReport);
                    break;
            }

            AddDomainEvents(changeReport.DomainEvents, entry.Entity);
        }

        protected virtual void ApplyPlusConceptsForAddedEntity(EntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            CheckAndSetId(entry);
            SetCreationAuditProperties(entry.Entity, userId);
            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Created));
        }

        protected virtual void ApplyPlusConceptsForModifiedEntity(EntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            SetModificationAuditProperties(entry.Entity, userId);
            if (entry.Entity is ISoftDelete && entry.Entity.As<ISoftDelete>().IsDeleted)
            {
                SetDeletionAuditProperties(entry.Entity, userId);
                changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Deleted));
            }
            else
            {
                changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Updated));
            }
        }

        protected virtual void ApplyPlusConceptsForDeletedEntity(EntityEntry entry, long? userId, EntityChangeReport changeReport)
        {
            if (IsHardDeleteEntity(entry))
            {
                changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Deleted));
                return;
            }

            CancelDeletionForSoftDelete(entry);
            SetDeletionAuditProperties(entry.Entity, userId);
            changeReport.ChangedEntities.Add(new EntityChangeEntry(entry.Entity, EntityChangeType.Deleted));
        }

        protected virtual bool IsHardDeleteEntity(EntityEntry entry)
        {
            if (CurrentUnitOfWorkProvider?.Current?.Items == null)
            {
                return false;
            }

            if (!CurrentUnitOfWorkProvider.Current.Items.ContainsKey(UnitOfWorkExtensionDataTypes.HardDelete))
            {
                return false;
            }

            var hardDeleteItems = CurrentUnitOfWorkProvider.Current.Items[UnitOfWorkExtensionDataTypes.HardDelete];
            if (!(hardDeleteItems is HashSet<string> objects))
            {
                return false;
            }

            var hardDeleteKey = EntityHelper.GetHardDeleteKey(entry.Entity);
            return objects.Contains(hardDeleteKey);
        }

        protected virtual void AddDomainEvents(List<DomainEventEntry> domainEvents, object entityAsObj)
        {
            var generatesDomainEventsEntity = entityAsObj as IGeneratesDomainEvents;
            if (generatesDomainEventsEntity == null)
            {
                return;
            }

            if (generatesDomainEventsEntity.DomainEvents.IsNullOrEmpty())
            {
                return;
            }

            domainEvents.AddRange(generatesDomainEventsEntity.DomainEvents.Select(eventData => new DomainEventEntry(entityAsObj, eventData)));
            generatesDomainEventsEntity.DomainEvents.Clear();
        }

        protected virtual long? GetAuditUserId()
        {
            return null;
        }

        protected virtual void CheckAndSetId(EntityEntry entry)
        {
            var entity = entry.Entity as IEntity<Guid>;
            if (entity != null && entity.Id == Guid.Empty)
            {
                var idPropertyEntry = entry.Property("Id");

                if (idPropertyEntry != null && idPropertyEntry.Metadata.ValueGenerated == ValueGenerated.Never)
                {
                    entity.Id = GuidGenerator.Create();
                }
            }
        }

        protected virtual void SetCreationAuditProperties(object entityAsObj, long? userId)
        {
            EntityAuditingHelper.SetCreationAuditProperties(entityAsObj, userId);
        }

        protected virtual void SetModificationAuditProperties(object entityAsObj, long? userId)
        {
            EntityAuditingHelper.SetModificationAuditProperties(entityAsObj, userId);
        }

        protected virtual void CancelDeletionForSoftDelete(EntityEntry entry)
        {
            if (!(entry.Entity is ISoftDelete))
            {
                return;
            }

            entry.Reload();
            entry.State = EntityState.Modified;
            entry.Entity.As<ISoftDelete>().IsDeleted = true;
        }

        protected virtual void SetDeletionAuditProperties(object entityAsObj, long? userId)
        {
            if (entityAsObj is IHasDeletionTime)
            {
                var entity = entityAsObj.As<IHasDeletionTime>();

                if (entity.DeletionTime == null)
                {
                    entity.DeletionTime = DateTime.Now;
                }
            }

            if (entityAsObj is IHasDeletionTime)
            {
                var entity = entityAsObj.As<IDeletionAudited>();

                if (entity.DeleterUserId != null)
                {
                    return;
                }

                if (userId == null)
                {
                    entity.DeleterUserId = null;
                    return;
                }
            }
        }

        protected virtual Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T));
            ReplaceExpressionVisitor replaceExpressionVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameterExpression);
            Expression left = replaceExpressionVisitor.Visit(expression1.Body);
            ReplaceExpressionVisitor replaceExpressionVisitor2 = new ReplaceExpressionVisitor(expression2.Parameters[0], parameterExpression);
            Expression right = replaceExpressionVisitor2.Visit(expression2.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), new ParameterExpression[1]
            {
            parameterExpression
            });
        }
    }
}