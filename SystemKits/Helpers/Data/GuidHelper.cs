using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// GUID帮助类
    /// </summary>
    public class GuidHelper
    {
        #region Generate Function         
        /// <summary>
        /// 生成一个GUID
        /// </summary>
        /// <returns></returns>
        public static String Generate()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }
        #endregion
    }
}
