using KGM.Framework.Domain.Core;
using KGM.Framework.Infrastructure;
using System.Collections.Generic;
namespace KGM.Framework.Domain
{
    /// <summary>
    /// log类，与实体数据库一致
    /// </summary>
    [MappingTable(TableName = "MST_PositionFiles")]
    public class PositionFilesEntity : AggregateRoot
    {
        /// <summary>
        /// 仓库
        /// </summary>
        public string cWhCode { get; set; }
        /// <summary>
        /// 工位
        /// </summary>
        public string PositionCode { get; set; }
    }
}
