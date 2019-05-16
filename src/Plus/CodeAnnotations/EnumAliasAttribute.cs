using System;

namespace Plus.CodeAnnotations
{
    /// <summary>
    /// 枚举属性，定义别名
    /// </summary>
    public class EnumAliasAttribute : Attribute
    {
        public string Alias { get; set; }

        public EnumAliasAttribute(string alias)
        {
            Alias = alias;
        }
    }
}