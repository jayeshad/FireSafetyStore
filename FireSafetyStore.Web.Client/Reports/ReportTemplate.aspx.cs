﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FireSafetyStore.Web.Client.Reports
{
    public partial class ReportTemplate : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    String reportFolder = System.Configuration.ConfigurationManager.AppSettings["SSRSReportsFolder"].ToString();
                    
                    rvSiteMapping.Height = Unit.Pixel(Convert.ToInt32(Request["Height"]) - 58);
                    rvSiteMapping.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;

                    rvSiteMapping.ServerReport.ReportServerUrl = new Uri("SSRS URL"); // Add the Reporting Server URL 
                    rvSiteMapping.ServerReport.ReportPath = String.Format("/{0}/{1}", reportFolder, Request["ReportName"].ToString());

                    rvSiteMapping.ServerReport.Refresh();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}