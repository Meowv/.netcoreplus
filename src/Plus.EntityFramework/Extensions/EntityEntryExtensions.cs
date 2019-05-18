using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace Plus.EntityFramework.Extensions
{
    public static class EntityEntryExtensions
    {
        /// <summary>
        /// 检查实体及其关联的所属实体是否已更改
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static bool CheckOwnedEntityChange(this EntityEntry entry)
        {
            return entry.State == EntityState.Modified ||
                   entry.References.Any(r =>
                       r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned() && CheckOwnedEntityChange(r.TargetEntry));
        }
    }
}