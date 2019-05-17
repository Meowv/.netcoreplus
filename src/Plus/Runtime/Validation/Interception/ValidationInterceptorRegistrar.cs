using Castle.Core;
using Castle.MicroKernel;
using Plus.Dependency;
using Plus.Services;
using System.Reflection;

namespace Plus.Runtime.Validation.Interception
{
    /// <summary>
    /// ValidationInterceptorRegistrar
    /// </summary>
    internal static class ValidationInterceptorRegistrar
    {
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (typeof(IApplicationService).GetTypeInfo().IsAssignableFrom(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(ValidationInterceptor)));
            }
        }
    }
}