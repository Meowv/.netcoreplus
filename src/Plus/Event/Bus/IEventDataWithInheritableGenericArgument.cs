namespace Plus.Event.Bus
{
    /// <summary>
    /// 此接口必须由具有单个泛型参数的事件数据类实现
    /// </summary>
    public interface IEventDataWithInheritableGenericArgument
    {
        /// <summary>
        /// 获取创建该类的参数
        /// </summary>
        /// <returns></returns>
        object[] GetConstructorArgs();
    }
}