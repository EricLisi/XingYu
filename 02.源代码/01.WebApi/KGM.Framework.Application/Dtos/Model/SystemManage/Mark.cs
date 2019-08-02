using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 更改标识
    /// </summary>
    [DataContract]
    public class Mark
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        [DataMember]
       public bool EnabledMark { get; set; }




    }
}
