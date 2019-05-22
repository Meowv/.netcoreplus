using Plus.Dependency;
using System.Linq;
using System.Transactions;

namespace Plus.Domain.Uow
{
    /// <summary>
    /// UnitOfWorkManager
    /// </summary>
    public class UnitOfWorkManager : IUnitOfWorkManager, ITransientDependency
    {
        private readonly IIocResolver _iocResolver;

        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        private readonly IUnitOfWorkDefaultOptions _defaultOptions;

        public IActiveUnitOfWork Current => _currentUnitOfWorkProvider.Current;

        public UnitOfWorkManager(IIocResolver iocResolver, ICurrentUnitOfWorkProvider currentUnitOfWorkProvider, IUnitOfWorkDefaultOptions defaultOptions)
        {
            _iocResolver = iocResolver;
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
            _defaultOptions = defaultOptions;
        }

        public IUnitOfWorkCompleteHandle Begin()
        {
            return Begin(new UnitOfWorkOptions());
        }

        public IUnitOfWorkCompleteHandle Begin(TransactionScopeOption scope)
        {
            return Begin(new UnitOfWorkOptions
            {
                Scope = scope
            });
        }

        public IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options)
        {
            options.FillDefaultsForNonProvidedOptions(_defaultOptions);
            IUnitOfWork current = _currentUnitOfWorkProvider.Current;
            if (options.Scope == TransactionScopeOption.Required && current != null)
            {
                return new InnerUnitOfWorkCompleteHandle();
            }
            IUnitOfWork uow = _iocResolver.Resolve<IUnitOfWork>();
            uow.Completed += delegate
            {
                _currentUnitOfWorkProvider.Current = null;
            };
            uow.Failed += delegate
            {
                _currentUnitOfWorkProvider.Current = null;
            };
            uow.Disposed += delegate
            {
                _iocResolver.Release(uow);
            };
            if (current != null)
            {
                options.FillOuterUowFiltersForNonProvidedOptions(current.Filters.ToList());
            }
            uow.Begin(options);
            _currentUnitOfWorkProvider.Current = uow;
            return uow;
        }
    }
}