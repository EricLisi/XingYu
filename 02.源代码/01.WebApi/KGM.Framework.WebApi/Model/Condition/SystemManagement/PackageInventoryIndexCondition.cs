using KGM.Framework.Infrastructure;
using Newtonsoft.Json;

namespace KGM.Framework.WebApi.Model.Condition
{
    /// <summary>
    /// 包装仓库过滤条件
    /// </summary>
    public class PackageInventoryIndexCondition: PagerCondition
    {
        /// <summary>
        /// 图号编码
        /// </summary>
        [JsonProperty(PropertyName = "encode", NullValueHandling = NullValueHandling.Ignore)]
        public string EnCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [JsonProperty(PropertyName = "cinvname", NullValueHandling = NullValueHandling.Ignore)]
        public string cInvName { get; set; }
    }
}
