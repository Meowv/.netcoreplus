using MongoDB.Bson.Serialization;
using System;

namespace Plus.MongoDb.IdGenerator
{
    public abstract class IntIdGeneratorBase : IIdGenerator
    {
        private readonly string _idCollectionName;

        protected IntIdGeneratorBase(string idCollectionName)
        {
            _idCollectionName = idCollectionName;
        }

        protected IntIdGeneratorBase()
            : this("IDs")
        {
        }

        public object GenerateId(object container, object document)
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty(object id)
        {
            throw new NotImplementedException();
        }
    }
}