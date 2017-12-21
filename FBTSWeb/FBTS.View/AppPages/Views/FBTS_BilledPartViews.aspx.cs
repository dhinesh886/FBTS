using FBTS.Business.Manager;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.IO;
using Microsoft.Reporting.WebForms;
using FBTS.Library.Common;



namespace FBTS.View.AppPages.Views
{
    public partial class FBTS_BilledPartViews : System.Web.UI.Page
    {
        private readonly GenericManager _genericClass = new GenericManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Stage"] != null)
                TransactionStageControlId.ActiveStage = Request.QueryString["Stage"].ToString();
            if (IsPostBack) return;

            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.WHCType),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.AllType),
                            new KeyValuePairItem(Constants.masterType,Constants.TableAccounts)
                        };
            _genericClass.LoadDropDown(ddlLocation, filter, null, UserContext.DataBaseInfo);
            LOCATION = UserContext.UserProfile.Branch;


            filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter4, Constants.DdlFilterGroups),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextMaterialType),
                            new KeyValuePairItem(Constants.masterType, Constants.DdlMATCategories)
                        };
            _genericClass.LoadDropDown(ddlPartType, filter, null, UserContext.DataBaseInfo);
        }

        #region Properties
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }

        public string LOCATION { get { return ddlLocation.SelectedValue.Trim(); } set { ddlLocation.SelectedValue = value.Trim(); } }
        public string PARTTYPE { get { return ddlPartType.SelectedValue.Trim(); } set { ddlPartType.SelectedValue = value.Trim(); } }

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

        #endregion

        protected void lnkView_Click(object sender, EventArgs e)
        {
            BindReport("FBTS.Reports.Rdls.PartDetails.rdlc", "PartDetails");
        }

        private KeyValuePairItems GetConfigurations()
        {
            var configurations = new KeyValuePairItems
            {
                new KeyValuePairItem("PARTTYPE", PARTTYPE),
                new KeyValuePairItem("FROMDATE", FROMDATE),
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


            ReportViewer.ProcessingMode = ProcessingMode.Local;
            LocalReport rep = ReportViewer.LocalReport;

            ReportDataSource dsMaintenanceDS = new ReportDataSource();
            dsMaintenanceDS.Name = datasetName;// Data Source Name
            dsMaintenanceDS.Value = objContainer.Result;// Data Values
            // rep.DataSources.Clear();


            Assembly assembly = Assembly.LoadFrom(Server.MapPath(@"~\bin\FBTS.Reports.dll"));
            Stream stream = assembly.GetManifestResourceStream(reportUrl);
            ReportViewer.LocalReport.LoadReportDefinition(stream);

            //ReportViewer1.LocalReport.ReportPath = Server.MapPath(reportUrl);//Report Path

            ReportViewer.LocalReport.Refresh();
            rep.DataSources.Clear();
            rep.DataSources.Add(dsMaintenanceDS);
            //RDLC report DataSource(Tablix Datasource name)
            ReportParameter p1 = new ReportParameter("partType", ddlPartType.SelectedItem.Text);
            ReportParameter p2 = new ReportParameter("location", ddlLocation.SelectedItem.Text);
            ReportParameter p3 = new ReportParameter("reportDetail", "Detail");
            ReportParameter p4 = new ReportParameter("logged", UserContext.UserProfile.Name);
            ReportParameter p5 = new ReportParameter("fromDate", FROMDATE == Convert.ToDateTime(Constants.DefaultDate) ? string.Empty : Dates.FormatDate(FROMDATE, Constants.Format02));
            ReportParameter p6 = new ReportParameter("toDate", TODATE == Convert.ToDateTime(Constants.DefaultDate) ? string.Empty : Dates.FormatDate(TODATE, Constants.Format02));

            ReportViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6 });
            rep.Refresh();
        }
    }
}