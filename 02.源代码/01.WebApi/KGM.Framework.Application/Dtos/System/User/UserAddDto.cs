using System;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 用户Dto
    /// </summary>
    [DataContract]
    public class UserAddDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserAddDto()
        {

            this.Id = System.Guid.NewGuid().ToString();
            this.SortCode = 0;
            this.CreatorTime = DateTime.Now;
            this.EnabledMark = true;
            this.DeleteMark = false;

        }


        /// <summary>
        /// Id
        /// </summary>
        //[DataMember]
        public string Id { get; set; }

        /// <summary>
        /// 工号
        /// </summary> 
        [DataMember]
        public string EnCode { get; set; }
     
        ///<summary>
        /// 角色Id
        /// </summary> 
        [DataMember]
        public string RoleId { get; set; }



        ///<summary>
        /// 电话
        /// </summary> 
        [DataMember]
        public string Mobile { get; set; }


        /// <summary>
        /// 密码
        /// </summary> 
        [DataMember]
        public string PassWord { get; set; }

        /// <summary>
        /// 账户
        /// </summary> 
        [DataMember]
        public string Account { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary> 
        [DataMember]
        public string RealName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary> 
        [DataMember]
        public string NickName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary> 
        [DataMember]
        public string HeadIcon { get; set; }

        /// <summary>
        /// 公司ID
        /// </summary>
        [DataMember]
        public string CompanyId { get; set; }

        ///<summary>
        /// 角色性别
        /// </summary> 
        [DataMember]
        public string Gender { get; set; }

        ///<summary>
        /// 生日
        /// </summary> 
        [DataMember]
        public DateTime? Birthday { get; set; }

        ///<summary>
        /// 邮箱
        /// </summary> 
        [DataMember]
        public string Email { get; set; }

        ///<summary>
        /// 电话
        /// </summary> 
        [DataMember]
        public string PhoneTel{ get; set; }


        ///<summary>
        /// QQ
        /// </summary> 
        [DataMember]
        public string OICQ { get; set; }


        ///<summary>
        /// 微信
        /// </summary> 
        [DataMember]
        public string WeChat { get; set; }

        ///// <summary>
        ///// 公司编号
        ///// </summary>
        //[DataMember]
        //public string CompanyCode { get; set; }

        ///// <summary>
        ///// 公司名称
        ///// </summary>
        //[DataMember]
        //public string CompanyName { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        [DataMember]
        public string DepartmentId { get; set; }

        ///// <summary>
        ///// 部门编号
        ///// </summary>
        //[DataMember]
        //public string DepartmentCode { get; set; }

        ///// <summary>
        ///// 部门名称
        ///// </summary>
        //[DataMember]
        //public string DepartmentName { get; set; }

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
        [DataMember]
        public virtual bool? EnabledMark { get; set; }

        /// <summary>
        /// 描述
        /// </summary> 
        [DataMember]
        public virtual string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
       //[DataMember]
        public virtual DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        //[DataMember]
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
