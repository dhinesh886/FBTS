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
using System.Web.UI.WebControls;


namespace FBTS.View.AppPages.CPanel
{
    public partial class FBTS_ManageMaterialHierarchy : System.Web.UI.Page
    {
        public readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        private readonly GenericManager _genericClass = new GenericManager();
        private int _newPageIndex = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            SetPageProperties();

            fillClassddl();
            fillTypeddl();
        }
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        public string ActiveStep
        {
            get { return hidActiveStep.Value; }
            set
            {
                hidActiveStep.Value = value;
            }
        }
        public string ClassId { get { return txtClassId.Text; } set { txtClassId.Text = value; } }
        public string ClassName { get { return txtClassDescription.Text; } set { txtClassDescription.Text = value; } }
        public string ClassAction { get { return hidClassAction.Value; } set { hidClassAction.Value = value; } }
        public string SelectedClassId { get { return hidClassId.Value; } set { hidClassId.Value = value; } }
        public DateTime ClassCreatedDate
        {
            get
            {
                if (hidClassCreated.Value.Trim() == string.Empty || hidClassCreated.Value == null)
                    return DateTime.Now;
                else
                    return Convert.ToDateTime(hidClassCreated.Value.Trim());
            }
            set { hidClassCreated.Value = value.ToString(); }
        }
        public MaterialHierarchies MaterialClassGridViewDataSource
        {
            set
            {
                gvClass.DataSource = value;
                if (_newPageIndex >= 0)
                {
                    gvClass.PageIndex = _newPageIndex;
                }
                gvClass.DataSource = value;
                gvClass.DataBind();
                uplView.Update();
                ActiveStep = "1";
            }
        }

        public string TypeClassId
        {
            get
            {
                return ddlClassType.SelectedValue.Trim();
            }
            set
            {                
               FBTS.Library.WebControls.WebControls.SetCurrentComboIndex(ddlClassType, value);
            }
        }

        public string TypeId { get { return txtTypeId.Text; } set { txtTypeId.Text = value; } }
        public string TypeName { get { return txtTypeDescription.Text; } set { txtTypeDescription.Text = value; } }
        public string TypeAction { get { return hidTypeAction.Value; } set { hidTypeAction.Value = value; } }
        public string SelectedTypeId { get { return hidTypeId.Value; } set { hidTypeId.Value = value; } }
        public DateTime TypeCreatedDate
        {
            get
            {
                if (hidTypeCreated.Value.Trim() == string.Empty || hidTypeCreated.Value == null)
                    return DateTime.Now;
                else
                    return Convert.ToDateTime(hidTypeCreated.Value.Trim());
            }
            set { hidTypeCreated.Value = value.ToString(); }
        }
        public MaterialHierarchies MaterialTypesGridViewDataSource
        {
            set
            {
                gvTypes.DataSource = value;
                if (_newPageIndex >= 0)
                {
                    gvTypes.PageIndex = _newPageIndex;
                }
                gvTypes.DataBind();
                uplView.Update();
                ActiveStep = "2";
            }
        }
        public string GroupClassId
        {
            get
            {
                return ddlClassGroups.SelectedValue.Trim();
            }
            set
            {
                FBTS.Library.WebControls.WebControls.SetCurrentComboIndex(ddlClassGroups, value);
            }
        }

        public string GroupTypeId
        {
            get
            {
                return ddlTypeGroups.SelectedValue.Trim();
            }
            set
            {
                FBTS.Library.WebControls.WebControls.SetCurrentComboIndex(ddlTypeGroups, value);
            }
        }

        public string GroupId { get { return txtGroupId.Text; } set { txtGroupId.Text = value; } }
        public string GroupName { get { return txtGroupDescription.Text; } set { txtGroupDescription.Text = value; } }
        //public decimal Margin
        //{
        //    get { return txtMargin.Text.Trim().ToDecimal(2); }
        //    set
        //    {
        //        if (value == 0)
        //            txtMargin.Text = string.Empty;
        //        else
        //            txtMargin.Text = value.ToString(Constants.DecimalFormate);
        //    }
        //}
        public string GroupAction { get { return hidGroupAction.Value; } set { hidGroupAction.Value = value; } }
        public string SelectedGroupId { get { return hidGroupId.Value; } set { hidGroupId.Value = value; } }       
        public DateTime GroupCreatedDate
        {
            get
            {
                if (hidGroupCreated.Value.Trim() == string.Empty || hidGroupCreated.Value == null)
                    return DateTime.Now;
                else
                    return Convert.ToDateTime(hidGroupCreated.Value.Trim());

            }
            set { hidGroupCreated.Value = value.ToString(); }
        }
        public MaterialHierarchies MaterialGroupGridViewDataSource
        {
            set
            {
                gvGroup.DataSource = value;
                if (_newPageIndex >= 0)
                {
                    gvGroup.PageIndex = _newPageIndex;
                }
                gvGroup.DataBind();
                ActiveStep = "3";
                uplView.Update();
            }
        }
        
        private void fillClassddl()
        {
            var filter = new KeyValuePairItems
                        {                           
                            new KeyValuePairItem(Constants.filter4, Constants.DdlFilterClass),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextMaterialClass),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlMATCategories)
                        };
            BindMaterialClass(BindType.List);
            _genericClass.LoadDropDown(ddlClassType, filter, null, UserContext.DataBaseInfo);
            _genericClass.LoadDropDown(ddlClassGroups, filter, null, UserContext.DataBaseInfo);
        }
        private void fillTypeddl()
        {
            var filter = new KeyValuePairItems
                        {                      
                            new KeyValuePairItem(Constants.filter1, GroupClassId),
                            new KeyValuePairItem(Constants.filter4, Constants.DdlFilterTypes),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextMaterialType),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlMATCategories)
                        };
            _genericClass.LoadDropDown(ddlTypeGroups, filter, null, UserContext.DataBaseInfo);
        }
        protected void lnkAddNewClass_Click(object sender, EventArgs e)
        {
            clearForm();
            ActiveStep = "1";
        }
        protected void lnkAddNewType_Click(object sender, EventArgs e)
        {
            clearForm();
            ActiveStep = "2";
        }
        protected void lnkAddNewGroup_Click(object sender, EventArgs e)
        {
            clearForm();
            ActiveStep = "3";
        }
        public void BindMaterialClass(BindType bindType)
        {
            var queryargument = new QueryArgument(UserContext.DataBaseInfo)
            {
                BindType = bindType,
                FilterKey = bindType == BindType.Form ? SelectedClassId : string.Empty,
                QueryType = Constants.MatGroupClass
            };
            var hierarchy = _controlPanel.GetMaterialHierarchies(queryargument);

            if (bindType == BindType.Form)
            {
                var materialClass = hierarchy.FirstOrDefault();
                if (materialClass == null) return;
                ClassId = materialClass.Id;
                ClassName = materialClass.Description;
                ClassCreatedDate = materialClass.CreatedDate;
            }
            else
            {
                MaterialClassGridViewDataSource = hierarchy;
            }
        }
        public void BindMaterialTypes(BindType bindType)
        {
            var queryargument = new QueryArgument(UserContext.DataBaseInfo)
            {
                BindType = bindType,
                FilterKey = bindType == BindType.Form ? SelectedTypeId : string.Empty,
                filter1 = SelectedClassId,
                QueryType = Constants.MatGroupType
            };
            var hierarchy = _controlPanel.GetMaterialHierarchies(queryargument);
            if (bindType == BindType.Form)
            {
                var materialType = hierarchy.FirstOrDefault();
                if (materialType == null) return;
                TypeClassId = ((MaterialType)materialType).MaterialClass.Id;
                TypeId = materialType.Id;
                TypeName = materialType.Description;
                TypeCreatedDate = materialType.CreatedDate;
            }
            else
            {
                MaterialTypesGridViewDataSource = hierarchy;
            }
        }
        public void BindMaterialGroups(BindType bindType)
        {
            var queryargument = new QueryArgument(UserContext.DataBaseInfo)
            {
                BindType = bindType,
                FilterKey = bindType == BindType.Form ? SelectedGroupId : string.Empty,
                filter1 = SelectedTypeId,
                QueryType = Constants.MatGroupGroup
            };
            var hierarchy = _controlPanel.GetMaterialHierarchies(queryargument);
            if (bindType == BindType.Form)
            {
                var materialGroup = hierarchy.FirstOrDefault();
                if (materialGroup == null) return;
                GroupClassId = ((MaterialGroup)materialGroup).MaterialClass.Id;
                GroupTypeId = ((MaterialGroup)materialGroup).MaterialType.Id;
                GroupId = materialGroup.Id;
                GroupName = materialGroup.Description;
                GroupCreatedDate = materialGroup.CreatedDate;
                //Margin = materialGroup.Margin;
            }
            else
            {
                MaterialGroupGridViewDataSource = hierarchy;
            }
        }
        protected void Grid_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var gv = sender as GridView;
            var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
            if (gv != null && lbl != null)
                lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);
            var materialHierarchie = e.Row.DataItem as MaterialHierarchy;
            var hidCreatedDate = e.Row.FindControl("hidCreatedDate") as HiddenField;
            if (hidCreatedDate != null)
                hidCreatedDate.Value = materialHierarchie.CreatedDate.ToString();
        }
       
        protected void LoadClassDetails(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            SelectedClassId = lnkbtn.CommandArgument;
            ClassAction = Constants.UpdateAction;
            ActiveStep = "1";
            BindMaterialClass(BindType.Form);
            txtClassId.Enabled = false;
        }
        protected void LoadClassTypeDetails(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            SelectedClassId = lnkbtn.CommandArgument;
            ClassAction = Constants.UpdateAction;
            ActiveStep = "2";
            BindMaterialTypes(BindType.List);
        }
        protected void LoadTypeDetails(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            SelectedTypeId = lnkbtn.CommandArgument;
            TypeAction = Constants.UpdateAction;
            ActiveStep = "2";
            BindMaterialTypes(BindType.Form);
            txtTypeId.Enabled = false;
        }
        protected void LoadTypeGroupDetails(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            SelectedTypeId = lnkbtn.CommandArgument;
            TypeAction = Constants.UpdateAction;
            ActiveStep = "3";
            BindMaterialGroups(BindType.List);

        }
        protected void LoadGroupDetails(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            SelectedGroupId = lnkbtn.CommandArgument;
            GroupAction = Constants.UpdateAction;
            ActiveStep = "3";
            BindMaterialGroups(BindType.Form);
            txtGroupId.Enabled = false;
        }

        protected void lnkSaveClass_Click(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (UpdateDetails(MaterialHierarchyType.Class))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.ClassSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "CLASS SAVED",
                  GlobalCustomResource.ClassSaved, true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.ClassFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "CLASS UPDATE FAILED",
                  GlobalCustomResource.ClassFailed, true);
            }
            fillClassddl();
            BindMaterialClass(BindType.List);
            clearForm();
            if (lnkbtn.ID == "lnkNext")
                ActiveStep = "2";
        }

        protected void lnkSaveType_Click(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (UpdateDetails(MaterialHierarchyType.Type))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.TypeSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "TYPE SAVED",
                  GlobalCustomResource.TypeSaved, true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.TypeFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "TYPE UPDATE FAILED",
                  GlobalCustomResource.TypeFailed, true);
            }
            fillTypeddl();
            BindMaterialTypes(BindType.List);
            clearForm();
            if (lnkbtn.ID == "lnkSaveTypeBtm")
                ActiveStep = "3";
        }
        protected void lnkSaveGroup_Click(object sender, EventArgs e)
        {
            if (UpdateDetails(MaterialHierarchyType.Group))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.GroupSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "GROUP SAVED",
                  GlobalCustomResource.GroupSaved, true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.GroupFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "GROUP UPDATE FAILED",
                  GlobalCustomResource.GroupFailed, true);
            }
            BindMaterialGroups(BindType.List);
            clearForm();
        }       
        public bool UpdateDetails(MaterialHierarchyType argsType)
        {
            MaterialHierarchy matGroup;
            switch (argsType)
            {
                case MaterialHierarchyType.Class:
                    {
                        // Assign new action if action is empty or view
                        if (ClassAction == string.Empty || ClassAction == Constants.ViewAction)
                        {
                            ClassAction = Constants.InsertAction;
                        }
                        matGroup = new MaterialClass
                        {
                            Id = ClassId.ToUpper(),
                            Description = ClassName,
                            Action = ClassAction,   
                            CreatedDate=ClassCreatedDate
                        };
                        break;
                    }
                case MaterialHierarchyType.Type:
                    {
                        // Assign new action if action is empty or view
                        if (TypeAction == string.Empty || TypeAction == Constants.ViewAction)
                        {
                            TypeAction = Constants.InsertAction;
                        }
                        matGroup = new MaterialType
                        {
                            Id = TypeId.ToUpper(),
                            MaterialClass = new MaterialClass { Id = TypeClassId },
                            Description = TypeName,
                            Action = TypeAction,
                            CreatedDate=TypeCreatedDate
                        };
                        break;
                    }
                default:
                    {
                        // Assign new action if action is empty or view
                        if (GroupAction == string.Empty || GroupAction == Constants.ViewAction)
                        {
                            GroupAction = Constants.InsertAction;
                        }
                        matGroup = new MaterialGroup
                        {
                            Id = GroupId.ToUpper(),
                            MaterialClass = new MaterialClass { Id = GroupClassId },
                            MaterialType = new MaterialType { Id = GroupTypeId },
                            Description = GroupName,
                            Action = GroupAction,
                            CreatedDate = GroupCreatedDate,
                            //Margin = Margin
                        };
                        break;
                    }
            }
            matGroup.DataBaseInfo = UserContext.DataBaseInfo;
            matGroup.MaterialHierarchyType = argsType;
            return _controlPanel.SetMaterialHierarchies(new MaterialHierarchies { matGroup });
        }
        protected void ddlClassType_SelectedIndexChanged(object sender, EventArgs e)
        {            
            ActiveStep = "2";
            SelectedTypeId = string.Empty;
            SelectedClassId = TypeClassId;
            BindMaterialTypes(BindType.List);
        }
        protected void ddlClassGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveStep = "3";
            fillTypeddl();
        }
        protected void ddlTypeGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Bind Analysis
            ActiveStep = "3";
            SelectedTypeId = GroupTypeId;
            SelectedGroupId = string.Empty;
            BindMaterialGroups(BindType.List);
        }
        private void clearForm()
        {
            ClassId = string.Empty;
            ClassName = string.Empty;
            TypeId = string.Empty;
            TypeName = string.Empty;
            TypeClassId = string.Empty;
            GroupId = string.Empty;
            GroupName = string.Empty;
            GroupClassId = string.Empty;
            GroupTypeId = string.Empty;
            //Margin = 0;
            ClassAction = Constants.InsertAction;
            TypeAction = Constants.InsertAction;
            GroupAction = Constants.InsertAction;
            txtClassId.Enabled = true;
            txtGroupId.Enabled = true;
            txtTypeId.Enabled = true;
            ClassCreatedDate = DateTime.Now;
            TypeCreatedDate = DateTime.Now;
            GroupCreatedDate = DateTime.Now;

        }
        protected void gvClass_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            _newPageIndex = e.NewPageIndex;
            BindMaterialClass(BindType.List);
        }

        protected void gvTypes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            _newPageIndex = e.NewPageIndex;
            BindMaterialTypes(BindType.List);
        }

        protected void gvGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            _newPageIndex = e.NewPageIndex;
           BindMaterialGroups(BindType.List);
        }
        private void SetPageProperties()
        {
            var menuSettings = GeneralUtilities.GetCurrentMenuSettings(UserContext,
                QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            if (menuSettings == null) return;
            pageTitle.InnerText = menuSettings.PageTitle;
        }
    }
}