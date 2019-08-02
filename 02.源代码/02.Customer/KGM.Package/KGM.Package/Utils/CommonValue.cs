using KGM.Package.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KGM.Package.Utils
{
    public class CommonValue
    {

        /// <summary>
        /// api的路径
        /// </summary>
        public static Uri WebAPIUri;

        /// <summary>
        /// 登录界面
        /// </summary>
        public static FrmLogin login = null;

        /// <summary>
        /// 主界面
        /// </summary>
        public static FrmMain main = null;

        /// <summary>
        /// token
        /// </summary>
        public static string token = null;

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public static UserModel userInfo;


        public static string LeftPrinter;

        public static string RightPrinter; 

        public static string GROUPID = "";
        public static string qty = "";
    }
}
