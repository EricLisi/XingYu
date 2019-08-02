using Newtonsoft.Json;

namespace KGM.Framework.WebApi.Model.Condition
{
    /// <summary>
    /// 分页过滤对象
    /// </summary>
    public class PagerCondition
    {
        /// <summary>
        /// 每页多少行
        /// </summary>
        [JsonProperty(PropertyName = "rows", NullValueHandling = NullValueHandling.Ignore, Required = Required.Always)]
        public int Rows { get; set; }

        /// <summary>
        /// 当前第几页
        /// </summary>
        [JsonProperty(PropertyName = "page", NullValueHandling = NullValueHandling.Ignore, Required = Required.Always)]
        public int Page { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        [JsonProperty(PropertyName = "sidx", NullValueHandling = NullValueHandling.Ignore)]
        public string SIdx { get; set; }

        /// <summary>
        /// 排序顺序
        /// </summary>
        [JsonProperty(PropertyName = "sord", NullValueHandling = NullValueHandling.Ignore)]
        public string Sord { get; set; }
    }
}
