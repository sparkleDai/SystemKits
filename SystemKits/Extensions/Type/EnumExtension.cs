using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtension
    {
        #region FetchDescription
        /// <summary>
        /// 获取枚举值的描述文本<seealso cref="DescriptionAttribute"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FetchDescription(this Enum value)
        {
            var attribute = value.FetchAttribute<DescriptionAttribute>();
            if (attribute == null)
            {
                return value.ToString();
            }
            else
            {
                return attribute.Description;
            }
        }
        #endregion

        #region FetchAttribute
        /// <summary>
        /// 获取枚举值的<seealso cref="Attribute"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T FetchAttribute<T>(this Enum value) where T : Attribute
        {
            //TEST
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (T[])fi.GetCustomAttributes(typeof(T), false);
            if (attributes.Length <= 0)
            {
                return null;
            }
            T attribute = attributes[0];
            return attribute;
        }
        #endregion
    }
}
