using System;

namespace Plus.Domain.Entities
{
    /// <summary>
    /// EntityTypeInfo
    /// </summary>
    public class EntityTypeInfo
    {
        public Type EntityType { get; private set; }

        public Type DeclaringType { get; private set; }

        public EntityTypeInfo(Type entityType, Type declaringType)
        {
            EntityType = entityType;
            DeclaringType = declaringType;
        }
    }
}