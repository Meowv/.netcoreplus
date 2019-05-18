using Castle.MicroKernel.Registration;
using Microsoft.EntityFrameworkCore;
using Plus.Dependency;
using System;

namespace Plus.EntityFramework.Configuration
{
    /// <summary>
    /// PlusEfCoreConfiguration
    /// </summary>
    public class PlusEfCoreConfiguration : IPlusEfCoreConfiguration
    {
        private readonly IIocManager _iocManager;

        public PlusEfCoreConfiguration(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }

        public void AddDbContext<TDbContext>(Action<PlusDbContextConfiguration<TDbContext>> action) where TDbContext : DbContext
        {
            _iocManager.IocContainer.Register(
                Component.For<IPlusDbContextConfigurer<TDbContext>>().Instance(
                    new PlusDbContextConfigurerAction<TDbContext>(action)
                ).IsDefault()
            );
        }
    }
}