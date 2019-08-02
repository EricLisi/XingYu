using System;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 平台
    /// </summary>
    [DataContract]
    public class PlatModel
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string F_Id { get; set; }//ID
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string F_EnCode { get; set; }//编码

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string F_FullName { get; set; }//名称

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string F_HomePageUrl { get; set; }//网址


    }
}
