using Castle.MicroKernel.Registration;
using Plus.EntityFramework.Configuration;
using Plus.EntityFramework.Repositories;
using Plus.EntityFramework.Uow;
using Plus.Modules;
using Plus.Orm;
using Plus.Reflection;
using System;
using System.Reflection;

namespace Plus.EntityFramework
{
    /// <summary>
    /// EntityFramework 数据访问层
    /// </summary>
    [DependsOn(typeof(PlusLeadershipModule))]
    public class PlusEntityFrameworkModule : PlusModule
    {
        private readonly ITypeFinder _typeFinder;

        public PlusEntityFrameworkModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public override void PreInitialize()
        {
            IocManager.Register<IPlusEfCoreConfiguration, PlusEfCoreConfiguration>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssembly(typeof(PlusEntityFrameworkModule).GetAssembly());

            IocManager.IocContainer.Register(
                Component.For(typeof(IDbContextProvider<>))
                    .ImplementedBy(typeof(UnitOfWorkDbContextProvider<>))
                    .LifestyleTransient()
                );

            RegisterGenericRepositoriesAndMatchDbContexes();
        }

        private void RegisterGenericRepositoriesAndMatchDbContexes()
        {
            var dbContextTypes =
                _typeFinder.Find(type =>
                {
                    var typeInfo = type.GetTypeInfo();
                    return typeInfo.IsPublic &&
                           !typeInfo.IsAbstract &&
                           typeInfo.IsClass &&
                           typeof(PlusDbContext).IsAssignableFrom(type);
                });

            if (dbContextTypes.IsNullOrEmpty())
            {
                Logger.Warn("No class found derived from PlusDbContext.");
                return;
            }

            foreach (var dbContextType in dbContextTypes)
            {
                Logger.Debug("Registering DbContext: " + dbContextType.AssemblyQualifiedName);

                IocManager.Resolve<IEfGenericRepositoryRegistrar>().RegisterForDbContext(dbContextType, IocManager, EfCoreAutoRepositoryTypes.Default);

                IocManager.IocContainer.Register(
                    Component.For<ISecondaryOrmRegistrar>()
                        .Named(Guid.NewGuid().ToString("N"))
                        .Instance(new EfCoreBasedSecondaryOrmRegistrar(dbContextType, IocManager.Resolve<IDbContextEntityFinder>()))
                        .LifestyleTransient()
                );
            }

            IocManager.Resolve<IDbContextTypeMatcher>().Populate(dbContextTypes);
        }
    }
}