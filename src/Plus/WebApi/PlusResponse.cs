using Plus.CodeAnnotations;
using System;

namespace Plus.WebApi
{
    [Serializable]
    public class Response
    {
        public string Code { get; set; } = "";

        public string Message { get; set; } = "";

        public string FullMessage { get; set; } = "";

        public DateTime Timestapm { get; set; } = DateTime.Now;

        public bool IsSuccess => Code.IsNullOrEmpty();

        public void SetMessage(ResponseStatusCode code)
        {
            SetMessage(code, code.ToAlias());
        }

        public void SetMessage(ResponseStatusCode code, string message)
        {
            SetMessage(code.ToInt().ToString(), message);
        }

        public void SetMessage(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public void HandleException(Exception ex)
        {
            SetMessage(ResponseStatusCode.InternalServerError, ex.Message);
            FullMessage = ex.InnerException?.StackTrace.ToString();
        }
    }
}