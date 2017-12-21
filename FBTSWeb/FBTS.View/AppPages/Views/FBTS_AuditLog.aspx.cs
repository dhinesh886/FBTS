using FBTS.Business.Manager;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View.AppPages.Views
{
    public partial class FBTS_AuditLog : System.Web.UI.Page
    {
        private readonly GenericManager _genericClass = new GenericManager();
        public readonly ReportManager _reportManager = new ReportManager();

        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString["Stage"] != null)
                TransactionStageControlId.ActiveStage = Request.QueryString["Stage"].ToString();

           
        }

        private int _newPageIndex = -1;
        public string SR { get { return txtSR.Text; } set { txtSR.Text = value.Trim(); } }

        public void BindData(BindType bindType)
        {
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                filter1 = SR,
                FilterKey = Constants.AuditLog
            };
            var ordView = _reportManager.GetAuditLogData(queryArgument);
            GVListData.DataSource = ordView;
            if (_newPageIndex >= 0)
            {
                GVListData.PageIndex = _newPageIndex;
            }
            GVListData.DataSource = ordView;
            GVListData.DataBind();
            uplView.Update();
        }

        protected void GVListData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text;
            }
            var gv = sender as GridView;
            var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
            if (gv != null && lbl != null)
                lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);
        }

        protected void GVListData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            _newPageIndex = e.NewPageIndex;
            BindData(BindType.List);
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            BindData(BindType.List);
        }



        //private void BindReport(string reportUrl, string datasetName)
        //{
        //    var configurations = GetConfigurations();

        //    configurations.Insert(0, new KeyValuePairItem(Constants.RptParaUserId,
        //            UserContext.UserProfile.UCode.ToString()));
        //    configurations.Insert(1, new KeyValuePairItem(Constants.RptParaRptId,
        //            QueryStringManagement.GetValue(Constants.ReportCodeQsKey)));

        //    configurations.Insert(2, new KeyValuePairItem(Constants.RptParaRptId,
        //            QueryStringManagement.GetValue(Constants.ReportCodeQsKey)));

        //    var objContainer = _genericClass.GetReportData(configurations, UserContext.DataBaseInfo);

        //    AuditLogViewer.ProcessingMode = ProcessingMode.Local;
        //    LocalReport rep = AuditLogViewer.LocalReport;

        //    ReportDataSource dsMaintenanceDS = new ReportDataSource();
        //    dsMaintenanceDS.Name = datasetName;// Data Source Name
        //    dsMaintenanceDS.Value = objContainer.Result;// Data Values            

        //    Assembly assembly = Assembly.LoadFrom(Server.MapPath(@"~\bin\FBTS.Reports.dll"));
        //    Stream stream = assembly.GetManifestResourceStream(reportUrl);
        //    AuditLogViewer.LocalReport.LoadReportDefinition(stream);

        //    AuditLogViewer.LocalReport.Refresh();
        //    rep.DataSources.Clear();
        //    rep.DataSources.Add(dsMaintenanceDS);
        //    //RDLC report DataSource(Tablix Datasource name)
        //    ReportParameter p1 = new ReportParameter("logged", UserContext.UserProfile.Name);
        //    ReportParameter p2 = new ReportParameter("sr", SR == string.Empty ? "All SRs" : SR);
        //    AuditLogViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
        //    rep.Refresh();
        //}
    }
}