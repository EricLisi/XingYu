using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 角色Dto
    /// </summary>
    [DataContract]
    public class RoleModel
    {
        /// <summary>
        ///角色信息
        /// </summary>
        [DataMember]
        public RoleGetDto Role { get; set; }

        ///// <summary>
        ///// 权限集合
        ///// </summary>
        //[DataMember]
        //public List<RoleDeployGetDto>  RDList { get; set; }





    }
}
