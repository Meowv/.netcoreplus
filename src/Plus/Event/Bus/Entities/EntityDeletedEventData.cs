using System;

namespace Plus.Event.Bus.Entities
{
    [Serializable]
    public class EntityDeletedEventData<TEntity> : EntityChangedEventData<TEntity>
    {
        public EntityDeletedEventData(TEntity entity)
            : base(entity)
        {
        }
    }
}