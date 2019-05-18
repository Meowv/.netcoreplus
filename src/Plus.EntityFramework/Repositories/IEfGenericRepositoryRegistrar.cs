using Plus.Dependency;
using Plus.Domain.Repositories;
using System;

namespace Plus.EntityFramework.Repositories
{
    /// <summary>
    /// IEfGenericRepositoryRegistrar
    /// </summary>
    public interface IEfGenericRepositoryRegistrar
    {
        void RegisterForDbContext(Type dbContextType, IIocManager iocManager, AutoRepositoryTypesAttribute defaultAutoRepositoryTypesAttribute);
    }
}