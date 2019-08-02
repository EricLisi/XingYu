using KGM.Framework.Domain.Model.U8;
using System.Collections.Generic;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 用户档案的仓储接口
    /// </summary>
    public interface IU8Repository
    {
        /// <summary>
        /// 查寻U8仓库
        /// </summary>
        /// <returns></returns>
        List<U8Entity.WarehouseEntity> QueryWarehouse(string Code);
        /// <summary>
        /// 查询所有U8存货
        /// </summary>
        /// <returns></returns>
        List<U8Entity.InventorysEntity> QueryInventorys();
        /// <summary>
        /// 根据编码查U8存货
        /// </summary>
        /// <param name="InvCode"></param>
        /// <param name="cWhCode"></param>
        /// <returns></returns>
        List<U8Entity.InventorysEntity> QueryInventorysByCode(string InvCode, string cWhCode);
        /// <summary>
        /// 根据仓库查U8存货
        /// </summary>
        /// <param name="WhCode"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        List<U8Entity.InventorysEntity> QueryInventorysByWhCode(string WhCode, string position);
        /// <summary>
        /// 未分配的库存数据
        /// </summary>
        /// <param name="cWhCode"></param>
        /// <param name="cInvCode"></param>
        /// <returns></returns>
        List<U8Entity.InventorysEntity> GetUndoList(string cWhCode, string cInvCode);

    }
}
