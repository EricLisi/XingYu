using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 用户Id
    /// </summary>
    [DataContract]
    public class UpPassWord
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [DataMember]
       public string Id { get; set; }

        /// <summary>
        /// 用户新密码
        /// </summary>
        [DataMember]
        public string NewPassWord { get; set; }


    }
}
