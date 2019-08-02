using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 生成拣货单对象
    /// </summary>
    [DataContract]
 
    public class PackListModelDto
    {/// <summary>
     /// 结果
     /// </summary> 
        [DataMember] public int r { get; set; }


        /// <summary>
        /// 返回信息
        /// </summary> 
        [DataMember] public string msg { get; set; }


        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_KDAccout { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string ORDERNO { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string MAINID { get; set; }



        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_CustomerCode { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_CustomerName { get; set; }


        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_KDAccount { get; set; }


        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_KDPwd { get; set; }



        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string CustomerName { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_KDMonthAccout { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_ApiKey { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_KDNId { get; set; }



        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_WarehouseId { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_Contacts { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_TelePhone { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_MobilePhone { get; set; }



        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_EXPNO { get; set; }



        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string SENDERPHONE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string SENDERPROVINCE { get; set; }


        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string SENDERCITY { get; set; }


        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string CCUSADDRESS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string CustomerCode { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_sendPerson { get; set; }


        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_phone { get; set; }


        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string PROVINCE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string CITY { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string F_sendAddress { get; set; }


        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string NOTES { get; set; }



        /// <summary>
        /// 
        /// </summary> 
        [DataMember] public string LOGISTICCODE { get; set; }


    }
}
