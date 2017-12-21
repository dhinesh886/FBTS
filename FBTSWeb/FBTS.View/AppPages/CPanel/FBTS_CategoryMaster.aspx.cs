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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View.AppPages.CPanel
{
    public partial class FBTS_CategoryMaster : System.Web.UI.Page
    {
        private readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            SetPageProperties();
            BindData(null);
        }

        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        public string FRType { get { return hidType.Value.Trim(); } set { hidType.Value = value.Trim(); } }
        public void BindData(Categories categories)
        {
            if (categories == null)
            {
                var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    filter1=Constants.PartType,
                    filter2 = FRType,
                    FilterKey = Constants.TableMCatDetls
                };
                categories = _controlPanel.GetCategories(queryArgument);
            }
            GridViewTable.DataSource = categories;
            GridViewTable.DataBind();
            uplView.Update();
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            var categories = new Categories();
            lnkAddNew.Enabled = true;
            lnkAddNew.Style.Add("cursor", "Pointer");

            foreach (GridViewRow gvRow in GridViewTable.Rows)
            {
                categories.Add(new Category
                {
                    CatType = Constants.PartType,
                    CatCode = FRType,
                    ID = ((TextBox)gvRow.FindControl("txtID")).Text.ToUpper(),
                    Description = ((TextBox)gvRow.FindControl("txtDesp")).Text,
                    CreatedDate = Dates.ToDateTime(((HiddenField)gvRow.FindControl("hidCreatedDate")).Value.Trim(), DateFormat.Format_01),
                    Action = (string.IsNullOrEmpty((((HiddenField)gvRow.FindControl("hdnAction")).Value)) ? Constants.UpdateAction : ((HiddenField)gvRow.FindControl("hdnAction")).Value),
                    Suspend = false
                });
            }
            var firstOrDefault = categories.FirstOrDefault();
            if (firstOrDefault != null)
                firstOrDefault.DataBaseInfo = UserContext.DataBaseInfo;
            var controlPanel = new ControlPanelManager();
            if (controlPanel.SetCategory(categories))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.ModalitySaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Category SAVED", GlobalCustomResource.ModalitySaved, true);
                BindData(null);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.ModalityFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Category UPDATE FAILED", GlobalCustomResource.ModalityFailed, true);
            }
        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            var catgories = new Categories();
            lnkAddNew.Enabled = false;
            lnkAddNew.Style.Add("cursor", "Not-allowed");
            catgories.AddRange(from GridViewRow gvRow in GridViewTable.Rows
                               select new Category
                               {
                                   ID = ((TextBox)gvRow.FindControl("txtID")).Text,
                                   Description = ((TextBox)gvRow.FindControl("txtDesp")).Text,
                                   CreatedDate = Dates.ToDateTime(((HiddenField)gvRow.FindControl("hidCreatedDate")).Value.Trim(), DateFormat.Format_01),
                                   Action = Constants.UpdateAction
                               });

            catgories.Add(new Category
            {
                Action = Constants.InsertAction
            });
            BindData(catgories);
        }

        protected void GridViewTable_OnRowDataBoound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var gv = sender as GridView;
            var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
            if (gv != null && lbl != null)
                lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);
            var category = e.Row.DataItem as Category;
            if (category == null) return;
            var textBox = e.Row.Cells[0].FindControl("txtID") as TextBox;
            if (textBox != null)
            {
                textBox.Text = category.ID;
                if ((category.Action == Constants.UpdateAction || string.IsNullOrEmpty(category.Action)))
                {
                    textBox.Attributes.Add("readonly", "true");
                }
            }
            var box = e.Row.Cells[0].FindControl("txtDesp") as TextBox;
            if (box != null)
                box.Text = category.Description;

            var hidCreatedDate = e.Row.Cells[0].FindControl("hidCreatedDate") as HiddenField;
            if (hidCreatedDate != null)
                hidCreatedDate.Value = Convert.ToDateTime(Constants.DefaultDate) == category.CreatedDate ? Dates.FormatDate(UserContext.CurrentDate, Constants.Format02)
                                                                                                           : Dates.FormatDate(category.CreatedDate, Constants.Format02);


            var hiddenField = e.Row.Cells[0].FindControl("hdnAction") as HiddenField;
            if (hiddenField != null)
                hiddenField.Value = category.Action;

            var hidSuspent = e.Row.Cells[0].FindControl("hidSuspent") as HiddenField;
            if (hidSuspent != null)
                hidSuspent.Value = category.Suspend.ToString();

            var lnkaction = e.Row.Cells[0].FindControl("lnkBan") as LinkButton;
            if (lnkaction != null)
            {
                if (category.Suspend)
                {
                    lnkaction.Text = "Include";
                    lnkaction.ToolTip = "Click here to Include";
                }
                if (category.Description.Trim() == string.Empty)
                    lnkaction.Visible = false;
                else
                    lnkaction.Visible = true;
            }
            
        }

        private void SetPageProperties()
        {
            var menuSettings = GeneralUtilities.GetCurrentMenuSettings(UserContext,
                QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            if (menuSettings == null) return;
            pageTitle.InnerText = menuSettings.PageTitle;
            FRType = menuSettings.Type;
        }

        protected void lnkPDF_Click(object sender, EventArgs e)
        {
            ExportData dt = new ExportData();
            dt.VbExportPdf(GridViewTable, "TEST", "TEST");
        }

        protected void lnkBan_Click(object sender, EventArgs e)
        {

            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            var id = ((TextBox)row.FindControl("txtID")).Text.Trim();
            if (id == string.Empty) return;
         
            var hidSuspent = ((HiddenField)row.Cells[0].FindControl("hidSuspent")).Value.ToBool();

            var categories = new Categories();
            categories.Add(new Category
            {
                CatType = Constants.PartType,
                CatCode = FRType,
                ID = ((TextBox)row.FindControl("txtID")).Text.ToUpper(),
                Description = ((TextBox)row.FindControl("txtDesp")).Text,
                CreatedDate = Dates.ToDateTime(((HiddenField)row.FindControl("hidCreatedDate")).Value.Trim(), DateFormat.Format_01),
                Action = Constants.UpdateAction,
                Suspend = !hidSuspent
            });

            var firstOrDefault = categories.FirstOrDefault();
            if (firstOrDefault != null)
                firstOrDefault.DataBaseInfo = UserContext.DataBaseInfo;

            var controlPanel = new ControlPanelManager();
            if (controlPanel.SetCategory(categories))
            {
                CustomMessageControl.MessageBodyText = hidSuspent ? GlobalCustomResource.ModalityIncluded : GlobalCustomResource.ModalitySuspended;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, hidSuspent ? "Modality Included" : "Modality Suspended",
                  hidSuspent ? GlobalCustomResource.ModalityIncluded : GlobalCustomResource.ModalitySuspended, true);
                lnkAddNew.Enabled = true;
                lnkAddNew.Style.Add("cursor", "Pointer");
                BindData(null);
            }
            else
            {
                CustomMessageControl.MessageBodyText = hidSuspent ? GlobalCustomResource.ModalityIncludedFailed : GlobalCustomResource.ModalitySuspendedFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, hidSuspent ? "Modality Included FAILED" : "Modality Suspended FAILED",
                   hidSuspent ? GlobalCustomResource.ModalityIncludedFailed : GlobalCustomResource.ModalitySuspendedFailed, true);
            }
        }
    }
}