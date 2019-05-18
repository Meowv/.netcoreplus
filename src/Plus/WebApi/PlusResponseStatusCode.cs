using Plus.CodeAnnotations;

namespace Plus.WebApi
{
    public enum ResponseStatusCode
    {
        [EnumAlias("参数有误")]
        RequestParameterIsWrong = -1,

        [EnumAlias("错误消息返回")]
        Error = 0,

        [EnumAlias("成功返回")]
        Ok = 1,

        [EnumAlias("服务器内部错误")]
        InternalServerError = 100,

        [EnumAlias("系统已关闭")]
        SystemClose = 200,

        [EnumAlias("签名失败")]
        SignatureFailed = 300,

        [EnumAlias("身份授权失败")]
        Unauthorized = 401
    }
}