using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 颜色帮助类
    /// </summary>
    public class ColorHelper
    {
        #region ToColor
        /// <summary>
        /// 从Argb字符串中转换成颜色。        
        /// </summary>
        /// <param name="argbString">ARGB每个值占两个字符，长度必须为8</param>
        /// <returns></returns>
        public static Color ToColor(string argbString)
        {
            if (String.IsNullOrEmpty(argbString))
            {
                throw new Exception("argbString is null or empty.");
            }

            if (argbString.Length != 8)
            {
                throw new Exception("argbString长度必须为8");
            }

            int a = Convert.ToByte(argbString.Substring(0, 2), 16);
            int r = Convert.ToByte(argbString.Substring(2, 2), 16);
            int g = Convert.ToByte(argbString.Substring(4, 2), 16);
            int b = Convert.ToByte(argbString.Substring(6, 2), 16);
            return Color.FromArgb(a, r, g, b);
        }
        #endregion

        #region GenerateRandomColor
        /// <summary>
        /// 生成随机颜色
        /// </summary>
        /// <returns></returns>
        public static Color GenerateRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);

            //  为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;

            return System.Drawing.Color.FromArgb(int_Red, int_Green, int_Blue);
        }
        #endregion
    }
}
