using Plus.Domain.Entities;
using Plus.Domain.Entities.Auditing;
using System;

namespace Plus.Services.Dto
{
    [Serializable]
    public abstract class FullAuditedEntityDto : FullAuditedEntityDto<int>
    {
    }

    [Serializable]
    public abstract class FullAuditedEntityDto<TPrimaryKey> : AuditedEntityDto<TPrimaryKey>, IFullAudited, IAudited, ICreationAudited, IHasCreationTime, IModificationAudited, IHasModificationTime, IDeletionAudited, IHasDeletionTime, ISoftDelete
    {
        public bool IsDeleted { get; set; }

        public long? DeleterUserId { get; set; }

        public DateTime? DeletionTime { get; set; }
    }
}