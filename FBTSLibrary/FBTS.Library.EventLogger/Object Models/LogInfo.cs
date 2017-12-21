using System;

namespace FBTS.Library.EventLogger
{
    public class LogInfo
    {
        public UserInfo UserInfo { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public Exception Exception { get; set; }
        public string EventType { get; set; }
        public string EventName { get; set; }
        public string CustomMessage { get; set; }

        public LogInfo()
        {
            UserInfo = new UserInfo();
            ClientInfo = new ClientInfo();
        }
    }
}
