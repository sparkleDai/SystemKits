using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Data
{
    /// <summary>
    /// DataTable扩展
    /// </summary>
    public static class DataTableExtension
    {
        #region ToList
        /// <summary> 
        /// 将DataTable转换成List
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> ToList<T>(this DataTable dt) where T : new()
        {
            // 定义集合 
            IList<T> modelList = new List<T>();

            // 获得此模型的类型 
            Type type = typeof(T);    
            // 获得此模型的公共属性 
            PropertyInfo[] propertys = typeof(T).GetProperties();

            foreach (DataRow dr in dt.Rows)
            {
                T model = dr.ToModel<T>(propertys);
                modelList.Add(model);
            }

            return modelList;

        }
        #endregion

        #region ToModel
        /// <summary> 
        /// 将DataRow转换成模型
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T ToModel<T>(this DataRow dr) where T : new()
        {
            // 获得此模型的公共属性 
            PropertyInfo[] propertys = typeof(T).GetProperties();
            T model = dr.ToModel<T>(propertys);           
            return model;
        }
        #endregion

        #region ToModel
        /// <summary> 
        /// 将DataRow转换成模型
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static T ToModel<T>(this DataRow dr, PropertyInfo[] propertys) where T : new()
        {
            T model = new T();            
            string tempName = "";
            foreach (PropertyInfo pi in propertys)
            {
                tempName = pi.Name;

                // 检查DataTable是否包含此列 
                if (dr.Table.Columns.Contains(tempName))
                {
                    // 判断此属性是否有Setter 
                    if (!pi.CanWrite)
                    {
                        continue;
                    }
                    object value = dr[tempName];
                    if (value != DBNull.Value)
                    {
                        pi.SetValue(model, value, null);
                    }
                }
            }
            return model;
        }
        #endregion

        #region ToRowList
        /// <summary>
        /// 将DataTable转换成DataRow的列表
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<DataRow> ToRowList(this DataTable t)
        {
            List<DataRow> list = new List<DataRow>();
            foreach (DataRow dr in t.Rows)
            {
                list.Add(dr);
            }
            return list;
        }
        #endregion

        #region ToColumnList
        /// <summary>
        /// 将DataTable转换成DataColumn列表
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<DataColumn> ToColumnList(this DataTable t)
        {
            List<DataColumn> list = new List<DataColumn>();
            foreach (DataColumn col in t.Columns)
            {
                list.Add(col);
            }
            return list;
        }
        #endregion
    }
}
