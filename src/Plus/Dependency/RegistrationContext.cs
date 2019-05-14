using System.Reflection;

namespace Plus.Dependency
{
    internal class RegistrationContext : IRegistrationContext
    {
        /// <summary>
        /// Gets the registering Assembly.
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Reference to the IOC Container to register types.
        /// </summary>
        public IIocManager IocManager { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="iocManager"></param>
        internal RegistrationContext(Assembly assembly, IIocManager iocManager)
        {
            Assembly = assembly;
            IocManager = iocManager;
        }
    }
}