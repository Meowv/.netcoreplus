using Plus.Dependency;
using Plus.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plus.Event.Bus.Entities
{
    public class EntityChangeEventHelper : ITransientDependency, IEntityChangeEventHelper
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public IEventBus EventBus { get; set; }

        public EntityChangeEventHelper(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
            EventBus = NullEventBus.Instance;
        }

        public virtual void TriggerEvents(EntityChangeReport changeReport)
        {
            TriggerEventsInternal(changeReport);
            if (!changeReport.IsEmpty() && _unitOfWorkManager.Current != null)
            {
                _unitOfWorkManager.Current.SaveChanges();
            }
        }

        public Task TriggerEventsAsync(EntityChangeReport changeReport)
        {
            TriggerEventsInternal(changeReport);
            if (changeReport.IsEmpty() || _unitOfWorkManager.Current == null)
            {
                return Task.FromResult(0);
            }
            return _unitOfWorkManager.Current.SaveChangesAsync();
        }

        public virtual void TriggerEntityCreatingEvent(object entity)
        {
            TriggerEventWithEntity(typeof(EntityCreatingEventData<>), entity, triggerInCurrentUnitOfWork: true);
        }

        public virtual void TriggerEntityCreatedEventOnUowCompleted(object entity)
        {
            TriggerEventWithEntity(typeof(EntityCreatedEventData<>), entity, triggerInCurrentUnitOfWork: false);
        }

        public virtual void TriggerEntityUpdatingEvent(object entity)
        {
            TriggerEventWithEntity(typeof(EntityUpdatingEventData<>), entity, triggerInCurrentUnitOfWork: true);
        }

        public virtual void TriggerEntityUpdatedEventOnUowCompleted(object entity)
        {
            TriggerEventWithEntity(typeof(EntityUpdatedEventData<>), entity, triggerInCurrentUnitOfWork: false);
        }

        public virtual void TriggerEntityDeletingEvent(object entity)
        {
            TriggerEventWithEntity(typeof(EntityDeletingEventData<>), entity, triggerInCurrentUnitOfWork: true);
        }

        public virtual void TriggerEntityDeletedEventOnUowCompleted(object entity)
        {
            TriggerEventWithEntity(typeof(EntityDeletedEventData<>), entity, triggerInCurrentUnitOfWork: false);
        }

        public virtual void TriggerEventsInternal(EntityChangeReport changeReport)
        {
            TriggerEntityChangeEvents(changeReport.ChangedEntities);
            TriggerDomainEvents(changeReport.DomainEvents);
        }

        protected virtual void TriggerEntityChangeEvents(List<EntityChangeEntry> changedEntities)
        {
            foreach (EntityChangeEntry changedEntity in changedEntities)
            {
                switch (changedEntity.ChangeType)
                {
                    case EntityChangeType.Created:
                        TriggerEntityCreatingEvent(changedEntity.Entity);
                        TriggerEntityCreatedEventOnUowCompleted(changedEntity.Entity);
                        break;
                    case EntityChangeType.Updated:
                        TriggerEntityUpdatingEvent(changedEntity.Entity);
                        TriggerEntityUpdatedEventOnUowCompleted(changedEntity.Entity);
                        break;
                    case EntityChangeType.Deleted:
                        TriggerEntityDeletingEvent(changedEntity.Entity);
                        TriggerEntityDeletedEventOnUowCompleted(changedEntity.Entity);
                        break;
                    default:
                        throw new PlusException("Unknown EntityChangeType: " + changedEntity.ChangeType);
                }
            }
        }

        protected virtual void TriggerDomainEvents(List<DomainEventEntry> domainEvents)
        {
            foreach (DomainEventEntry domainEvent in domainEvents)
            {
                EventBus.Trigger(domainEvent.EventData.GetType(), domainEvent.SourceEntity, domainEvent.EventData);
            }
        }

        protected virtual void TriggerEventWithEntity(Type genericEventType, object entity, bool triggerInCurrentUnitOfWork)
        {
            Type type = entity.GetType();
            Type eventType = genericEventType.MakeGenericType(type);
            if (triggerInCurrentUnitOfWork || _unitOfWorkManager.Current == null)
            {
                EventBus.Trigger(eventType, (IEventData)Activator.CreateInstance(eventType, entity));
            }
            else
            {
                _unitOfWorkManager.Current.Completed += delegate
                {
                    EventBus.Trigger(eventType, (IEventData)Activator.CreateInstance(eventType, entity));
                };
            }
        }
    }
}