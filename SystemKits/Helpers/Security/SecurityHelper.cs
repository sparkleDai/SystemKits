using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    /// <summary>
    /// 加密帮助类
    /// </summary>
    public class SecurityHelper
    {

        #region EncryptRadom Function              
        /// <summary>
        /// 随机加密
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static String EncryptRadom(String original)
        {
            String radomStr = GenerateRadomStr();
            int unixTime = ConvertToUnixTimeStamp(DateTime.Now);
            String encryptStr = Sha1Hex(original + radomStr + unixTime.ToString());
            return encryptStr;
        }
        #endregion

        #region Sha1Hex
        /// <summary>
        /// SHA1的十六进制加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Sha1Hex(string value)
        {
            SHA1 algorithm = SHA1.Create();
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            string sh1 = "";
            for (int i = 0; i < data.Length; i++)
            {
                sh1 += data[i].ToString("x2").ToUpperInvariant();
            }
            return sh1;
        }
        #endregion

        #region GenerateRadomStr
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length">随机串的长度</param>
        /// <returns></returns>
        public static string GenerateRadomStr(int length = 16)
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string str = "";
            Random rad = new Random();
            for (int i = 0; i < length; i++)
            {
                str += chars.Substring(rad.Next(0, chars.Length - 1), 1);
            }
            return str;
        }
        #endregion

        #region ConvertToUnixTimeStamp      
        /// <summary>  
        /// 将DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>double</returns>  
        public static int ConvertToUnixTimeStamp(DateTime time)
        {
            int intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = Convert.ToInt32((time - startTime).TotalSeconds);
            return intResult;
        }
        #endregion       

   
    }
}
