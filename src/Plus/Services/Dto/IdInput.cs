namespace Plus.Services.Dto
{
    public class IdInput : IdInput<int>
    {
        public IdInput()
        {
        }

        public IdInput(int id)
            : base(id)
        {
        }
    }

    public class IdInput<TId> : IInputDto, IDto
    {
        public TId Id { get; set; }

        public IdInput()
        {
        }

        public IdInput(TId id)
        {
            Id = id;
        }
    }
}