namespace Plus
{
    /// <summary>
    /// ActionOutput
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionOutput<T> : ActionOutput
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public T Result { get; set; }
    }
}