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
    public partial class FBTS_Location : System.Web.UI.Page
    {
        public readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        private readonly GenericManager _genericClass = new GenericManager();
        private Locations _locations;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            SetPageProperties();

            var filter = new KeyValuePairItems
                        {                           
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextRegion),
                            new KeyValuePairItem(Constants.masterType, Constants.DdlRegion)
                        };
            _genericClass.LoadDropDown(ddlRegion, filter, null, UserContext.DataBaseInfo);
            divButtons.Visible = false;
            GridViewTable.Columns[3].Visible = false;
            BindData();
            LocationDetails = _locations;
        }

        public Locations LocationDetails
        {
            get
            {
                if (ViewState["DetailData"] == null) return null;
                return XmlUtilities.ToObject<Locations>(ViewState["DetailData"].ToString());
            }
            set
            {
                ViewState["DetailData"] = value.ToXml();
            }
        }

        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        public string Type { get { return hidType.Value.Trim(); } set { hidType.Value = value.Trim(); } }
        public string Region { get { return ddlRegion.SelectedValue.Trim(); } set { ddlRegion.SelectedValue = value.Trim(); } }
      
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            var locations = new Locations();
            lnkAddNew.Enabled = false;
            lnkAddNew.Style.Add("cursor", "Not-allowed"); 
            foreach (GridViewRow gvRow in GridViewTable.Rows)
            {
                locations.Add(new Location
                {
                    Id = ((TextBox)gvRow.FindControl("txtId")).Text.Trim(),
                    Description = ((TextBox)gvRow.FindControl("txtDescription")).Text.Trim(),
                    Action = Constants.UpdateAction,
                    Suspend = ((HiddenField)gvRow.FindControl("hidSuspent")).Value.Trim().ToBool(),
                });
            }

            locations.Add(new Location
            {
                Created = DateTime.Now,
                Action = Constants.InsertAction,
                Suspend=false,
            });
            BindData(locations);
        }
        public void BindData()
        {            
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                filter1 = Type,
                //filter2 = Constants.LedgerSub,
                filter4 = Constants.RetriveList,
                filter2 = Region,
                FilterKey = Constants.TableAccounts
            };
            _locations = _controlPanel.GetLocation(queryArgument);
            
            BindData(_locations);
        }
        private void BindData(Locations locations)
        {
            GridViewTable.DataSource = locations;
            GridViewTable.DataBind();
            uplView.Update();
        }
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            var locations = new Locations();
            _locations = LocationDetails;
            foreach (GridViewRow gvRow in GridViewTable.Rows)
            {
                locations.Add(new Location
                {
                    Type = Type,
                    Catg1 = Region,
                    Catg2 = ddlRegion.SelectedItem.Text,
                    Id = ((TextBox)gvRow.FindControl("txtId")).Text.Trim().ToUpper(),
                    Description = ((TextBox)gvRow.FindControl("txtDescription")).Text,
                    Parent = Constants.LabelWarehouse,
                    Created=Dates.ToDateTime(((HiddenField)gvRow.FindControl("hidCreatedDate")).Value.Trim(), DateFormat.Format_01),                    
                    Suspend =((HiddenField)gvRow.FindControl("hidSuspent")).Value.Trim().ToBool(),
                    Action = (string.IsNullOrEmpty((((HiddenField)gvRow.FindControl("hdnAction")).Value)) ? Constants.UpdateAction : ((HiddenField)gvRow.FindControl("hdnAction")).Value),
                });
            }
            var firstOrDefault = locations.FirstOrDefault();
            if (firstOrDefault != null)
                firstOrDefault.DataBaseInfo = UserContext.DataBaseInfo;
            

            if (_controlPanel.SetLocation(locations))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.LocationSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "LOCATION SAVED",
                  GlobalCustomResource.LocationSaved, true);
                lnkAddNew.Enabled = true;
                lnkAddNew.Style.Add("cursor", "Pointer");
                BindData();
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.LocationFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "LOCATION UPDATE FAILED",
                  GlobalCustomResource.LocationFailed, true);
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
                var location = e.Row.DataItem as Location;
                if (location == null) return;
                var textBox = e.Row.Cells[0].FindControl("txtId") as TextBox;
                if (textBox != null)
                    textBox.Text = location.Id;
                var box = e.Row.Cells[0].FindControl("txtDescription") as TextBox;
                if (box != null)
                    box.Text = location.Description;
                var hiddenField = e.Row.Cells[0].FindControl("hdnAction") as HiddenField;
                if (hiddenField != null)
                    hiddenField.Value = location.Action;

                var hidCreatedDate = e.Row.Cells[0].FindControl("hidCreatedDate") as HiddenField;
                if (location.Created == Convert.ToDateTime(Constants.DefaultDate))
                    hidCreatedDate.Value = string.Empty;
                else
                    hidCreatedDate.Value = Dates.FormatDate(location.Created, Constants.Format02);

                var hidSuspent = e.Row.Cells[0].FindControl("hidSuspent") as HiddenField;
                if (hidSuspent != null)
                    hidSuspent.Value = location.Suspend.ToString();

                var lnkaction = e.Row.Cells[0].FindControl("lnkBan") as LinkButton;
                if (lnkaction != null)
                {
                    if (location.Suspend)
                    {
                        lnkaction.Text = "Include";
                        lnkaction.ToolTip = "Click here to Include";
                    }
                }

                if ((e.Row.DataItem as Location).Action == Constants.UpdateAction || string.IsNullOrEmpty((e.Row.DataItem as Location).Action))
                {
                    (e.Row.Cells[0].FindControl("txtId") as TextBox).Attributes.Add("readonly", "true");
                }                
            }
        }
        private void SetPageProperties()
        {
            var menuSettings = GeneralUtilities.GetCurrentMenuSettings(UserContext,
                QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            if (menuSettings != null)
            {
                pageTitle.InnerText = menuSettings.PageTitle;
                Type = menuSettings.Type;
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            var id = ((TextBox)row.FindControl("txtID")).Text.Trim();
            if (id == string.Empty) return;
            var locations = new Locations();

            //foreach (GridViewRow gvRow in GridViewTable.Rows)
            //{
            //    locations.Add(new Location
            //    {
            //        Type = Type,
            //        Id = id,
            //        Parent = Constants.LabelWarehouse,
            //        Suspend = false,
            //        Action = Constants.DeleteAction,
            //    });
            //}

            //var firstOrDefault = locations.FirstOrDefault();
            //if (firstOrDefault != null)
            //    firstOrDefault.DataBaseInfo = UserContext.DataBaseInfo;
            //if (_controlPanel.SetLocation(locations))
            //{
            //    CustomMessageControl.MessageBodyText = GlobalCustomResource.LocationSaved;
            //    CustomMessageControl.MessageType = MessageTypes.Success;
            //    CustomMessageControl.ShowMessage();
            //    AuditLog.LogEvent(UserContext, SysEventType.INFO, "LOCATION Deleted",
            //      GlobalCustomResource.LocationSaved, true);
            //    lnkAddNew.Enabled = true;
            //    lnkAddNew.Style.Add("cursor", "Pointer");
            //    BindData();
            //}
            //else
            //{
            //    CustomMessageControl.MessageBodyText = GlobalCustomResource.LocationFailed;
            //    CustomMessageControl.MessageType = MessageTypes.Error;
            //    CustomMessageControl.ShowMessage();
            //    AuditLog.LogEvent(UserContext, SysEventType.INFO, "LOCATION DELETE FAILED",
            //      GlobalCustomResource.LocationFailed, true);
            //}
        }

        protected void lnkBan_Click(object sender, EventArgs e)
        {
            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            var id = ((TextBox)row.FindControl("txtID")).Text.Trim();
            if (id == string.Empty) return;
            var locations = new Locations();
            var hidSuspent = ((HiddenField)row.Cells[0].FindControl("hidSuspent")).Value.ToBool();
            locations.Add(new Location
            {
                Type = Type,
                Catg1 = Region,
                Catg2 = ddlRegion.SelectedItem.Text,
                Id = ((TextBox)row.FindControl("txtId")).Text.Trim().ToUpper(),
                Description = ((TextBox)row.FindControl("txtDescription")).Text,
                Parent = Constants.LabelWarehouse,
                Created = Dates.ToDateTime(((HiddenField)row.FindControl("hidCreatedDate")).Value.Trim(), DateFormat.Format_01),
                Suspend = !hidSuspent,
                Action = Constants.UpdateAction,
            });
          
            var firstOrDefault = locations.FirstOrDefault();
            if (firstOrDefault != null)
                firstOrDefault.DataBaseInfo = UserContext.DataBaseInfo;


            if (_controlPanel.SetLocation(locations))
            {
                CustomMessageControl.MessageBodyText = hidSuspent ? GlobalCustomResource.LocationIncluded : GlobalCustomResource.LocationSuspended;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO,  hidSuspent ?"LOCATION Included": "LOCATION Suspended",
                  hidSuspent ? GlobalCustomResource.LocationIncluded : GlobalCustomResource.LocationSuspended, true);
                lnkAddNew.Enabled = true;
                lnkAddNew.Style.Add("cursor", "Pointer");
                BindData();
            }
            else
            {
                CustomMessageControl.MessageBodyText = hidSuspent ? GlobalCustomResource.LocationIncludedFailed : GlobalCustomResource.LocationSuspendedFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, hidSuspent ? "LOCATION Included FAILED" : "LOCATION Suspended FAILED",
                   hidSuspent ? GlobalCustomResource.LocationIncludedFailed : GlobalCustomResource.LocationSuspendedFailed, true);
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
            if (ddlRegion.SelectedIndex == 0)
            {
                divButtons.Visible = false;
                GridViewTable.Columns[3].Visible = false;
            }
            else
            {
                divButtons.Visible = true;
                GridViewTable.Columns[3].Visible = true;
            }
            uplActions.Update();
        }
    }
}