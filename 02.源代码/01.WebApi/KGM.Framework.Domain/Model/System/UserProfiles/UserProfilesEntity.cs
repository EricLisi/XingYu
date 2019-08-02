using KGM.Framework.Domain.Core;
using KGM.Framework.Infrastructure;
using System;
namespace KGM.Framework.Domain
{
    /// <summary>
    /// 用户档案 与数据库结构一致
    /// </summary> 
    [MappingTable(TableName = "MST_UserProfiles")]
    public class UserProfilesEntity : AggregateRoot
    {
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool? IsAdmin { get; set; }
        /// <summary>
        /// 加密秘钥
        /// </summary>
        public string Secretkey { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary> 
        public string RealName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary> 
        public string NickName { get; set; }
        /// <summary>
        /// 账户
        /// </summary> 
        public string Account { get; set; }
        /// <summary>
        /// 工号
        /// </summary> 
        public string EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string cWhName { get; set; }
        /// <summary>
        ///工位
        /// </summary>
        public string WorkStation { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string Profession { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public string Storage { get; set; }
        /// <summary>
        /// 冻结状态
        /// </summary>
        public string Status { get; set; }


    }
}
