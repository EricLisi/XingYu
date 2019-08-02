using KGM.Framework.Infrastructure;
using System.ComponentModel;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 用户机构配置 与数据库结构一致
    /// </summary> 
    [MappingTable(TableName = "Sys_User_Department")]
    public class UserDepartmentEntity : AggregateRoot
    {
        /// <summary>
        /// 用户Id
        /// </summary> 
        public string UserId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary> 
        public string DepartmentId { get; set; }

      

      
    }
}
