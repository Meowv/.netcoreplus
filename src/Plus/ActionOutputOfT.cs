namespace Plus
{
    /// <summary>
    /// Action Output <see cref="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionOutput<T> : ActionOutput
    {
        /// <summary>
        /// <see cref="Result"/>
        /// </summary>
        public T Result { get; set; }
    }
}