using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 获取所有公司和部门
    /// </summary>
    [DataContract]
    public class InsUpUserPermission
    {
        /// <summary>
        /// 公司集合
        /// </summary>
        [DataMember]
        public String strModuleId { get; set; }
        /// <summary>
        /// 部门集合
        /// </summary>
        [DataMember]
        public  string strModuleButtonId { get; set; }


    }
}
