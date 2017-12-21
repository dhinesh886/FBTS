using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace FBTS.Library.Common
{
    public static class WebControls
    {
        public static IEnumerable<Control> All(this ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                foreach (Control grandChild in control.Controls.All())
                    yield return grandChild;

                yield return control;
            }
        }
        /// 
        /// <param name="ddlList">DropDown List Control</param>
        /// <param name="defaultText">Default Text</param>
        /// <remarks></remarks>
        public static void ClearDdl(DropDownList ddlList, string defaultText)
        {
            ddlList.Items.Clear();
            if (!string.IsNullOrEmpty(defaultText))
            {
                ddlList.Items.Insert(0, new ListItem(defaultText, string.Empty));
            }
        }
        /// <summary>
        /// To clear list and insert default value on zeroth position
        /// </summary>
        /// <param name="ddlList"></param>
        /// <param name="defaultText"></param>
        public static void ClearList(ListBox ddlList, string defaultText)
        {
            ddlList.Items.Clear();
            if (!string.IsNullOrEmpty(defaultText))
            {
                ddlList.Items.Insert(0, new ListItem(defaultText, string.Empty));
            }
        } 

        /// <summary>
        ///     To set the given value as the selected value.
        /// </summary>
        /// <param name="drpList">DropDown List Control</param>
        /// <param name="argsValue">Value which needs to set as current selected value</param>
        /// <remarks></remarks>
        public static void SetCurrentComboIndex(DropDownList drpList, string argsValue)
        {
            int i = 0;
            foreach (ListItem listitem in drpList.Items)
            {
                if (listitem.Value.ToUpper().Trim() == argsValue.ToUpper().ToTrimString())
                    drpList.SelectedIndex = i;
                i = i + 1;
            }
        }
        
        public static List<ListItem> SetCheckboxListSelectedItem(List<ListItem> listItems, List<string> selectedItems)
        {
            foreach (var list in listItems.Where(x => x.Selected))
            {
                list.Selected = false;
            }
            if (selectedItems == null || selectedItems.Count == 0)
                return listItems;

            foreach (var item in selectedItems)
            {
                foreach (var list in listItems.Where(x => x.Value.Trim() == item.Trim()))
                { list.Selected = true; }
            }
            return listItems;
        }
    }
}