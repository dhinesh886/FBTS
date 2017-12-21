using FBTS.Library.Common;
using FBTS.Model.Transaction.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View.UserControls.ForecastingCommon
{
    public partial class DynamicGridControl : System.Web.UI.UserControl
    {
        private int _newPageIndex = -1;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public OrderTransactions OrderTxns
        {
            get
            {
                return XmlUtilities.ToObject<OrderTransactions>(ViewState["OrderTxns"].ToString());
            }
            set
            {
                ViewState["OrderTxns"] = value.ToXml();
                BindGridViewListData(value);
            }
        }
        private void BindGridViewListData(OrderTransactions orderTxns)
        {
            GVListData.DataSource = orderTxns.ToList();
            if (_newPageIndex >= 0)
                GVListData.PageIndex = _newPageIndex;
            GVListData.DataSource = orderTxns.ToList();
            GVListData.DataBind();

        }
        protected void GVListData_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GVListData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}