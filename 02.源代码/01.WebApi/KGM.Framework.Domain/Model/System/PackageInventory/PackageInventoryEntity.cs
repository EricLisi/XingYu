using KGM.Framework.Domain.Core;
using KGM.Framework.Infrastructure;
using System.Collections.Generic;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 包装仓库类 与数据库结构一致
    /// </summary> 
    [MappingTable(TableName = "MST_PackageInventory")]
    public class PackageInventoryEntity : AggregateRoot
    {
        /// <summary>
        /// 规格
        /// </summary>
        public string cInvStd { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string ClassIfyName { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string cInvCode { get; set; }
        /// <summary>
        /// 定容
        /// </summary> 
        public int? ConstantVolume { get; set; }
        /// <summary>
        ///  物料编码
        /// </summary> 
        public string cInvName { get; set; }
        /// <summary>
        /// 图号
        /// </summary> 
        public string EnCode { get; set; }
        /// <summary>
        /// 入库名称
        /// </summary> 
        public string PutcWhName { get; set; }
        /// <summary>
        /// 出库名称
        /// </summary> 
        public string OutcWhName { get; set; }
        /// <summary>
        /// 排序
        /// </summary> 
        public string Desc { get; set; }
        /// <summary>
        /// 装箱数
        /// </summary> 
        public int? Packing { get; set; }
        /// <summary>
        /// 调出仓库
        /// </summary> 
        public string OutStorage { get; set; }
        /// <summary>
        /// 调入仓库
        /// </summary> 
        public string PutStorage { get; set; }
        /// <summary>
        /// 冻结状态
        /// </summary>
        public string FreezeStatus { get; set; }

        /// <summary>
        /// 自定义项1
        /// </summary> 
        public string Define1 { get; set; }
        /// <summary>
        /// 自定义项2
        /// </summary>
        public string Define2 { get; set; }
        /// <summary>
        /// 自定义项2
        /// </summary>
        public string Define3 { get; set; }
        /// <summary>
        /// 自定义项2
        /// </summary>
        public string Define4 { get; set; }
        /// <summary>
        /// 自定义项2
        /// </summary>
        public string Define5 { get; set; }
        /// <summary>
        /// 自定义项2
        /// </summary>
        public string Define6 { get; set; }
        /// <summary>
        /// 自定义项2
        /// </summary>
        public string Define7 { get; set; }
        /// <summary>
        /// 自定义项2
        /// </summary>
        public string Define8 { get; set; }
        /// <summary>
        /// 自定义项2
        /// </summary>
        public string Define9 { get; set; }
        /// <summary>
        /// 自定义项2
        /// </summary>
        public string Define10 { get; set; }
    }
}
