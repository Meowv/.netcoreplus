using Castle.Core;
using Castle.Core.Logging;
using Plus.Dependency;
using System.Threading;

namespace Plus.Domain.Uow
{
    public class AsyncLocalCurrentUnitOfWorkProvider : ICurrentUnitOfWorkProvider, ITransientDependency
    {
        private class LocalUowWrapper
        {
            public IUnitOfWork UnitOfWork { get; set; }

            public LocalUowWrapper(IUnitOfWork unitOfWork)
            {
                UnitOfWork = unitOfWork;
            }
        }

        private static readonly AsyncLocal<LocalUowWrapper> AsyncLocalUow = new AsyncLocal<LocalUowWrapper>();

        [DoNotWire]
        public IUnitOfWork Current
        {
            get
            {
                return GetCurrentUow();
            }
            set
            {
                SetCurrentUow(value);
            }
        }

        public ILogger Logger { get; set; }

        public AsyncLocalCurrentUnitOfWorkProvider()
        {
            Logger = NullLogger.Instance;
        }

        private static IUnitOfWork GetCurrentUow()
        {
            IUnitOfWork unitOfWork = AsyncLocalUow.Value?.UnitOfWork;
            if (unitOfWork == null)
            {
                return null;
            }
            if (unitOfWork.IsDisposed)
            {
                AsyncLocalUow.Value = null;
                return null;
            }
            return unitOfWork;
        }

        private static void SetCurrentUow(IUnitOfWork value)
        {
            lock (AsyncLocalUow)
            {
                if (value == null)
                {
                    if (AsyncLocalUow.Value != null)
                    {
                        if (AsyncLocalUow.Value.UnitOfWork?.Outer == null)
                        {
                            AsyncLocalUow.Value.UnitOfWork = null;
                            AsyncLocalUow.Value = null;
                        }
                        else
                        {
                            AsyncLocalUow.Value.UnitOfWork = AsyncLocalUow.Value.UnitOfWork.Outer;
                        }
                    }
                }
                else if (AsyncLocalUow.Value?.UnitOfWork == null)
                {
                    if (AsyncLocalUow.Value != null)
                    {
                        AsyncLocalUow.Value.UnitOfWork = value;
                    }
                    AsyncLocalUow.Value = new LocalUowWrapper(value);
                }
                else
                {
                    value.Outer = AsyncLocalUow.Value.UnitOfWork;
                    AsyncLocalUow.Value.UnitOfWork = value;
                }
            }
        }
    }
}