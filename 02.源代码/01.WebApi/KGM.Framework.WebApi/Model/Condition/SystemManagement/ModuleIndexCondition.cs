using KGM.Framework.Infrastructure;
using Newtonsoft.Json;

namespace KGM.Framework.WebApi.Model.Condition
{
    /// <summary>
    /// 模块主页面过滤条件
    /// </summary> 
    public class ModuleIndexCondition: PagerCondition
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [JsonProperty(PropertyName = "userid", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

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

        /// <summary>
        /// 父模块Id
        /// </summary>
        [JsonProperty(PropertyName = "moduleid", NullValueHandling = NullValueHandling.Ignore)]
        public string ModuleId { get; set; }  
    }
}
