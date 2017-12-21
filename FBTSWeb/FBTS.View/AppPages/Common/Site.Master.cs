using System;
using System.Web.UI;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.App.Library;

// ReSharper disable CheckNamespace
namespace FBTS.View
// ReSharper restore CheckNamespace
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsSessionExpired())
            {
                AuditLog.LogEvent(SysEventType.ERROR, "MASTERPAGE ERROR", "Error:Signout From MasterPage", new Exception("Session Expired!"));
                Response.Redirect(Constants.SessionExpiryPageUrl, true);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1D);
            Response.Expires = -1500;
            Response.CacheControl = "no-cache";
            try
            {
                AuditLog.LogEvent(SysEventType.ERROR, "Master Page", "Now on Master Page...", null);
                var objUserContext = SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey);
                if (objUserContext == null) throw new Exception("Session Expired/ Unauthorized Access found");
                AuditLog.LogEvent(SysEventType.ERROR, "Master Page Session", "Session not Expired..", null);
                // Page title display
                Page.Title = ConfigurationReader<string>.GetAppConfigurationValue(Constants.AppVersionConfKey);
                EzyHeaderControl.DataSource = objUserContext;
                EzyHeaderControl.BindHeader();
                EzyMenuControl.DataSource = objUserContext.Menus;
                EzyMenuControl.BindMenu();
            }
            catch (Exception ex)
            {
                AuditLog.LogEvent(SysEventType.ERROR, "MASTERPAGE ERROR", "Error:Signout From MasterPage", ex);
                Response.Redirect(Constants.LogoutIntermediatePageUrl, false);
            }
        }

        public void SetFocus(Control txtbx)
        {
            ToolkitScriptManager1.SetFocus(txtbx);
        }

        public void RegisterPostbackTrigger(Control triggerControl)
        {
            ToolkitScriptManager1.RegisterPostBackControl(triggerControl);
        }

        private bool IsSessionExpired()
        {
            var userContext = SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey);
            // Check for App session expiry
            if (userContext == null)
            {
                return true;
            }

            // Bypass SSO validation if it set in config
            var bypassSSOAuthentication = ConfigurationReader<string>.GetAppConfigurationValue(Constants.BypassSSOAuthentication);
            if (!string.IsNullOrEmpty(bypassSSOAuthentication) && bypassSSOAuthentication == "true")
            {
                return false;
            }

            // Check for SSO session Expiry
            var ssoId = GetSSOIDFromRequestHeader();
            if (string.IsNullOrEmpty(ssoId) || ssoId != userContext.UserProfile.LoginId.Trim())
            {
                AuditLog.LogEvent(SysEventType.INFO, "Login Authentication", "APP CONTEXT SSO ID : " + userContext.UserProfile.LoginId.Trim() + " REQUEST HEADER SSO ID : " + ssoId, null);
                return true;
            }

            return false;
        }
        private string GetSSOIDFromRequestHeader()
        {
            try
            {
                var ssoKey = ConfigurationReader<string>.GetAppConfigurationValue(Constants.SSOIDKey);
                var allRaw = Request.ServerVariables["ALL_RAW"];
                if (!allRaw.Contains(ssoKey))
                {
                    AuditLog.LogEvent(SysEventType.INFO, "Login Authentication", "SSO ID NOT FOUND IN THE REQUEST", null);
                    return null;
                }
                var startPosition = allRaw.IndexOf(ssoKey);
                var endPosition = allRaw.IndexOf("\r", startPosition);
                var currentKeyValue = allRaw.Substring(startPosition, endPosition - startPosition);
                var splittedArray = currentKeyValue.Split(':');
                if (splittedArray != null && splittedArray.Length > 0)
                {
                    AuditLog.LogEvent(SysEventType.INFO, "Login Authentication", "SSO ID : " + splittedArray[1], null);
                    return splittedArray[1].Trim();
                }
                AuditLog.LogEvent(SysEventType.INFO, "Login Authentication", "Failed to split key value pair", null);
            }
            catch (Exception ex)
            {
                AuditLog.LogEvent(SysEventType.ERROR, "Login Authentication", "GetSSOID failed with an exception ", ex);
            }
            return null;
        }
    }
}