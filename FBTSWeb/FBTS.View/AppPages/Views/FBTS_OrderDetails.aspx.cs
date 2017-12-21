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
    public partial class FBTS_OrderDetails : System.Web.UI.Page
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
            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter4, Constants.DdlFilterTypes),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextMaterialType),
                            new KeyValuePairItem(Constants.masterType, Constants.DdlMATCategories)
                        };
            _genericClass.LoadDropDown(ddlCategory, filter, null, UserContext.DataBaseInfo);

            //var filter = new KeyValuePairItems
            //            {
            //                new KeyValuePairItem(Constants.filter1, Constants.DvStages),
            //                new KeyValuePairItem(Constants.filter2, Constants.BillTrackingType),
            //                new KeyValuePairItem(Constants.filter3, UserContext.UserId.ToString()),
            //                new KeyValuePairItem(Constants.masterType, "USTG"),
            //                new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextMaterialType)
            //            };
            //_genericClass.LoadDropDown(ddlCategory, filter, null, UserContext.DataBaseInfo);

            filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.WHCType),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.AllType),
                            new KeyValuePairItem(Constants.masterType,Constants.TableAccounts)
                        };
            _genericClass.LoadDropDown(ddlLocation, filter, null, UserContext.DataBaseInfo);
            LOCATION = UserContext.UserProfile.Branch;
            FROMDATE = DateTime.Now.AddDays(-1);
            TODATE = DateTime.Now;
        }

        public string ORDERTYPE { get { return ddlOrderType.SelectedValue.Trim(); } set { ddlOrderType.SelectedValue = value.Trim(); } }
        public string TYPE { get { return ddlType.SelectedValue.Trim(); } set { ddlType.SelectedValue = value.Trim(); } }
        public string CATEGORIES { get { return ddlCategory.SelectedValue.Trim(); } set { ddlCategory.SelectedValue = value.Trim(); } }
        public string LOCATION { get { return ddlLocation.SelectedValue.Trim(); } set { ddlLocation.SelectedValue = value.Trim(); } }
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
            BindReport("FBTS.Reports.Rdls.BackOrderStockTransfer2.rdlc", "Orders");
        }

        private KeyValuePairItems GetConfigurations()
        {
            var configurations = new KeyValuePairItems
            {
                new KeyValuePairItem("ORDERTYPE", ORDERTYPE),
                new KeyValuePairItem("TYPE", TYPE),
                new KeyValuePairItem("CATEGORIES", CATEGORIES),
                new KeyValuePairItem("LOCATION", LOCATION),
                new KeyValuePairItem("SR", SR),
                new KeyValuePairItem("FROMDATE", FROMDATE),
                new KeyValuePairItem("TODATE", TODATE)
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


            OrderViewer.ProcessingMode = ProcessingMode.Local;
            LocalReport rep = OrderViewer.LocalReport;

            ReportDataSource dsMaintenanceDS = new ReportDataSource();
            dsMaintenanceDS.Name = datasetName;// Data Source Name
            dsMaintenanceDS.Value = objContainer.Result;// Data Values
            // rep.DataSources.Clear();


            Assembly assembly = Assembly.LoadFrom(Server.MapPath(@"~\bin\FBTS.Reports.dll"));
            Stream stream = assembly.GetManifestResourceStream(reportUrl);
            OrderViewer.LocalReport.LoadReportDefinition(stream);

            OrderViewer.LocalReport.Refresh();
            rep.DataSources.Clear();
            rep.DataSources.Add(dsMaintenanceDS);
            //RDLC report DataSource(Tablix Datasource name)
            ReportParameter p1 = new ReportParameter("location", ddlLocation.SelectedItem.Text);//Passing the Parameters
            ReportParameter p2 = new ReportParameter("category", ddlCategory.SelectedItem.Text);
            ReportParameter p3 = new ReportParameter("orderType", ddlOrderType.SelectedValue.ToString().Trim() == string.Empty ? string.Empty : ddlOrderType.SelectedItem.Text);
            ReportParameter p4 = new ReportParameter("type", ddlType.SelectedValue.ToString().Trim() == string.Empty ? string.Empty : ddlType.SelectedItem.Text);
            ReportParameter p5 = new ReportParameter("reportDetail", "Detail");
            ReportParameter p6 = new ReportParameter("logged", UserContext.UserProfile.Name);
            ReportParameter p7 = new ReportParameter("fromDate", FROMDATE == Convert.ToDateTime(Constants.DefaultDate) ? string.Empty : Dates.FormatDate(FROMDATE, Constants.Format02));
            ReportParameter p8 = new ReportParameter("toDate", TODATE == Convert.ToDateTime(Constants.DefaultDate) ? string.Empty : Dates.FormatDate(TODATE, Constants.Format02));

            OrderViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8 });
            rep.Refresh();
        }

        protected void chkSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSearch.Checked)
                divTxt.Visible = true;
            else
                divTxt.Visible = false;
        }
    }
}