using System.Reflection;

namespace Plus.Dependency
{
    public class ConventionalRegistrationContext : IRegistrationContext
    {
        public Assembly Assembly
        {
            get;
            private set;
        }

        public IIocManager IocManager
        {
            get;
            private set;
        }

        internal ConventionalRegistrationContext(Assembly assembly, IIocManager iocManager)
        {
            Assembly = assembly;
            IocManager = iocManager;
        }
    }
}