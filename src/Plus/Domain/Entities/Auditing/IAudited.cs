namespace Plus.Domain.Entities.Auditing
{
    /// <summary>
    /// IAudited
    /// </summary>
    public interface IAudited : ICreationAudited, IHasCreationTime, IModificationAudited, IHasModificationTime
    {
    }

    /// <summary>
    /// IAudited
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IAudited<TUser> : IAudited, ICreationAudited, IHasCreationTime, IModificationAudited, IHasModificationTime, ICreationAudited<TUser>, IModificationAudited<TUser> where TUser : IEntity<long>
    {
    }
}