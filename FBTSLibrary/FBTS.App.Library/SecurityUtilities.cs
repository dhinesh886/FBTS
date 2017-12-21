using System;
using System.Linq;
using FBTS.Model.Control;

namespace FBTS.App.Library
{
    public static class SecurityUtilities
    {
        public static bool IsValidMenuId(UserContext userContext, string menuId)
        {
            if (String.Equals(menuId, Guid.Empty.ToString(), StringComparison.CurrentCultureIgnoreCase)) return true;
            var mainMenus = (userContext == null ? new Menus(): userContext.Menus);
            return mainMenus.Any(mainMenu => mainMenu.SubMenus.FirstOrDefault(x => String.Equals(x.MenuId, menuId, StringComparison.CurrentCultureIgnoreCase)) != null);
        }
        public static string GetMenuCode(UserContext userContext, string menuId)
        {
            var menuCode = string.Empty;
            var mainMenus = userContext.Menus;
            foreach (var firstOrDefault in mainMenus.Select(mainMenu => mainMenu.SubMenus.FirstOrDefault(
                x => String.Equals(x.MenuId, menuId, StringComparison.CurrentCultureIgnoreCase))).Where(firstOrDefault => firstOrDefault != null))
            {
                menuCode = firstOrDefault.MenuCode;
                break;
            }

            return menuCode;
        }
    }
}