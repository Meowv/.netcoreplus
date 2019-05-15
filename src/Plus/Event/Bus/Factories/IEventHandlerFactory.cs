using Plus.Event.Bus.Handlers;

namespace Plus.Event.Bus.Factories
{
    /// <summary>
    /// 为负责创建，获取和发布事件处理程序定义的接口
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// 获取事件处理程序
        /// </summary>
        /// <returns></returns>
        IEventHandler GetHandler();

        /// <summary>
        /// 释放事件处理程序
        /// </summary>
        /// <param name="handler"></param>
        void ReleaseHandler(IEventHandler handler);
    }
}