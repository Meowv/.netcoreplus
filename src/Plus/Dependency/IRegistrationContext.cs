using System.Reflection;

namespace Plus.Dependency
{
    public interface IRegistrationContext
    {
        Assembly Assembly { get; }

        IIocManager IocManager { get; }
    }
}