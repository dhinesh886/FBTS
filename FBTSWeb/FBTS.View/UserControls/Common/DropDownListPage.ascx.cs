using System;
using System.Web.UI.WebControls;

namespace FBTS.View.UserControls.Common
{
    public partial class DropDownListPage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public DropDownList DropDown
        {
            get { return ddl; }
        }
        public string DropDownValue
        {
            get { return ddl.SelectedValue.Trim(); }
            set { ddl.SelectedValue = value.Trim(); }
        }
        public string DropDownText
        {
            get { return ddl.SelectedItem.Text.Trim(); }
        }
        public string DropDownLabel
        {
            set { lbl.Text = value.Trim(); }
        }
        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}