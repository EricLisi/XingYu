using System;
using System.Configuration;
using System.Data;
using System.Drawing.Printing;

namespace KGM.Package.Utils
{
    public static class AppUtil
    {
        #region 打印设置
        /// <summary>
        /// 绑定打印机
        /// </summary>
        public static void BindPrinter(ref System.Windows.Forms.ComboBox cmbPrinter, string dPrinter)
        {
            cmbPrinter.Items.Clear();
            //绑定本地打印机
            PrintDocument prtdoc = new PrintDocument();
            string strDefaultPrinter = dPrinter == string.Empty ? prtdoc.PrinterSettings.PrinterName : dPrinter;
            int index = 0;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                if (PrinterSettings.InstalledPrinters[i] == strDefaultPrinter)
                {
                    index = i;
                }
                cmbPrinter.Items.Add(PrinterSettings.InstalledPrinters[i]);
            }

            cmbPrinter.SelectedIndex = index;
        }
        #endregion

        #region 配置文件
        /// <summary>
        /// 修改AppSettings中配置
        /// </summary>
        /// <param name="key">key值</param>
        /// <param name="value">相应值</param>
        public static bool SetConfigValue(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[key] != null)
                    config.AppSettings.Settings[key].Value = value;
                else
                    config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取AppSettings中某一节点值
        /// </summary>
        /// <param name="key"></param>
        public static string GetConfigValue(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] != null)
                return config.AppSettings.Settings[key].Value;
            else

                return string.Empty;
        }
    
        #endregion
    }
}
