using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Plus.Dependency
{
    public interface IConventionalRegistrationContext
    {
        /// <summary>
        /// Gets the registering Assembly.
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// Reference to the IOC Container to register types.
        /// </summary>
        IIocManager IocManager { get; }

        /// <summary>
        /// Registration configuration.
        /// </summary>
        ConventionalRegistrationConfig Config { get; }
    }
}