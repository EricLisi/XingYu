using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 获取所有公司和部门
    /// </summary>
    [DataContract]
    public class GetRolePermission
    {
        /// <summary>
        /// 菜单集合
        /// </summary>
        [DataMember]
        public List<string> modules { get; set; }
        /// <summary>
        /// 按钮集合
        /// </summary>
        [DataMember]
        public List<string> buttons { get; set; }
        /// <summary>
        /// 列集合
        /// </summary>
        [DataMember]
        public List<string> columns { get; set; }
        ///// <summary>
        ///// 页面集合
        ///// </summary>
        //[DataMember]
        //public List<string> forms { get; set; }

    }
}
