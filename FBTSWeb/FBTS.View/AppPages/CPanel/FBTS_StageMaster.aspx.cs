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
    public partial class FBTS_StageMaster : System.Web.UI.Page
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
                            new KeyValuePairItem(Constants.filter1,Constants.DvTeam),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlWFComponents)
                        };
            Teams = _genericClass.LoadList(filter, null, UserContext.DataBaseInfo);
            if (UserContext.UserProfile.Designation.Id.Trim() == Constants.AdminDesignationId)            
                IsEnableAddButton = true;
            else
                IsEnableAddButton = false;

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
        public string Type { get { return hidType.Value.Trim(); } set { hidType.Value = value.Trim(); } }
        public string StageId { get { return txtStageId.Text.Trim(); } set { txtStageId.Text = value.Trim(); } }
        public string StageName { get { return txtStageName.Text.Trim(); } set { txtStageName.Text = value.Trim(); } }
        public string Link { get { return txtLink.Text.Trim(); } set { txtLink.Text = value.Trim(); } }
        public string SubLink { get { return ddlSunLink.SelectedValue.Trim(); } set { ddlSunLink.SelectedValue = value.Trim(); } }
        public bool IsEnableAddButton
        {
            set
            {
                lnkAddNew.Enabled = value;
                
                if (value)
                    lnkAddNew.Style.Add("cursor", "Pointer"); 
                else
                    lnkAddNew.Style.Add("cursor", "Not-allowed");                     
            }
        }
        public bool IsEnableLinkText
        {
            set
            {
                txtLink.Enabled = value;
            }
        }
        public bool IsEnableSubLink
        {
            set
            {
                ddlSunLink.Enabled = value;
            }
        }
        public bool IsVisiableReferenceAddButton
        {
            set
            {
                if (value)
                    divRefAdd.Style.Add("display", "block");
                else
                    divRefAdd.Style.Add("display", "none");
                uplForm.Update();
            }
        }
        public void IsEnableCell(int index,bool value)
        {
            GVReferance.Columns[index].Visible = value;
        }
        public List<ListItem> Teams
        {
            get { return TeamId.CategoriMode; }
            set
            {
                TeamId.CategoriMode = value;
                TeamId.Header = "Teams";
                uplForm.Update();
            }
        }
        public string LabelName
        {
            set { txtLabelName.Text = value.Trim(); }
            get { return txtLabelName.Text.Trim(); }            
        }
        public string TextOrDate
        {
            get { return ddlTextorDate.SelectedValue.Trim(); }
        }
        public WFComponentSubs ReferencesData
        {
            get
            {
                WFComponentSubs references = new WFComponentSubs();
                foreach(GridViewRow gvRow in GVReferance.Rows)
                {
                    references.Add(new WFComponentSub
                    {
                        WFCType=Type,
                        WFCCode=StageId,
                        WFCSCode = ((Label)gvRow.FindControl("lblRowIndex")).Text.Trim(),
                        WFCDesp=gvRow.Cells[1].Text.Trim(),
                        Relation1 = gvRow.Cells[2].Text.Trim(),
                    });
                }
                return references;
            }
            set
            {
                GVReferance.DataSource = value.ToList();
                GVReferance.DataBind();
                uplForm.Update();
            }
        }
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            DivAction = true;
            ClearForm();
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = Constants.Team,
                FilterKey = Constants.StageGenrateNumberType
            };
            StageId = _genericClass.GetNewMasterNumber(queryArgument);
            Action = Constants.InsertAction;
            uplForm.Update();
        }
        public void BindData(BindType bindType)
        {
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = StageId.ToString(),
                filter1 = Type,
                filter4 = bindType == BindType.List ? Constants.RetriveList : Constants.RetriveForm,
                FilterKey = Constants.TableWFComponents
            };
            var stageMasters = _controlPanel.GetStages(queryArgument);
            if (stageMasters != null)
            {
                if (bindType == BindType.Form)
                {
                    var firstOrDefault = stageMasters.FirstOrDefault();
                    if (firstOrDefault == null) return;
                    StageId = firstOrDefault.Id;
                    StageName = firstOrDefault.Stage;
                    Link = firstOrDefault.ActionLink;
                    Teams = WebControls.SetCheckboxListSelectedItem(Teams, firstOrDefault.Relation1.SplitTo<string>(new string[] { Constants.DelimeterSinglePipe }).ToList());
                    ReferencesData = firstOrDefault.Referances.FirstOrDefault().WFCDesp == null ? new WFComponentSubs() : firstOrDefault.Referances;
                    SubLink = firstOrDefault.Relation2;
                    uplForm.Update();
                }
                else
                {
                    GridViewTable.DataSource = stageMasters;
                    if (_newPageIndex >= 0)
                    {
                        GridViewTable.PageIndex = _newPageIndex;
                    }
                    GridViewTable.DataSource = stageMasters;
                    GridViewTable.DataBind();
                    UplView.Update();
                }
            }
        }
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            string relation1 = Constants.DelimeterSinglePipe + Teams.Where(x => x.Selected).Select(a => a.Value).ToList().ToCharSeperatedString(Constants.DelimeterSinglePipe);
            DataViewSetupInfo stageMasters = new DataViewSetupInfo();
            stageMasters.Add(new DataViewSetup
            {
                DataType = Type,
                Id = StageId,
                Stage = StageName,
                Relation1 = relation1,
                Relation2=SubLink,
                ActionLink = Link,
                Suspend = false,
                Referances=ReferencesData,
                DataBaseInfo = UserContext.DataBaseInfo,
                Action = Action
            });
            if (_controlPanel.SetStages(stageMasters))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.StageMasterSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Stage MASTER SAVED",
                  GlobalCustomResource.StageMasterSaved, true);
                ClearForm();
                DivAction = false;
                BindData(BindType.List);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.StageMasterFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Stage MASTER UPDATE FAILED",
                  GlobalCustomResource.StageMasterFailed, true);

            }
        }
        private void ClearForm()
        {
            StageId = string.Empty;
            StageName = string.Empty;
            Link = string.Empty;
            Teams = WebControls.SetCheckboxListSelectedItem(Teams, null);
            ReferencesData = new WFComponentSubs();
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            ClearForm();
            Action = lnkbtn.CommandName;
            StageId = lnkbtn.CommandArgument;
            if (UserContext.UserProfile.Designation.Id.Trim() == Constants.AdminDesignationId)
            {
                IsEnableLinkText = true;
                IsEnableSubLink = true;
                IsVisiableReferenceAddButton = true;
                IsEnableCell(3, true);
            }
            else
            {
                IsEnableLinkText = false;
                IsEnableSubLink = false;
                IsVisiableReferenceAddButton = false;
                IsEnableCell(3, false);
            }
            BindData(BindType.Form);
            DivAction = true;
        }
        protected void GridViewTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var gv = sender as GridView;
            var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
            if (gv != null && lbl != null)
                lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);
        }
        protected void GridViewTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            _newPageIndex = e.NewPageIndex;
            BindData(BindType.List);
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            DivAction = false;
        }

        protected void lnkAddRef_Click(object sender, EventArgs e)
        {
            var references = ReferencesData;
            references.Add(new WFComponentSub
            {
                WFCDesp = LabelName,
                Relation1 = TextOrDate
            });
            ReferencesData = references;
            LabelName = string.Empty;
            ddlTextorDate.SelectedIndex = 0;
        }

        protected void GVReferance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var gv = sender as GridView;
            var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
            if (gv != null && lbl != null)
                lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            var srno = ((Label)row.FindControl("lblRowIndex")).Text.Trim();
            
            var references = new WFComponentSubs();
            foreach (var reference in ReferencesData.ToList())
            {
                if (!(reference.WFCSCode.Trim() == srno))
                    references.Add(reference);
            }

            ReferencesData = references;
        }
    }
}