using FBTS.Business.Manager;
using FBTS.Library.Common;
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
    public partial class FBTS_CompleteBillTracking : System.Web.UI.Page
    {

        private readonly GenericManager _genericClass = new GenericManager();

        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString["Stage"] != null)
                TransactionStageControlId.ActiveStage = Request.QueryString["Stage"].ToString();

            RenderingExtension extension = BillTrackingViewer.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals("PDF", StringComparison.CurrentCultureIgnoreCase));
            System.Reflection.FieldInfo info = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            info.SetValue(extension, false);

            FROMDATE = DateTime.Now.AddDays(-1);
            TODATE = DateTime.Now;
        }

        public string SR { get { return txtSR.Text; } set { txtSR.Text = value.Trim(); } }
        public DateTime FROMDATE
        {
            get
            {
                if (txtFromDate.Text == string.Empty)
                    return Convert.ToDateTime(Constants.DefaultDate);
                else
                    return Dates.ToDateTime(txtFromDate.Text.Trim(), DateFormat.Format_01);
            }
            set
            {
                if (value == Dates.ToDateTime(Constants.DefaultDate, DateFormat.Format_02))
                    txtFromDate.Text = string.Empty;
                else txtFromDate.Text = Dates.FormatDate(value, Constants.Format02);
            }
        }

        public DateTime TODATE
        {
            get
            {
                if (txtFromDate.Text == string.Empty)
                    return Convert.ToDateTime(Constants.DefaultDate);
                else
                    return Dates.ToDateTime(txtToDate.Text.Trim(), DateFormat.Format_01);
            }
            set
            {
                if (value == Dates.ToDateTime(Constants.DefaultDate, DateFormat.Format_02))
                    txtToDate.Text = string.Empty;
                else txtToDate.Text = Dates.FormatDate(value, Constants.Format02);
            }
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            BindReport("FBTS.Reports.Rdls.BillTrackingComplete.rdlc", "BillTracking");
        }

        private KeyValuePairItems GetConfigurations()
        {        
            var configurations = new KeyValuePairItems
            {
                new KeyValuePairItem("SR", SR),               
                new KeyValuePairItem("FROMDATE",FROMDATE),
                new KeyValuePairItem("TODATE", TODATE),
            };

            if (txtFromDate.Text.Trim() == string.Empty || txtToDate.Text.Trim() == string.Empty)
            {
                var configuration = configurations.Where(x => x.Key == "FROMDATE");
                if (configuration.Any())
                {
                    configuration.FirstOrDefault().Value = string.Empty;
                }
                configuration = configurations.Where(x => x.Key == "TODATE");
                if (configuration.Any())
                {
                    configuration.FirstOrDefault().Value = string.Empty;
                }
            }
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

            BillTrackingViewer.ProcessingMode = ProcessingMode.Local;
            LocalReport rep = BillTrackingViewer.LocalReport;

            ReportDataSource dsMaintenanceDS = new ReportDataSource();
            dsMaintenanceDS.Name = datasetName;// Data Source Name
            dsMaintenanceDS.Value = objContainer.Result;// Data Values            

            Assembly assembly = Assembly.LoadFrom(Server.MapPath(@"~\bin\FBTS.Reports.dll"));
            Stream stream = assembly.GetManifestResourceStream(reportUrl);
            BillTrackingViewer.LocalReport.LoadReportDefinition(stream);

            BillTrackingViewer.LocalReport.Refresh();
            rep.DataSources.Clear();
            rep.DataSources.Add(dsMaintenanceDS);
            //RDLC report DataSource(Tablix Datasource name)
            ReportParameter p1 = new ReportParameter("logged", UserContext.UserProfile.Name);
            ReportParameter p2 = new ReportParameter("fromDate", FROMDATE == Convert.ToDateTime(Constants.DefaultDate) ? string.Empty : Dates.FormatDate(FROMDATE, Constants.Format02));
            ReportParameter p3 = new ReportParameter("toDate", TODATE == Convert.ToDateTime(Constants.DefaultDate) ? string.Empty : Dates.FormatDate(TODATE, Constants.Format02));
            ReportParameter p4 = new ReportParameter("sr", SR == string.Empty ? "All SRs" : SR);
            BillTrackingViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4 });
            rep.Refresh();
        }

    }
}