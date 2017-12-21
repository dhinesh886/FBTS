using FBTS.App.Library;
using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.View.Resources.ResourceFiles;
using System;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

namespace FBTS.View.AppPages.CPanel
{
    public partial class FBTS_AccessLevelSetup : System.Web.UI.Page
    {
        public readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        private readonly GenericManager _genericClass = new GenericManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            SetPageProperties();
            BindData(BindType.List);
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
        public string Action { get { return hidAction.Value; } set { hidAction.Value = value; } }
        public string SelectedAccessLevel { get { return hidKey.Value.Trim(); } set { hidKey.Value = value; } }
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            var designations = new Designations();
            lnkAddNew.Enabled = false;
            lnkAddNew.Style.Add("cursor", "Not-allowed");            
            designations.AddRange(from GridViewRow gvRow in GridViewTable.Rows
                                  select new Designation
                                  {
                                      Id = ((TextBox)gvRow.FindControl("txtCode")).Text,
                                      Description = ((TextBox)gvRow.FindControl("txtDescription")).Text,
                                      Level = ((TextBox)gvRow.FindControl("txtAccess")).Text,
                                      Action = Constants.UpdateAction
                                  });
            designations.Add(new Designation
            {
                Action = Constants.InsertAction
            });
            BindAccessLevel(designations);
        }
        public void BindData(BindType bindType)
        {
            if (bindType == BindType.Form)
            {
                var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    Key = SelectedAccessLevel                    
                };
                var menus = _controlPanel.GetMenuAccessRights(queryArgument);
                BindMenuRights(menus);
            }
            else
            {
                var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {                    
                    filter1 = Constants.SysDesignationCType,
                    filter2 = Constants.SysDesignation,
                    filter4 = bindType == BindType.List ? Constants.RetriveList : Constants.RetriveForm,
                    FilterKey = Constants.TableMCatDetls
                };
                var designations = _controlPanel.GetDesignations(queryArgument);
                BindAccessLevel(designations);
            }
        }
        public void BindAccessLevel(Designations designations)
        {
            GridViewTable.DataSource = designations;
            GridViewTable.DataBind();
            UplView.Update();
        }
        public void BindMenuRights(Menus menus)
        {
            gvMenuRights.DataSource = menus;
            gvMenuRights.DataBind();
            uplForm.Update();
        }
        protected void lnkSaveDesignation_Click(object sender, EventArgs e)
        {   
            var designations = new Designations();
            foreach (GridViewRow gvRow in GridViewTable.Rows)
            {
                designations.Add(new Designation
                {
                    Id = ((TextBox)gvRow.FindControl("txtCode")).Text,
                    Description = ((TextBox)gvRow.FindControl("txtDescription")).Text,
                    Level = ((TextBox)gvRow.FindControl("txtAccess")).Text,
                    Action = (string.IsNullOrEmpty((((HiddenField)gvRow.FindControl("hdnAction")).Value)) ?
                                                    Constants.UpdateAction : ((HiddenField)gvRow.FindControl("hdnAction")).Value),
                    SlNo = ((HiddenField)gvRow.FindControl("hdnSno")).Value,
                    CreatedDate = Dates.ToDateTime(((HiddenField)gvRow.FindControl("hidCreatedDate")).Value, DateFormat.Format_05)
                });
            }
                var firstOrDefault = designations.FirstOrDefault();
                if (firstOrDefault != null)
                    firstOrDefault.DataBaseInfo = UserContext.DataBaseInfo;

                if (_controlPanel.SetDesignation(designations))            
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.DesignationSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "DESIGNATION SAVED",
                  GlobalCustomResource.DesignationSaved, true);
                lnkAddNew.Enabled = true;
                lnkAddNew.Style.Add("cursor", "Pointer");
                BindData(BindType.List);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.DesignationFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "DESIGNATION UPDATE FAILED",
                  GlobalCustomResource.DesignationFailed, true);
            }
        }
      
        protected void GridViewTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var gv = sender as GridView;
                var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
                if (gv != null && lbl != null)
                    lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);
                var designation = e.Row.DataItem as Designation;
                if (designation != null)
                {
                    var textBox = e.Row.Cells[0].FindControl("txtCode") as TextBox;
                    if (textBox != null)
                    {
                        textBox.Text = designation.Id;
                        if (designation.Action == Constants.UpdateAction ||
                        string.IsNullOrEmpty(designation.Action))
                        {
                            textBox.Attributes.Add("readonly", "true");
                        }
                    }
                    var box = e.Row.Cells[0].FindControl("txtDescription") as TextBox;
                    if (box != null)
                        box.Text = designation.Description;
                    var level = e.Row.Cells[0].FindControl("txtAccess") as TextBox;
                    if (level != null)
                        level.Text = designation.Level;
                    var hiddenField = e.Row.Cells[0].FindControl("hdnAction") as HiddenField;
                    if (hiddenField != null)
                        hiddenField.Value = designation.Action;
                    var field = e.Row.Cells[0].FindControl("hdnSno") as HiddenField;
                    if (field != null)
                        field.Value = designation.SlNo;
                    var createdDate = e.Row.Cells[0].FindControl("hidCreatedDate") as HiddenField;
                    if (field != null)
                        createdDate.Value = designation.CreatedDate.ToString();

                }
            }
        }
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            var menus = new Menus();
            
            foreach (GridViewRow gvRow in gvMenuRights.Rows)
            {
                var checkBox = gvRow.FindControl("chkSelect") as CheckBox;
                if (checkBox != null && checkBox.Checked)
                {
                    menus.Add(new FBTS.Model.Control.Menu
                    {
                        MenuCode = ((HiddenField)gvRow.FindControl("hidMenuCode")).Value,
                        MenuOrder = Convert.ToDecimal(((TextBox)gvRow.FindControl("txtLevel")).Text)
                    });
                }
            }
            var menuAccessRights = new MenuAccessRights
            {
                AccessLevelId = SelectedAccessLevel,
                AccessRights = menus,
                DataBaseInfo = UserContext.DataBaseInfo
            };
            if (_controlPanel.SetMenuAccessRights(menuAccessRights))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.AccessRightSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "ACCESS RIGHT SAVED",
                  GlobalCustomResource.AccessRightSaved, true);
                lnkAddNew.Enabled = true;
                lnkAddNew.Style.Add("cursor", "Pointer");
                BindData(BindType.List);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.AccessRightFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "ACCESS RIGHT UPDATE FAILED",
                  GlobalCustomResource.AccessRightFailed, true);
            } 
        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
             DivAction = false;
        }
        private void SetPageProperties()
        {
            var menuSettings = GeneralUtilities.GetCurrentMenuSettings(UserContext,
                QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            if (menuSettings != null)
            {
                pageTitle.InnerText = menuSettings.PageTitle;
            }
        }

        protected void gvMenuRights_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var gv = sender as GridView;
                var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
                if (gv != null && lbl != null)
                    lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);

                var menu = e.Row.DataItem as FBTS.Model.Control.Menu;
                if (menu != null)
                {
                    var textBox = e.Row.Cells[0].FindControl("txtMenu") as TextBox;
                    if (textBox != null)
                        textBox.Text = menu.MenuName;
                    var box = e.Row.Cells[0].FindControl("txtLevel") as TextBox;
                    if (box != null)
                        box.Text = menu.MenuOrder.ToString(CultureInfo.InvariantCulture);
                    var hiddenField1 = e.Row.Cells[0].FindControl("hidMenuCode") as HiddenField;
                    if (hiddenField1 != null)
                        hiddenField1.Value = menu.MenuCode;
                    var chkSelect = e.Row.Cells[0].FindControl("chkSelect") as CheckBox;
                    if (chkSelect != null)
                        chkSelect.Checked = menu.MenuAvailable;
                }
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            DivAction = true;
            Action = Constants.UpdateAction;
            lblAccessLevel.Text = lnkbtn.CommandName;
            SelectedAccessLevel = lnkbtn.CommandArgument;
            BindData(BindType.Form);
        }
    }
}