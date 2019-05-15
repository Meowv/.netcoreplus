using Castle.DynamicProxy;
using Plus.Aspects;
using Plus.Dependency;

namespace Plus.Runtime.Validation.Interception
{
    /// <summary>
    /// 这个拦截器用于拦截类的方法调用，类的方法必须经过验证。
    /// </summary>
    public class ValidationInterceptor : IInterceptor
    {
        private readonly IIocResolver _iocResolver;

        public ValidationInterceptor(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public void Intercept(IInvocation invocation)
        {
            if (PlusCrossCuttingConcerns.IsApplied(invocation.InvocationTarget, PlusCrossCuttingConcerns.Validation))
            {
                invocation.Proceed();
                return;
            }

            using (var validator = _iocResolver.ResolveAsDisposable<MethodInvocationValidator>())
            {
                validator.Object.Initialize(invocation.MethodInvocationTarget, invocation.Arguments);
                validator.Object.Validate();
            }

            invocation.Proceed();
        }
    }
}