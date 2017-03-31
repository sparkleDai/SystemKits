using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 几何帮助类
    /// </summary>
    public class GeometryHelper
    {
        #region CalculateP1ToP2P3Distance Function    
        /// <summary>
        /// 计算点p1到点p2、p3组成的直线的距离
        /// </summary>
        /// <param name="p1_x"></param>
        /// <param name="p1_y"></param>
        /// <param name="p2_x"></param>
        /// <param name="p2_y"></param>
        /// <param name="p3_x"></param>
        /// <param name="p3_y"></param>
        /// <returns></returns>
        public static double CalculateP1ToP2P3Distance(double p1_x, double p1_y, double p2_x, double p2_y, double p3_x, double p3_y)
        {
            //利用海伦公式求出面积
            double p1p2 = Math.Sqrt((p1_x - p2_x) * (p1_x - p2_x) + (p1_y - p2_y) * (p1_y - p2_y));
            double p2p3 = Math.Sqrt((p2_x - p3_x) * (p2_x - p3_x) + (p2_y - p3_y) * (p2_y - p3_y));
            double p3p1 = Math.Sqrt((p1_x - p3_x) * (p1_x - p3_x) + (p1_y - p3_y) * (p1_y - p3_y));
            double L = p1p2 + p2p3 + p3p1;
            double p = L / 2;
            double S = Math.Sqrt(p * (p - p1p2) * (p - p2p3) * (p - p3p1));

            //利用面积公式求出距离.S=(底*高)/2;
            double d = (2 * S) / p2p3;
            return d;
        }
        #endregion        
    }
}
