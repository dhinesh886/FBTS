using System.Web;

namespace FBTS.Library.Statemanagement
{
    public static class QueryStringManagement
    {
        public static string GetValue(string argsKey)
        {
            string msQueryValue = string.Empty;
            try
            {
                if (HttpContext.Current.Request.QueryString[argsKey] != null)
                {
                    msQueryValue = HttpContext.Current.Request.QueryString[argsKey];
                }
            }
            catch
            {
                throw;
            }
            return msQueryValue;
        }
        public static string GetValue(string argsKey, string defaultValue)
        {
            string msQueryValue = string.Empty;
            try
            {
                if (HttpContext.Current.Request.QueryString[argsKey] != null)
                {
                    msQueryValue = HttpContext.Current.Request.QueryString[argsKey];
                }
                else
                {
                    msQueryValue = defaultValue;
                }
            }
            catch
            {
                throw;
            }
            return msQueryValue;
        }
    }
}