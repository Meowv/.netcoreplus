using System;

namespace Plus.Domain.Repositories
{
    /// <summary>
    /// 用于为实体定义自动存储库类型，可以用于 DbContext 类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRepositoryTypesAttribute : Attribute
    {
        public Type RepositoryInterface { get; }

        public Type RepositoryInterfaceWithPrimaryKey { get; }

        public Type RepositoryImplementation { get; }

        public Type RepositoryImplementationWithPrimaryKey { get; }

        public bool WithDefaultRepositoryInterfaces { get; set; }

        public AutoRepositoryTypesAttribute(
            Type repositoryInterface,
            Type repositoryInterfaceWithPrimaryKey,
            Type repositoryImplementation,
            Type repositoryImplementationWithPrimaryKey)
        {
            RepositoryInterface = repositoryInterface;
            RepositoryInterfaceWithPrimaryKey = repositoryInterfaceWithPrimaryKey;
            RepositoryImplementation = repositoryImplementation;
            RepositoryImplementationWithPrimaryKey = repositoryImplementationWithPrimaryKey;
        }
    }
}