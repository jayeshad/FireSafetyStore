using System;
using System.Web.Mvc;
using FireSafetyStore.Web.Client.Infrastructure.DbContext;
using FireSafetyStore.Web.Client.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Linq;
using FireSafetyStore.Web.Client.Infrastructure.Common;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using CrystalDecisions.Web;

namespace FireSafetyStore.Web.Client.Controllers
{
    public class ReportController : Controller
    {
        private readonly ADOHelper adoHelper;
        private FiresafeDbContext db = new FiresafeDbContext();
        public ReportController()
        {
            adoHelper = new ADOHelper(ConfigurationManager.ConnectionStrings["FiresafeDbContext"].ConnectionString);
        }
        public ActionResult Index(string type, string id)
        {
            ReportClass report = new ReportClass();
            if(type == "stock")
            {
                report.FileName = Server.MapPath("~/Reports/StockReport.rpt");
                report.Load();
                report.SetDataSource(db.StockReports.ToList());
            }

            if (type == "sales")
            {
                report.FileName = Server.MapPath("~/Reports/StockReport.rpt");
                report.Load();
                report.SetDataSource(db.StockReports.ToList());
            }

            if(type == "invoice")
            {
                ReportDocument reportDoc = new ReportDocument();
                reportDoc.Load(Server.MapPath("~/Reports/Invoice.rpt"));
                var param1 = new List<SqlParameter>();
                param1.Add(new SqlParameter("@OrderId", new Guid(id)));
                var header = adoHelper.ExecuteDataTable("InvoiceReportHeader", param1);
                reportDoc.Database.Tables["InvoiceReportHeader"].SetDataSource(header);

                var param2 = new List<SqlParameter>();
                param2.Add(new SqlParameter("@OrderId", new Guid(id)));
                var body = adoHelper.ExecuteDataTable("InvoiceReportBody", param2);
                reportDoc.Database.Tables["InvoiceReportBody"].SetDataSource(body);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream fs = reportDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                fs.Seek(0, SeekOrigin.Begin);
                return File(fs, "application/pdf");
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
