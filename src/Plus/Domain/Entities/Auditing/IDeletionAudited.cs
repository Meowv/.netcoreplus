namespace Plus.Domain.Entities.Auditing
{
    public interface IDeletionAudited : IHasDeletionTime, ISoftDelete
    {
        long? DeleterUserId
        {
            get;
            set;
        }
    }

    public interface IDeletionAudited<TUser> : IDeletionAudited, IHasDeletionTime, ISoftDelete where TUser : IEntity<long>
    {
        TUser DeleterUser
        {
            get;
            set;
        }
    }
}