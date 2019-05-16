namespace Plus
{
    /// <summary>
    /// 输出 <see cref="T"/>
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