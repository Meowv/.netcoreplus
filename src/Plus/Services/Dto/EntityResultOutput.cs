using System;

namespace Plus.Services.Dto
{
    [Serializable]
    public class EntityResultOutput : EntityResultOutput<int>, IEntityDto, IEntityDto<int>, IDto
    {
        public EntityResultOutput()
        {
        }

        public EntityResultOutput(int id)
            : base(id)
        {
        }
    }

    [Serializable]
    public class EntityResultOutput<TPrimaryKey> : EntityDto<TPrimaryKey>, IOutputDto, IDto
    {
        public EntityResultOutput()
        {
        }

        public EntityResultOutput(TPrimaryKey id)
            : base(id)
        {
        }
    }
}