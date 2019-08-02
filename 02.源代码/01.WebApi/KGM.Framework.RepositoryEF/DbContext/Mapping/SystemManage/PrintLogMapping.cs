using KGM.Framework.Domain;
using KGM.Framework.RepositoryEF.Core;
using Microsoft.EntityFrameworkCore;

namespace KGM.Framework.RepositoryEF
{
    /// <summary>
    /// log映射
    /// </summary>
    public class PrintLogMapping : EntityMapping<PrintLogEntity>
    {
        #region 接口实现
        public new static void MappingToTable(ModelBuilder modelBuilder, bool bAutoMapping = true)
        {
            //映射log表
            var printlog = EntityMapping<PrintLogEntity>.MappingToTable(modelBuilder, bAutoMapping);
        }
        #endregion
    }
}
