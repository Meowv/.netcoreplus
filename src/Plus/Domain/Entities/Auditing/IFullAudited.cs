namespace Plus.Domain.Entities.Auditing
{
    /// <summary>
    /// IFullAudited
    /// </summary>
    public interface IFullAudited : IAudited, ICreationAudited, IHasCreationTime, IModificationAudited, IHasModificationTime, IDeletionAudited, IHasDeletionTime, ISoftDelete
    {
    }

    /// <summary>
    /// IFullAudited
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IFullAudited<TUser> : IAudited<TUser>, IAudited, ICreationAudited, IHasCreationTime, IModificationAudited, IHasModificationTime, ICreationAudited<TUser>, IModificationAudited<TUser>, IFullAudited, IDeletionAudited, IHasDeletionTime, ISoftDelete, IDeletionAudited<TUser> where TUser : IEntity<long>
    {
    }
}