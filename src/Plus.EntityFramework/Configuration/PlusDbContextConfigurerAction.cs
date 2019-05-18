using Microsoft.EntityFrameworkCore;
using System;

namespace Plus.EntityFramework.Configuration
{
    /// <summary>
    /// PlusDbContextConfigurerAction
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class PlusDbContextConfigurerAction<TDbContext> : IPlusDbContextConfigurer<TDbContext>
        where TDbContext : DbContext
    {
        public Action<PlusDbContextConfiguration<TDbContext>> Action { get; set; }

        public PlusDbContextConfigurerAction(Action<PlusDbContextConfiguration<TDbContext>> action)
        {
            Action = action;
        }

        public void Configure(PlusDbContextConfiguration<TDbContext> configuration)
        {
            Action(configuration);
        }
    }
}