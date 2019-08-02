namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 登录对象
    /// </summary>
    public class UserGetAuthToken
    {
        /// <summary>
        /// 用户账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
    }
}
