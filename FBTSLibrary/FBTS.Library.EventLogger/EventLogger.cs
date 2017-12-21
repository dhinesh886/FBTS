using System;

namespace FBTS.Library.EventLogger
{
    public static class EventLogger
    {
        public static void LogEvent(string eventType, string eventName,
            string customMessage, Exception exception)
        {
            //LogInfo logInfo = new LogInfo
            //{
            //    ClientInfo = new ClientInfo
            //    {
            //        ClientApplication = userContext.ClientProfile.ClientApplication,
            //        ClientIpAddress = userContext.ClientProfile.ClientIpAddress,
            //        ClientMachineName = userContext.ClientProfile.ClientMachineName,
            //    },
            //    UserInfo = new UserInfo
            //    {
            //        CompanyId = userContext.CompanyProfile.Id,
            //        CompanyName = userContext.CompanyProfile.Name,
            //        Email = userContext.UserProfile.Email,
            //        UserId = userContext.UserProfile.UCode,
            //        LoginId = userContext.UserProfile.LoginId,
            //    },
            //    Exception = exception,
            //    EventType =  eventType.ToString(),
            //    EventName = eventName,
            //    CustomMessage = customMessage
            //};
            switch (eventType)
            {
                case "DEBUG": 
                    Logger.Logger.Debug(customMessage);
                    break;
                case "ERROR":
                    Logger.Logger.Error(customMessage,exception);
                    break;
                case "FATAL":
                    Logger.Logger.Fatal(customMessage, exception);
                    break;
                case "INFO":
                    Logger.Logger.Info(customMessage);
                    break;
                case "WARN":
                    Logger.Logger.Warn(customMessage);
                    break;
                default:
                    Logger.Logger.Error(customMessage,exception);
                    break;
            }  
             
        } 
    }
}