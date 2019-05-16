using System.Reflection;

namespace Plus.Dependency
{
    /// <summary>
    /// IRegistrationContext
    /// </summary>
    public interface IRegistrationContext
    {
        Assembly Assembly { get; }

        IIocManager IocManager { get; }
    }
}