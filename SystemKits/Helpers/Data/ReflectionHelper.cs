using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public class ReflectionHelper
    {
        #region FetchValue
        /// <summary>
        /// 依据字段名称从T中获取字段的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public static object FetchValue<T>(T t, string fieldName)
        {
            Type type = t.GetType();
            object dest = type.GetField(fieldName).GetValue(t);
            return dest;
        }
        #endregion

        #region FetchValueFromT2ByT1
        /// <summary>
        /// 依据T1的值，从T1中找出该值的字段名,
        /// 将该字段名结合前缀propertyPrefix和后缀suffix，组合出新的属性名，
        /// 从T2中找到新属性名对应的值.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <param name="t1Value"></param>
        /// <param name="propertyPrefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static object FetchValueFromT2ByT1<T1, T2>(T1 t1, T2 t2, object t1Value, String propertyPrefix, String suffix)
        {
            Type type = t1.GetType();
            FieldInfo[] fieldInfos = type.GetFields();
            String propertyName = "";
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                if (fieldInfo.GetValue(t1).ToString().Equals(t1Value))
                {
                    propertyName = fieldInfo.Name;
                    break;
                }
            }
            if (String.IsNullOrWhiteSpace(propertyName))
            {
                return null;
            }

            type = t2.GetType();
            object dest = type.GetField(propertyPrefix + propertyName + suffix).GetValue(t2);
            return dest;
        }
        #endregion

        #region GenerateT1FieldOfT2ValueDictionary
        /// <summary>
        /// 生成T1字段中对应T2值的字典
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="propertyPrefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static Dictionary<String, String> GenerateT1FieldOfT2ValueDictionary<T1, T2>(String propertyPrefix, String suffix)
        {
            Type type = typeof(T2);
            FieldInfo[] fieldInfos_t2 = type.GetFields();
            String propertyName = "";
            FieldInfo[] fieldInfos_t1 = typeof(T1).GetFields();
            T2 t2 = default(T2);
            Dictionary<String, String> dict = new Dictionary<string, string>();
            foreach (FieldInfo fieldInfo_t1 in fieldInfos_t1)
            {
                propertyName = fieldInfo_t1.Name;
                foreach (FieldInfo fieldInfo_t2 in fieldInfos_t2)
                {
                    if (fieldInfo_t2.Name.Equals(propertyPrefix + propertyName + suffix))
                    {
                        dict[propertyName] = String.Format("{0}", type.GetField(propertyPrefix + propertyName + suffix).GetValue(t2));
                        break;
                    }
                }
            }
            return dict;
        }
        #endregion

        #region GenerateT1ValueOfT2ValueDictionary
        /// <summary>
        /// 生成T1字段值对应T2字段值的字典
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="propertyPrefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static Dictionary<String, String> GenerateT1ValueOfT2ValueDictionary<T1, T2>(String propertyPrefix, String suffix)
        {
            Type type_t1 = typeof(T1);
            Type type_t2 = typeof(T2);
            FieldInfo[] fieldInfos_t2 = type_t2.GetFields();
            String propertyName = "";
            FieldInfo[] fieldInfos_t1 = type_t1.GetFields();
            T1 t1 = default(T1);
            T2 t2 = default(T2);
            Dictionary<String, String> dict = new Dictionary<string, string>();
            foreach (FieldInfo fieldInfo_t1 in fieldInfos_t1)
            {
                propertyName = fieldInfo_t1.Name;
                foreach (FieldInfo fieldInfo_t2 in fieldInfos_t2)
                {
                    if (fieldInfo_t2.Name.Equals(propertyPrefix + propertyName + suffix))
                    {
                        dict[String.Format("{0}", type_t1.GetField(propertyName).GetValue(t1))] =
                            String.Format("{0}", type_t2.GetField(propertyPrefix + propertyName + suffix).GetValue(t2));
                        break;
                    }
                }
            }
            return dict;
        }
        #endregion
    }
}
