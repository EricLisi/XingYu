using KGM.Framework.Infrastructure;
using Newtonsoft.Json;

namespace KGM.Framework.WebApi.Model.Condition
{
    /// <summary>
    /// 用户档案过滤条件
    /// </summary>
    public class UserProfilesIndexCondition: PagerCondition
    {
        /// <summary>
        /// 工号编码
        /// </summary>
        [JsonProperty(PropertyName = "encode", NullValueHandling = NullValueHandling.Ignore)]
        public string EnCode { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [JsonProperty(PropertyName = "account", NullValueHandling = NullValueHandling.Ignore)]
        public string Account { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [JsonProperty(PropertyName = "realname", NullValueHandling = NullValueHandling.Ignore)]
        public string RealName { get; set; }
    }
}
