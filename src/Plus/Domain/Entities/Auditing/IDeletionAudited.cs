namespace Plus.Domain.Entities.Auditing
{
    /// <summary>
    /// IDeletionAudited
    /// </summary>
    public interface IDeletionAudited : IHasDeletionTime, ISoftDelete
    {
        long? DeleterUserId { get; set; }
    }

    /// <summary>
    /// IDeletionAudited
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IDeletionAudited<TUser> : IDeletionAudited, IHasDeletionTime, ISoftDelete where TUser : IEntity<long>
    {
        TUser DeleterUser { get; set; }
    }
}