namespace FBTS.Library.Common
{
    public static class SecurityUtilities
    {
        public static string HtmlEncode(string inputString)
        {
            return string.IsNullOrEmpty(inputString) ? null : System.Web.HttpUtility.HtmlEncode(inputString);
        }

        public static string HtmlDecode(string inputString)
        {
            return string.IsNullOrEmpty(inputString) ? null : System.Web.HttpUtility.HtmlDecode(inputString);
        }
    }
}