using KGM.Framework.Domain.Core;
using KGM.Framework.Infrastructure;

namespace KGM.Framework.Domain.Model.U8
{
    public class U8Entity : AggregateRoot
    {
        /// <summary>
        /// 仓库
        /// </summary>
        [MappingTable(TableName = "Warehouse")]
        public class WarehouseEntity
        {
            /// <summary>
            /// 仓库编码
            /// </summary>
            public string CWhCode { get; set; }

            /// <summary>
            /// 仓库名称
            /// </summary>
            public string CWhName { get; set; }
        }
        /// <summary>
        /// U8存货数据
        /// </summary>
        [MappingTable(TableName = "Inventorys")]
        public class InventorysEntity
        {
            /// <summary>
            /// 检验状态码
            /// </summary>
            public int status { get; set; }
            /// <summary>
            /// 已装箱
            /// </summary>
            public int boxed { get; set; }
            /// <summary>
            /// 定容
            /// </summary>
            public int? ConstantVolume { get; set; }
            /// <summary>
            /// 余量
            /// </summary>        
            public int? Margin { get; set; }
            /// <summary>
            /// 箱数
            /// </summary>        
            public int? BoxCount { get; set; }
            /// <summary>
            /// 存货编码
            /// </summary>        
            public string cInvCode { get; set; }
            /// <summary>
            /// 仓库编码
            /// </summary>
            public string cWhCode { get; set; }
            /// <summary>
            /// 仓库名称
            /// </summary>
            public string cWhname { get; set; }
            /// <summary>
            /// 批号
            /// </summary>
            public string cbatch { get; set; }
            /// <summary>
            /// 存货名称
            /// </summary>
            public string cInvName { get; set; }
            /// <summary>
            /// 规格型号 
            /// </summary>
            public string cInvStd { get; set; }
            /// <summary>
            /// 数量
            /// </summary>
            public int iquantity { get; set; }
            /// <summary>
            /// 结存辅计量数量 
            /// </summary>
            public double iNum { get; set; }
            /// <summary>
            /// 检验状态
            /// </summary>
            public string cCheckState { get; set; }

            /// <summary>
            /// 检验状态
            /// </summary>
            public string th { get; set; }

            /// <summary>
            /// 检验状态
            /// </summary>
            public string lr { get; set; }

            /// <summary>
            /// 检验状态
            /// </summary>
            public string define3 { get; set; }

            public string desct { get; set; }
        }
    }
}
