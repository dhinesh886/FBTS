using FBTS.App.Library;
using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.Model.Transaction;
using FBTS.View.Resources.ResourceFiles;
using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View.AppPages.CPanel
{
    public partial class FBTS_PartMaster : System.Web.UI.Page
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
                            new KeyValuePairItem(Constants.filter1, Constants.UnitTypes),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextUnit),
                            new KeyValuePairItem(Constants.masterType, Constants.DdlWFComponents)
                        };
            _genericClass.LoadDropDown(ddlUnit, filter, null, UserContext.DataBaseInfo);
            filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter4, Constants.DdlFilterTypes),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextMaterialType),
                            new KeyValuePairItem(Constants.masterType, Constants.DdlMATCategories)
                        };
            _genericClass.LoadDropDown(ddlPartType, filter, null, UserContext.DataBaseInfo);
           
            BindData(BindType.List);
        }
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        public string Action { get { return hidAction.Value; } set { hidAction.Value = value; } }
        public bool DivAction
        {
            set
            {
                divForm.Visible = value;
                divView.Visible = !value;
                uplActions.Update();
            }
        }
        public DateTime CreatedDate
        {
            get { return Dates.ToDateTime(lblCreadet.Text.Trim(), DateFormat.Format_01); }
            set
            {
                if (value == Convert.ToDateTime(Constants.DefaultDate))
                    lblCreadet.Text = string.Empty;
                else
                    lblCreadet.Text = Dates.FormatDate(value, Constants.Format02);
            }
        }
        public string Code { get { return txtPartNo.Text.Trim().ToUpper(); } set { txtPartNo.Text = value.Trim(); } }
        public string Description { get { return txtDesp.Text.Trim(); } set { txtDesp.Text = value.Trim(); } }
        public string DetailedDescription { get { return txtDDesp.Text.Trim(); } set { txtDDesp.Text = value.Trim(); } }
        public decimal SalesPrice
        {
            get { return txtSalesPrice.Text.Trim().ToDecimal(2); }
            set
            {
                if (value == 0)
                    txtSalesPrice.Text = string.Empty;
                else
                    txtSalesPrice.Text = value.ToString(Constants.DecimalFormate);
            }
        }
        public string PartSearch { get { return txtSearch.Text.Trim(); } set { txtSearch.Text = value.Trim(); } }
        public string Unit { get { return ddlUnit.SelectedValue; } set { ddlUnit.SelectedValue = value.Trim(); } }
        //public string Type { get { return hidType.Value.Trim(); } set { hidType.Value = value.Trim(); } }
        public string PartType { get { return ddlPartType.SelectedValue.Trim(); } set { ddlPartType.SelectedValue = value.Trim(); } }
        public string PartGroup { get { return ddlPartGroup.SelectedValue.Trim(); } set { ddlPartGroup.SelectedValue = value.Trim(); } }
        public DateTime PriceValideDate { get {return Dates.ToDateTime(txtPriceValidDate.Text.Trim(), DateFormat.Format_01); } set { txtPriceValidDate.Text = Dates.FormatDate(value, Constants.Format02); } }
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            clearForm();
            DivAction = true;
        }
        public void BindData(BindType bindType)
        {
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = bindType == BindType.List ? PartSearch : Code,
                filter1 = string.Empty,
                filter2 = PartType,
                filter3 = PartGroup,
                filter4 = bindType == BindType.List ? Constants.RetriveList : Constants.RetriveForm,
                FilterKey = Constants.TableMaterials
            };
            var parts = _controlPanel.GetMaterials(queryArgument);
            if (bindType == BindType.List)
            {
               // Datatable = parts;
                BindData(parts);
               // BindData();
            }
            else
            {
                var firstOrDefault = parts.FirstOrDefault();
                if (firstOrDefault == null) return;
                Description = firstOrDefault.Description;
                DetailedDescription = firstOrDefault.DetailedDescription;
                Unit = firstOrDefault.Unit;
                SalesPrice = firstOrDefault.SalesPrice;
                CreatedDate = firstOrDefault.Created;
                PartType = firstOrDefault.MaterialType.Id;
                fillPartGroupDdl();
                PartGroup = firstOrDefault.MaterialGroup.Id;
                PriceValideDate = firstOrDefault.PriceValidDate;
                uplForm.Update();
            }
        }
        private void BindData(Materials parts)
        //private void BindData()
        {
            GridViewTable.DataSource =  parts;
            if (_newPageIndex >= 0)
            {
                GridViewTable.PageIndex = _newPageIndex;
            }
            GridViewTable.DataSource =  parts;
            GridViewTable.DataBind();
            uplView.Update();
        }
        //public Materials Datatable
        //{
        //    get
        //    {
        //        return XmlUtilities.ToObject<Materials>(ViewState["Datatable"].ToString());
        //    }
        //    set
        //    {
        //        ViewState["Datatable"] = value.ToXml();
        //    }
        //}
        protected void LoadDetails(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            clearForm();
            DivAction = true;
            Action = lnkbtn.CommandName;
            Code = lnkbtn.CommandArgument;            
            BindData(BindType.Form);
        }      
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            var parts = new Materials();
            parts.Add(new Material
            {                
                PartNumber = Code,
                Description = Description,
                DetailedDescription = DetailedDescription,
                MaterialType = new MaterialType { Id = PartType },
                MaterialGroup=new MaterialGroup{Id = PartGroup},
                Unit = Unit,
                SalesPrice = SalesPrice,   
                Created=CreatedDate,
                PriceValidDate=PriceValideDate
            });
           
            var firstOrDefault = parts.FirstOrDefault();
            if (firstOrDefault != null)
            {
                firstOrDefault.DataBaseInfo = UserContext.DataBaseInfo;
                firstOrDefault.Action = Action;
            }
            if (_controlPanel.SetMaterials(parts))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.MaterialSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "MATERIALS SAVED",
                  GlobalCustomResource.MaterialSaved, true);
                DivAction = false;
                clearForm();
                BindData(BindType.List);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.MaterialFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "MATERIALS UPDATE FAILED",
                  GlobalCustomResource.MaterialFailed, true);
            }
        }
        private void clearForm()
        {           
            CreatedDate = DateTime.Now;
            Code = string.Empty;
            Description = string.Empty;
            DetailedDescription = string.Empty;
            SalesPrice = 0;            
            Unit = string.Empty;
            PartType = string.Empty;
            PartGroup = string.Empty;
            Action = Constants.InsertAction;
            PriceValideDate = UserContext.CompanyProfile.FinancialYearEnd.GetValueOrDefault();
            uplForm.Update();
        }
        protected void GridViewTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var gv = sender as GridView;
                var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
                if (gv != null && lbl != null)
                    lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);

                var part = e.Row.DataItem as Material;
                var hidSuspent = e.Row.Cells[0].FindControl("hidSuspent") as HiddenField;
                if (hidSuspent != null)
                    hidSuspent.Value = part.Suspend.ToString();

                var lnkaction = e.Row.Cells[0].FindControl("lnkBan") as LinkButton;
                if (lnkaction != null)
                {
                    if (part.Suspend)
                    {
                        lnkaction.Text = "Include";
                        lnkaction.ToolTip = "Click here to Include";
                    }
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
            }
        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            DivAction = false;
        }

        protected void GridViewTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            _newPageIndex = e.NewPageIndex;
            clearForm();
            BindData(BindType.List);
            //BindData();
        }

        protected void ddlPartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PartType == string.Empty)
                return;
            fillPartGroupDdl();
        }
        private void fillPartGroupDdl()
        {
            var filter = new KeyValuePairItems
                        {                            
                            new KeyValuePairItem(Constants.filter1, PartType),
                            new KeyValuePairItem(Constants.filter4, Constants.DdlFilterGroups),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextMaterialGroup),
                            new KeyValuePairItem(Constants.masterType, Constants.DdlMATCategories)
                        };
            _genericClass.LoadDropDown(ddlPartGroup, filter, null, UserContext.DataBaseInfo);
        }

        protected void lnkBan_Click(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            Action = lnkbtn.CommandName;
            Code = lnkbtn.CommandArgument;

            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            var hidSuspent = ((HiddenField)row.Cells[0].FindControl("hidSuspent")).Value.ToBool();

            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = Code,
                filter1 = hidSuspent ? "false" : "true",
                FilterKey = Constants.TableMaterials
            };
            _controlPanel.Cancle(queryArgument);

            CustomMessageControl.MessageBodyText = GlobalCustomResource.MaterialSaved;
            CustomMessageControl.MessageBodyText = hidSuspent ? GlobalCustomResource.PartIncluded : GlobalCustomResource.PartSuspended;
            CustomMessageControl.MessageType = MessageTypes.Success;
            CustomMessageControl.ShowMessage();
            AuditLog.LogEvent(UserContext, SysEventType.INFO, hidSuspent ? "Part Included" : "Part Suspended",
              hidSuspent ? GlobalCustomResource.PartIncluded : GlobalCustomResource.PartSuspended, true);
            Code = string.Empty;
            BindData(BindType.List);
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindData(BindType.List);
            //var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            //{
            //    Key = PartSearch,
            //    //filter1 = string.Empty,
            //    //filter2 = PartType,
            //    //filter3 = PartGroup,
            //    filter4 = Constants.RetriveList,
            //    FilterKey = Constants.TableMaterials
            //};
            //var parts = _controlPanel.GetMaterials(queryArgument);
            //BindData(parts);
        }
    }
}