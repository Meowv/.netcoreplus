using System;
using System.Reflection;

namespace Plus.CodeAnnotations
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举别名
        /// </summary>
        /// <param name="_enum"></param>
        /// <returns></returns>
        public static string ToAlias(this Enum _enum)
        {
            var type = _enum.GetType();
            var field = type.GetField(_enum.ToString());
            if (field == null)
            {
                return string.Empty;
            }

            var result = string.Empty;

            var customAttributes = field.GetCustomAttributes(typeof(EnumAliasAttribute), inherit: false);
            var array = customAttributes;
            for (int i = 0; i < array.Length; i++)
            {
                var enumAliasAttribute = (EnumAliasAttribute)array[i];
                result = enumAliasAttribute.Alias;
            }
            return result;
        }
    }
}