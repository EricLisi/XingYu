using System;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 下拉框
    /// </summary>
    [DataContract]
    public class UserModel
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public UserGetDto user { get; set; }//用户信息
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Competence { get; set; }//权限信息
      

    }
}
