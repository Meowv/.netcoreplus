using System;

namespace Plus.Domain.Entities.Auditing
{
    public static class EntityAuditingHelper
    {
        public static void SetCreationAuditProperties(object entityAsObj, long? userId)
        {
            if (!(entityAsObj is IHasCreationTime hasCreationTime))
            {
                return;
            }
            if (hasCreationTime.CreationTime == default)
            {
                hasCreationTime.CreationTime = DateTime.Now;
            }
            if (entityAsObj is ICreationAudited && userId.HasValue)
            {
                ICreationAudited creationAudited = entityAsObj as ICreationAudited;
                if (!creationAudited.CreatorUserId.HasValue)
                {
                    creationAudited.CreatorUserId = userId;
                }
            }
        }

        public static void SetModificationAuditProperties(object entityAsObj, long? userId)
        {
            if (entityAsObj is IHasModificationTime)
            {
                Extensions.As<IHasModificationTime>(entityAsObj).LastModificationTime = DateTime.Now;
            }
            if (entityAsObj is IModificationAudited)
            {
                IModificationAudited modificationAudited = Extensions.As<IModificationAudited>(entityAsObj);
                if (!userId.HasValue)
                {
                    modificationAudited.LastModifierUserId = null;
                }
                else
                {
                    modificationAudited.LastModifierUserId = userId;
                }
            }
        }
    }
}