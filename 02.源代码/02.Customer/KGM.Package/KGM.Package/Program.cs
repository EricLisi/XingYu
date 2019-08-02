using KGM.Package.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KGM.Package
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Stimulsoft.Base.StiLicense.Key = ConstValue.S_KEY;
            CommonValue.LeftPrinter = AppUtil.GetConfigValue("lprinter");
            CommonValue.RightPrinter = AppUtil.GetConfigValue("rprinter");
           
            CommonValue.login = new FrmLogin();
         
            Application.Run(CommonValue.login);
        }
    }
}
