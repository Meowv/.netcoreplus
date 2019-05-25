using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Plus.Configuration;
using Plus.Configuration.Startup;
using Plus.Domain.Uow;
using Plus.Modules;
using Plus.Reflection;
using Plus.Runtime.Caching.Configuration;

namespace Plus.Dependency.Installers
{
    /// <summary>
    /// PlusCoreInstaller
    /// </summary>
    internal class PlusCoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<DefaultSettings>().ImplementedBy<DefaultSettings>().LifestyleSingleton(),
                Component.For<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>().ImplementedBy<UnitOfWorkDefaultOptions>().LifestyleSingleton(),
                Component.For<IPlusStartupConfiguration, PlusStartupConfiguration>().ImplementedBy<PlusStartupConfiguration>().LifestyleSingleton(),
                Component.For<IPlusModuleManager, PlusModuleManager>().ImplementedBy<PlusModuleManager>().LifestyleSingleton(),
                Component.For<IAssemblyFinder, AssemblyFinder>().ImplementedBy<AssemblyFinder>().LifestyleSingleton(),
                Component.For<ITypeFinder, TypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
                Component.For<ICachingConfiguration, CachingConfiguration>().ImplementedBy<CachingConfiguration>().LifestyleSingleton(),
                Component.For<IValidationConfiguration, ValidationConfiguration>().ImplementedBy<ValidationConfiguration>().LifestyleSingleton()
            );
        }
    }
}