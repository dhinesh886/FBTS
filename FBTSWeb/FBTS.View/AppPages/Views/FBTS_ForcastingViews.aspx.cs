using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.Model.Transaction.Transactions;
using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View.AppPages.Views
{
    public partial class FBTS_ForcastingViews : System.Web.UI.Page
    {
        public readonly ReportManager _reportManager=new ReportManager();
        protected void Page_Init(object sender, EventArgs e)
        {
            ForecastingGridViewListControlId.chkClick += FormFill;
            ForecastingGridViewListControlId.onGVListDataPageIndexChanging += onGridListPageIndexChanges;
            ForecastingGridViewListControlId.onGVListDataSorting += onGVListDataSorting;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Stage"] != null)
                TransactionStageControlId.ActiveStage = Request.QueryString["Stage"].ToString();
            if (IsPostBack) return;
        }
        public OrderTransactions OrderTxns { get; set; }
        public void BindData(BindType bindType)
        {
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = Constants.PurchaseRequestTdType,
                filter1 = Dates.FormatDate(FromDate, Constants.Format05),
                filter2 = Dates.FormatDate(ToDate, Constants.Format05),
                filter3 = UserContext.UserProfile.Branch,
                filter4 = Constants.RetriveForm,
                FilterKey = Constants.TableOrderDetail
            };
            var ordView = _reportManager.GetForecatingViewData(queryArgument);
            ForecastingGridViewListControlId.IsVisiableColumn(6, true);
            ForecastingGridViewListControlId.IsVisiableColumn(7, true);
            ForecastingGridViewListControlId.IsVisiableColumn(8, true);
            ForecastingGridViewListControlId.IsVisiableColumn(9, true);
            ForecastingGridViewListControlId.IsVisiableColumn(10, true);
            ForecastingGridViewListControlId.IsVisiableColumn(11, true);
            ForecastingGridViewListControlId.IsVisiableColumn(12, true);
            ForecastingGridViewListControlId.IsVisiableColumn(13, true);
            ForecastingGridViewListControlId.IsVisiableColumn(14, true);
            ForecastingGridViewListControlId.IsVisiableColumn(18, false);
            if (BindType.Export == bindType)
            {
                OrderTxns = ordView;
            }
            else
            {
                ForecastingGridViewListControlId.OrderTxns = ordView;
            }
            //var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            //{
            //    Key = Constants.PurchaseRequestTdType,
            //    filter1 = Dates.FormatDate(FromDate, Constants.Format05),
            //    filter2 = Dates.FormatDate(ToDate, Constants.Format05),
            //    filter3 = UserContext.UserProfile.Branch,
            //    filter4 = Constants.RetriveList,
            //    FilterKey = Constants.TableOrderDetail
            //};
            //var ordView = _reportManager.GetForecatingViewData(queryArgument);
            //ForecastingGridViewListControlId.changeActionName = Constants.ViewAction;
            //ForecastingGridViewListControlId.OrderTxns = ordView;

            uplForm.Update();
        }
        #region

        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        public DateTime FromDate
        {
            get { return Dates.ToDateTime(txtFromDate.Text.Trim(), DateFormat.Format_01); }
            set
            {
                if (value == Convert.ToDateTime(Constants.DefaultDate))
                    txtFromDate.Text = Dates.FormatDate(DateTime.Now, Constants.Format02);
                else
                    txtFromDate.Text = Dates.FormatDate(value, Constants.Format02);
            }
        }
        public DateTime ToDate
        {
            get { return Dates.ToDateTime(txtToDate.Text.Trim(), DateFormat.Format_01); }
            set
            {
                if (value == Convert.ToDateTime(Constants.DefaultDate))
                    txtToDate.Text = Dates.FormatDate(DateTime.Now, Constants.Format02);
                else
                    txtToDate.Text = Dates.FormatDate(value, Constants.Format02);
            }
        }

        #endregion
        #region Visibility
        public bool IsVisiableBackDiv
        {
            set
            {
                divBack.Visible = value;
                uplActions.Update();
            }
        }
        public bool IsVisiableDateDiv
        {
            set
            {
                divDate.Visible = value;
                uplActions.Update();
            }
        }
        #endregion
        protected void FormFill(object sender, EventArgs e)
        {
            var txnId = ForecastingGridViewListControlId.OrderNumber;
            var amdno = ForecastingGridViewListControlId.Amdno;
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = Constants.PurchaseRequestTdType,
                filter1 = Dates.FormatDate(FromDate, Constants.Format05),
                filter2 = Dates.FormatDate(ToDate, Constants.Format05),
                filter3 = UserContext.UserProfile.Branch,
                filter4 = Constants.RetriveForm,
                FilterKey = Constants.TableOrderDetail
            };
            var ordView = _reportManager.GetForecatingViewData(queryArgument);
            ForecastingGridViewListControlViewId.IsVisiableColumn(6, true);
            ForecastingGridViewListControlViewId.IsVisiableColumn(7, true);
            ForecastingGridViewListControlViewId.IsVisiableColumn(8, true);
            ForecastingGridViewListControlViewId.IsVisiableColumn(9, true);
            ForecastingGridViewListControlViewId.IsVisiableColumn(10, true);
            ForecastingGridViewListControlViewId.IsVisiableColumn(11, true);
            ForecastingGridViewListControlViewId.IsVisiableColumn(12, true);
            ForecastingGridViewListControlViewId.IsVisiableColumn(13, true);
            ForecastingGridViewListControlViewId.IsVisiableColumn(14, true);
            ForecastingGridViewListControlViewId.IsVisiableColumn(18, false);
            ForecastingGridViewListControlViewId.OrderTxns = ordView;
            uplView.Update();
            IsVisiableBackDiv = true;
            IsVisiableDateDiv = false;
        }
        protected void lnkView_Click(object sender, EventArgs e)
        {
            BindData(BindType.List);
        }
        protected void onGridListPageIndexChanges(object sender, GridViewPageEventArgs e)
        {
            BindData(BindType.List);
        }
        protected void onGVListDataSorting(object sender, GridViewSortEventArgs e)
        {
            BindData(BindType.List);
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            IsVisiableBackDiv = false;
            IsVisiableDateDiv = true;
        }
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=ExportGridData.xls");
            Response.ContentType = "File/Data.xls";
            StringWriter StringWriter = new System.IO.StringWriter();
            HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);

            ForecastingGridViewListControlViewId.GVObject.RenderControl(HtmlTextWriter);
            Response.Write(StringWriter.ToString());
            Response.End();
        }
        public void ExportToExcel()
        {
            string fileName = "MyFilename.xls";

            DataGrid dg = new DataGrid();
            dg.AllowPaging = false;
            dg.DataSource = OrderTxns;
            dg.DataBind();

            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Buffer = true;
            System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            System.Web.HttpContext.Current.Response.Charset = "";
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition",
              "attachment; filename=" + fileName);

            System.Web.HttpContext.Current.Response.ContentType =
              "application/vnd.ms-excel";
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlTextWriter =
              new System.Web.UI.HtmlTextWriter(stringWriter);
            dg.RenderControl(htmlTextWriter);
            System.Web.HttpContext.Current.Response.Write(stringWriter.ToString());
            //System.Web.HttpContext.Current.Response.End();
            System.Web.HttpContext.Current.Response.Clear();
        } 

        protected void lnkExpexcel_Click(object sender, EventArgs e)
        {
            BindData(BindType.Export);
            ExportToExcel();
            //ExportGridToExcel();
        }
    }
}