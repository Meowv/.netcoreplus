using System;

namespace Plus.CodeAnnotations
{
    /// <summary>
    /// 枚举属性，定义别名
    /// </summary>
    public class EnumAliasAttribute : Attribute
    {
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 枚举别名属性
        /// </summary>
        /// <param name="alias"></param>
        public EnumAliasAttribute(string alias)
        {
            Alias = alias;
        }
    }
}