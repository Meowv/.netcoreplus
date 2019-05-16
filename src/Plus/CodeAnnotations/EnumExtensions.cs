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
            Type type = _enum.GetType();
            FieldInfo field = type.GetField(_enum.ToString());
            if (field == null)
            {
                return string.Empty;
            }

            string result = string.Empty;

            object[] customAttributes = field.GetCustomAttributes(typeof(EnumAliasAttribute), inherit: false);
            object[] array = customAttributes;
            for (int i = 0; i < array.Length; i++)
            {
                EnumAliasAttribute enumAliasAttribute = (EnumAliasAttribute)array[i];
                result = enumAliasAttribute.Alias;
            }
            return result;
        }
    }
}