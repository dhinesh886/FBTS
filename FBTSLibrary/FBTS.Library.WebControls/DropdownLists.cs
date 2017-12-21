using System.Web.UI.WebControls; 

namespace FBTS.Library.WebControls
{
    public static class WebControls
    {
        /// 
        /// <param name="ddlList">DropDown List Control</param>
        /// <param name="defaultText">Default Text</param>
        /// <remarks></remarks>
        public static void ClearDdl(DropDownList ddlList, string defaultText)
        {
            ddlList.Items.Clear();
            if (!string.IsNullOrEmpty(defaultText))
            {
                ddlList.Items.Insert(0, new ListItem(defaultText, ""));
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
                if (listitem.Value.ToUpper().Trim() == argsValue.ToUpper().Trim())
                    drpList.SelectedIndex = i;
                i = i + 1;
            }
        }
 
    }
}