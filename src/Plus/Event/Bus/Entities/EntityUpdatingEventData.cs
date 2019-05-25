using System;

namespace Plus.Event.Bus.Entities
{
    [Serializable]
    public class EntityUpdatingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        public EntityUpdatingEventData(TEntity entity)
            : base(entity)
        {
        }
    }
}