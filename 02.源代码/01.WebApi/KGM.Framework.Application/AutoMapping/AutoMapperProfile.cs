using AutoMapper;
using KGM.Framework.Application.Dtos;
using KGM.Framework.Domain;

namespace KGM.Framework.Application.AutoMapping
{
    /// <summary>
    /// AutoMapper注册类
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AutoMapperProfile()
        {
            #region  任务注入
            CreateMap<TaskEntity, TaskSingleDto>();
            CreateMap<TaskSingleDto, TaskEntity>();
            #endregion
            #region  用户注入
            CreateMap<UserEntity, UserSingleDto>();
            CreateMap<UserSingleDto, UserEntity>();
            #endregion
            #region 用户档案注入
            CreateMap<UserProfilesEntity, UserProfilesSingleDto>();
            CreateMap<UserProfilesSingleDto, UserProfilesEntity>();
            #endregion
            #region 包装仓库注入
            CreateMap<PackageInventoryEntity, PackageInventorySingleDto>();
            CreateMap<PackageInventorySingleDto, PackageInventoryEntity>();
            #endregion
            #region log注入
            CreateMap<PrintLogEntity, PrintLogSingleDto>();
            CreateMap<PrintLogSingleDto, PrintLogEntity>();
            #endregion
            #region 工位档案服务注入
            CreateMap<PositionFilesEntity, PositionFilesSingleDto>();
            CreateMap<PositionFilesSingleDto, PositionFilesEntity>();
            #endregion
        }
    }
}
