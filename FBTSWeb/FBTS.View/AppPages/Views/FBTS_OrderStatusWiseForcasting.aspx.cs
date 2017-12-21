﻿using FBTS.Business.Manager;
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
    public partial class FBTS_OrderStatusWiseForcasting : System.Web.UI.Page
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
                            new KeyValuePairItem(Constants.filter1, Constants.WHCType),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.AllType),
                            new KeyValuePairItem(Constants.masterType,Constants.TableAccounts)
                        };
            _genericClass.LoadDropDown(ddlLocation, filter, null, UserContext.DataBaseInfo);
            LOCATION = UserContext.UserProfile.Branch;
        }

        public string ORDERTYPE { get { return ddlOrderType.SelectedValue.Trim(); } set { ddlOrderType.SelectedValue = value.Trim(); } }
        public string LOCATION { get { return ddlLocation.SelectedValue.Trim(); } set { ddlLocation.SelectedValue = value.Trim(); } }
        public string FR { get { return txtFR.Text; } set { txtFR.Text = value.Trim(); } }
        public DateTime FROMDATE
        {
            get { return Dates.ToDateTime(txtFromDate.Text.Trim(), DateFormat.Format_01); }
            set
            {
                if (value == Dates.ToDateTime(Constants.DefaultDate, DateFormat.Format_02))
                    txtFromDate.Text = string.Empty;
                else txtFromDate.Text = Dates.FormatDate(value, Constants.Format02);
            }
        }

        public DateTime TODATE
        {
            get { return Dates.ToDateTime(txtToDate.Text.Trim(), DateFormat.Format_01); }
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
                new KeyValuePairItem("LOCATION", LOCATION),
                new KeyValuePairItem("FR", FR),
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


            OrderStatusWiseViewver.ProcessingMode = ProcessingMode.Local;
            LocalReport rep = OrderStatusWiseViewver.LocalReport;

            ReportDataSource dsMaintenanceDS = new ReportDataSource();
            dsMaintenanceDS.Name = datasetName;// Data Source Name
            dsMaintenanceDS.Value = objContainer.Result;// Data Values            

            Assembly assembly = Assembly.LoadFrom(Server.MapPath(@"~\bin\FBTS.Reports.dll"));
            Stream stream = assembly.GetManifestResourceStream(reportUrl);
            OrderStatusWiseViewver.LocalReport.LoadReportDefinition(stream);

            OrderStatusWiseViewver.LocalReport.Refresh();
            rep.DataSources.Clear();
            rep.DataSources.Add(dsMaintenanceDS);
            //ReportParameter p1 = new ReportParameter("rptParam1", "Customer Vendor Master");//Passing the Parameters
            //ReportParameter p2 = new ReportParameter("rptParam2", " ");//Passing the Parameters
            //OrderStatusWiseViewver.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
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