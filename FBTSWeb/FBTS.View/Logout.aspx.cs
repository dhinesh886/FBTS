using FBTS.Library.Common;
using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;            
            Title = ConfigurationReader<string>.GetAppConfigurationValue(Constants.AppVersionConfKey);
            lblCopyRightYear.Text =
                ConfigurationReader<string>.GetAppConfigurationValue(Constants.AppCopyRightConfKey);
            //linkLogin.NavigateUrl = Constants.LoginPageUrl;
        }
    }
}