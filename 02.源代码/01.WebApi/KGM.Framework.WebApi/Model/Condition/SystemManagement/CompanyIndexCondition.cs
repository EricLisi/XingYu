using KGM.Framework.Infrastructure;
using Newtonsoft.Json;

namespace KGM.Framework.WebApi.Model.Condition
{
    /// <summary>
    /// 公司主页面过滤条件
    /// </summary>
    public class CompanyIndexCondition: PagerCondition
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

        /// <summary>
        /// 公司Id
        /// </summary>
        [JsonProperty(PropertyName = "companyid", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyId { get; set; }
        /// <summary>
        ///父 Id
        /// </summary>
        [JsonProperty(PropertyName = "parentid", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentId { get; set; }

    }
}
