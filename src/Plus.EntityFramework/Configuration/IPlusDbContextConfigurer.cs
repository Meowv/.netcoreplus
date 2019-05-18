using Microsoft.EntityFrameworkCore;

namespace Plus.EntityFramework.Configuration
{
    /// <summary>
    /// IPlusDbContextConfigurer
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IPlusDbContextConfigurer<TDbContext>
        where TDbContext : DbContext
    {
        void Configure(PlusDbContextConfiguration<TDbContext> configuration);
    }
}