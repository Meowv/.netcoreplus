using System;
using System.Collections.Generic;
using System.Text;

namespace Plus
{
    /// <summary>
    /// 输出
    /// </summary>
    public class ActionOutput
    {
        /// <summary>
        /// 错误列表
        /// </summary>
        public IList<string> Errors { get; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success => Errors.Count == 0;

        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; set; }

        public ActionOutput() => Errors = new List<string>();

        /// <summary>
        /// 添加错误消息
        /// </summary>
        /// <param name="error">错误消息</param>
        /// <param name="exception">异常</param>
        public void AddError(string error, Exception exception = null)
        {
            Errors.Add(error);
            Exception = exception;
        }

        /// <summary>
        /// 获取错误消息
        /// </summary>
        /// <returns></returns>
        public string GetErrorMessage()
        {
            if (Errors.Count > 0)
            {
                var stringBuilder = new StringBuilder();
                foreach (string error in Errors)
                {
                    stringBuilder.AppendLine(error);
                }
                return stringBuilder.ToString();
            }
            return string.Empty;
        }
    }
}