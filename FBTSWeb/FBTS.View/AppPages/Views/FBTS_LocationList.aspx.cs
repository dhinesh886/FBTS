using FBTS.Business.Manager;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View.AppPages.Views
{
    public partial class FBTS_LocationList : System.Web.UI.Page
    {
        private readonly GenericManager _genericClass = new GenericManager();

        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Stage"] != null)
                TransactionStageControlId.ActiveStage = Request.QueryString["Stage"].ToString();
            if (IsPostBack) return;
            BindReport("FBTS.Reports.Rdls.LocationDetails.rdlc", "LocationList");
        }

        public string LOCATION = "W";

        private KeyValuePairItems GetConfigurations()
        {
            var configurations = new KeyValuePairItems
            {
               new KeyValuePairItem("LOCATION", LOCATION),    
            };
            return configurations;
        }

        private void BindReport(string reportUrl, string datasetName)
        {
            var configurations = GetConfigurations();

            configurations.Insert(0, new KeyValuePairItem(Constants.RptParaUserId,
                    UserContext.UserProfile.UCode.ToString()));
            configurations.Insert(1, new KeyValuePairItem(Constants.RptParaRptId,
                    QueryStringManagement.GetValue(Constants.ReportCodeQsKey)));

            configurations.Insert(2, new KeyValuePairItem(Constants.RptParaRptId,
                    QueryStringManagement.GetValue(Constants.ReportCodeQsKey)));

            var objContainer = _genericClass.GetReportData(configurations, UserContext.DataBaseInfo);


            LocationViewer.ProcessingMode = ProcessingMode.Local;
            LocalReport rep = LocationViewer.LocalReport;

            ReportDataSource dsMaintenanceDS = new ReportDataSource();
            dsMaintenanceDS.Name = datasetName;// Data Source Name
            dsMaintenanceDS.Value = objContainer.Result;// Data Values
            // rep.DataSources.Clear();


            Assembly assembly = Assembly.LoadFrom(Server.MapPath(@"~\bin\FBTS.Reports.dll"));
            Stream stream = assembly.GetManifestResourceStream(reportUrl);
            LocationViewer.LocalReport.LoadReportDefinition(stream);


            //ReportViewer1.LocalReport.ReportPath = Server.MapPath(reportUrl);//Report Path

            LocationViewer.LocalReport.Refresh();
            rep.DataSources.Clear();
            rep.DataSources.Add(dsMaintenanceDS);
            //RDLC report DataSource(Tablix Datasource name)
            //ReportParameter p1 = new ReportParameter("rptParam1", "Customer Vendor Master");//Passing the Parameters
            //ReportParameter p2 = new ReportParameter("rptParam2", " ");//Passing the Parameters
            //ReportParameter tax = new ReportParameter("taxParam", tax_detail);//Passing the Parameters
            //LocationViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2, tax });
            rep.Refresh();
        }

    }
}