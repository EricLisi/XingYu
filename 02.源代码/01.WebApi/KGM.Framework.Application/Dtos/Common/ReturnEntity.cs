using System;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 角色Dto
    /// </summary>
    [DataContract]
    public class ReturnEntity
    {
       
        /// <summary>
        /// 值
        /// </summary>
        [DataMember]
        public object  rows { get; set; }

        /// <summary>
        ///总行数
        /// </summary>
        [DataMember]
        public int records { get; set; }


        /// <summary>
        ///总页数
        /// </summary>
        [DataMember]
        public int total { get; set; }

        /// <summary>
        ///当前页
        /// </summary>
        [DataMember]
        public int page { get; set; }
 

    }
}
