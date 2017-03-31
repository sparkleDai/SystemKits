using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class EnumHelper
    {
        #region ToList Function
        /// <summary>
        /// 生成枚举列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<T> ToList<T>()
        {
            Type type = typeof(T);
            Array array = Enum.GetValues(type);
            List<T> enumList = new List<T>();
            foreach (var item in array)
            {
                enumList.Add((T)item);
            }
            return enumList;
        }
        #endregion

        #region GetEnumByDescription
        /// <summary>
        /// 通过枚举的描述获取对应的枚举值
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetEnumByDescription<T>(string description)
        {
            List<T> enumList = ToList<T>();
            string descriptionTemp = "";
            Enum itemEnum = default(Enum);
            Type type = typeof(T);
            foreach (var item in enumList)
            {
                itemEnum = (Enum)Enum.Parse(type, item.ToString());
                descriptionTemp = itemEnum.FetchDescription();
                if (string.IsNullOrEmpty(descriptionTemp))
                {
                    continue;
                }

                if (descriptionTemp == description)
                {
                    dynamic enumTemp = item;
                    return (T)enumTemp;
                }
            }

            return default(T);
        }
        #endregion
        
        #region GetEnum
        public static T GetEnum<T>(string enumStr)
        {
            var dest = (T)Enum.Parse(typeof(T), enumStr);
            return dest;
        }
        #endregion
    }
}
