using System;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 权限表Dto
    /// </summary>
    [DataContract]
    public class CompetenceGetDto
    {
        /// <summary>
        /// 初始化本身
        /// </summary>
        public CompetenceGetDto()
        {
            this.Id = System.Guid.NewGuid().ToString();
            this.Layers = 0;
            this.IsMenu = false;
            this.IsExpand = false;
            this.IsPublic = false;
            this.AllowEdit = false;
            this.AllowDelete = false;
            this.SortCode = 0;
            this.DeleteMark = false;
            this.EnabledMark = false;
        }
        #region Property Members 

        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        [DataMember]
        public  string ParentId { get; set; }

        /// <summary>
        /// 层次
        /// </summary>
        [DataMember]
        public  int? Layers { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [DataMember]
        public  string EnCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public  string FullName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [DataMember]
        public  string Icon { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        [DataMember]
        public  string UrlAddress { get; set; }

        /// <summary>
        /// 目标
        /// </summary>
        [DataMember]
        public  string Target { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        [DataMember]
        public bool? IsMenu { get; set; }

        /// <summary>
        /// 展开
        /// </summary>
        [DataMember]
        public bool? IsExpand { get; set; }

        /// <summary>
        /// 公共
        /// </summary>
        [DataMember]
        public bool? IsPublic { get; set; }

        /// <summary>
        /// 允许编辑
        /// </summary>
        [DataMember]
        public bool? AllowEdit { get; set; }

        /// <summary>
        /// 允许删除
        /// </summary>
        [DataMember]
        public bool? AllowDelete { get; set; }
        /// <summary>
        /// 是否App
        /// </summary>
        [DataMember]
        public bool? IsApp { get; set; }
        #endregion

        /// <summary>
        /// 描述
        /// </summary> 
        [DataMember]
        public  string Description { get; set; }

        /// <summary>
        /// 排序码
        /// </summary> 
        [DataMember]
        public int? SortCode { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary> 
        [DataMember]
        public  bool? DeleteMark { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary> 
        [DataMember]
        public bool? EnabledMark { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public  DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        [DataMember]
        public  string CreatorUserId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DataMember]
        public  DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>
        [DataMember]
        public  string LastModifyUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [DataMember]
        public  DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 删除用户
        /// </summary> 
        [DataMember]
        public  string DeleteUserId { get; set; }

    }
}
