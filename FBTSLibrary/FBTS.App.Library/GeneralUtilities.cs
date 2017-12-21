using System;
using System.Linq;
using FBTS.Library.Common;
using FBTS.Model.Common.XmlObjectModel;
using FBTS.Model.Control;

namespace FBTS.App.Library
{
    public static class GeneralUtilities
    {
        public static Settings GetCurrentMenuSettings(UserContext userContext, string menuId)
        {
            var settings = new Settings();
            var mainMenus = userContext.Menus;
            foreach (var mainMenu in mainMenus)
            {
                var subMenus = mainMenu.SubMenus;
                var firstOrDefault =
                    subMenus.FirstOrDefault(
                        x => String.Equals(x.MenuId, menuId, StringComparison.CurrentCultureIgnoreCase));
                if (firstOrDefault != null)
                {
                    if (!string.IsNullOrEmpty(firstOrDefault.MenuSettings))
                    {
                        settings = XmlUtilities.ToObject<Settings>(firstOrDefault.MenuSettings);
                    }
                    break;
                }
            }
            return settings;
        } 
    }
}