using KGM.Framework.Domain;
using KGM.Framework.RepositoryEF.Core;
using Microsoft.EntityFrameworkCore;

namespace KGM.Framework.RepositoryEF
{
    public class PositionFilesMapping : EntityMapping<PositionFilesEntity>
    {
          #region 接口实现
        public new static void MappingToTable(ModelBuilder modelBuilder, bool bAutoMapping = true)
        {
            //映射log表
            var printlog = EntityMapping<PositionFilesEntity>.MappingToTable(modelBuilder, bAutoMapping);
        }
        #endregion
    }
}
