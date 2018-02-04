using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Telerik.Reporting.Services;

namespace FireSafetyStore.Web.Client.Infrastructure.Common
{
    public abstract class ReportsControllerBase : ApiController
    {
        public IReportServiceConfiguration ReportServiceConfiguration { get; set; }
    }
}