 
using FBTS.Data.Manager;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.Library.Common;

namespace FBTS.Business.Manager
{
    public class UserAuthenticationManager
    {
        public UserContext AuthenticateUser(string argsClientId, string argsUserid, string argsPassword,
            string argsDatabase)
        {
            UserContext userContext = null;
            var userAccesControlData = new UserAccessControlManager();
            var loginUserData = userAccesControlData.AuthenticateUser(argsClientId, argsUserid, argsPassword,
                argsDatabase);
            if (loginUserData != null && loginUserData.IsAuthenticated)
            {
                userContext = new UserContext
                {
                    CompanyProfile = loginUserData.CompanyProfile,
                    UserProfile = loginUserData.UserProfile,
                    CurrentDate = loginUserData.CurrentDate
                };
                if (userContext.UserProfile != null)
                {
                    userContext.UserId = userContext.UserProfile.UCode;
                    userContext.DataBaseInfo = new DataBaseInfo
                    {
                        DbServer = loginUserData.UserProfile.DbServer,
                        DbName = loginUserData.UserProfile.DbName,
                        DbPassword = loginUserData.UserProfile.DbPassword,
                        DbUserid = loginUserData.UserProfile.DbUserid
                    };


                    var clientInfo = SystemInfo.GetClientProfile();
                    var clientProfile = new ClientProfile
                    {
                        ClientApplication = clientInfo.ClientApplication,
                        ClientIpAddress = clientInfo.ClientIpAddress,
                        ClientMachineName = clientInfo.ClientMachineName
                    };
                    userContext.ClientProfile = clientProfile;
                }
                if (loginUserData.CompanyProfile != null)
                {
                    userContext.SmtpInfo = new SmtpInfo
                    {
                        SmtpHostIn = loginUserData.CompanyProfile.SmtpHostIn,
                        SmtpHostInPort = loginUserData.CompanyProfile.SmtpHostInPort,
                        SmtpHostOut = loginUserData.CompanyProfile.SmtpHostOut,
                        SmtpHostOutPort = loginUserData.CompanyProfile.SmtpHostOutPort,
                        SmtpPassword = loginUserData.CompanyProfile.SmtpPassword,
                        SmtpUserName = loginUserData.CompanyProfile.SmtpUserName
                    };
                }
                userContext.Menus = loginUserData.Menus;
                var businessManger = new ControlPanelManager(); 
                userContext.Stages = businessManger.GetStagesByUser(userContext.UserId, userContext.DataBaseInfo);
            } 
            return userContext;
        }

        public KeyValuePairItems ValidateUser(string argsClientId, string argsUserid)
        {
            var userAccesControlData = new UserAccessControlManager();
            return userAccesControlData.ValidateUser(argsClientId, argsUserid);
        }
    }
}