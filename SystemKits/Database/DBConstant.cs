using System;
using System.Configuration;

namespace SystemKits.DBUtility
{
    /// <summary>
    /// 数据库常量
    /// </summary>
    internal class DBConstant
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        internal static string ConnectionString
        {
            get
            {
                string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];
                return _connectionString;
            }
        }

        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName">web.config中连接串的名称</param>
        /// <returns></returns>
        internal static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.AppSettings[configName];
            return connectionString;
        }


    }
}
