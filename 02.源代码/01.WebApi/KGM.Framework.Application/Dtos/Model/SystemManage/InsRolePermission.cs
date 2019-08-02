using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 保存菜单权限
    /// </summary>
    [DataContract]
    public class InsRolePermission
    {
        /// <summary>
        /// 菜单集合
        /// </summary>
        [DataMember]
        public String strModuleId { get; set; }
        /// <summary>
        /// 按钮集合
        /// </summary>
        [DataMember]
        public  string strModuleButtonId { get; set; }

        /// <summary>
        /// 列集合
        /// </summary>
        [DataMember]
        public String strModuleColumnId { get; set; }
        ///// <summary>
        ///// 页面集合
        ///// </summary>
        //[DataMember]
        //public string strModuleFormId { get; set; }
    }
}
