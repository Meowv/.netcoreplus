using System;

namespace Plus.Event.Bus.Entities
{
    [Serializable]
    public class EntityCreatingEventData<TEntity> : EntityChangingEventData<TEntity>
    {
        public EntityCreatingEventData(TEntity entity)
            : base(entity)
        {
        }
    }
}