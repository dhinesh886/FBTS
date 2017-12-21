using System;
using System.Web.UI;
using FBTS.Model.Common;
using FBTS.Model.Control;


namespace FBTS.App.Library
{
    public static class WebPage    {
        public static void ValidatePage(this Page page, UserContext userContext, string menuId)
        {
            if (SecurityUtilities.IsValidMenuId(userContext, menuId)) return;
            AuditLog.LogEvent(SysEventType.ERROR, "UnAuthorized Access found!!",
                "Error:UnAuthorized Access found!!", new Exception("UnAuthorized Access found!!"));
            page.Response.Redirect(Constants.Error404Url, false);
        }
    }
}
