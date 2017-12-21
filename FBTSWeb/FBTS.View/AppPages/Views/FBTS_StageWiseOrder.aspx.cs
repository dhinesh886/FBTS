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
    public partial class FBTS_StageWiseOrder : System.Web.UI.Page
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
            //var filter = new KeyValuePairItems
            //            {
            //                new KeyValuePairItem(Constants.filter1, Constants.DvStages),
            //                new KeyValuePairItem(Constants.filter2, Constants.BillTrackingType),
            //                new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextTeam),
            //                new KeyValuePairItem(Constants.masterType, Constants.DdlWFComponents)
            //            };

            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.DvStages),
                            new KeyValuePairItem(Constants.filter2, Constants.BillTrackingType),
                            new KeyValuePairItem(Constants.filter3, UserContext.UserId.ToString()),
                            new KeyValuePairItem(Constants.masterType, "USTG"),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextTeam)
                        };
            _genericClass.LoadDropDown(ddlStage, filter, null, UserContext.DataBaseInfo);

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

        public string TYPE { get { return ddlOrderType.SelectedValue.Trim(); } set { ddlOrderType.SelectedValue = value.Trim(); } }
        public string TEAM { get { return ddlStage.SelectedValue.Trim(); } set { ddlStage.SelectedValue = value.Trim(); } }
        public string LOCATION { get { return ddlLocation.SelectedValue.Trim(); } set { ddlLocation.SelectedValue = value.Trim(); } }
        public string SR { get { return txtSR.Text; } set { txtSR.Text = value.Trim(); } }
        public string FILTER { get { return hidFILTER.Value; } set { hidFILTER.Value = value; } }
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

        
        string LOGGED = string.Empty;
        string STAGE = string.Empty;
        //string FILTER = string.Empty;
        string CATEGORY = string.Empty;

        string STATUS = string.Empty;

        string ORDCAT = string.Empty;

        string PENDING_ORD = string.Empty; 



        protected void lnkView_Click(object sender, EventArgs e)
        {
            if (FILTER == "PGC")            
                BindReport("FBTS.Reports.Rdls.GPRSDetails.rdlc", "GPRSPending");
            else if (FILTER == Constants.GPRSC09)
                BindReport("FBTS.Reports.Rdls.GPRSFresh.rdlc", "GPRSFresh");  
            else
                BindReport("FBTS.Reports.Rdls.StageDetails.rdlc", "StageDetails");
        }

        private KeyValuePairItems GetConfigurations()
        {
            if (TEAM == "06")
            {
                LOGGED = Constants.TRNLogedOFF;
                FILTER = Constants.VerificationDeviation;
            }                
            else if (TEAM == "07")
            {
                LOGGED = Constants.TRNInProcessOFF;
                FILTER = Constants.VerificationDeviation;
                ORDCAT = Constants.DeviationType;
            }
            else if (TEAM == "08")
            {
                //FILTER = Constants.VerificationDeviation;
                FILTER = Constants.OrderingType;
                ddlStage_SelectedIndexChanged(this, EventArgs.Empty);
                chkPending_CheckedChanged(this, EventArgs.Empty);
                chkDeviation_CheckedChanged(this, EventArgs.Empty);
            }
            else if (TEAM == "09")
            {
                FILTER = Constants.TxnDebriefingType;
            }
            else if (TEAM == "10")
            {
                FILTER = Constants.TxnTrackingType;
                ddlStage_SelectedIndexChanged(this, EventArgs.Empty);
                chkClosed_CheckedChanged(this, EventArgs.Empty);
            }
            else if (TEAM == "12")
            {
                //FILTER = Constants.GPRSC09;
                CATEGORY = "01";
                ddlStage_SelectedIndexChanged(this, EventArgs.Empty);
                chkGPRSPending_CheckedChanged(this, EventArgs.Empty);
            }
            else if (TEAM == "13")
            {
                //FILTER = Constants.GPRSC09;
                CATEGORY = "02";
                ddlStage_SelectedIndexChanged(this, EventArgs.Empty);
                chkGPRSPending_CheckedChanged(this, EventArgs.Empty);
            }


            var configurations = new KeyValuePairItems
            {
                new KeyValuePairItem("LOGGED", LOGGED),
                new KeyValuePairItem("STAGE", STAGE),
                new KeyValuePairItem("FILTER", FILTER),
                new KeyValuePairItem("CATEGORY", CATEGORY),
                new KeyValuePairItem("LOCATION", LOCATION),
                new KeyValuePairItem("SR", SR),               
                new KeyValuePairItem("FROMDATE",FROMDATE),
                new KeyValuePairItem("TODATE", TODATE),

                new KeyValuePairItem("STATUS", STATUS),

                new KeyValuePairItem("ORDCAT", ORDCAT),

                new KeyValuePairItem("PENDING_ORD", PENDING_ORD),

                new KeyValuePairItem("TYPE", TYPE),

            };

            if (txtFromDate.Text.Trim() == string.Empty || txtToDate.Text.Trim() == string.Empty)
            {
                var configuration = configurations.Where(x => x.Key == "FROMDATE");
                if(configuration.Any())
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

            StageWiseOrderViewer.ProcessingMode = ProcessingMode.Local;
            LocalReport rep = StageWiseOrderViewer.LocalReport;

            ReportDataSource dsMaintenanceDS = new ReportDataSource();
            dsMaintenanceDS.Name = datasetName;// Data Source Name
            dsMaintenanceDS.Value = objContainer.Result;// Data Values            

            Assembly assembly = Assembly.LoadFrom(Server.MapPath(@"~\bin\FBTS.Reports.dll"));
            Stream stream = assembly.GetManifestResourceStream(reportUrl);
            StageWiseOrderViewer.LocalReport.LoadReportDefinition(stream);

            StageWiseOrderViewer.LocalReport.Refresh();
            rep.DataSources.Clear();
            rep.DataSources.Add(dsMaintenanceDS);
            //RDLC report DataSource(Tablix Datasource name)
            ReportParameter p1 = new ReportParameter("rptParam1", ddlStage.Text);
            ReportParameter p2 = new ReportParameter("rptParam2", FILTER);

            ReportParameter p3 = new ReportParameter("location", ddlLocation.SelectedItem.Text);
            ReportParameter p4 = new ReportParameter("stage", ddlStage.SelectedItem.Text);
            ReportParameter p5 = new ReportParameter("logged", UserContext.UserProfile.Name);

            ReportParameter p6 = new ReportParameter("orderType", ddlOrderType.SelectedValue.ToString().Trim() == string.Empty ? string.Empty : ddlOrderType.SelectedItem.Text);
            ReportParameter p7 = new ReportParameter("pending", chkPending.Checked ? "Pending" : string.Empty);
            ReportParameter p8 = new ReportParameter("fromDate", FROMDATE == Convert.ToDateTime(Constants.DefaultDate) ? string.Empty : Dates.FormatDate(FROMDATE, Constants.Format02));
            ReportParameter p9 = new ReportParameter("toDate", TODATE == Convert.ToDateTime(Constants.DefaultDate) ? string.Empty : Dates.FormatDate(TODATE, Constants.Format02));

            //ReportParameter p7 = new ReportParameter("deviationOrder", UserContext.UserProfile.Name);
            //ReportParameter p3 = new ReportParameter("rptParam3", STATUS);//Passing the Parameters
            //ReportParameter tax = new ReportParameter("taxParam", tax_detail);//Passing the Parameters
            StageWiseOrderViewer.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4, p5, p6, p7, p8, p9 });
            rep.Refresh();
        }

        protected void chkSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSearch.Checked)
                divTxt.Visible = true;
            else
                divTxt.Visible = false;
        }

        protected void chkClosed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClosed.Checked)
                STATUS = Constants.TRNCompletedOFF;
            else
                STATUS = Constants.TRNInProcessOFF;
        }

        protected void ddlStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TEAM == "10")
                divClosed.Visible = true;
            else
                divClosed.Visible = false;

            if (TEAM == "08")
            {
                divPending.Visible = true;
                divDeviation.Visible = true;
            }
            else
            {
                divPending.Visible = false;
                divDeviation.Visible = false;
            }    

            if(TEAM == "12" || TEAM == "13")
            {
                divOrderType.Visible = true;
                divGPRSPending.Visible = true;
            }
            else
            {
                divOrderType.Visible = false;
                divGPRSPending.Visible = false;
            }                      
        }

        protected void chkPending_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPending.Checked)
            {
                PENDING_ORD = Constants.TRNCompletedOFF;
                FILTER = "POR";
                chkDeviation.Checked = false;
            }            
        }

        protected void chkDeviation_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDeviation.Checked)
            {
                PENDING_ORD = Constants.TRNInProcessOFF;
                FILTER = "DOR";
                chkPending.Checked = false;
            }            
        }

        protected void chkGPRSPending_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGPRSPending.Checked)
                FILTER = "PGC";
            else
                FILTER = Constants.GPRSC09;
        }

        protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FILTER = Constants.GPRSC09;
        }
    }
}