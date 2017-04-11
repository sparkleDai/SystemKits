using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class ArithmeticExtension
    {
        #region CeilToInt
        public static int CeilToInt(this float v)
        {
            return (int)Math.Ceiling(v);
        }

        public static int CeilToInt(this double v)
        {
            return (int)Math.Ceiling(v);
        }
        #endregion


        #region FloorToInt
        public static int FloorToInt(this float v)
        {
            return (int)Math.Floor(v);
        }

        public static int FloorToInt(this double v)
        {
            return (int)Math.Floor(v);
        }
        #endregion

    }
}
