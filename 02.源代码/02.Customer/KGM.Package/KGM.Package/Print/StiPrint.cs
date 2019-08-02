using System;
using System.Collections.Generic;
using System.Text;
using Stimulsoft.Report;
using System.Data;
using KGM.Package.Utils;

namespace KGM.Package.Print
{
    public partial class StiPrint
    {
        private StiReport report;

        public StiPrint(string path)
        {
            report = new StiReport(); 
            report.Load(path);
            report.Compile();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="dataName">数据源名</param>
        /// <param name="dt">数据源</param>
        /// <param name="Qty">数量</param>
        public void Print(string dataName, DataTable dt, short Qty, string printer)
        {
            System.Drawing.Printing.PrinterSettings ps = new System.Drawing.Printing.PrinterSettings();
            ps.Copies = Qty;
            ps.PrinterName = printer;
            report.RegData(dataName, dt);
            report.Render(false);
            report.Print(false, ps);
        }

        /// <summary>
        /// 设计
        /// </summary>
        public void Desgin()
        {
            report.Design(true);
        }
    }
}
