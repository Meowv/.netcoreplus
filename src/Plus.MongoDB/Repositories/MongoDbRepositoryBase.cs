using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Plus.Dependency;
using Plus.Domain.Entities;
using Plus.Domain.Entities.Auditing;
using Plus.Domain.Repositories;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Plus.MongoDb.Repositories
{
    /// <summary>
    /// MongoDB Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class MongoDbRepositoryBase<TEntity> : MongoDbRepositoryBase<TEntity, int>, IRepository<TEntity>, IRepository<TEntity, int>, IRepository, ITransientDependency where TEntity : class, IEntity<int>
    {
        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }
    }

    /// <summary>
    /// MongoDB Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class MongoDbRepositoryBase<TEntity, TPrimaryKey> : PlusRepositoryBase<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IMongoDatabaseProvider _databaseProvider;

        public virtual IMongoDatabase Database => _databaseProvider.Database;

        public abstract string CollectionName { get; }

        public virtual IMongoCollection<TEntity> Collection
        {
            get
            {
                string text = CollectionName;
                if (Extensions.IsNullOrEmpty(CollectionName))
                {
                    text = typeof(TEntity).Name;
                }
                return _databaseProvider.Database.GetCollection<TEntity>(text, null);
            }
        }

        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable(new AggregateOptions() { AllowDiskUse = true });
        }

        public override TEntity Get(TPrimaryKey id)
        {
            var val = Builders<TEntity>.Filter.Eq((TEntity e) => e.Id, id);
            TEntity entity = IFindFluentExtensions.First(IMongoCollectionExtensions.Find(Collection, val, null), default);
            if (entity == null)
            {
                throw new EntityNotFoundException("There is no such an entity with given primary key. Entity type: " + typeof(TEntity).FullName + ", primary key: " + id);
            }
            return entity;
        }
        public override async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            FilterDefinition<TEntity> query = Builders<TEntity>.Filter.Eq((TEntity e) => e.Id, id);
            TEntity entity = await IFindFluentExtensions.FirstAsync(IMongoCollectionExtensions.Find(Collection, query, null), default);
            if (entity == null)
            {
                throw new EntityNotFoundException("There is no such an entity with given primary key. Entity type: " + typeof(TEntity).FullName + ", primary key: " + id);
            }
            return entity;
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            FilterDefinition<TEntity> val = Builders<TEntity>.Filter.Eq((TEntity e) => e.Id, id);
            return IFindFluentExtensions.FirstOrDefault(IMongoCollectionExtensions.Find(Collection, val, null), default);
        }

        public override TEntity Insert(TEntity entity)
        {
            SetCreationAuditProperties(entity);
            CheckAndSetDefaultValue(entity);
            Collection.InsertOne(entity, null, default);
            return entity;
        }

        public override TEntity Update(TEntity entity)
        {
            throw new NotImplementedException("更新实体指定列及查询条件，暂未实现");
        }

        public override void Delete(TEntity entity)
        {
            throw new NotImplementedException("删除实体，暂未实现");
        }

        public override void Delete(TPrimaryKey id)
        {
            TEntity val = this.Get(id);
            this.Delete(val);
        }

        protected virtual void SetCreationAuditProperties(object entityAsObj)
        {
            if (entityAsObj is IHasCreationTime val)
            {
                if (val.CreationTime == default)
                {
                    val.CreationTime = DateTime.Now;
                }
                if (entityAsObj is ICreationAudited)
                {
                }
            }
        }

        protected virtual void CheckAndSetDefaultValue(object entityAsObj)
        {
            PropertyInfo[] properties = entityAsObj.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.PropertyType == typeof(string) && propertyInfo.GetValue(entityAsObj) == null)
                {
                    propertyInfo.SetValue(entityAsObj, string.Empty);
                }
            }
        }
    }
}