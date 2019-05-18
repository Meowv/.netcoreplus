using Microsoft.EntityFrameworkCore;
using System;

namespace Plus.EntityFramework.Configuration
{
    /// <summary>
    /// IPlusEfCoreConfiguration
    /// </summary>
    public interface IPlusEfCoreConfiguration
    {
        void AddDbContext<TDbContext>(Action<PlusDbContextConfiguration<TDbContext>> action) where TDbContext : DbContext;
    }
}