namespace Plus.Domain.Entities.Auditing
{
    /// <summary>
    /// ICreationAudited
    /// </summary>
    public interface ICreationAudited : IHasCreationTime
    {
        long? CreatorUserId { get; set; }
    }

    /// <summary>
    /// ICreationAudited
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface ICreationAudited<TUser> : ICreationAudited, IHasCreationTime where TUser : IEntity<long>
    {
        TUser CreatorUser { get; set; }
    }
}