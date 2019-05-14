using Castle.Core.Logging;
using Plus.Dependency;

namespace Plus
{
    public class PlusEngine
    {
        private bool _initialized = false;

        public IIocManager IocManager { get; private set; }

        public ILogger Logger { get; private set; }

        public static PlusEngine Instance { get; private set; }

        static PlusEngine()
        {
            Instance = new PlusEngine();
        }

        public void Initialize(IIocManager iocManage)
        {
            if (!_initialized)
            {
                Logger = NullLogger.Instance;
                IocManager = iocManage;
                _initialized = true;
                return;
            }
            throw new PlusException("PlusEngine 初始化");
        }

        public void PostInitialize()
        {
            if (_initialized)
            {

            }
        }

        public void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where T : class
        {
            IocManager.Register(typeof(T), typeof(T), lifeStyle);
        }

        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocManager.Register(typeof(TType), typeof(TImpl), lifeStyle);
        }

        public T Resolve<T>() where T : class
        {
            return IocManager.Resolve<T>();
        }
    }
}