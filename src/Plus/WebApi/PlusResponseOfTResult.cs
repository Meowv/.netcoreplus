namespace Plus.WebApi
{
    public class Response<TResult> : Response where TResult : class
    {
        public TResult Result { get; set; }
    }
}