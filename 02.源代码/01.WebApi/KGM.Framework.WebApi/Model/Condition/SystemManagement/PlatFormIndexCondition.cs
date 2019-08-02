using Newtonsoft.Json;

namespace KGM.Framework.WebApi.Model.Condition
{
    /// <summary>
    /// 平台主页面过滤条件
    /// </summary> 
    public class PlatFormIndexCondition : PagerCondition
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        [JsonProperty(PropertyName = "encode", NullValueHandling = NullValueHandling.Ignore)]
        public string EnCode { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [JsonProperty(PropertyName = "fullname", NullValueHandling = NullValueHandling.Ignore)]
        public string FullName { get; set; }
    }
}
