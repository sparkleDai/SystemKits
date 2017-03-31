using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    /// <summary>
    /// DES加密/解密帮助类
    /// </summary>
    public class DesEncryptHelper
    {
        #region Encrypt
        /// <summary>
        /// 进行DES加密。
        /// </summary>
        /// <param name="source">要加密的字符串。</param>
        /// <param name="key">密钥，且必须为8位。</param>
        /// <returns>以Base64格式返回的加密字符串。</returns>
        public static string EncryptToBase64(string source, string key)
        {
            var bytes = EncryptToBytes(source, key);
            string str = Convert.ToBase64String(bytes);
            return str;
        }

        /// <summary>
        /// 进行DES加密。
        /// </summary>
        /// <param name="source">要加密的字符串。</param>
        /// <param name="key">密钥，且必须为8位。</param>
        /// <returns>返回的十六进制的加密字符串。</returns>
        public static string EncryptToHexString(string source, string key)
        {
            var bytes = EncryptToBytes(source, key);
            StringBuilder ret = new StringBuilder();
            foreach (byte b in bytes)
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }


        /// <summary>
        /// 进行DES加密。
        /// </summary>
        /// <param name="source">要加密的字符串。</param>
        /// <param name="key">密钥，且必须为8位。</param>
        /// <returns>返回加密后的字节数组</returns>
        public static byte[] EncryptToBytes(string source, string key)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(source);
                des.Key = Encoding.ASCII.GetBytes(key);
                des.IV = Encoding.ASCII.GetBytes(key);
                var ms = new MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                var bytes = ms.ToArray();
                ms.Close();
                return bytes;
            }
        }
        #endregion

        #region Decrypt
        /// <summary>
        /// 进行DES解密。
        /// </summary>
        /// <param name="source">要解密的Base64字符串</param>
        /// <param name="key">密钥，且必须为8位。</param>
        /// <returns>已解密的字符串。</returns>
        public static string DecryptFromBase64(string source, string key)
        {
            byte[] inputByteArray = Convert.FromBase64String(source);
            return DecryptFromBytes(inputByteArray, key);
        }

        /// <summary>
        /// 进行DES解密。
        /// </summary>
        /// <param name="source">要解密的十六进制字符串</param>
        /// <param name="key">密钥，且必须为8位。</param>
        /// <returns>已解密的字符串。</returns>
        public static string DecryptFromHexString(string source, string key)
        {
            int len;
            len = source.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(source.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            return DecryptFromBytes(inputByteArray, key);
        }


        /// <summary>
        /// 进行DES解密。
        /// </summary>
        /// <param name="source">要解密的字节数组</param>
        /// <param name="key">密钥，且必须为8位。</param>
        /// <returns>已解密的字符串。</returns>
        public static string DecryptFromBytes(byte[] source, string key)
        {
            if (source == null || source.Length <= 0)
            {
                return "";
            }

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = Encoding.ASCII.GetBytes(key);
                des.IV = Encoding.ASCII.GetBytes(key);
                var ms = new MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(source, 0, source.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        #endregion


    }
}
