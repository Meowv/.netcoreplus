namespace Plus.Services.Dto
{
    public class NullableIdInput : NullableIdInput<int>
    {
        public NullableIdInput()
        {
        }

        public NullableIdInput(int? id)
            : base(id)
        {
        }
    }

    public class NullableIdInput<TId> : IInputDto, IDto where TId : struct
    {
        public TId? Id { get; set; }

        public NullableIdInput()
        {
        }

        public NullableIdInput(TId? id)
        {
            Id = id;
        }
    }
}