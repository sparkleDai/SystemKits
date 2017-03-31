using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 颜色扩展类
    /// </summary>
    public static  class ColorExtension
    {
        #region ToArgbString
        /// <summary>
        /// 将颜色转换成ARGB字符串储存
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToArgbString(this Color source)
        {
            string str = $"{source.A.ToString("X2")}{source.R.ToString("X2")}{source.G.ToString("X2")}{source.B.ToString("X2")}";
            return str;
        }
        #endregion
    }
}
