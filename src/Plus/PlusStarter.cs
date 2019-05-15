using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Plus.Configuration.Startup;
using Plus.Dependency;
using Plus.Dependency.Installers;
using Plus.Domain.Uow;
using Plus.Modules;
using Plus.Runtime.Validation.Interception;
using System;
using System.Reflection;

namespace Plus
{
    public class PlusStarter : IDisposable
    {
        protected bool IsDisposed;

        private ILogger _logger;

        private IPlusModuleManager _moduleManager;

        public Type StartupModule { get; }

        public IIocManager IocManager { get; }

        private PlusStarter(Type startupModule, Action<PlusStarterOptions> optionsAction = null)
        {
            PlusStarterOptions uPrimeStarterOptions = new PlusStarterOptions();
            optionsAction?.Invoke(uPrimeStarterOptions);
            if (!((TypeInfo)typeof(PlusModule)).IsAssignableFrom(startupModule))
            {
                throw new ArgumentException("startupModule should be derived from PlusModule.");
            }
            StartupModule = startupModule;
            IocManager = uPrimeStarterOptions.IocManager;
            _logger = NullLogger.Instance;
            AddInterceptorRegistrars();
        }

        public static PlusStarter Create<TStartupModule>(Action<PlusStarterOptions> optionsAction = null) where TStartupModule : PlusModule
        {
            return new PlusStarter(typeof(TStartupModule), optionsAction);
        }

        public static PlusStarter Create(Type startupModule, Action<PlusStarterOptions> optionsAction = null)
        {
            return new PlusStarter(startupModule, optionsAction);
        }

        public virtual void Initialize()
        {
            PlusEngine.Instance.Initialize(IocManager);
            ResolveLogger();
            try
            {
                RegisterStarter();
                _logger.Debug("PlusStarter 初始化.");
                PlusEngine.Instance.IocManager.IocContainer.Install(new PlusCoreInstaller());
                PlusEngine.Instance.PostInitialize();
                IocManager.Resolve<PlusStartupConfiguration>().Initialize();
                _moduleManager = PlusEngine.Instance.Resolve<PlusModuleManager>();
                _moduleManager.Initialize(StartupModule);
                _moduleManager.StartModules();
                _logger.Debug("PlusStarter 初始化完成.");
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.ToString(), ex);
                throw;
            }
        }

        public virtual void Dispose()
        {
            if (!IsDisposed)
            {
                IsDisposed = true;
                _moduleManager?.ShutdownModules();
            }
        }

        private void ResolveLogger()
        {
            if (IocManager.IsRegistered<ILoggerFactory>())
            {
                _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(PlusStarter));
            }
        }

        private void RegisterStarter()
        {
            if (!IocManager.IsRegistered<PlusStarter>())
            {
                IocManager.IocContainer.Register(
                     Component.For<PlusStarter>().Instance(this)
                );
            }
        }

        public void AddInterceptorRegistrars()
        {
            ValidationInterceptorRegistrar.Initialize(IocManager);
            UnitOfWorkRegistrar.Initialize(IocManager);
        }
    }
}