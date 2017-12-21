using System.Web;

namespace FBTS.Library.Statemanagement
{
    public static class SessionManagement<T>
    {
        public static T GetValue(string argsKey)
        {
            T msSessionValue = default(T);
            try
            {
                if (HttpContext.Current.Session[argsKey] != null)
                {
                    msSessionValue = (T) HttpContext.Current.Session[argsKey];
                }
            }
            catch
            {
                throw;
            }

            return msSessionValue;
        }
        public static T GetValue(string argsKey, T defaultValue)
        {
            T msSessionValue = default(T);
            try
            {
                if (HttpContext.Current.Session[argsKey] != null)
                {
                    msSessionValue = (T)HttpContext.Current.Session[argsKey];
                }
                else
                {
                    msSessionValue = defaultValue;
                }
            }
            catch
            {
                throw;
            }

            return msSessionValue;
        }
        public static void SetValue(string argsKey, T argsValue)
        {
            try
            {
                HttpContext.Current.Session[argsKey] = argsValue;
            }
            catch
            {
                throw;
            }
        }

        public static string SessionId()
        {
            return HttpContext.Current.Session.SessionID;
        }
    }
}