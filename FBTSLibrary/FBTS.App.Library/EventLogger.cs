using System;
using FBTS.Library.EventLogger;
using FBTS.Model.Common;
using FBTS.Model.Control;

namespace FBTS.App.Library
{
    public static class AuditLog
    { 
        public static void LogEvent(SysEventType eventType, string eventName, string customMessage,
            Exception exception)
        {
            EventLogger.LogEvent(eventType.ToString(), eventName, customMessage, exception);
        }
        public static void LogEvent( SysEventType eventType, string eventName, string customMessage)
        {
            EventLogger.LogEvent(eventType.ToString(), eventName, customMessage, null);
        }
        public static void LogEvent(UserContext userContext, SysEventType eventType, string eventName,
          string customMessage, Exception exception, bool dbLog)
        {
            if (dbLog)
            {
                var logInfo = new LogInfo
                {
                    ClientInfo = new ClientInfo
                    {
                        ClientApplication = userContext.ClientProfile.ClientApplication,
                        ClientIpAddress = userContext.ClientProfile.ClientIpAddress,
                        ClientMachineName = userContext.ClientProfile.ClientMachineName,
                    },
                    UserInfo = new UserInfo
                    {
                        CompanyId = userContext.CompanyProfile.Id,
                        CompanyName = userContext.CompanyProfile.Name,
                        Email = userContext.UserProfile.Email,
                        UserId = userContext.UserProfile.UCode,
                        LoginId = userContext.UserProfile.LoginId,
                    },
                    Exception = exception,
                    EventType = eventType.ToString(),
                    EventName = eventName,
                    CustomMessage = customMessage
                };

                //semaphore call
                
            }
            EventLogger.LogEvent(eventType.ToString(), eventName, customMessage, exception);
        }
        public static void LogEvent(UserContext userContext, SysEventType eventType, string eventName,
         string customMessage, bool dbLog)
        {
            if (dbLog)
            {
                var logInfo = new LogInfo
                {
                    ClientInfo = new ClientInfo
                    {
                        ClientApplication = userContext.ClientProfile.ClientApplication,
                        ClientIpAddress = userContext.ClientProfile.ClientIpAddress,
                        ClientMachineName = userContext.ClientProfile.ClientMachineName,
                    },
                    UserInfo = new UserInfo
                    {
                        CompanyId = userContext.CompanyProfile.Id,
                        CompanyName = userContext.CompanyProfile.Name,
                        Email = userContext.UserProfile.Email,
                        UserId = userContext.UserProfile.UCode,
                        LoginId = userContext.UserProfile.LoginId,
                    }, 
                    EventType = eventType.ToString(),
                    EventName = eventName,
                    CustomMessage = customMessage
                };

                //semaphore call
                
            }
            EventLogger.LogEvent(eventType.ToString(), eventName, customMessage, null);
        }
    }
}