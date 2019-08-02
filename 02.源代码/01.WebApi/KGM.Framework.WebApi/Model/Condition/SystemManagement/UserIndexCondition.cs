using KGM.Framework.Infrastructure;
using Newtonsoft.Json;

namespace KGM.Framework.WebApi.Model.Condition
{
    /// <summary>
    /// 用户主页面过滤条件
    /// </summary> 
    public class UserIndexCondition: PagerCondition
    {
        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty(PropertyName = "encode", NullValueHandling = NullValueHandling.Ignore)]
        public string EnCode { get; set; }
          
        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty(PropertyName = "realname", NullValueHandling = NullValueHandling.Ignore)]
        public string RealName { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>
        [JsonProperty(PropertyName = "companyid", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyId { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        [JsonProperty(PropertyName = "departmentid", NullValueHandling = NullValueHandling.Ignore)]
        public string DepartmentId { get; set; }
    }
}
