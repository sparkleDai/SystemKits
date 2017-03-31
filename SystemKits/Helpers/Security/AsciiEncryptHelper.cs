using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Ascii加密/解密帮助类
    /// </summary>
    public class AsciiEncryptHelper
    {
        #region AsciiEncrypt
        /// <summary>
        /// ASCII码串来加密数据
        /// </summary>
        /// <returns></returns>
        public static String AsciiEncrypt(String source)
        {
            //产生8位的随机加密的KEY
            string key = SecurityHelper.GenerateRadomStr(8);

            //加密字符串
            string encryptStr = DesEncryptHelper.EncryptToBase64(source, key);

            //产生8位密码存放位置的随机字符串
            //第1位标识第1位密码在密码容器中的存放位置
            //第2位标识第2位密码在密码容器中的存放位置
            //依此类推存放8位随机密码
            string keyFlag = SecurityHelper.GenerateRadomStr(8);

            //产生存放密码的容器
            string keyContainer = GuidHelper.Generate() + GuidHelper.Generate() + GuidHelper.Generate();
            keyContainer = Convert.ToBase64String(Encoding.UTF8.GetBytes(keyContainer));
            //由于密码是小写字母、大写字母和9个数字随机生成的，所以其ASCII码必定小于大写Z的ASCII码
            int z_ascii = 122;
            keyContainer = keyContainer.Substring(0, z_ascii);

            //将密码存放到密码容器中
            int flagPosition = 0;
            for (int i = 0; i < keyFlag.Length; i++)
            {
                flagPosition = (int)keyFlag[i];
                keyContainer = keyContainer.Insert(flagPosition, key.Substring(i, 1));
            }

            //将加密码串BASE64编码后，再与密码标识串和密码容器拼接成新的加密串。
            encryptStr = keyFlag + ";" + keyContainer + Convert.ToBase64String(Encoding.UTF8.GetBytes(encryptStr));
            return encryptStr;
        }
        #endregion

        #region AsciiDecrypt
        /// <summary>
        /// ASCII解密
        /// </summary>
        /// <returns></returns>
        public static String AsciiDecrypt(String source)
        {
            if (String.IsNullOrEmpty(source))
            {
                throw new Exception("source is null or empty.");
            }

            //加密码至少要9位以上
            if (source.Length < 9)
            {
                throw new Exception("source length < 9");
            }

            //加密串第9位固定为';'
            if (source[8] != ';')
            {
                throw new Exception("source is illegal.9 position must be ';'");
            }

            //从加密串中分离中密码标识串与密码容器及真实的加密串
            var tempArr = source.Split(';');
            string keyFlag = tempArr[0];//密码标识串
            string keyContainerAndEncryptStr = tempArr[1];//密码容器及真实的加密串

            //提取加密的key
            int flagPosition = 0;
            string key = "";
            for (int i = keyFlag.Length - 1; i >= 0; i--)
            {
                flagPosition = (int)keyFlag[i];
                key = keyContainerAndEncryptStr.Substring(flagPosition, 1) + key;
                keyContainerAndEncryptStr = keyContainerAndEncryptStr.Remove(flagPosition, 1);
            }

            //提取真实的加密串
            int z_ascii = 122;
            keyContainerAndEncryptStr = keyContainerAndEncryptStr.Substring(z_ascii);
            keyContainerAndEncryptStr = Encoding.UTF8.GetString(Convert.FromBase64String(keyContainerAndEncryptStr));

            //解密加密串
            string decryptStr = DesEncryptHelper.DecryptFromBase64(keyContainerAndEncryptStr, key);

            return decryptStr;
        }
        #endregion

        #region IsAsciiEncrypt
        /// <summary>
        /// 是否为ASCII加密串
        /// </summary>
        /// <returns></returns>
        public static bool IsAsciiEncrypt(String source)
        {
            if (String.IsNullOrEmpty(source))
            {
                return false;
            }

            //加密码至少要9位以上
            if (source.Length < 9)
            {
                return false;
            }

            //加密串第9位固定为';'
            if (source[8] != ';')
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
