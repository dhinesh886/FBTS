using FBTS.App.Library;
using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.View.Resources.ResourceFiles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View.AppPages.CPanel
{
    public partial class FBTS_TeamMaster : System.Web.UI.Page
    {
        public readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        private readonly GenericManager _genericClass = new GenericManager();
        private int _newPageIndex = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));

            SetPageProperties();

            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter4, Constants.DdlFilterTypes),
                            new KeyValuePairItem(Constants.masterType, Constants.DdlMATCategories)
                        };
            Categories = _genericClass.LoadList(filter, null, UserContext.DataBaseInfo);
            filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1,UserContext.UserProfile.Designation.Id.Trim() == "SA"? "":UserContext.UserProfile.Designation.Id.Trim()),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlFilterUser)
                        };
            Users = _genericClass.LoadList(filter, null, UserContext.DataBaseInfo);
            BindData(BindType.List);
        }
        private void SetPageProperties()
        {
            var menuSettings = GeneralUtilities.GetCurrentMenuSettings(UserContext,
                QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            if (menuSettings == null) return;
            pageTitle.InnerText = menuSettings.PageTitle;
            Type = menuSettings.Type;
        }

        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
         public bool DivAction
        {
            set
            {
                divForm.Visible = value;
                divView.Visible = !value;
                uplActions.Update();
            }
        }
         public string Action { get { return hidAction.Value.Trim(); } set { hidAction.Value = value.Trim(); } }
         public string Type
         {
             get { return hidType.Value.Trim(); }
             set { hidType.Value = value.Trim(); }
         }
       
        public string TeamId { get { return txtTeamId.Text.Trim(); } set { txtTeamId.Text = value.Trim(); } }
        public string TeamName { get { return txtTeamName.Text.Trim(); } set { txtTeamName.Text = value.Trim(); } }
        public List<ListItem> Categories
        {
            get { return CategoriControlId.CategoriMode; }
            set
            {
                CategoriControlId.CategoriMode = value;
                CategoriControlId.Header = "Categories";                
                uplForm.Update();
            }
        }
        public List<ListItem> Users
        {
            get { return UserControl.CategoriMode; }
            set
            {
                UserControl.CategoriMode = value;
                UserControl.Header = "Users";
                uplForm.Update();
            }
        }
        
        private void ClearForm()
        {
            TeamId = string.Empty;
            TeamName = string.Empty;
            Categories = WebControls.SetCheckboxListSelectedItem(Categories, null);
            Users = WebControls.SetCheckboxListSelectedItem(Users, null);
        }
        public void BindData(BindType bindType)
        {
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = TeamId.ToString(),
                filter1 = Type,
                filter4 = bindType == BindType.List ? Constants.RetriveList : Constants.RetriveForm,
                FilterKey = Constants.TableWFComponents
            };
            var teamMasters = _controlPanel.GetTeams(queryArgument);
            if (teamMasters != null)
            {
                if (bindType == BindType.Form)
                {                  
                    var firstOrDefault = teamMasters.FirstOrDefault();
                    if (firstOrDefault == null) return;
                    TeamId = firstOrDefault.ComponentId;
                    TeamName = firstOrDefault.ComponentDesp;
                    Categories = WebControls.SetCheckboxListSelectedItem(Categories, firstOrDefault.Relation1.SplitTo<string>(new string[] { Constants.DelimeterSinglePipe }).ToList());

                    Users = WebControls.SetCheckboxListSelectedItem(Users, firstOrDefault.wfComponentSubs.Select(x => x.WFCSCode).ToList());
                    uplForm.Update(); 
                }
                else
                {
                    var team = teamMasters.ToList();
                    if (UserContext.UserProfile.Designation.Id.Trim() != "SA")
                    {
                        if (teamMasters.Any())
                         team = teamMasters.Where(x => x.ComponentId.Trim() != "13").ToList();
                    }
                    GridViewTable.DataSource = team;
                    if (_newPageIndex >= 0)
                    {
                        GridViewTable.PageIndex = _newPageIndex;
                    }
                    GridViewTable.DataSource = team;
                    GridViewTable.DataBind();
                    UplView.Update();
                }
            }
        }
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            string relation1 = Categories.Where(x=>x.Selected).Select(a => a.Value).ToList().ToCharSeperatedString(Constants.DelimeterSinglePipe); 
            WFComponents teamMasters = new WFComponents();
            teamMasters.Add(new WFComponent
            {
                ComponentType = Type,
                ComponentId = TeamId,
                ComponentDesp = TeamName,
                Relation1 = relation1,
                Action=Action,
                DataBaseInfo=UserContext.DataBaseInfo,
            });
            WFComponentSubs wfComponentSubs = new WFComponentSubs();
            wfComponentSubs.AddRange(Users.Where(x => x.Selected).ToList().Select(row => new WFComponentSub
            {
                WFCSCode = row.Value
            }));
            var firstOrDefault = teamMasters.FirstOrDefault();
            if (firstOrDefault != null)
                firstOrDefault.wfComponentSubs = wfComponentSubs;

            if (_controlPanel.SetTeams(teamMasters))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.TeamMasterSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "TEAM MASTER SAVED",
                  GlobalCustomResource.TeamMasterSaved, true);
                ClearForm();
                DivAction = false;
                BindData(BindType.List);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.TeamMasterFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "TEAM MASTER UPDATE FAILED",
                  GlobalCustomResource.TeamMasterFailed, true);
            }
        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            DivAction = false;
        }
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            DivAction = true;
            ClearForm();
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = Constants.Team,
                FilterKey = Constants.TeamGenrateNumberType
            };
            TeamId = _genericClass.GetNewMasterNumber(queryArgument);
            Action = Constants.InsertAction;
            uplForm.Update();
        }

        protected void GridViewTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            _newPageIndex = e.NewPageIndex;
            BindData(BindType.List);
        }

        protected void GridViewTable_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void GridViewTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var gv = sender as GridView;
            var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
            if (gv != null && lbl != null)
                lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            Action = lnkbtn.CommandName;
            TeamId = lnkbtn.CommandArgument;
            BindData(BindType.Form);
            DivAction = true;
        }
    }
}