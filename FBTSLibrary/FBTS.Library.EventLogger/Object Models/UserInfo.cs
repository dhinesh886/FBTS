using System;

namespace FBTS.Library.EventLogger
{
    public class UserInfo
    {
        public Guid UserId { get; set; }
        public string LoginId { get; set; }
        public string Email { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string SessionId { get; set; } 
    }
}
