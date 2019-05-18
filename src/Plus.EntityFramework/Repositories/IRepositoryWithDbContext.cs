using Microsoft.EntityFrameworkCore;

namespace Plus.EntityFramework.Repositories
{
    /// <summary>
    /// IRepositoryWithDbContext
    /// </summary>
    public interface IRepositoryWithDbContext
    {
        DbContext GetDbContext();
    }
}