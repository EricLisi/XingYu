namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 登录对象
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// 用户账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
    }
}
