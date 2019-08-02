using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KGM.Framework.Application.Dtos.U8;
using KGM.Framework.Domain;

namespace KGM.Framework.Application.Services
{
    /// <summary>
    /// 用户服务实现
    /// </summary>
    public class U8Service : IU8Service
    {
        #region 私有成员
        private readonly IU8Repository _repository;
        private readonly IMapper _mapper;
        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public U8Service(IU8Repository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        #endregion
        #region 接口实现
        /// <summary>
        /// 获取U8仓库
        /// </summary>
        /// <returns></returns>
        public async Task<List<U8Warehouse>> GetAllWarehouse(string Code)
        {
            return await Task.Run(() =>
            {
                var list = _repository.QueryWarehouse(Code);
                return _mapper.Map<List<U8Warehouse>>(list);
            });
        }
        /// <summary>
        /// 获取U8存货
        /// </summary>
        /// <returns></returns>
        public async Task<List<Inventorys>> GetInventorys()
        {
            return await Task.Run(() =>
            {
                var list = _repository.QueryInventorys();
                return _mapper.Map<List<Inventorys>>(list);
            });
        }
        /// <summary>
        /// 根据编码获取U8存货
        /// </summary>
        /// <param name="InvCode"></param>
        /// <param name="cWhCode"></param>
        /// <returns></returns>
        public async Task<List<Inventorys>> QueryInventorysByCode(string InvCode, string cWhCode)
        {
            return await Task.Run(() =>
            {
                var list = _repository.QueryInventorysByCode(InvCode, cWhCode);
                return _mapper.Map<List<Inventorys>>(list);
            });
        }
        /// <summary>
        /// 根据仓库获取U8存货
        /// </summary>
        /// <param name="WhCode"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public async Task<List<Inventorys>> QueryInventorysByWhCode(string WhCode, string position)
        {
            return await Task.Run(() =>
            {
                var list = _repository.QueryInventorysByWhCode(WhCode, position);
                return _mapper.Map<List<Inventorys>>(list);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WhCode"></param>
        /// <param name="cInvCode"></param>
        /// <returns></returns>
        public async Task<List<Inventorys>> QueryUndoList(string WhCode, string cInvCode)
        {
            return await Task.Run(() =>
            {
                var list = _repository.GetUndoList(WhCode, cInvCode);
                return _mapper.Map<List<Inventorys>>(list);
            });
        }
        #endregion
    }
}
