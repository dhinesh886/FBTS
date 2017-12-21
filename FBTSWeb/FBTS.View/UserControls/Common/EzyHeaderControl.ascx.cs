using System;
using System.Web.UI;
using FBTS.App.Library;
using FBTS.Model.Common;
using FBTS.Model.Control;

namespace FBTS.View.UserControls.Common
{
    public partial class EzyHeaderControl : UserControl
    {
        public UserContext DataSource { get; set; }
        public void BindHeader()
        {
            if (DataSource != null)
            {
                DisplayHeaderInformation();
            }
            else
            {
                AuditLog.LogEvent(SysEventType.ERROR, "MENU ERROR",
                   "Error : Error occured while binding header control", new Exception("Header Control datasource is not supplied/null"));
            }
        }
        private void DisplayHeaderInformation()
        {
            //lblCopyRightYear.Text = ConfigurationReader.GetAppConfigurationValue(Constants.AppCopyRightConfKey); 
            lblUserName.Text = DataSource.UserProfile.Name;
            aLogOut.HRef = Constants.LogoutIntermediatePageUrl;
            // Logo/User Image Display
            //imgLogo.ImageUrl = Constants.LogosDirectory + DataSource.CompanyProfile.Logo;
            imgAvatar.ImageUrl = Constants.UserImagesDirectory + "img.png";
        }
    }
}