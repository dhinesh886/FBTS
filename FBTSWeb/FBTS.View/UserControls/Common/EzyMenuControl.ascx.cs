using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using FBTS.App.Library;
using FBTS.View.Resources.ResourceFiles;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;

namespace FBTS.View.UserControls.Common
{
    public partial class EzyMenuControl : UserControl
    {
        public Menus DataSource { get; set; }
        public void BindMenu()
        {
            if (DataSource != null)
            {
                 menu.InnerHtml = LoadMenus();
            }
            else
            {
                AuditLog.LogEvent(SysEventType.ERROR, "MENU ERROR",
                   "Error : Error occured while binding menu", new Exception("Menu datasource is not supplied/null"));
            }
        }

        private string LoadMenus()
        {
            var menuMarkup = new StringBuilder();
            try
            {
                var menuId = QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString());
                
                var mainMenus = DataSource;
                foreach (var mainMenu in mainMenus)
                {
                    var mainMenuMarkup = new StringBuilder();
                    var subMenuMarkup = new StringBuilder();
                    var subMenus = mainMenu.SubMenus;
                    var activeMenuCode = menuId;
                    var firstOrDefault = subMenus.FirstOrDefault(x => String.Equals(x.MenuId, menuId, StringComparison.CurrentCultureIgnoreCase));
                    if (firstOrDefault != null)
                    {
                        activeMenuCode = firstOrDefault.MenuCode;
                    }
                    // Check whether the current menu is an active parent or not.
                    //0,1 -> activeCSS, 2 -> url or Javascript, 3 -> Menu title, 4 -> SubMenu Markup
                    mainMenuMarkup.AppendFormat(GlobalCustomResource.MainMenu,
                        (mainMenu.MenuCode.Trim() == activeMenuCode.Substring(0, 12)) ? Constants.ActiveMenuUlCss : string.Empty,
                        (mainMenu.MenuCode.Trim() == activeMenuCode.Substring(0, 12)) ? Constants.ActiveMenuAnchorCss : string.Empty,
                        (string.IsNullOrEmpty(mainMenu.MenuUrl) ? Constants.HrefJs : mainMenu.MenuUrl), //Constants.DefaultAppPagesDirectory + mainMenu.MenuUrl),
                        (string.IsNullOrEmpty(mainMenu.MenuUrl) ? GlobalCustomResource.ArrowNode : string.Empty),
                        mainMenu.MenuName,
                        Constants.SubMenuPlaceHolder, mainMenu.MenuIcon);


                    foreach (var menuItem in subMenus)
                    {
                        // Check whether the current menu is an active child or not.
                        subMenuMarkup.AppendFormat(GlobalCustomResource.SubMenu,
                            menuItem.MenuCode.Trim() == activeMenuCode ? Constants.SelectedSubMenuCss : string.Empty,
                            Constants.WebPageRootHtml + menuItem.MenuUrl,
                            menuItem.MenuName);
                    }
                    // add the submenu markup with the main menu markup 
                    mainMenuMarkup.Replace(Constants.SubMenuPlaceHolder,
                        !String.IsNullOrEmpty(subMenuMarkup.ToString())
                            ? string.Format(GlobalCustomResource.SubMenuWrapper, subMenuMarkup)
                            : string.Empty);
                    menuMarkup.Append(mainMenuMarkup);
                }                
            }
            catch(Exception Ex)
            {
                AuditLog.LogEvent(SysEventType.ERROR, "Menu Binding", "Exception on Binding Menu..", Ex);
            }
            return menuMarkup.ToString();
        }
    }
}