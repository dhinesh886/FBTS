using FBTS.App.Library;
using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.View.Resources.ResourceFiles;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View.AppPages.CPanel
{
    public partial class FBTS_UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtEmail.Attributes.Add("onblur", "return validateEmail(<%=" + txtEmail.ClientID + "%>);");
            string Password = txtPassword.Text;
            txtPassword.Attributes.Add("value", Password);
            if (!IsPostBack)
            {
                this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
                SetPageProperties();
                var filter = new KeyValuePairItems
                        {   
                            new KeyValuePairItem(Constants.filter1, Constants.CountryType),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextCountry),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlCountry)
                        };
                _genericClass.LoadDropDown(ddlCountry, filter, null, UserContext.DataBaseInfo);

                filter = new KeyValuePairItems
                        {
                            //new KeyValuePairItem(Constants.filter1, Constants.BranchCType),
                            //new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextBranch),
                            //new KeyValuePairItem(Constants.masterType,Constants.DdlBranch)
                            new KeyValuePairItem(Constants.filter1, Constants.WHCType),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextLocation),
                            new KeyValuePairItem(Constants.masterType,Constants.TableAccounts)
                        };
                _genericClass.LoadDropDown(ddlBranch, filter, null, UserContext.DataBaseInfo);

                filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.SysDesignationCType),
                            new KeyValuePairItem(Constants.filter2, Constants.SysDesignation),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextDesig),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlCatHeaderData)
                        };
                _genericClass.LoadDropDown(ddlDesignation, filter, null, UserContext.DataBaseInfo);

                filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.DeptCType),
                            new KeyValuePairItem(Constants.filter2, Constants.DdlDepartment),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextDept),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlCatHeaderData)
                        };
                _genericClass.LoadDropDown(ddlDept, filter, null, UserContext.DataBaseInfo);

                filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.WHCType),
                            new KeyValuePairItem(Constants.filter2, Constants.DvConfigWh),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextWh),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlCatHeaderData)
                        };
                _genericClass.LoadDropDown(ddlWH, filter, null, UserContext.DataBaseInfo);
                //Reserve a spot in Session for the UploadDetail object
                SessionManagement<UploadDetail>.SetValue(Constants.ImportDataSessionKey, new UploadDetail { IsReady = false });
                BindData(BindType.List);
            }
        }
        private readonly GenericManager _genericClass = new GenericManager();
        private readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }


        #region Form Fields

        public Guid UserCode
        {
            get
            {
                return string.IsNullOrEmpty(hidKey.Value) ? new Guid() : new Guid(hidKey.Value);
            }
            set { hidKey.Value = value.ToString(); }
        }
        public string UserLoginId { get { return txtUserId.Text; } set { txtUserId.Text = value; } }
        public string Password { get { return txtPassword.Text; } set { txtPassword.Text = value; txtPassword.Attributes.Add("value", value); } }
        public string UserName { get { return txtUserName.Text; } set { txtUserName.Text = value; } }
        public string LastName { get { return txtLastName.Text.Trim(); } set { txtLastName.Text = value.Trim(); } }
        public string Address { get { return txtStreet.Text; } set { txtStreet.Text = value; } }
        public string City { get { return txtCity.Text; } set { txtCity.Text = value; } }        
        public string Country { get { return ddlCountry.SelectedValue.Trim(); } set { ddlCountry.SelectedValue = value; } }
        public string State { get { return ddlState.SelectedValue.Trim(); } set { ddlState.SelectedValue = value; } }
        public string ZipCode { get { return txtPostCode.Text; } set { txtPostCode.Text = value; } }
        public string Mobile { get { return txtMobile.Text; } set { txtMobile.Text = value; } }
        public string OfficePhone { get { return txtOffPhone.Text; } set { txtOffPhone.Text = value; } }
        public string ResidentialPhone { get { return txtResPhone.Text; } set { txtResPhone.Text = value; } }
        public string Email { get { return txtEmail.Text; } set { txtEmail.Text = value; } }
        public DateTime Dob 
        {
            get { return Dates.ToDateTime(txtDob.Text.Trim(),DateFormat.Format_01); }
            set
            {
                if (value == Convert.ToDateTime(Constants.DefaultDate))
                    txtDob.Text = string.Empty;
                else
                    txtDob.Text = Dates.FormatDate(value, Constants.Format02);
            } 
        }
        public string EmployeeId { get { return txtEmpId.Text; } set { txtEmpId.Text = value; } }
        public string Gender { get { return ddlGender.SelectedValue.Trim(); } set { ddlGender.SelectedValue = value; } }
        public string Designation { get { return ddlDesignation.SelectedValue.Trim(); } set { ddlDesignation.SelectedValue = value; } }        
        public string ReportingTo { get { return ddlReporting.SelectedValue.Trim(); } set { ddlReporting.SelectedValue = value.ToUpper(); } }
        public string Branch { get { return ddlBranch.SelectedValue.Trim(); } set { ddlBranch.SelectedValue = value; } }

        public DateTime ActiveTill
        {
            get { return Dates.ToDateTime(txtActiveTill.Text.Trim(), DateFormat.Format_01); }
            set
            {
                if (value == Convert.ToDateTime(Constants.DefaultDate))
                    txtActiveTill.Text = string.Empty;
                else
                    txtActiveTill.Text = Dates.FormatDate(value, Constants.Format02);
            }
        }
        public string Warehouse { get { return ddlWH.SelectedValue.Trim(); } set { ddlWH.SelectedValue = value; } }
        public string Department { get { return ddlDept.SelectedValue.Trim(); } set { ddlDept.SelectedValue = value; } }

        public string Avator
        {
            get { return hidFileName.Value; }
            set
            {
                hidFileName.Value = value;
                imgUserImage.ImageUrl = Constants.UserImagesDirectory.Replace("~", "../../") + "/" + value;
            }
        }

        public string Bu
        {
            get
            {
                if (string.IsNullOrEmpty(hidBu.Value.Trim()))
                    return UserContext.UserProfile.Bu;
                return hidBu.Value;
            }
            set { hidBu.Value = value; }
        }       
        public string Status { get { return hidStatus.Value; } set { hidStatus.Value = value; } }
        public DateTime CreatedDate
        {
            get { return Dates.ToDateTime(hidCreated.Value.Trim(), DateFormat.Format_01); }
            set
            {
                if (value == Convert.ToDateTime(Constants.DefaultDate))
                    hidCreated.Value = Dates.FormatDate(DateTime.Now, Constants.Format02);
                else
                    hidCreated.Value = Dates.FormatDate(value, Constants.Format02);
            }
        }
        public string LastPasswordChanged { get; set; }
        public string SelectedAccessLevel
        {
            get { return hidSelectedDesign.Value; }
            set
            {
                hidSelectedDesign.Value = value;
            }
        }
        public string Action { get { return hidAction.Value; } set { hidAction.Value = value; } }
        //public string Keyword { get { return txtSearch.Text; } set { txtSearch.Text = value; } }
        #endregion

        public void BindData(BindType bindType)
        {
             var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                FilterKey = UserCode.ToString(),
                BindType = bindType,
                //Keyword = Keyword                
            };
             var userProfiles = _controlPanel.GetUserProfiles(queryArgument);
            if (userProfiles != null)
            {
                if (bindType == BindType.Form)
                {
                    var objUserProfile = userProfiles.FirstOrDefault();
                    if (objUserProfile != null)
                    {
                        UserCode = objUserProfile.UCode;
                        UserLoginId = objUserProfile.LoginId.Trim();
                        Password = objUserProfile.Password.Trim();
                        UserName = objUserProfile.Name.Trim();
                        //LastName=objUserProfile.
                        Address = objUserProfile.Address.Trim();
                        City = objUserProfile.City.Trim();
                        Country = objUserProfile.Country.Trim();
                        var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.CountryType),
                            new KeyValuePairItem(Constants.filter2, Country),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextState),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlCatHeaderData)
                        };
                        _genericClass.LoadDropDown(ddlState, filter, null,UserContext.DataBaseInfo);
                        State = objUserProfile.State.Trim();
                        ZipCode = objUserProfile.Zip.Trim();
                        Email = objUserProfile.Email.Trim();
                        Mobile = objUserProfile.Mobile.Trim();
                        ResidentialPhone = objUserProfile.ResPhone.Trim();
                        OfficePhone = objUserProfile.OffPhone.Trim();
                        Dob = objUserProfile.Dob.GetValueOrDefault();
                        EmployeeId = objUserProfile.EmpId.Trim();
                        Gender = objUserProfile.Gender.Trim();
                        filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.key, objUserProfile.UCode.ToString()),
                            new KeyValuePairItem(Constants.filter1, objUserProfile.Designation.Level.ToString(CultureInfo.InvariantCulture)),                            
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextRManager),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlRManager)
                        };
                        _genericClass.LoadDropDown(ddlReporting, filter, null, UserContext.DataBaseInfo);
                        ReportingTo = objUserProfile.ReportingTo.ToString().Trim();
                        Warehouse = objUserProfile.Wh.Trim();
                        Department = objUserProfile.Dept.Trim();
                        Designation = objUserProfile.Designation.Id.Trim();
                        Avator = objUserProfile.Avatar.Trim();
                        ActiveTill = objUserProfile.ActiveTill.GetValueOrDefault();
                        Branch = objUserProfile.Branch.Trim().Trim();
                        Bu = objUserProfile.Bu.Trim();
                        Status = objUserProfile.Off.Trim();
                    }
                    lnkSave.Visible = hidAction.Value != Constants.ViewAction;
                    uplForm.Update();
                }
                else
                {
                    var userProfile = userProfiles.ToList();
                    if (UserContext.UserProfile.Designation.Id.Trim() != "SA")
                    {
                        if (userProfiles.Any())
                            userProfile = userProfiles.Where(x => x.Designation.Id.Trim() != "SA").ToList();
                    }
                    GridViewTable.DataSource = userProfile;
                    GridViewTable.DataBind();
                    uplView.Update();
                }
            }
        }

        public void BindMenuRights(Menus menus)
        {
            gvMenuRights.DataSource = menus;
            gvMenuRights.DataBind();
            uplAccess.Update();
        }
        protected void GridViewTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var gv = sender as GridView;
            var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
            if (gv != null && lbl != null)
                lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);
        }
        protected void gvMenuRights_OnRowDataBoound(object sender, GridViewRowEventArgs e)
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
        protected void GridViewTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void GridViewTable_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            if (UpdateUserDetails())
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.UserProfileSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "USERPROFILE SAVED",
                  GlobalCustomResource.UserProfileSaved, true);
                ClearForm();
                BindData(BindType.List);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.UserProfileFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "USERPROFILE UPDATE FAILED",
                  GlobalCustomResource.UserProfileFailed, true);               
            }           
        }
        public bool UpdateUserDetails()
        {
            // Assign new action if action is empty or view
            if (Action == string.Empty || Action == Constants.ViewAction)
            {
                Action = Constants.InsertAction;
            }
            
            if (Action == Constants.InsertAction)
            {
                CreatedDate = UserContext.CurrentDate;
                UserCode = Guid.NewGuid();
            }

            var objProfile = new UserProfile
            {
                UType = Constants.UserType,
                UCode = UserCode,
                Created = CreatedDate,
                LoginId = UserLoginId,
                Password = Password,                
                Name = UserName,
                Address = Address,
                City = City,
                Country = Country,
                State = State,                
                Zip = ZipCode,
                Mobile = Mobile,
                OffPhone = OfficePhone,
                ResPhone = ResidentialPhone,
                Email = Email,
                Dob = Dob,
                EmpId = EmployeeId,
                Gender = Gender,
                Designation = new Designation { Id = Designation },
                ReportingTo = new Guid(ReportingTo),
                Branch = Branch,
                ActiveTill = ActiveTill,
                Wh = Warehouse,
                Dept = Department,
                Avatar = Avator,

                LastPasswordChanged = Dates.ToDateTime(LastPasswordChanged, DateFormat.Format_05),
                Bu = Bu,
                Off = Status,
                DefaultLink = Constants.DefaultHomeLink,
                Action = Action,
                DataBaseInfo = UserContext.DataBaseInfo
            };

            var userProfiles = new UserProfiles { objProfile };
            return _controlPanel.ManageUsers(userProfiles);
        }
        protected void lnkSaveAccessLevel_Click(object sender, EventArgs e)
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
                UserId = UserCode,
                AccessRights = menus,
                DataBaseInfo = UserContext.DataBaseInfo
            };
            //UserProfilePresenter.SaveMenuRights(menuAccessRights)
            if (_controlPanel.SetMenuAccessRights(menuAccessRights))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.UserProfileSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "USERPROFILE SAVED",
                  GlobalCustomResource.UserProfileSaved, true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.UserProfileFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "USERPROFILE UPDATE FAILED",
                  GlobalCustomResource.UserProfileFailed, true);
            }


        }
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        protected void LoadDetails(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            Action = lnkbtn.CommandName;
            UserCode = new Guid(lnkbtn.CommandArgument);
            BindData(BindType.Form);
        }
        protected void LoadAccessDetails(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            Action = Constants.UpdateAction;
            lblAccessLevel.Text = lnkbtn.CommandName;
            var arguments = lnkbtn.CommandArgument.Split(new[] { "||" }, StringSplitOptions.None);
            UserCode = new Guid(arguments[0]);
            SelectedAccessLevel = arguments[1];
            uplForm.Update();
            var queryargument = new QueryArgument(UserContext.DataBaseInfo)
            {
                FilterKey = UserCode.ToString(),
                Key = SelectedAccessLevel
            };
            var menus = _controlPanel.GetMenuAccessRights(queryargument);
            BindMenuRights(menus);
        }
        private void ClearForm()
        {
            lnkSave.Visible = true;
            UserCode = Guid.Empty;
            UserLoginId = string.Empty;
            Password = string.Empty;
            UserName = string.Empty;

            Address = string.Empty;
            City = string.Empty;
            Branch = string.Empty;
            Country = string.Empty;
            ddlReporting.Items.Clear();
            ddlState.Items.Clear();
            ZipCode = string.Empty;
            ActiveTill = Convert.ToDateTime(Constants.DefaultDate);
            Avator = string.Empty;
            Mobile = string.Empty;
            Email = string.Empty;
            CreatedDate = Convert.ToDateTime(Constants.DefaultDate);
            Dob = Convert.ToDateTime(Constants.DefaultDate);
            LastPasswordChanged = string.Empty;
            EmployeeId = string.Empty;
            Department = string.Empty;
            Warehouse = string.Empty;
            Gender = string.Empty;
            ReportingTo = string.Empty;
            OfficePhone = string.Empty;
            ResidentialPhone = string.Empty;
            Designation = string.Empty;
            hidAction.Value = Constants.InsertAction;
            uplForm.Update();
        }
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.CountryType),
                            new KeyValuePairItem(Constants.filter2, Country),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextState),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlCatHeaderData)
                        };
            _genericClass.LoadDropDown(ddlState, filter, null, UserContext.DataBaseInfo);
        }
        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var filter = new KeyValuePairItems
                        {                           
                            new KeyValuePairItem(Constants.filter1, Designation),                            
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextRManager),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlRManager)
                        };
            _genericClass.LoadDropDown(ddlReporting, filter, null, UserContext.DataBaseInfo);
        }

        protected void FileUploadComplete(object sender, EventArgs e)
        {
            if (fudUserImages.PostedFile != null && fudUserImages.PostedFile.ContentLength > 0)
            {
                if (Constants.UserImageFormats.Contains(Path.GetExtension(fudUserImages.PostedFile.FileName)))
                {
                    var objUpload = (UploadDetail)Session[Constants.ImportDataSessionKey];
                    //Let the webservie know that we are not yet ready
                    objUpload.IsReady = false;
                    //build the local path where upload all the files
                    var msPath1 = Server.MapPath(Constants.UserImagesDirectory);
                    new DirectoryInfo(msPath1).CreateDirectory();

                    var msFileName = Path.GetFileName(fudUserImages.PostedFile.FileName.Replace(" ", ""));
                    //Build the strucutre and stuff it into session
                    objUpload.ContentLength = fudUserImages.PostedFile.ContentLength;
                    objUpload.FileName = msFileName;
                    objUpload.UploadedLength = 0;
                    //Let the polling process know that we are done initializing ...
                    objUpload.IsReady = true;

                    //set the buffer size to something larger.
                    //the smaller the buffer the longer it will take to download, 
                    //but the more precise your progress bar will be.
                    const int bufferSize = 1;
                    var buffer = new byte[bufferSize];

                    //Writing the byte to disk
                    using (var fs = new FileStream(Path.Combine(msPath1, msFileName), FileMode.Create))
                    {
                        //Aslong was we haven't written everything ...
                        while (objUpload.UploadedLength < objUpload.ContentLength)
                        {
                            //Fill the buffer from the input stream
                            int bytes = fudUserImages.PostedFile.InputStream.Read(buffer, 0, bufferSize);
                            //Writing the bytes to the file stream
                            fs.Write(buffer, 0, bytes);
                            //Update the number the webservice is polling on to the session
                            objUpload.UploadedLength += bytes;
                        }
                    }
                    Avator = msFileName;
                    // Load the uploaded file into image control
                    var srcFile = Constants.UserImagesDirectory.Replace("~", "../../") + "/" + msFileName;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "TestAlert",
                        "window.parent.document.getElementById('" + imgUserImage.ClientID + "').src='" + srcFile +
                        "?id="
                        + (new Random().Next(9999)) + "';", true);
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
    }
}