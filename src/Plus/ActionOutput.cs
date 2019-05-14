using System;
using System.Collections.Generic;
using System.Text;

namespace Plus
{
    /// <summary>
    /// Action Output
    /// </summary>
    public class ActionOutput
    {
        /// <summary>
        /// Errors List
        /// </summary>
        public IList<string> Errors { get; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success => Errors.Count == 0;

        /// <summary>
        /// Exception
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