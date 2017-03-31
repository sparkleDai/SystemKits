using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {
        #region FetchFileEncodeType
        /// <summary>
        /// 获取文件的编码
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Encoding FetchFileEncodeType(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            Byte[] buffer = br.ReadBytes(2);
            System.Text.Encoding encoding = System.Text.Encoding.Default;
            if (buffer[0] >= 0xEF)
            {
                if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                {
                    encoding = Encoding.UTF8;
                }
                else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                {
                    encoding = Encoding.BigEndianUnicode;
                }
                else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                {
                    encoding = Encoding.Unicode;
                }
                else
                {
                    encoding = Encoding.Default;
                }
            }
            else
            {
                encoding = Encoding.Default;
            }
            br.Close();
            fs.Close();
            return encoding;
        }
        #endregion
    }
}
