using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.Model.Transaction;
using FBTS.Model.Transaction.Transactions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View.AppPages.Views
{
    public partial class FBTS_OrderView : System.Web.UI.Page
    {

        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }

        public readonly ReportManager _reportManager = new ReportManager();
        private readonly GenericManager _genericClass = new GenericManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Stage"] != null)
                TransactionStageControlId.ActiveStage = Request.QueryString["Stage"].ToString();
            if (IsPostBack) return;

            //var filter = new KeyValuePairItems
            //            {                           
            //                new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextRegion),
            //                new KeyValuePairItem(Constants.masterType, Constants.DdlRegion)
            //            };
            //_genericClass.LoadDropDown(ddlRegion, filter, null, UserContext.DataBaseInfo);

            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.PartType),
                            new KeyValuePairItem(Constants.filter1, Constants.DvMode),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextModality),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlCatHeaderData)
                        };
            _genericClass.LoadDropDown(ddlModality, filter, null, UserContext.DataBaseInfo);

            FromDate = DateTime.Now.AddDays(-1);
            ToDate = DateTime.Now;
        }

        private int _newPageIndex = -1;
        public string SR { get { return txtSRSearch.Text; } set { txtSRSearch.Text = value.Trim(); } }
        //public string Region { get { return ddlRegion.SelectedValue.Trim(); } set { ddlRegion.SelectedValue = value.Trim(); } }
        public string Modality { get { return ddlModality.SelectedValue.Trim(); } set { ddlModality.SelectedValue = value.Trim(); } }

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

        public void BindData(BindType bindType)
        {
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
               //Key = Region,
                filter1 = Convert.ToDateTime(Constants.DefaultDate) == FromDate ? string.Empty : Dates.FormatDate(FromDate, Constants.Format05),
                filter2 = Convert.ToDateTime(Constants.DefaultDate) == ToDate ? string.Empty : Dates.FormatDate(ToDate, Constants.Format05),
                filter3 = Modality,
                filter4 = Constants.RetriveTempType,
                filter5 = SR,
                FilterKey = Constants.TableOrderDetail
            };
            var ordView = _reportManager.GetOrderDetailViewData(queryArgument);
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

            //var ord = e.Row.DataItem as OrderHead;

            //if (ord.RelatedSR == null)
            //    GVListData.Columns[2].Visible = false;

            //GridViewRow row = new GridViewRow(0, 15, DataControlRowType.Header, DataControlRowState.Normal);
            //TableHeaderCell cell = new TableHeaderCell();
            //cell.Text = "Ordering";
            //cell.ColumnSpan = 2;
            //row.Controls.Add(cell);

            //cell = new TableHeaderCell();
            //cell.ColumnSpan = 2;
            //cell.Text = "GPRS/C09";
            //row.Controls.Add(cell);

            //row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            //GVListData.HeaderRow.Parent.Controls.AddAt(0, row);            
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
    }
}