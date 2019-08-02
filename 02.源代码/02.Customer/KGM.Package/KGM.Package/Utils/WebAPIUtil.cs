using KGM.Package.Models;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace KGM.Package.Utils
{
    public static class WebAPIUtil
    {
        /// <summary>
        /// 将一个对象转换成json
        /// </summary>
        /// <returns></returns>
        public static string ConvertObjToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 将一个json转换为对象
        /// </summary>
        /// <returns></returns>
        public static T ConvertJsonToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// post方式提交 json
        /// </summary>
        /// <param name="Url">方法url 不需要全路径 只需要方法段路径</param>
        /// <param name="JSONData">传入的json信息</param>
        /// <returns></returns>
        public static string PostAPIByJson(string Url, string JSONData)
        {
            return GetWebAPIResponseData(Url, "post", ConstValue.DEFALUT_CONTENTTYPE, JSONData);
        }

        /// <summary>
        /// get方式提交 json
        /// </summary>
        /// <param name="Url">方法url 不需要全路径 只需要方法段路径</param> 
        /// <returns></returns>
        public static string GetAPIByJson(string Url)
        {
            return GetWebAPIResponseData(Url, "get", ConstValue.DEFALUT_CONTENTTYPE, string.Empty);
        }


        /// <summary>
        /// delete方式提交 json
        /// </summary>
        /// <param name="Url">方法url 不需要全路径 只需要方法段路径</param> 
        /// <returns></returns>
        public static string DeleteAPIByJson(string Url)
        {
            return GetWebAPIResponseData(Url, "delete", ConstValue.DEFALUT_CONTENTTYPE, string.Empty);
        }



        /// <summary>
        /// post方式提交 json  返回一个result对象
        /// </summary>
        /// <param name="Url">方法url 不需要全路径 只需要方法段路径</param>
        /// <param name="JSONData">传入的json信息</param>
        /// <returns></returns>
        public static KgmApiResultEntity PostAPIByJsonToAPIResult(string Url, string JSONData)
        {
            string returnjson = GetWebAPIResponseData(Url, "post", ConstValue.DEFALUT_CONTENTTYPE, JSONData);
            return JsonConvert.DeserializeObject<KgmApiResultEntity>(returnjson);
        }

        /// <summary>
        /// get方式提交 json 返回一个result对象
        /// </summary>
        /// <param name="Url">方法url 不需要全路径 只需要方法段路径</param> 
        /// <returns></returns>
        public static KgmApiResultEntity GetToAPIByJsonToAPIResult(string Url)
        {
            string returnjson = GetWebAPIResponseData(Url, "get", ConstValue.DEFALUT_CONTENTTYPE, string.Empty);
            return JsonConvert.DeserializeObject<KgmApiResultEntity>(returnjson);
        }


        /// <summary>
        /// delete方式提交 json 返回一个result对象
        /// </summary>
        /// <param name="Url">方法url 不需要全路径 只需要方法段路径</param> 
        /// <returns></returns>
        public static KgmApiResultEntity DeleteToAPIByJsonToAPIResult(string Url)
        {
            string returnjson = GetWebAPIResponseData(Url, "delete", ConstValue.DEFALUT_CONTENTTYPE, string.Empty);
            return JsonConvert.DeserializeObject<KgmApiResultEntity>(returnjson);
        }


        /// <summary>
        /// Put方式提交 json  返回一个泛型对象
        /// </summary>
        /// <param name="Url">方法url 不需要全路径 只需要方法段路径</param>
        /// <param name="JSONData">传入的json信息</param>
        /// <returns></returns>
        public static KgmApiResultEntity PutToAPIByJsonToAPIResult(string Url, string JSONData)
        {
            string returnjson = GetWebAPIResponseData(Url, "put", ConstValue.DEFALUT_CONTENTTYPE, JSONData);
            return JsonConvert.DeserializeObject<KgmApiResultEntity>(returnjson);
        }


        /// <summary>
        /// post方式提交 json  返回一个泛型对象
        /// </summary>
        /// <param name="Url">方法url 不需要全路径 只需要方法段路径</param>
        /// <param name="JSONData">传入的json信息</param>
        /// <returns></returns>
        public static T PostAPIByJsonToGeneric<T>(string Url, string JSONData)
        {
            string returnjson = GetWebAPIResponseData(Url, "post", ConstValue.DEFALUT_CONTENTTYPE, JSONData);
            return JsonConvert.DeserializeObject<T>(returnjson);
        }

        /// <summary>
        /// get方式提交 json  返回一个泛型对象
        /// </summary>
        /// <param name="Url">方法url 不需要全路径 只需要方法段路径</param>
        /// <param name="JSONData">传入的json信息</param>
        /// <returns></returns>
        public static T GetAPIByJsonToGeneric<T>(string Url)
        {
            string returnjson = GetWebAPIResponseData(Url, "get", ConstValue.DEFALUT_CONTENTTYPE, string.Empty);
            return JsonConvert.DeserializeObject<T>(returnjson);
        }

        /// <summary>
        /// delete方式提交 json  返回一个泛型对象
        /// </summary>
        /// <param name="Url">方法url 不需要全路径 只需要方法段路径</param>
        /// <param name="JSONData">传入的json信息</param>
        /// <returns></returns>
        public static T DeleteAPIByJsonToGeneric<T>(string Url)
        {
            string returnjson = GetWebAPIResponseData(Url, "delete", ConstValue.DEFALUT_CONTENTTYPE, string.Empty);
            return JsonConvert.DeserializeObject<T>(returnjson);
        }


        /// <summary>
        /// Put方式提交 json  返回一个泛型对象
        /// </summary>
        /// <param name="Url">方法url 不需要全路径 只需要方法段路径</param>
        /// <param name="JSONData">传入的json信息</param>
        /// <returns></returns>
        public static T PutAPIByJsonToGeneric<T>(string Url, string JSONData)
        {
            string returnjson = GetWebAPIResponseData(Url, "put", ConstValue.DEFALUT_CONTENTTYPE, JSONData);
            return JsonConvert.DeserializeObject<T>(returnjson);
        }


        /// <summary>
        /// 调用API
        /// </summary>
        /// <param name="url">方法url</param>
        /// <param name="method">post get put...</param>
        /// <param name="body">参数格式</param>
        /// <param name="contentType">参数类型</param>
        /// <returns></returns>
        private static string GetWebAPIResponseData(string url, string method, string contentType, string body)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(CommonValue.WebAPIUri.AbsoluteUri + url);
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = method;
            httpWebRequest.Timeout = 20000; 
            if (!string.IsNullOrEmpty(CommonValue.token))
            {
                httpWebRequest.Headers.Add(string.Format("Authorization:Bearer {0} ", CommonValue.token));
            }

            if (!string.IsNullOrEmpty(body) && method.ToLower() != "get")
            {
                byte[] btBodys = Encoding.UTF8.GetBytes(body);
                httpWebRequest.ContentLength = btBodys.Length;
                httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);
            }

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();
            httpWebRequest.Abort();
            httpWebResponse.Close();

            return responseContent;
        }


        /// <summary>
        /// 将json转换为DataTable
        /// </summary>
        /// <param name="strJson">得到的json</param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string strJson)
        {
            //转换json格式
            strJson = strJson.Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名   
            //strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            //strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split('*');

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split('#');

                        if (strCell[0].Substring(0, 1) == "\"")
                        {
                            int a = strCell[0].Length;
                            dc.ColumnName = strCell[0].Substring(1, a - 2);
                        }
                        else
                        {
                            dc.ColumnName = strCell[0];
                        }
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
        }
    }
}
