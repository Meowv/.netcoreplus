using Castle.Core;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Plus.Domain.Uow
{
    /// <summary>
    /// 工作单元基类
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork, IActiveUnitOfWork, IUnitOfWorkCompleteHandle, IDisposable
    {
        private readonly List<DataFilterConfiguration> _filters;

        private bool _isBeginCalledBefore;

        private bool _isCompleteCalledBefore;

        private bool _succeed;

        private Exception _exception;

        private int? _tenantId;

        public string Id { get; }

        [DoNotWire]
        public IUnitOfWork Outer { get; set; }

        public UnitOfWorkOptions Options { get; private set; }

        public IReadOnlyList<DataFilterConfiguration> Filters => _filters.ToImmutableList();

        public Dictionary<string, object> Items { get; set; }

        protected IUnitOfWorkDefaultOptions DefaultOptions { get; }

        protected IConnectionStringResolver ConnectionStringResolver { get; }

        public bool IsDisposed { get; private set; }

        protected IUnitOfWorkFilterExecuter FilterExecuter{ get; }

        public event EventHandler Completed;

        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        public event EventHandler Disposed;

        protected UnitOfWorkBase(IConnectionStringResolver connectionStringResolver, IUnitOfWorkDefaultOptions defaultOptions, IUnitOfWorkFilterExecuter filterExecuter)
        {
            FilterExecuter = filterExecuter;
            DefaultOptions = defaultOptions;
            ConnectionStringResolver = connectionStringResolver;
            Id = Guid.NewGuid().ToString("N");
            _filters = defaultOptions.Filters.ToList();
            Items = new Dictionary<string, object>();
        }

        public void Begin(UnitOfWorkOptions options)
        {
            PreventMultipleBegin();
            Options = options;
            SetFilters(options.FilterOverrides);
            BeginUow();
        }

        public abstract void SaveChanges();

        public abstract Task SaveChangesAsync();

        public IDisposable DisableFilter(params string[] filterNames)
        {
            List<string> disabledFilters = new List<string>();
            foreach (string text in filterNames)
            {
                int filterIndex = GetFilterIndex(text);
                if (_filters[filterIndex].IsEnabled)
                {
                    disabledFilters.Add(text);
                    _filters[filterIndex] = new DataFilterConfiguration(_filters[filterIndex], false);
                }
            }
            disabledFilters.ForEach(ApplyDisableFilter);
            return new DisposeAction(delegate
            {
                EnableFilter(disabledFilters.ToArray());
            });
        }

        public IDisposable EnableFilter(params string[] filterNames)
        {
            List<string> enabledFilters = new List<string>();
            foreach (string text in filterNames)
            {
                int filterIndex = GetFilterIndex(text);
                if (!_filters[filterIndex].IsEnabled)
                {
                    enabledFilters.Add(text);
                    _filters[filterIndex] = new DataFilterConfiguration(_filters[filterIndex], true);
                }
            }
            enabledFilters.ForEach(ApplyEnableFilter);
            return new DisposeAction(delegate
            {
                DisableFilter(enabledFilters.ToArray());
            });
        }

        public bool IsFilterEnabled(string filterName)
        {
            return GetFilter(filterName).IsEnabled;
        }

        public IDisposable SetFilterParameter(string filterName, string parameterName, object value)
        {
            int filterIndex = GetFilterIndex(filterName);
            DataFilterConfiguration dataFilterConfiguration = new DataFilterConfiguration(_filters[filterIndex]);
            object oldValue = null;
            bool hasOldValue = dataFilterConfiguration.FilterParameters.ContainsKey(parameterName);
            if (hasOldValue)
            {
                oldValue = dataFilterConfiguration.FilterParameters[parameterName];
            }
            dataFilterConfiguration.FilterParameters[parameterName] = value;
            _filters[filterIndex] = dataFilterConfiguration;
            ApplyFilterParameterValue(filterName, parameterName, value);
            return new DisposeAction(delegate
            {
                if (hasOldValue)
                {
                    SetFilterParameter(filterName, parameterName, oldValue);
                }
            });
        }

        public void Complete()
        {
            PreventMultipleComplete();
            try
            {
                CompleteUow();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception exception)
            {
                Exception ex = _exception = exception;
                throw;
            }
        }

        public async Task CompleteAsync()
        {
            PreventMultipleComplete();
            try
            {
                await CompleteUowAsync();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception exception)
            {
                Exception ex = _exception = exception;
                throw;
            }
        }

        public void Dispose()
        {
            if (_isBeginCalledBefore && !IsDisposed)
            {
                IsDisposed = true;
                if (!_succeed)
                {
                    OnFailed(_exception);
                }
                DisposeUow();
                OnDisposed();
            }
        }

        protected virtual void BeginUow()
        {

        }

        protected abstract void CompleteUow();

        protected abstract Task CompleteUowAsync();

        protected abstract void DisposeUow();

        protected virtual void ApplyDisableFilter(string filterName)
        {
            FilterExecuter.ApplyDisableFilter(this, filterName);
        }

        protected virtual void ApplyEnableFilter(string filterName)
        {
            FilterExecuter.ApplyEnableFilter(this, filterName);
        }

        protected virtual void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            FilterExecuter.ApplyFilterParameterValue(this, filterName, parameterName, value);
        }

        protected virtual string ResolveConnectionString(ConnectionStringResolveArgs args)
        {
            return ConnectionStringResolver.GetNameOrConnectionString(args);
        }

        protected virtual void OnCompleted()
        {
            Extensions.InvokeSafely(this.Completed, (object)this);
        }

        protected virtual void OnFailed(Exception exception)
        {
            Extensions.InvokeSafely<UnitOfWorkFailedEventArgs>(this.Failed, (object)this, new UnitOfWorkFailedEventArgs(exception));
        }

        protected virtual void OnDisposed()
        {
            Extensions.InvokeSafely(this.Disposed, (object)this);
        }

        private void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
                throw new PlusException("This unit of work has started before. Can not call Start method more than once.");
            }
            _isBeginCalledBefore = true;
        }

        private void PreventMultipleComplete()
        {
            if (_isCompleteCalledBefore)
            {
                throw new PlusException("Complete is called before!");
            }
            _isCompleteCalledBefore = true;
        }

        private void SetFilters(List<DataFilterConfiguration> filterOverrides)
        {
            for (var i = 0; i < _filters.Count; i++)
            {
                var filterOverride = filterOverrides.FirstOrDefault(f => f.FilterName == _filters[i].FilterName);
                if (filterOverride != null)
                {
                    _filters[i] = filterOverride;
                }
            }
        }

        private DataFilterConfiguration GetFilter(string filterName)
        {
            DataFilterConfiguration dataFilterConfiguration = _filters.FirstOrDefault((DataFilterConfiguration f) => f.FilterName == filterName);
            if (dataFilterConfiguration == null)
            {
                throw new PlusException("Unknown filter name: " + filterName + ". Be sure this filter is registered before.");
            }
            return dataFilterConfiguration;
        }

        private int GetFilterIndex(string filterName)
        {
            int num = _filters.FindIndex((DataFilterConfiguration f) => f.FilterName == filterName);
            if (num < 0)
            {
                throw new PlusException("Unknown filter name: " + filterName + ". Be sure this filter is registered before.");
            }
            return num;
        }

        public override string ToString()
        {
            return "[UnitOfWork " + Id + "]";
        }

        public virtual IDisposable SetTenantId(int? tenantId)
        {
            return SetTenantId(tenantId, true);
        }

        public virtual IDisposable SetTenantId(int? tenantId, bool switchMustHaveTenantEnableDisable)
        {
            var oldTenantId = _tenantId;
            _tenantId = tenantId;

            IDisposable mustHaveTenantEnableChange;
            if (switchMustHaveTenantEnableDisable)
            {
                mustHaveTenantEnableChange = tenantId == null
                    ? DisableFilter(PlusDataFilters.MustHaveTenant)
                    : EnableFilter(PlusDataFilters.MustHaveTenant);
            }
            else
            {
                mustHaveTenantEnableChange = NullDisposable.Instance;
            }

            var mayHaveTenantChange = SetFilterParameter(PlusDataFilters.MayHaveTenant, PlusDataFilters.Parameters.TenantId, tenantId);
            var mustHaveTenantChange = SetFilterParameter(PlusDataFilters.MustHaveTenant, PlusDataFilters.Parameters.TenantId, tenantId ?? 0);

            return new DisposeAction(() =>
            {
                mayHaveTenantChange.Dispose();
                mustHaveTenantChange.Dispose();
                mustHaveTenantEnableChange.Dispose();
                _tenantId = oldTenantId;
            });
        }

        public int? GetTenantId()
        {
            return _tenantId;
        }
    }
}