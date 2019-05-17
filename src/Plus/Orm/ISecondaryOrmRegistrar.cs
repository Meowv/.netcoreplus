using Plus.Dependency;
using Plus.Domain.Repositories;

namespace Plus.Orm
{
    /// <summary>
    /// ISecondaryOrmRegistrar
    /// </summary>
    public interface ISecondaryOrmRegistrar
    {
        string OrmContextKey { get; }

        void RegisterRepositories(IIocManager iocManager, AutoRepositoryTypesAttribute defaultRepositoryTypes);
    }
}