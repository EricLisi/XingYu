using System;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 平台Dto
    /// </summary>
    [DataContract]
    public class RolePlatGetDto
    {
        /// <summary>
        /// 初始化本身
        /// </summary>
        public RolePlatGetDto() {
            this.Id = System.Guid.NewGuid().ToString();
            this.CreatorTime = DateTime.Now;
            this.EnabledMark = false;
            this.DeleteMark = false;
            this.SortCode = 0;
        }
        /// <summary>
        /// 用户Id
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        ///角色Id
        /// </summary>
        [DataMember]
        public string RoleId { get; set; }

        /// <summary>
        ///平台Id
        /// </summary>
        [DataMember]
        public string PlatId { get; set; }

        /// <summary>
        /// 描述
        /// </summary> 
         [DataMember]
        public virtual string Description { get; set; }

        /// <summary>
        /// 排序码
        /// </summary> 
  
        public virtual int? SortCode { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary> 

        public virtual bool? DeleteMark { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary> 

        public virtual bool? EnabledMark { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>

        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>

        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>

        public virtual DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>

        public virtual string LastModifyUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 删除用户
        /// </summary> 
        public virtual string DeleteUserId { get; set; }

    

    }
}
