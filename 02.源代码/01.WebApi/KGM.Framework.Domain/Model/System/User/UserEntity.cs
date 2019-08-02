using KGM.Framework.Domain.Core;
using KGM.Framework.Infrastructure;
using System;
using System.Collections.Generic;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 用户类 与数据库结构一致
    /// </summary> 
    [MappingTable(TableName = "Sys_User")]
    public class UserEntity : AggregateRoot
    {
        /// <summary>
        /// 工号
        /// </summary> 
        public string EnCode { get; set; }

        /// <summary>
        /// 账户
        /// </summary> 
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

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

    } 

}
