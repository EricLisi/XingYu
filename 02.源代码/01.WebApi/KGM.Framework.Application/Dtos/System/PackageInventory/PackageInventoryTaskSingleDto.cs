using Newtonsoft.Json;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 任务/档案晒图
    /// </summary>
   public class PackageInventoryTaskSingleDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 任务Id
        /// </summary>
        public string TaskId { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string cInvCode { get; set; }
        /// <summary>
        /// 物料mc
        /// </summary>
        public string cInvName { get; set; }
        /// <summary>
        /// 任务数量
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string cBatch { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string WorkCode { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 分组Id
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// 强制打印
        /// </summary>
        public bool Compel { get; set; }
        /// <summary>
        /// 工位
        /// </summary>
        public string WorkStations { get; set; }

    }
}
