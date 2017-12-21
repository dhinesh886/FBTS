using FBTS.App.Library;
using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.View.Resources.ResourceFiles;
using System;
using System.Web.UI;

namespace FBTS.View
{
    public partial class Default : Page
    {
        private UserContext _userContext = null;
        protected void Page_Load(object sender, EventArgs e)
        {
           
           

            if (IsPostBack) return;
            var req = Request.ServerVariables;
            // Global Information Display
            Title = ConfigurationReader<string>.GetAppConfigurationValue(Constants.AppVersionConfKey);
            lblCopyRightYear.Text =
                ConfigurationReader<string>.GetAppConfigurationValue(Constants.AppCopyRightConfKey);
            AuthenticateUser();
        }
       
        private void AuthenticateUser()
        {
            try
            {
                var ssoId = GetSSOIDFromRequestHeader();
                if (string.IsNullOrEmpty(ssoId))
                {
                    lblMessage.InnerHtml = GlobalCustomResource.Default_lnkLogin_Click_Please_Enter_Valid_userid_and_password__;
                    lblMessage.Visible = true;
                }
                else
                {
                    var clientId = ConfigurationReader<string>.GetAppConfigurationValue(Constants.ClientIdConfKey);
                    var dbinfoKey = ConfigurationReader<string>.GetAppConfigurationValue(Constants.TrnsDbInfoKey);
                    if (AuthenticateUser(clientId, ssoId, string.Empty, dbinfoKey))
                    {                        
                        Response.Redirect(Constants.DefaultAppPagesDirectory + _userContext.UserProfile.DefaultLink, false);
                    }
                    else
                    {
                        lblMessage.InnerHtml = GlobalCustomResource.Default_lnkLogin_Click_Invalid_Userid_or_Password__;
                        lblMessage.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerHtml = GlobalCustomResource.Default_lnkLogin_Click_Login_Failed_Error;
                AuditLog.LogEvent(SysEventType.ERROR, "Login Authentication", "Login failed with exception", ex);
                lblMessage.Visible = true;
            }
        }
        private string GetSSOIDFromRequestHeader()
        {
            try
            {
                //var temp = "Cache-Control: max-age=0 Connection: keep-alive Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8 Accept-Encoding: gzip, deflate, sdch Accept-Language: en-US,en;q=0.8 Cookie: logintry=1%3B1489664199226; TARGET=HTTPS%3A//ssostagingck.registrar.ge.com/PfIdpSmAuth/PFRedirectProdProtected.jsp%3FfedHost%3Dhttps%3A//fss.stage.gecompany.com/fss%26resumePath%3D%252Fidp%252FVdlj5%252FresumeSAML20%252Fidp%252FSSO.ping; RSOSESSION=vFkrWaOZdYaLOQeaOzXMNCoTG1KN7C+pLwtsvx3ITOApBWwmhZVft0VLnOF/nggklw1NVgKirxMAZ6ortM7KkZy58Lpx3NiJm2vTtPUHZQY8LXWzR5G0Xs0Vifu8V2UHCmYrmBXgICTFsYZepS1GRSAD5SyRIwmSl5fR/brQYQprkHzW74OudIfQgw1NJTf5ZDavj3Ubd03S+ZjHFNrDNPfDqyl6VM1n+1LTSJKbVTleH6KdidGjcZB+DYNHveuRLlENCRmXA/Ah4ZCZ1Q4GyHAa7MYVWnnbxJi9kIq7P9kqKAh35E213yAUJyNNZf6vf/fEd2OJjjI8WGFwpYNNMznLqDlN/fgCAiNKVv1hC2Q5svjI7QgVWvyo0Qplp1oVs0ZMwn88zDSqrbF1tqtmKEZaVLZWhr+4JrQgGBTxv2bwN3g3iZoWlr6IgCOnglq2GebW9/pLC4eY/1vtJtVx6+LZrLkOazl+b+MBno5C4ednL4dpboSSmriDMVUmjZisz/fcGdT3p2+UTebBEffwcaRiKeJyWsqFSnTtdkXBcBpKx8P+augaZRyoEZExnpEowSLiPqdyEJPdhccCHq9VuIgmTZolAH7gBkK/9xfwKgZWA7pe2sMW/h6vo/aKwmxrQP8bYjUMj2w9G5+eUF63+zXtBaXT4cW/YvaDgDbN42Lz5dAXfJ6z8JJ/EKE3DGENlvdCBPCgzvLNmwJMninbwnMC1+VY2NJe3ZOQqLmjAySVyZzNakyPKRJCyk2pPWzzv59PA/WrkAZBzWheKkW2lLigPRlN5fL9BCxkgVmOz+dCFstdD+8sGGbHdcgWWIUXSKx3YkeSRIRhOYbSNRT+Jxy1ypiKiqjb93NbLtBQk5qhokm/5eEjBgE2owGrs5LUIyHZn3Z4kCtZr5T1JhMzwRiXupRT48ohA0ZrkBtoQ6heQNu01HDzz/zRAsKUmr0nPaHO4J1UJpsP4WGsU1qpCFPhbkhwr9jlmFvWmkMJdJAkxndic8Meb072M8aADaEQ9idpQSxo+XcG+zhFVwJYIaxPuutEw2trbDBtkrSffnYWA1vOeUy2ArH4L0T8WE7Pubn00PwXJ5x0gpk0jxf4l7gWHYuZDiqvDCd6aD+goM9f5B5EK3v5wUizbkaBXwQywkDwFgDb2Egu9JG24Vsj2W3rxPxzr7CJ; SMSESSION=HzPJAbXPpZrhT/VFCSaJeaMfdQ4iVvT4CJ7QzxLoGCnMHM7lA/uHphCgl2tUeMgKPaDr1Ri0hG+BGNQwO3lIqO04ataTnL9/yx5II/KNquu3sJIQcz6PUYWAvAtef9i7Xa5aRPewd5xgZQ2e1zC/S6dQjguEHzLKEAO5HrmmkFXpiY8hp/vX0MGHLIvE2sk/c6eCay35XeWFuBmT5wFFlklhE29LhLebd5VBrJHWLVlJCwXlH4/v6iUkxdRsV8ePfx/IRTHRb59h6HcWVa2ZqeBBlw6AYkyP3MwGCI+Dpqz3OootExSAyrO1b227ggEAAwICD/9pS7XyDCGM+ZdjLxPaPHLRBgbDa33UeVMyTLClwFhH48THIf2V05xv4I9GAu7ZZLnVCtzFKv/D7lPvPb+rsIIQg0i685SLfhUcB0/X4apITmgD/569wbtg85C1SCTE9WlP5Opn+CpFHs82VP9OxT+NvFdHq9FeN43aAI+c/h+i0dLHKebooX8mV7FqsfOC4Bah8AmxJMAHsWHDlBRwfJiDYO5Plm8HXcnL8rnMrlcYaex9P8vEdaLeQDA9kY3pUki6++zOERveeprqrryyNt0tpmIKkWR0tRbqNpSHRCCo7fLdMaWMc4TLbKtenLQ7FHacjYXNRdnqFl/PRHOJrU+KGqA3RltWR8bs9cH4BkMsMahNnIZzbRxp8AKrrmO5QtVZcmMII6oGHclo6ku1J+l/uhpC2oAeZrDbDxKcnsH0slX69c797TZut77G8HJqP17NFS3wRJuALN/H2AHjAUnWnEs5vS3vmH/++ARg4FSGpA0WO8IvSZg3JGvOjDKPucIROShUasgbgkldJIMpZuNqJO6eMniECAEO4Pgxe3M+Ybcgx7xbvwVFmjJZb95MQge2tGGctOj6BQPMqwGw7xmrLYoSFnBw8p9YiL6YYCfcJooUJ9hEYdkMoG1Q9tD4E6uFst2bQVnL/8kinFjWHBZFnf+quT5fRPrbRjsVnmOYuh1n4Yaa8OkoDCq9sfu2CYCcnZCRKAXgoGKlD4Ig/AwH6KE3GgX2+kIkd7AZxahmvGw+JErpr5DniBzd+qAAiCbz4X7zT+68z2NiMLanR9XxsKnm0ONt/st3IFRfr6olipkKWixI9ayubKQfRghRWfR0J4hE/8bscl5pT2ah72ZiRrJS; _shibsession_64656661756c7468632d666274732d737467=_f2101a5e3d7cbefa37a59a5b087366e5 Host: stg-bts.ap.health.ge.com Referer: https://fss.stage.gecompany.com/ User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36 Upgrade-Insecure-Requests: 1 DNT: 1 ShibSpoofCheck: 212484833638739896101279980556635823627 Shib-Application-ID: default Shib-Session-ID: _f2101a5e3d7cbefa37a59a5b087366e5 Shib-Identity-Provider: gefssstg Shib-Authentication-Instant: 2017-03-16T11:36:45.282Z Shib-Authentication-Method: urn:oasis:names:tc:SAML:2.0:ac:classes:unspecified Shib-AuthnContext-Class: urn:oasis:names:tc:SAML:2.0:ac:classes:unspecified Shib-Session-Index: AOdMtUPP4H71l0vJLSLHrjilE9e HTTP_GIVENNAME: Furqan HTTP_SN: Ahmad HTTP_mail: FurqanAhmad@ge.com sso_uid: 305022783";
                var ssoKey = ConfigurationReader<string>.GetAppConfigurationValue(Constants.SSOIDKey);
                var allRaw = Request.ServerVariables["ALL_RAW"];
                AuditLog.LogEvent(SysEventType.INFO, "Login Authentication:ALL_RAW", allRaw, null);
                if (!allRaw.Contains(ssoKey))
                {
                    AuditLog.LogEvent(SysEventType.INFO, "Login Authentication", "SSO ID NOT FOUND IN THE REQUEST", null);
                    return null;
                }
                AuditLog.LogEvent(SysEventType.INFO, "Login Authentication", "SSO ID FOUND IN THE REQUEST", null);
                var startPosition = allRaw.IndexOf(ssoKey);
                var endPosition = allRaw.IndexOf("\r", startPosition);
                var currentKeyValue = allRaw.Substring(startPosition, endPosition - startPosition);
                AuditLog.LogEvent(SysEventType.INFO, "Login Authentication", "SSO ID KEY VALUE: " + currentKeyValue, null);
                var splittedArray = currentKeyValue.Split(':');
                if (splittedArray != null && splittedArray.Length > 0)
                {
                    AuditLog.LogEvent(SysEventType.INFO, "Login Authentication", "SSO ID : " + splittedArray[1], null);
                    return splittedArray[1].Trim();
                }
                AuditLog.LogEvent(SysEventType.INFO, "Login Authentication", "Failed to split key value pair", null);
            }
            catch(Exception ex)
            {
                AuditLog.LogEvent(SysEventType.ERROR, "Login Authentication", "GetSSOID failed with an exception ", ex);
            }
            return null;
        }

        private bool AuthenticateUser(string argsClientId, string argsUserId, string argsPassword, string argsDatabase)
        {
            var loginPresenter = new UserAuthenticationManager();
            _userContext = loginPresenter.AuthenticateUser(argsClientId, argsUserId, argsPassword,
                argsDatabase);
            if (_userContext == null) return false;
            AuditLog.LogEvent(SysEventType.ERROR, "Session Creating", _userContext.UserProfile.Name, null);

            SessionManagement<UserContext>.SetValue(Constants.UserContextSessionKey, _userContext);

            AuditLog.LogEvent(SysEventType.ERROR, "Session Created", _userContext.UserProfile.Name, null);
            return true;
        }

    }
}