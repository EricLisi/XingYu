namespace KGM.Package.Models
{
    /// <summary>
    /// API返回对象
    /// </summary> 
    public class LoginApiResultEntity
    {
        /// <summary>
        /// 返回结果 true/false
        /// </summary> 
        public bool status { get; set; }

        /// <summary>
        /// 返回信息
        /// 当result为false是,返回错误信息
        /// 当result为true时,返回空或正确的信息
        /// </summary> 
        public string message { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary> 
        public UserModel user { get; set; }
    }
}
