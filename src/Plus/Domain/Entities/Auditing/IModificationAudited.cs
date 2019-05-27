namespace Plus.Domain.Entities.Auditing
{
    /// <summary>
    /// IModificationAudited
    /// </summary>
    public interface IModificationAudited : IHasModificationTime
    {
        long? LastModifierUserId { get; set; }
    }

    /// <summary>
    /// IModificationAudited
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IModificationAudited<TUser> : IModificationAudited, IHasModificationTime where TUser : IEntity<long>
    {
        TUser LastModifierUser { get; set; }
    }
}