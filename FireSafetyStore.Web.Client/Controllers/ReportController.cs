using System;
using System.Web.Mvc;
using FireSafetyStore.Web.Client.Infrastructure.DbContext;
using FireSafetyStore.Web.Client.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Linq;

namespace FireSafetyStore.Web.Client.Controllers
{
    public class ReportController : Controller
    {
        private FiresafeDbContext db = new FiresafeDbContext();

        public ActionResult Index(string name)
        {
            ReportClass report = new ReportClass();
            if(name == "stock")
            {
                report.FileName = Server.MapPath("~/Reports/StockReport.rpt");
                report.Load();
                report.SetDataSource(db.StockReports.ToList());
            }

            if (name == "sales")
            {
                report.FileName = Server.MapPath("~/Reports/StockReport.rpt");
                report.Load();
                report.SetDataSource(db.StockReports.ToList());
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf");
    }

        public ActionResult ReportHome()
        {
            return View();
        }

        public ActionResult ReportTemplate(string ReportName, string ReportDescription, int Width, int Height)
        {
            var rptInfo = new ReportInfo
            {
                ReportName = ReportName,
                ReportDescription = ReportDescription,
                ReportURL = String.Format("../../Reports/ReportTemplate.aspx?ReportName={0}&Height={1}", ReportName, Height),
                Width = Width,
                Height = Height
            };

            return View(rptInfo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
