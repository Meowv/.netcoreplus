namespace Plus.Runtime.Validation
{
    /// <summary>
    /// 此接口用于在方法执行之前对输入进行规范化
    /// </summary>
    public interface IShouldNormalize
    {
        /// <summary>
        /// 方法最后在方法执行之前调用(如果存在，则在验证之后调用)
        /// </summary>
        void Normalize();
    }
}