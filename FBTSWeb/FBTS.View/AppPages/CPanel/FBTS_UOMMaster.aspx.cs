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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ezy.ERP.View.AppPages
{
    public partial class Ezy_UOMMaster : System.Web.UI.Page
    {
        public readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        private readonly GenericManager _genericClass = new GenericManager();
        //private int _newPageIndex = -1;

        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            SetPageProperties();
            BindData();
        }

        public void BindData()
        {
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {                
                filter1 = Constants.UnitTypes,
                filter4 = Constants.RetriveList,
                FilterKey = Constants.TableWFComponents
            };
            var units = _controlPanel.GetUOM(queryArgument);
            BindData(units);
        }

        public void BindData(Units units)
        {
            GridViewTable.DataSource = units;
            GridViewTable.DataBind();
            uplView.Update();
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            var units = new Units();
            lnkAddNew.Enabled = true;
            lnkAddNew.Style.Add("cursor", "Pointer");
            foreach (GridViewRow gvRow in GridViewTable.Rows)
            {
                units.Add(new FBTS.Model.Control.Unit
                {
                    Id = ((TextBox)gvRow.FindControl("txtPrimary")).Text.Trim().ToUpper(),
                    Description = ((TextBox)gvRow.FindControl("txtSecondary")).Text.Trim().ToUpper(),
                    Suspend = ((HiddenField)gvRow.FindControl("hidSuspend")).Value.Trim().ToBool(),
                    Action = (string.IsNullOrEmpty((((HiddenField)gvRow.FindControl("hdnAction")).Value)) ? Constants.UpdateAction : ((HiddenField)gvRow.FindControl("hdnAction")).Value),
                });
            }
            var firstOrDefault = units.FirstOrDefault();
            if (firstOrDefault != null)
                firstOrDefault.DataBaseInfo = UserContext.DataBaseInfo;

            if (_controlPanel.SetUom(units))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.UOMSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "UOM SAVED",
                  GlobalCustomResource.UOMSaved, true);
                lnkAddNew.Enabled = true;
                lnkAddNew.Style.Add("cursor", "Pointer");
                BindData();
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.UOMFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "UOM UPDATE FAILED",
                  GlobalCustomResource.UOMFailed, true);
            }
        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            var units = new Units();
            lnkAddNew.Enabled = false;
            lnkAddNew.Style.Add("cursor", "Not-allowed");
            foreach (GridViewRow gvRow in GridViewTable.Rows)
            {
                units.Add(new FBTS.Model.Control.Unit
                {
                    Id = ((TextBox)gvRow.FindControl("txtPrimary")).Text,
                    Description = ((TextBox)gvRow.FindControl("txtSecondary")).Text,
                    Suspend = ((HiddenField)gvRow.FindControl("hidSuspend")).Value.Trim().ToBool(),
                    Action = Constants.UpdateAction
                });
            }
            units.Add(new FBTS.Model.Control.Unit
            {
                Action = Constants.InsertAction,
                Suspend=false
            });
            BindData(units);
        }

        protected void GridViewTable_OnRowDataBoound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var gv = sender as GridView;
            var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
            if (gv != null && lbl != null)
                lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);
            var unit = e.Row.DataItem as FBTS.Model.Control.Unit;
            if (unit == null) return;
            var textBox = e.Row.Cells[0].FindControl("txtPrimary") as TextBox;
            if (textBox != null)
            {
                textBox.Text = unit.Id;
                if (unit.Action == Constants.UpdateAction || string.IsNullOrEmpty(unit.Action))
                {
                    textBox.Attributes.Add("readonly", "true");
                }
            }
            var box = e.Row.Cells[0].FindControl("txtSecondary") as TextBox;
            if (box != null)
                box.Text = unit.Description;
            
            var hiddenField = e.Row.Cells[0].FindControl("hdnAction") as HiddenField;
            if (hiddenField != null)
                hiddenField.Value = unit.Action;

            var hidSuspent = e.Row.Cells[0].FindControl("hidSuspend") as HiddenField;
            if (hidSuspent != null)
                hidSuspent.Value = unit.Suspend.ToString();

            var lnkaction = e.Row.Cells[0].FindControl("lnkBan") as LinkButton;
            if (lnkaction != null)
            {
                if (unit.Suspend)
                {
                    lnkaction.Text = "Include";
                    lnkaction.ToolTip = "Click here to Include";
                }
            }
        }

        private void SetPageProperties()
        {
            var menuSettings = GeneralUtilities.GetCurrentMenuSettings(UserContext,
                QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            if (menuSettings != null)
            {
                //pageTitle.InnerText = menuSettings.PageTitle;
            }
        }

        protected void lnkBan_Click(object sender, EventArgs e)
        {
            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            var id = ((TextBox)row.FindControl("txtPrimary")).Text.Trim();
            if (id == string.Empty) return;
            var units = new Units();
            var hidSuspend = ((HiddenField)row.Cells[0].FindControl("hidSuspend")).Value.ToBool();
            units.Add(new FBTS.Model.Control.Unit
            {
                //Type = Type,
                Id = ((TextBox)row.FindControl("txtPrimary")).Text.Trim().ToUpper(),
                Description = ((TextBox)row.FindControl("txtSecondary")).Text,
                Suspend = !hidSuspend,
                Action = Constants.UpdateAction,
            });

            var firstOrDefault = units.FirstOrDefault();
            if (firstOrDefault != null)
                firstOrDefault.DataBaseInfo = UserContext.DataBaseInfo;


            if (_controlPanel.SetUom(units))
            {
                CustomMessageControl.MessageBodyText = hidSuspend ? GlobalCustomResource.UOMIncluded : GlobalCustomResource.UOMSuspended;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, hidSuspend ? "UOM Included" : "UOM Suspended",
                  hidSuspend ? GlobalCustomResource.UOMIncluded : GlobalCustomResource.UOMSuspended, true);
                lnkAddNew.Enabled = true;
                lnkAddNew.Style.Add("cursor", "Pointer");
                BindData();
            }
            else
            {
                CustomMessageControl.MessageBodyText = hidSuspend ? GlobalCustomResource.UOMIncludedFailed : GlobalCustomResource.UOMSuspendedFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, hidSuspend ? "UOM Included FAILED" : "UOM Suspended FAILED",
                   hidSuspend ? GlobalCustomResource.UOMIncludedFailed : GlobalCustomResource.UOMSuspendedFailed, true);
            }
        }
    }
}