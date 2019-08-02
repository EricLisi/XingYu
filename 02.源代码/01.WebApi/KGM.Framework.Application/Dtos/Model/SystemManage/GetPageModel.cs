using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 表头
    /// </summary>
    [DataContract]
    public class GetPageModel
    {
 
        /// <summary>
        /// 每页行数
        /// </summary>
        [DataMember]
        public int rows { get; set; }

        /// <summary>
        /// 第几页
        /// </summary>
        [DataMember]
        public int page { get; set; }
   
   

    }
}
