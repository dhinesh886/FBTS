using FBTS.Model.Transaction.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View.UserControls.ForecastingCommon
{
    public partial class DashBoardControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string SetHeader
        {
            set
            {
                divheader.InnerText = value.ToString();
            }
        }
        public DashBoards GvData
        {
            set
            {
                GVListData.DataSource = value.ToList();
                GVListData.DataBind();
            }
        }

        protected void GVListData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text;
            }
        }
    }
}