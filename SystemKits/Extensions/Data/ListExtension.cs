using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class ListExtension
    {
        #region Copy Function
        /// <summary>
        /// 复制(仅针对值类型,比如int、byte、enum)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> Copy<T>(this List<T> list)
        {
            if (list == null)
            {
                return null;
            }

            T temp = default(T);
            if (temp is ValueType)
            {
                throw new Exception(typeof(T).FullName + " is not ValueType");
            }

            List<T> newList = new List<T>();
            foreach (var item in list)
            {
                temp = item;
                newList.Add(temp);
            }
            return newList;
        }
        #endregion

        #region Remove
        /// <summary>
        /// 移除符合条件的第一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">数据源</param>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public static bool Remove<T>(this List<T> list, Func<T, bool> predicate)
        {
            if (ListHelper.IsEmpty(list))
            {
                return false;
            }

            if (predicate == null)
            {
                return false;
            }

            var removeObj = list.Find(t => predicate(t));
            if (removeObj == null)
            {
                return true;
            }

            bool isOk = list.Remove(removeObj);
            return isOk;
        }
        #endregion

        #region RemoveMulty
        /// <summary>
        /// 移除符合条件的所有对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">数据源</param>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public static bool RemoveMulty<T>(this List<T> list, Func<T, bool> predicate)
        {
            if (ListHelper.IsEmpty(list))
            {
                return false;
            }

            if (predicate == null)
            {
                return false;
            }

            var removeObjList = list.FindAll(t => predicate(t));
            if (removeObjList == null || removeObjList.Count <= 0)
            {
                return true;
            }

            foreach (var item in removeObjList)
            {
                list.Remove(item);
            }
            return true;
        }
        #endregion

        #region InsertNext
        /// <summary>
        /// 在指定位置的下一个位置插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">列表</param>
        /// <param name="postion">指定的位置</param>
        /// <param name="item">插入的项</param>
        public static void InsertNext<T>(this IList<T> list, int postion, T item)
        {
            int insertLoopSetIndex = postion + 1;
            if (insertLoopSetIndex < 0)
            {
                throw new Exception("给定的位置无法插入数据");
            }

            if (insertLoopSetIndex <= list.Count - 1)
            {
                list.Insert(insertLoopSetIndex, item);
            }
            else
            {
                list.Add(item);
            }
        }
        #endregion

        #region InsertPrevious
        /// <summary>
        /// 在指定位置的前一个位置插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">列表</param>
        /// <param name="postion">指定的位置</param>
        /// <param name="item">插入的项</param>
        public static void InsertPrevious<T>(this IList<T> list, int postion, T item)
        {
            int insertLoopSetIndex = postion - 1;
            if (insertLoopSetIndex >= 0 && insertLoopSetIndex <= list.Count - 1)
            {
                list.Insert(insertLoopSetIndex, item);
            }
            else
            {
                list.Insert(0, item);
            }
        }
        #endregion
    }
}
