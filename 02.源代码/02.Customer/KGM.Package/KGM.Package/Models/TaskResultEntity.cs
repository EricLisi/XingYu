using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace KGM.Package.Models
{
    public class TaskResultEntity
    {
        /// <summary>
        /// 返回结果 true/false
        /// </summary> 
        public bool state { get; set; }

        /// <summary>
        /// 返回信息
        /// 当result为false是,返回错误信息
        /// 当result为true时,返回空或正确的信息
        /// </summary> 
        public string message { get; set; }
        
        /// <summary>
        /// 返回数据
        /// </summary>
        public List<PackageInventoryEntity> data { get; set; }

        /// <summary>
        /// TaskId
        /// </summary>
        public string taskid { get; set; }


        /// <summary>
        /// GroupId
        /// </summary>
        public string groupid { get; set; }

        public int Compel { get; set; }
    }
}
