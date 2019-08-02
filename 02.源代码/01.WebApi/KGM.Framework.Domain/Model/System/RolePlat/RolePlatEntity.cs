using KGM.Framework.Infrastructure;
using System.ComponentModel;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 权限配置 与数据库结构一致
    /// </summary> 
    [MappingTable(TableName = "Sys_Role_Plat")]
    public class RolePlatEntity : AggregateRoot
    {
        /// <summary>
        /// 角色Id
        /// </summary> 
        public string RoleId { get; set; }

        /// <summary>
        /// 平台Id
        /// </summary> 
        public string PlatId { get; set; }

      

      
    }
}
