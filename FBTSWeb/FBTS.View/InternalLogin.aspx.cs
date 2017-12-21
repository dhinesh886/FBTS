using FBTS.App.Library;
using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.View.Resources.ResourceFiles;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View
{
    public partial class InternalLogin : Page
    {
        private UserContext _userContext = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            // Global Information Display
            Title = ConfigurationReader<string>.GetAppConfigurationValue(Constants.AppVersionConfKey);
            lblCopyRightYear.Text =
                ConfigurationReader<string>.GetAppConfigurationValue(Constants.AppCopyRightConfKey);
            ToolkitScriptManager1.SetFocus(txtUserId);
        }

        protected void txtUserId_TextChanged(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            try
            {
                if (string.IsNullOrEmpty(txtUserId.Text)) return;
                var clientId = ConfigurationReader<string>.GetAppConfigurationValue(Constants.ClientIdConfKey);
                if (!ValidateUser(clientId, txtUserId.Text.ToString(CultureInfo.InvariantCulture)))
                {
                    lblMessage.InnerHtml = GlobalCustomResource.Default_txtUserId_TextChanged_Invalid_Userid__;
                    lblMessage.Visible = true;
                }
                else
                {
                    if (ddlFinancialYear.Items.Count > 1)
                        ddlFinancialYear.SelectedIndex = 1;
                    ToolkitScriptManager1.SetFocus(txtPassword);
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerHtml = GlobalCustomResource.Default_lnkLogin_Click_Login_Failed_Error;
                AuditLog.LogEvent(SysEventType.ERROR, "Login Authentication", "Login failed with exception", ex);
                lblMessage.Visible = true;
            } 
        } 
        protected void lnkLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUserId.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    var clientId = ConfigurationReader<string>.GetAppConfigurationValue(Constants.ClientIdConfKey);
                    var userId = txtUserId.Text;
                    var password = txtPassword.Text;
                    var dataBase = ddlFinancialYear.SelectedValue.Trim();
                    if (AuthenticateUser(clientId, userId, password, dataBase))
                    {
                        Response.Redirect(Constants.DefaultAppPagesDirectory + _userContext.UserProfile.DefaultLink, false);
                    }
                    else
                    {
                        lblMessage.InnerHtml = GlobalCustomResource.Default_lnkLogin_Click_Invalid_Userid_or_Password__;
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    lblMessage.InnerHtml = GlobalCustomResource.Default_lnkLogin_Click_Please_Enter_Valid_userid_and_password__;
                    lblMessage.Visible = true;

                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerHtml = GlobalCustomResource.Default_lnkLogin_Click_Login_Failed_Error;
                AuditLog.LogEvent(SysEventType.ERROR, "Login Authentication", "Login failed with exception", ex);
                lblMessage.Visible = true;
            }
        }

        public bool AuthenticateUser(string argsClientId, string argsUserId, string argsPassword, string argsDatabase)
        {
            var loginPresenter = new UserAuthenticationManager();
            _userContext = loginPresenter.AuthenticateUser(argsClientId, argsUserId, argsPassword,
                argsDatabase);
            if (_userContext == null) return false; 
            SessionManagement<UserContext>.SetValue(Constants.UserContextSessionKey, _userContext);
            return true;
        }

        public bool ValidateUser(string argsClientId, string argsUserId)
        {
            var loginPresenter = new UserAuthenticationManager(); 
            var dbList = loginPresenter.ValidateUser(argsClientId, argsUserId);
            if (dbList == null || dbList.Count <= 0) return false;
            foreach (var kvPair in dbList)
            {
                ddlFinancialYear.Items.Add(new ListItem(kvPair.Value, kvPair.Key));
                ddlFinancialYear.SelectedIndex = 1;
            } 
            return true;
        }

        protected void txtFPUserId_TextChanged(object sender, EventArgs e)
        {
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
        }
    }
}