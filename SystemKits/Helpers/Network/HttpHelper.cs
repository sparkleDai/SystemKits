using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;

namespace System
{
    /// <summary>
    /// HTTP帮助类
    /// </summary>
    public class HttpHelper
    {
        #region Get
        /// <summary>
        /// 执行基本的命令方法,以Get方式
        /// </summary>
        /// <param name="apiurl">请求的URL</param>
        /// <param name="headers">请求头的key-value字典</param>
        /// <param name="needReturnHeader">true:返回响应头,数据将以{Header:headerDict,Data:responseStr}的json格式返回，
        /// 其中headerDict为响应头的字典格式的数据，responseStr为请求返回的响应字符串.false:直接返回响应数据</param>
        /// <returns></returns>
        public static string Get(string apiurl, Dictionary<string, string> headers = null, bool needReturnHeader = false)
        {
            WebRequest request = WebRequest.Create(apiurl);
            request.Method = RequestMethod.GET;
            if (headers != null)
            {
                foreach (var keyValue in headers)
                {
                    request.Headers.Add(keyValue.Key, keyValue.Value);
                }
            }
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding encode = Encoding.UTF8;
            StreamReader reader = new StreamReader(stream, encode);
            string resultJson = reader.ReadToEnd();
            if (needReturnHeader)
            {
                Dictionary<string, string> headerDict = new Dictionary<string, string>();
                foreach (var key in response.Headers.AllKeys)
                {
                    headerDict.Add(key, response.Headers[key]);
                }
                var temp = new
                {
                    Header = headerDict,
                    Data = resultJson
                };
                return temp.ToJson();
            }
            else
            {
                return resultJson;
            }
        }

        /// <summary>
        /// 执行基本的命令方法,以Get方式
        /// </summary>
        /// <param name="apiurl">请求的URL</param>
        /// <param name="headers">请求头的key-value字典</param>
        /// <param name="needReturnHeader">true:返回响应头,数据将以{Header:headerData,Data:responseStr}的json格式返回，
        /// 其中headerData为响应头的数据，responseStr为请求返回的响应字符串.false:直接返回响应数据</param>
        /// <returns></returns>
        public static T Get<T>(string apiurl, Dictionary<string, string> headers = null, bool needReturnHeader = false)
        {
            string resultJson = Get(apiurl, headers);
            var result = JsonConvert.DeserializeObject<T>(resultJson);
            return result;
        }
        #endregion

        #region Post
        /// <summary>
        /// 以Post方式提交命令
        /// </summary>
        /// <param name="apiurl">请求的URL</param>
        /// <param name="jsonString">请求的json参数</param>
        /// <param name="headers">请求头的key-value字典</param>
        public static string Post(string apiurl, string jsonString, Dictionary<string, string> headers = null)
        {
            WebRequest request = WebRequest.Create(apiurl);
            request.Method = RequestMethod.POST;
            request.ContentType = RequestContentType.APPLICATION_JSON;
            if (headers != null)
            {
                foreach (var keyValue in headers)
                {
                    request.Headers.Add(keyValue.Key, keyValue.Value);
                }
            }

            if (String.IsNullOrEmpty(jsonString))
            {
                request.ContentLength = 0;
            }
            else
            {
                byte[] bs = Encoding.UTF8.GetBytes(jsonString);
                request.ContentLength = bs.Length;
                Stream newStream = request.GetRequestStream();
                newStream.Write(bs, 0, bs.Length);
                newStream.Close();
            }


            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding encode = Encoding.UTF8;
            StreamReader reader = new StreamReader(stream, encode);
            string resultJson = reader.ReadToEnd();
            return resultJson;
        }

        /// <summary>
        /// 以Post方式提交命令
        /// </summary>
        /// <param name="apiurl">请求的URL</param>
        /// <param name="jsonString">请求的json参数</param>
        /// <param name="headers">请求头的key-value字典</param>
        public static T Post<T>(string apiurl, string jsonString, Dictionary<string, String> headers = null)
        {
            string resultJson = Post(apiurl, jsonString, headers);
            var result = JsonConvert.DeserializeObject<T>(resultJson);
            return result;
        }


        /// <summary>
        /// 以Post方式提交命令
        /// </summary>
        /// <param name="apiurl">请求的URL</param>
        /// <param name="dataStream">请求的数据流</param>
        /// <param name="headers">请求头的key-value字典</param>
        public static String Post(string apiurl, byte[] dataStream, Dictionary<String, String> headers = null)
        {
            WebRequest request = WebRequest.Create(apiurl);
            request.Method = RequestMethod.POST;
            request.ContentType = RequestContentType.APPLICATION_OCTET_STREAM;
            if (headers != null)
            {
                foreach (var keyValue in headers)
                {
                    request.Headers.Add(keyValue.Key, keyValue.Value);
                }
            }

            if (dataStream == null || dataStream.Length <= 0)
            {
                request.ContentLength = 0;
            }
            else
            {
                request.ContentLength = dataStream.Length;
                Stream newStream = request.GetRequestStream();
                newStream.Write(dataStream, 0, dataStream.Length);
                newStream.Close();
            }

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding encode = Encoding.UTF8;
            StreamReader reader = new StreamReader(stream, encode);
            string resultJson = reader.ReadToEnd();
            return resultJson;
        }
        #endregion
    }

    /// <summary>
    /// 请求内容类型
    /// </summary>
    internal class RequestContentType
    {
        /// <summary>
        /// JSON数据
        /// </summary>
        public const string APPLICATION_JSON = "application/json";

        /// <summary>
        /// 流数据
        /// </summary>
        public const string APPLICATION_OCTET_STREAM = "application/octet-stream";

        /// <summary>
        /// 模拟表单数据
        /// </summary>
        public const string MULTIPART_FORM_DATA = "multipart/form-data";
    }

    /// <summary>
    /// 请求方法
    /// </summary>
    internal class RequestMethod
    {
        public const string GET = "GET";

        public const string POST = "POST";
    }
}
