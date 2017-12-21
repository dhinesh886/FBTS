using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace FBTS.View.UserControls.Common
{
    public partial class Categorie : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtSearch.Attributes.Add("onkeyup", "Search" + this.ClientID + "(this);");
            Chk.Attributes.Add("onclick", "sortCheckBoxList" + this.ClientID + "(this);");  
        }
        public bool IsSearch
        {
            set { divSearch.Visible = value; }
        }
        public RepeatDirection repeatDirection
        {
            set
            {
                Chk.RepeatDirection = value;
            }
        }

     
        public List<ListItem> CategoriMode
        {
            get
            {
                List<ListItem> chkList = new List<ListItem>();

                for (int i = 0; i < Chk.Items.Count; i++)
                {
                    ListItem list = new ListItem();
                    list.Text = Chk.Items[i].Text.Trim();
                    list.Value = Chk.Items[i].Value.Trim();
                    list.Selected = Chk.Items[i].Selected;
                    chkList.Add(list);
                }
                return chkList;
            }
            set
            {
                var listOfItems = value;
                Chk.Items.Clear();
                foreach (var list in listOfItems)
                {
                    Chk.Items.Add(list);
                }
                uplComp.Update();
            }
        }
        public string Header
        {
            get { return header.InnerText.Trim(); }
            set { header.InnerText = value.Trim(); uplComp.Update(); }
        }
        public Boolean setAutopostBack
        {
            set
            {
                Chk.AutoPostBack = value;
                uplComp.Update();
            }
        }
        public delegate void OnSelectedIndexChanged(object sender, EventArgs e);

        public event OnSelectedIndexChanged chkClick;
        protected void Chk_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkClick != null)
                chkClick(sender, e);
            uplComp.Update();
        }      
    }
}