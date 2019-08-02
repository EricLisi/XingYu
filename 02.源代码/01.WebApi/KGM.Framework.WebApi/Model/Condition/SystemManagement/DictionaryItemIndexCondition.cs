using KGM.Framework.Infrastructure;
using Newtonsoft.Json;

namespace KGM.Framework.WebApi.Model.Condition
{
    /// <summary>
    /// 模块主页面过滤条件
    /// </summary> 
    public class DictionaryItemIndexCondition : PagerCondition
    {

        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty(PropertyName = "itemcode", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty(PropertyName = "itemname", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemName { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        [JsonProperty(PropertyName = "itemid", NullValueHandling = NullValueHandling.Ignore)]
        public string ItemId { get; set; }  
    }
}
