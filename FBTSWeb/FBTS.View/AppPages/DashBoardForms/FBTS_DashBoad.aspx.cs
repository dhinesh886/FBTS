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
using FBTS.App.Library;

namespace FBTS.View.AppPages.DashBoardForms
{
    public partial class FBTS_DashBoad : System.Web.UI.Page
    {
        public readonly ReportManager _reportManager = new ReportManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));

            DashBoardControlGPRSId.SetHeader = "GPRS";
            DashBoardControlC09Id.SetHeader = "C-09";
            DashBoardControlXRayId.SetHeader = "X-Ray";
            BindData("01");
            BindData("02");
            BindData("03");
        }
        #region properties
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        #endregion
        public void BindData(string catType)
        {
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                filter1 = catType,
                FilterKey = Constants.TableDashBoard
            };
            var dashBoards = _reportManager.GetDashBoards(queryArgument);
            if (catType == "01")
                DashBoardControlGPRSId.GvData = dashBoards;               
            else if(catType=="02")
                DashBoardControlC09Id.GvData = dashBoards;
            else if (catType == "03")
                DashBoardControlXRayId.GvData = dashBoards;
        }
    }
}