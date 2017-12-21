using System;

namespace FBTS.Library.EventLogger
{
    public class ClientInfo
    { 
        public string ClientApplication { get; set; }
        public string ClientMachineName { get; set; }
        public string ClientIpAddress { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string SessionId { get; set; }

    }
}
