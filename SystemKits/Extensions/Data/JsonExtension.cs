using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace System
{
    public static class JsonExtension
    {
        #region ToJson
        /// <summary>
        /// 将对象转换成json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            if (obj == null)
            {
                return "";
            }
            var json = JsonConvert.SerializeObject(obj);
            return json;
        }
        #endregion
    }
}
