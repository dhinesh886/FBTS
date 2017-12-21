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

namespace FBTS.View.AppPages.CPanel
{
    public partial class FBTS_DeleteAllTransactionData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
        }
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            DeleteAndBanPopup.Key1 = "Date";
            DeleteAndBanPopup.Action = Constants.DeleteAction;
            DeleteAndBanPopup.QueryType = Constants.DeleteAllTxn;
            DeleteAndBanPopup.Type = Constants.DeleteAllTxn;
            DeleteAndBanPopup.Show();
        }
    }
}