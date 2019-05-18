using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Plus.EntityFramework.Configuration
{
    /// <summary>
    /// PlusDbContextConfiguration
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class PlusDbContextConfiguration<TDbContext>
        where TDbContext : DbContext
    {
        public string ConnectionString { get; internal set; }

        public DbConnection ExistingConnection { get; internal set; }

        public DbContextOptionsBuilder<TDbContext> DbContextOptions { get; }

        public PlusDbContextConfiguration(string connectionString, DbConnection existingConnection)
        {
            ConnectionString = connectionString;
            ExistingConnection = existingConnection;

            DbContextOptions = new DbContextOptionsBuilder<TDbContext>();
        }
    }
}