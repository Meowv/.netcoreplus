using System;
using System.Runtime.Serialization;

namespace Plus.Domain.Entities
{
    /// <summary>
    /// EntityNotFoundException
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : PlusException
    {
        public Type EntityType { get; set; }

        public object Id { get; set; }

        public EntityNotFoundException()
        {

        }

        public EntityNotFoundException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public EntityNotFoundException(Type entityType, object id)
            : this(entityType, id, null)
        {

        }

        public EntityNotFoundException(Type entityType, object id, Exception innerException)
            : base($"There is no such an entity. Entity type: {entityType.FullName}, id: {id}", innerException)
        {
            EntityType = entityType;
            Id = id;
        }

        public EntityNotFoundException(string message)
            : base(message)
        {

        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}