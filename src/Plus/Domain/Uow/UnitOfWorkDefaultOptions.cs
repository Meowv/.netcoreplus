using Plus.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Plus.Domain.Uow
{
    internal class UnitOfWorkDefaultOptions : IUnitOfWorkDefaultOptions
    {
        public TransactionScopeOption Scope { get; set; }

        public bool IsTransactional { get; set; }

        public TimeSpan? Timeout { get; set; }

        public bool IsTransactionScopeAvailable { get; set; }

        public IsolationLevel? IsolationLevel { get; set; }

        public IReadOnlyList<DataFilterConfiguration> Filters => _filters;
        private readonly List<DataFilterConfiguration> _filters;

        public List<Func<Type, bool>> ConventionalUowSelectors { get; }

        public UnitOfWorkDefaultOptions()
        {
            _filters = new List<DataFilterConfiguration>();
            IsTransactional = true;
            Scope = TransactionScopeOption.Required;

            IsTransactionScopeAvailable = true;

            ConventionalUowSelectors = new List<Func<Type, bool>>
            {
                type => typeof(IRepository).IsAssignableFrom(type)
            };
        }

        public void RegisterFilter(string filterName, bool isEnabledByDefault)
        {
            if (_filters.Any(f => f.FilterName == filterName))
            {
                throw new PlusException("There is already a filter with name: " + filterName);
            }

            _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        }

        public void OverrideFilter(string filterName, bool isEnabledByDefault)
        {
            _filters.RemoveAll(f => f.FilterName == filterName);
            _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        }
    }
}