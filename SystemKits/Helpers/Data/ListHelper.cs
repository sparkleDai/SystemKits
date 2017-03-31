using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// list帮助类
    /// </summary>
    public static class ListHelper
    {
        #region IsEmpty Function
        /// <summary>
        /// List是否为空，即为null或者Count为零
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(IList<T> list)
        {
            if (list == null || list.Count <= 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region ToBindingList Function
        /// <summary>
        /// 将list转换成用于datagridview绑定的数据源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static BindingList<T> ToBindingList<T>(this List<T> list)
        {
            return new BindingList<T>(list);
        }
        #endregion 
    }
}
