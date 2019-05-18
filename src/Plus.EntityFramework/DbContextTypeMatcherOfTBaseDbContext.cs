using Plus.Dependency;
using Plus.Domain.Repositories;
using Plus.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Plus.EntityFramework
{
    /// <summary>
    /// DbContextTypeMatcher
    /// </summary>
    /// <typeparam name="TBaseDbContext"></typeparam>
    public abstract class DbContextTypeMatcher<TBaseDbContext> : IDbContextTypeMatcher, ISingletonDependency
    {
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        private readonly Dictionary<Type, List<Type>> _dbContextTypes;

        protected DbContextTypeMatcher(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
            _dbContextTypes = new Dictionary<Type, List<Type>>();
        }

        public void Populate(Type[] dbContextTypes)
        {
            foreach (Type type in dbContextTypes)
            {
                List<Type> list = new List<Type>();
                AddWithBaseTypes(type, list);
                foreach (Type item in list)
                {
                    Add(item, type);
                }
            }
        }

        public virtual Type GetConcreteType(Type sourceDbContextType)
        {
            if (!sourceDbContextType.GetTypeInfo().IsAbstract)
            {
                return sourceDbContextType;
            }
            List<Type> orDefault = _dbContextTypes.GetOrDefault(sourceDbContextType);
            if (orDefault.IsNullOrEmpty())
            {
                throw new PlusException("Could not find a concrete implementation of given DbContext type: " + sourceDbContextType.AssemblyQualifiedName);
            }
            if (orDefault.Count == 1)
            {
                return orDefault[0];
            }
            CheckCurrentUow();
            return GetDefaultDbContextType(orDefault, sourceDbContextType);
        }

        private void CheckCurrentUow()
        {
            if (_currentUnitOfWorkProvider.Current == null)
            {
                throw new PlusException("GetConcreteType method should be called in a UOW.");
            }
        }

        private static Type GetDefaultDbContextType(List<Type> dbContextTypes, Type sourceDbContextType)
        {
            List<Type> list = (from type in dbContextTypes
                               where !type.GetTypeInfo().IsDefined(typeof(AutoRepositoryTypesAttribute), inherit: true)
                               select type).ToList();
            if (list.Count == 1)
            {
                return list[0];
            }
            list = (from type in list
                    where type.GetTypeInfo().IsDefined(typeof(DefaultDbContextAttribute), inherit: true)
                    select type).ToList();
            if (list.Count == 1)
            {
                return list[0];
            }
            throw new PlusException($"Found more than one concrete type for given DbContext Type ({sourceDbContextType}). Found types: {(from c in dbContextTypes select c.AssemblyQualifiedName).JoinAsString(", ")}.");
        }

        private static void AddWithBaseTypes(Type dbContextType, List<Type> types)
        {
            types.Add(dbContextType);
            if (dbContextType != typeof(TBaseDbContext))
            {
                AddWithBaseTypes(dbContextType.GetTypeInfo().BaseType, types);
            }
        }

        private void Add(Type sourceDbContextType, Type targetDbContextType)
        {
            if (!_dbContextTypes.ContainsKey(sourceDbContextType))
            {
                _dbContextTypes[sourceDbContextType] = new List<Type>();
            }
            _dbContextTypes[sourceDbContextType].Add(targetDbContextType);
        }
    }
}