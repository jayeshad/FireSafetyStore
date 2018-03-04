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
using CrystalDecisions.Shared;

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
                ReportDocument stockReportDoc = new ReportDocument();
                stockReportDoc.Load(Server.MapPath("~/Reports/StockReport.rpt"));
                var stockReport = GetStockInfo("ALL");
                stockReportDoc.Database.Tables[0].SetDataSource(stockReport);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream fs = stockReportDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                fs.Seek(0, SeekOrigin.Begin);
                return File(fs, "application/pdf");
            }

            if(type == "invoice")
            {
                ReportDocument reportDoc = new ReportDocument();
                reportDoc.Load(Server.MapPath("~/Reports/InvoiceReport.rpt"));                
                var param1 = new List<SqlParameter>();
                param1.Add(new SqlParameter("@OrderId", new Guid(id)));
                var header = adoHelper.ExecuteDataTable("InvoiceReport", param1);
                reportDoc.Database.Tables[0].SetDataSource(header);

                //reportDoc.ParameterFields.Clear();
                var param2 = new List<SqlParameter>();
                param2.Add(new SqlParameter("@OrderId", new Guid(id)));
                var body = adoHelper.ExecuteDataTable("InvoiceReportBody", param2);
                reportDoc.Database.Tables[1].SetDataSource(body);

                

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

        private DataTable GetStockInfo(string type)
        {
            var result = new DataTable();
            if(type.Equals("ALL",StringComparison.InvariantCultureIgnoreCase))
            {
                var param1 = new List<SqlParameter>();
                param1.Add(new SqlParameter("@Mode", "ALL"));
                result = adoHelper.ExecuteDataTable("StockSummary", param1);
            }
            return result;
        }

        public ActionResult ReportHome()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
