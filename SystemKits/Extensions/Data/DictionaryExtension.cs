using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 字典扩展
    /// </summary>
    public static class DictionaryExtension
    {
        #region Remove Function       
        /// <summary>
        /// 移除满足条件的数据
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="where"></param>
        public static void Remove<TKey, TValue>(this IDictionary<TKey, TValue> dict, Func<KeyValuePair<TKey, TValue>, bool> where)
        {
            if (dict == null || where == null)
            {
                return;
            }
            var enumerableList = dict.Where(t => where(t));
            if (enumerableList == null || !enumerableList.Any())
            {
                return;
            }
            var list = enumerableList.ToList();
            foreach (var item in list)
            {
                dict.Remove(item);
            }
        }
        #endregion
    }
}
