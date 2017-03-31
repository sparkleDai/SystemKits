using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Base64扩展
    /// </summary>
    public static class Base64Extension
    {
        #region ToBase64
        /// <summary>
        /// 转换成base64的字符串
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ToBase64(this Stream stream)
        {
            if (stream == null || stream.Length <= 0)
            {
                return null;
            }

            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            String buffStr = Convert.ToBase64String(buffer);
            return buffStr;
        }

        /// <summary>
        /// 将字符串转换成Base64字串.
        /// </summary>
        /// <param name="sourceStr">需编码的字符串</param>
        /// <param name="encoding">默认为utf-8编码</param>
        /// <returns></returns>
        public static string ToBase64(this String sourceStr, Encoding encoding = null)
        {
            if (String.IsNullOrWhiteSpace(sourceStr))
            {
                return "";
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            String base64Str = Convert.ToBase64String(encoding.GetBytes(sourceStr));
            return base64Str;
        }

        /// <summary>
        /// 转换成base64的字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string ToBase64(this byte[] buffer)
        {
            if (buffer == null || buffer.Length <= 0)
            {
                return null;
            }

            String buffStr = Convert.ToBase64String(buffer);
            return buffStr;
        }
        #endregion
    }
}
