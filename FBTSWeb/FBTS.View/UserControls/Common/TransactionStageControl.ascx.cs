using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FBTS.View.UserControls.Common
{
    public partial class TransactionStageControl : UserControl
    {
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }

        public string ActiveStage { get { return hidActiveStage.Value.Trim(); } set { hidActiveStage.Value = value; } }
        public string SubLink { get { return hidSubLink.Value.Trim(); } set { hidSubLink.Value = value.Trim(); } }
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                if (Request.QueryString["SubLinkType"] != null)
                    SubLink = Request.QueryString["SubLinkType"].ToString();
                BindStages(UserContext.Stages);
            }
        }
        protected void OnTabChanged(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            ActiveStage = lnkbtn.CommandArgument;
           // Response.Redirect(lnkbtn.PostBackUrl);
            Response.Redirect(lnkbtn.PostBackUrl, false);
            Context.ApplicationInstance.CompleteRequest();
        }
        public void BindStages(DataViewSetupInfo stages)
        {
            if (stages == null) return;
            var frStages = stages.Where(x => x.Relation2.Trim() == SubLink).ToList();
            var tabs = ulTabs.Controls.All()
                        .Where(c => c.ID != null && c.ID.Contains("liTab") &&
                                    Convert.ToInt16(c.ID.Replace("liTab", string.Empty)) > frStages.Count);
            foreach (var tab in tabs)
            { 
                tab.Visible = false;
                
            }
                
            var activeTabs = ulTabs.Controls.All()
                            .Where( c => c.ID != null && c.ID.Contains("lnkTab") &&
                                        Convert.ToInt16(c.ID.Replace("lnkTab", string.Empty)) <= frStages.Count)
                            .OrderBy(x => x.ID);

            var dataIndex = 0;
            var dataArray = frStages.ToArray();
            if (frStages.Where(x => x.Stage.Trim() == ActiveStage).ToList().Count == 0)
            {
                var Actionlnk = frStages.FirstOrDefault().ActionLink.IndexOf('?') == -1 ? dataArray[dataIndex].ActionLink.ToString().Trim() + "?Stage=" + frStages.FirstOrDefault().Stage + "&SubLinkType=" + SubLink
                                                                                                 : dataArray[dataIndex].ActionLink.ToString().Trim() + "&Stage=" + frStages.FirstOrDefault().Stage + "&SubLinkType=" + SubLink;
               /// Response.Redirect(Actionlnk);
                Response.Redirect(Actionlnk, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            foreach (var tab in activeTabs)
            {
                ((LinkButton)tab).CommandArgument = dataArray[dataIndex].Stage;
                ((LinkButton)tab).Text = dataArray[dataIndex].FieldDescription;
                ((LinkButton)tab).PostBackUrl = dataArray[dataIndex].ActionLink.IndexOf('?') == -1 ? dataArray[dataIndex].ActionLink.ToString().Trim() + "?Stage=" + dataArray[dataIndex].Stage + "&SubLinkType=" + SubLink
                                                                                                : dataArray[dataIndex].ActionLink.ToString().Trim() + "&Stage=" + dataArray[dataIndex].Stage + "&SubLinkType=" + SubLink;

                //var request = (HttpWebRequest)WebRequest.Create(dataArray[dataIndex].ActionLink.ToString().Trim());
                //var postData = "Stage=" + dataArray[dataIndex].Stage;
                //var data = Encoding.ASCII.GetBytes(postData);
                //request.Method = "POST";
                //request.ContentType = "application/x-www-form-urlencoded";
                //request.ContentLength = data.Length;
                //using (var stream = request.GetRequestStream())
                //{
                //    stream.Write(data, 0, data.Length);
                //}
                //var response = (HttpWebResponse)request.GetResponse();

                //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //((LinkButton)tab).PostBackUrl = responseString;

                if(ActiveStage == dataArray[dataIndex].Stage.Trim())
                {
                    var li = tab.Parent as HtmlGenericControl;
                    var liClass = li.Attributes["class"].ToString();
                    li.Attributes["class"] = liClass + " active";
                   // ((LinkButton)tab).CssClass = "tab active";
                }
                else
                {
                    var li = tab.Parent as HtmlGenericControl;
                    var liClass = li.Attributes["class"].ToString();
                    li.Attributes["class"] = liClass.Replace(" active", string.Empty);
                }
                dataIndex++;
            } 
        }
    }
}