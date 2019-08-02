using KGM.Framework.Infrastructure;
using Newtonsoft.Json;

namespace KGM.Framework.WebApi.Model.Condition
{
    /// <summary>
    /// 模块主页面过滤条件
    /// </summary> 
    public class DictionaryIndexCondition : PagerCondition
    {

        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty(PropertyName = "encode", NullValueHandling = NullValueHandling.Ignore)]
        public string EnCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty(PropertyName = "fullname", NullValueHandling = NullValueHandling.Ignore)]
        public string FullName { get; set; }
        
    }
}
