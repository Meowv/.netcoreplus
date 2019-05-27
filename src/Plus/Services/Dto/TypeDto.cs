namespace Plus.Services.Dto
{
    public class TypeDto<T> : IDto
    {
        public T Value { get; set; }
    }
}