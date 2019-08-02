using Newtonsoft.Json;
using System;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 用户档案Dto
    /// </summary>
   public class UserProfilesSingleDto
    {
        /// <summary>
        /// 加密秘钥
        /// </summary>
        [JsonProperty(PropertyName = "isadmin", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsAdmin { get; set; }
        /// <summary>
        /// 加密秘钥
        /// </summary>
        [JsonProperty(PropertyName = "secretkey", NullValueHandling = NullValueHandling.Ignore)]
        public string Secretkey { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary> 
        [JsonProperty(PropertyName = "realname", NullValueHandling = NullValueHandling.Ignore)]
        public string RealName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary> 
        [JsonProperty(PropertyName = "nickname", NullValueHandling = NullValueHandling.Ignore)]
        public string NickName { get; set; }
        /// <summary>
        /// 账户
        /// </summary> 
        [JsonProperty(PropertyName = "account", NullValueHandling = NullValueHandling.Ignore)]
        public string Account { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [JsonProperty(PropertyName = "encode", NullValueHandling = NullValueHandling.Ignore)]
        public string EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>

        public string cWhName { get; set; }
        /// <summary>
        ///工位
        /// </summary>
        [JsonProperty(PropertyName = "workstation", NullValueHandling = NullValueHandling.Ignore)]
        public string WorkStation { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        [JsonProperty(PropertyName = "profession", NullValueHandling = NullValueHandling.Ignore)]
        public string Profession { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        [JsonProperty(PropertyName = "storage", NullValueHandling = NullValueHandling.Ignore)]
        public string Storage { get; set; }
        /// <summary>
        /// 冻结状态
        /// </summary>
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }
}
