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
    public partial class LoggingOff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var sSOLoggOffPageUrlkey = ConfigurationReader<string>.GetAppConfigurationValue(Constants.SSOLogOffStep2PageUrl);
            var logoutPageUrlKey = ConfigurationReader<string>.GetAppConfigurationValue(Constants.LoggedOffPageUrl);
            var logOffURL = string.Format(sSOLoggOffPageUrlkey, logoutPageUrlKey);
            Session.Clear();
            Response.Redirect(logOffURL, false);
        }
    }
}