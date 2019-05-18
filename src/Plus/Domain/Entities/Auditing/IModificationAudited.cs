namespace Plus.Domain.Entities.Auditing
{
    public interface IModificationAudited : IHasModificationTime
    {
        long? LastModifierUserId
        {
            get;
            set;
        }
    }

    public interface IModificationAudited<TUser> : IModificationAudited, IHasModificationTime where TUser : IEntity<long>
    {
        TUser LastModifierUser
        {
            get;
            set;
        }
    }
}