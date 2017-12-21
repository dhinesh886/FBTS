using System.Net;

namespace FBTS.Library.Common
{
    public static class SystemInfo
    {
        public static ClientInfo GetClientProfile()
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] addr = ipEntry.AddressList;
            return new ClientInfo
            {
                ClientMachineName = Dns.GetHostName(),
                ClientIpAddress = addr[addr.Length - 1].ToString(),
                ClientApplication = "Ezy Control Panel" 
            };
        }
    }
}