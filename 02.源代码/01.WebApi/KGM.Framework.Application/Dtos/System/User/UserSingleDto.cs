using Newtonsoft.Json;
using System;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 用户Dto
    /// </summary> 
    public class UserSingleDto
    {
        /// <summary>
        /// 工号
        /// </summary>
        [JsonProperty(PropertyName ="encode",NullValueHandling =NullValueHandling.Ignore)]
        public string EnCode { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
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

        /// <summary>
        /// 昵称
        /// </summary> 
        [JsonProperty(PropertyName = "nickname", NullValueHandling = NullValueHandling.Ignore)]
        public string NickName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary> 
        [JsonProperty(PropertyName = "headicon", NullValueHandling = NullValueHandling.Ignore)]
        public string HeadIcon { get; set; }
          
        /// <summary>
        /// 角色ID
        /// </summary> 
        [JsonProperty(PropertyName = "roleid", NullValueHandling = NullValueHandling.Ignore)]
        public string RoleId { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary> 
        [JsonProperty(PropertyName = "companyid", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyId { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary> 
        [JsonProperty(PropertyName = "companyname", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyFullName { get; set; }


        /// <summary>
        /// 部门ID
        /// </summary>  
        [JsonProperty(PropertyName = "departmentid", NullValueHandling = NullValueHandling.Ignore)]
        public string DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary> 
        [JsonProperty(PropertyName = "departmentname", NullValueHandling = NullValueHandling.Ignore)]
        public string DepartmentFullName { get; set; }
         
        /// <summary>
        /// 描述
        /// </summary> 
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        /// <summary>
        /// 手机
        /// </summary> 
        [JsonProperty(PropertyName = "mobile", NullValueHandling = NullValueHandling.Ignore)]
        public string Mobile { get; set; }

        /// <summary>
        /// 性别
        /// </summary> 
        [JsonProperty(PropertyName = "gender", NullValueHandling = NullValueHandling.Ignore)]
        public bool Gender { get; set; }

        /// <summary>
        /// 性别
        /// </summary> 
        [JsonProperty(PropertyName = "birthday", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 性别
        /// </summary> 
        [JsonProperty(PropertyName = "phonetel", NullValueHandling = NullValueHandling.Ignore)]
        public string Telephone { get; set; }

        /// <summary>
        /// QQ
        /// </summary> 
        [JsonProperty(PropertyName = "oicq", NullValueHandling = NullValueHandling.Ignore)]
        public string OICQ { get; set; }

        /// <summary>
        /// 微信
        /// </summary> 
        [JsonProperty(PropertyName = "wechat", NullValueHandling = NullValueHandling.Ignore)]
        public string WeChat { get; set; }

    }
}
