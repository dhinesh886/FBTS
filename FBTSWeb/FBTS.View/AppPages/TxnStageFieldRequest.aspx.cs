using System;
using System.Web.UI;

namespace FBTS.View
{
    public partial class TxnStageFieldRequest : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                //lblUserName.Text =
                   // (SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey)).UserProfile.Name;
            }
        }
        
    }
}