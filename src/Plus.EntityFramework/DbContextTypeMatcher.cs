using Plus.Domain.Uow;

namespace Plus.EntityFramework
{
    /// <summary>
    /// DbContextTypeMatcher
    /// </summary>
    public class DbContextTypeMatcher : DbContextTypeMatcher<PlusDbContext>
    {
        public DbContextTypeMatcher(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
            : base(currentUnitOfWorkProvider)
        {

        }
    }
}