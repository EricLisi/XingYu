using KGM.Framework.Domain;
using KGM.Framework.Domain.Model.U8;
using KGM.Framework.Infrastructure;
using KGM.Framework.RepositoryEF.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KGM.Framework.RepositoryEF.Repositories
{
    public class PrintLogRepository :BaseRepository<PrintLogEntity, PrintLogContext>, IPrintLogRepository
    {
        #region 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PrintLogRepository() : base()
        {

        }
        #endregion
    }
}
