using KGM.Framework.Application.Core;
using KGM.Framework.Application.Dtos.U8;
using KGM.Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KGM.Framework.Application.Services
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IU8Service
    {
        /// <summary>
        /// 读取U8仓库
        /// </summary>
        /// <returns></returns>
        Task<List<U8Warehouse>> GetAllWarehouse(string Code);
        /// <summary>
        /// 读取U8存货
        /// </summary>
        /// <returns></returns>
        Task<List<Inventorys>> GetInventorys();
        /// <summary>
        /// 根据编码读取U8存货
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cWhCode"></param>
        /// <returns></returns>
        Task<List<Inventorys>> QueryInventorysByCode(string code, string cWhCode);
        /// <summary>
        /// 根据仓库读取U8存货
        /// </summary>
        /// <param name="WhCode"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        Task<List<Inventorys>> QueryInventorysByWhCode(string WhCode,string position);

        /// <summary>
        /// 获取未操作数据
        /// </summary>
        /// <param name="WhCode"></param>
        /// <param name="cInvCode"></param>
        /// <returns></returns>

        Task<List<Inventorys>> QueryUndoList(string WhCode, string cInvCode);
    }
}
