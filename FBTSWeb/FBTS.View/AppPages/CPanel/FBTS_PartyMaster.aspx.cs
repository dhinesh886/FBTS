using FBTS.App.Library;
using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.Model.Transaction.Accounts;
using FBTS.View.Resources.ResourceFiles;
using FBTS.View.UserControls.Common;
using System;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View.AppPages.CPanel
{
    public partial class FBTS_PartyMaster : System.Web.UI.Page
    {
        public readonly TransactionManager _transactionManager = new TransactionManager();
        public readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        private readonly GenericManager _genericClass = new GenericManager();
        private int _newPageIndex = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));

            SetPageProperties();
            BasicAddress.IsVisiableLocationDdl = true;
            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.WHCType),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextLocation),
                            new KeyValuePairItem(Constants.masterType,Constants.TableAccounts)
                        };
            _genericClass.LoadDropDown(BasicAddress.LocationControl, filter, null, UserContext.DataBaseInfo);
            BasicAddress.SetCityLabel = Constants.LocationLablel;


        }
        private void SetPageProperties()
        {
            var menuSettings = GeneralUtilities.GetCurrentMenuSettings(UserContext,
                QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            if (menuSettings == null) return;
            pageTitle.InnerText = menuSettings.PageTitle;
            LType = menuSettings.Type;
            LMode = menuSettings.Mode;
            IsUpload = menuSettings.GetSettingsValue("IsUpload").ToBool();
            BindData(BindType.List);
        }

        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        public string Action { get { return hidAction.Value.Trim(); } set { hidAction.Value = value.Trim(); } }
        public string LType
        {
            get { return hidType.Value.Trim(); }
            set { hidType.Value = value.Trim(); }
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
        public string Code
        {
            get { return txtCode.Text.ToTrimString().ToUpper(); }
            set { txtCode.Text = value.Trim(); }
        }

        public string Name
        {
            get { return txtName.Text.Trim(); }
            set { txtName.Text = value.Trim(); }
        }
        public string LMode
        {
            get { return hidMode.Value.Trim(); }
            set { hidMode.Value = value; }
        }
        public string FGroup
        {
            get
            {
                return LMode == Constants.ASSET + Constants.INCOME ? Constants.INCOME + "." + Constants.LabelDealer :
                                                                    Constants.EXPENSE + "." + Constants.LabelCustomer;
            }
        }
        public bool IsUpload
        {
            get
            {
                return hdnIsUpload.Value.ToBool();
            }
            set
            {
                hdnIsUpload.Value = value.ToString();
            }
        }
        public string ContactPerson
        {
            get { return txtcontactperson.Text.Trim(); }
            set { txtcontactperson.Text = value.Trim(); }
        }
        
        public AddressControlVertical BasicAddress
        {
            get { return BasicAddressControl; }
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
        public void BindData(BindType bindType)
        {
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = Code.ToString(),
                filter1 = LType,
                filter2=Constants.LedgerSub,
                filter3=LMode,
                filter4 = bindType == BindType.List ? Constants.RetriveList : Constants.RetriveForm,
                FilterKey = Constants.TableAccounts
            };
            var accounts = _controlPanel.GetAccounts(queryArgument);
            if (accounts != null)
            {
                if (bindType == BindType.Form)
                {
                    var firstOrDefault = accounts.FirstOrDefault();
                    if (firstOrDefault == null) return;
                    Code = firstOrDefault.SName;
                    Name = firstOrDefault.Name;
                    CreatedDate = firstOrDefault.Created;
                    ContactPerson = firstOrDefault.ContactPerson;
                    BasicAddress.DataSource = firstOrDefault.Address;
                    uplForm.Update();
                }
                else
                {

                    GridViewTable.DataSource = accounts;
                    if (_newPageIndex >= 0)
                    {
                        GridViewTable.PageIndex = _newPageIndex;
                    }
                    GridViewTable.DataSource = accounts;
                    GridViewTable.DataBind();
                    if (IsUpload)
                    {
                        GridViewTable.Columns[3].Visible = false;
                    }
                    uplView.Update();
                }
            }
        }
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            if (IsUpload)
            {
                divForm.Visible = divView.Visible = false;
                uplActions.Update();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(3)", true);
            }
            else
            {
                txtCode.Enabled = true;
                clearForm();
                DivAction = true;
                Action = Constants.InsertAction;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(2)", true);
            }
        }
        private void clearForm()
        {
            Code = string.Empty;
            Name = string.Empty;
            ContactPerson = string.Empty;
            CreatedDate = DateTime.Now;
            BasicAddress.Clear();
            uplForm.Update();
            txtSearch.Text = string.Empty;
            uplActions.Update();
        }
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            if (Action == string.Empty || Action == Constants.ViewAction)
            {
                Action = Constants.InsertAction;
            }
            if(Action == Constants.InsertAction)
            {
                QueryArgument queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    FilterKey = Code,
                    QueryType = Constants.TableAccounts
                };
                if (_transactionManager.ValidateKey(queryArgument))
                {
                    CustomMessageControl.MessageBodyText = Code + " Party Code already exist";
                    CustomMessageControl.MessageType = MessageTypes.Error;
                    CustomMessageControl.ShowMessage();
                    return;
                }
            }
            var accounts = new Accounts();
            accounts.Add(new Account
            {
                SName = Code,
                Name = Name,
                Type = LType,
                Created = CreatedDate,
                Sub = Constants.LedgerSub,
                LMode=LMode,
                FGroup=FGroup,
                ContactPerson = ContactPerson,
                Address = BasicAddress.DataSource,
                Parent = LType == Constants.Customers ? Constants.LabelCustomer : LType == Constants.Vendors ? Constants.LabelVendor : string.Empty,
                Action = Action,
                DataBaseInfo = UserContext.DataBaseInfo,
            });
           
            if (_controlPanel.SetAccounts(accounts))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.PartyMasterSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "PARTYMASTER SAVED",
                    GlobalCustomResource.PartyMasterSaved, true);
                clearForm();
                DivAction = false;
                BindData(BindType.List);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.PartyMasterFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "PARTYMASTER UPDATE FAILED",
                    GlobalCustomResource.PartyMasterFailed, true);
            }
           
        }

        protected void GridViewTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var gv = sender as GridView;
            var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
            if (gv != null && lbl != null)
                lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);

            var account = e.Row.DataItem as Account;
            var hidSuspent = e.Row.Cells[0].FindControl("hidSuspent") as HiddenField;
            if (hidSuspent != null)
                hidSuspent.Value = account.Suspend.ToString();

            var lnkaction = e.Row.Cells[0].FindControl("lnkBan") as LinkButton;
            if (lnkaction != null)
            {
                if (account.Suspend)
                {
                    lnkaction.Text = "Include";
                    lnkaction.ToolTip = "Click here to Include";
                }
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            clearForm();
            Action = lnkbtn.CommandName;
            Code = lnkbtn.CommandArgument;
            txtCode.Enabled = false;                  
            BindData(BindType.Form);           
            DivAction = true;
            uplForm.Update();
        }

        protected void GridViewTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            _newPageIndex = e.NewPageIndex;
            clearForm();
            BindData(BindType.List);
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            DivAction = false;
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
            Code = row.Cells[1].Text.Trim();

            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = Code,
                filter1 = hidSuspent ? "false" : "true",
                FilterKey = Constants.TableAccounts
            };
            _controlPanel.Cancle(queryArgument);

            CustomMessageControl.MessageBodyText = GlobalCustomResource.MaterialSaved;
            CustomMessageControl.MessageBodyText = hidSuspent ? GlobalCustomResource.PartIncluded : GlobalCustomResource.PartSuspended;
            CustomMessageControl.MessageType = MessageTypes.Success;
            CustomMessageControl.ShowMessage();
            AuditLog.LogEvent(UserContext, SysEventType.INFO, hidSuspent ? "account Included" : "Part Suspended",
              hidSuspent ? GlobalCustomResource.PartIncluded : GlobalCustomResource.PartSuspended, true);
            Code = string.Empty;
            BindData(BindType.List);
        }

        protected void fileExcelUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            if (fileExcelUpload.PostedFile == null || fileExcelUpload.PostedFile.ContentLength == 0)
                return;
            var msPath1 = Path.GetPathRoot(fileExcelUpload.PostedFile.FileName);

            var fileName = (Path.GetFileName(fileExcelUpload.PostedFile.FileName));
            string strFileType = Path.GetExtension(fileName).ToLower();
            if (msPath1 == string.Empty)
            {
                msPath1 = Server.MapPath(Constants.UserExcelFileDirectory);
                new DirectoryInfo(msPath1).CreateDirectory();

                fileExcelUpload.PostedFile.SaveAs(Path.Combine(msPath1, fileName));
                return;
            }

            string path = Path.Combine(msPath1);

            string connString = "";

            //Connection String to Excel Workbook
            if (strFileType.Trim() == ".xls")
            {
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (strFileType.Trim() == ".xlsx")
            {
                //connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";
            }
            string query = "SELECT Code,Name,ContactPerson,Address,Location,Country,State,Zip,Email,Phone,Mobile,Web Site FROM [Customer$]";
            OleDbConnection conn = new OleDbConnection(connString);
            string script = string.Empty;
            string script1 = string.Empty;            
            int i = 1;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                OleDbCommand cmd = new OleDbCommand(query, conn);
                OleDbDataReader odr = cmd.ExecuteReader();
                var accounts = new Accounts();
                while (odr.Read())
                {
                    i++;
                    var code = odr["Code"].ToString().Trim();
                    if (string.IsNullOrEmpty(code))
                    {
                        script1 += " - " + "Code is empty @ line No. : " + i.ToString();
                    }
                    var name = odr["Name"].ToString().Trim();
                    if (string.IsNullOrEmpty(name))
                    {
                        script1 += " - " + "Code is empty @ line No. : " + i.ToString();
                    }
                    var loc = odr["Location"].ToString().Trim();
                    if (string.IsNullOrEmpty(loc))
                    {
                        script1 += " - " + "Location is empty @ line No. : " + i.ToString();
                    }
                    var country = odr["Country"].ToString().Trim();
                    if (string.IsNullOrEmpty(country))
                    {
                        script1 += " - " + "Country is empty @ line No. : " + i.ToString();
                    }
                    var state = odr["State"].ToString().Trim();
                    if (string.IsNullOrEmpty(state))
                    {
                        script1 += " - " + "State is empty @ line No. : " + i.ToString();
                    }
                    if (script == string.Empty && script1 == string.Empty)
                    {
                        Action = Constants.InsertAction;                        
                        accounts.Add(new Account
                        {
                            SName = code,
                            Name = name,
                            Type = LType,
                            Created = CreatedDate,
                            Sub = Constants.LedgerSub,
                            LMode = LMode,
                            FGroup = FGroup,
                            ContactPerson = odr["ContactPerson"].ToString().Trim(),
                            Address = new Address
                                        {
                                            Street = odr["Address"].ToString().Trim(),
                                            City = loc,
                                            State = state,
                                            Country = country,
                                            ZipCode = odr["Zip"].ToString().Trim(),
                                            Mobile = odr["Mobile"].ToString().Trim(),
                                            Email = odr["Email"].ToString().Trim(),
                                            Phone = odr["Phone"].ToString().Trim(),
                                            WebSite = odr["WebSite"].ToString().Trim(),
                                        },
                            Parent = LType == Constants.Customers ? Constants.LabelCustomer : LType == Constants.Vendors ? Constants.LabelVendor : string.Empty,
                            Action = Action,
                            DataBaseInfo = UserContext.DataBaseInfo,
                        });                        
                    }
                    else
                    {
                        CustomMessageControl.MessageBodyText = script + script1;
                        CustomMessageControl.MessageType = MessageTypes.Error;
                        CustomMessageControl.ShowMessage();
                    }
                }
                if (script == string.Empty && script1 == string.Empty)
                {
                    if (_controlPanel.SetAccounts(accounts))
                    {
                        CustomMessageControl.MessageBodyText = GlobalCustomResource.PartyMasterSaved;
                        CustomMessageControl.MessageType = MessageTypes.Success;
                        CustomMessageControl.ShowMessage();
                        AuditLog.LogEvent(UserContext, SysEventType.INFO, "PARTYMASTER SAVED",
                            GlobalCustomResource.PartyMasterSaved, true);
                        clearForm();
                        DivAction = false;
                        BindData(BindType.List);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
                    }
                    else
                    {
                        CustomMessageControl.MessageBodyText = GlobalCustomResource.PartyMasterFailed;
                        CustomMessageControl.MessageType = MessageTypes.Error;
                        CustomMessageControl.ShowMessage();
                        AuditLog.LogEvent(UserContext, SysEventType.INFO, "PARTYMASTER UPDATE FAILED",
                            GlobalCustomResource.PartyMasterFailed, true);
                    }
                }
            }
            catch (Exception ex)
            {                
                CustomMessageControl.MessageBodyText = ex.Message;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            uplView.Update();
        }

        protected void lnkUpload_Click(object sender, EventArgs e)
        {
            string fileName = hidFileName.Value.Trim();
            if (fileName == string.Empty) return;

            var msPath1 = Server.MapPath(Constants.UserExcelFileDirectory);
            new DirectoryInfo(msPath1).CreateDirectory();
            fileName = fileName.Replace(@"C:\fakepath\", string.Empty);

            string strFileType = Path.GetExtension(fileName).ToLower();
            string path = Path.Combine(msPath1, fileName);
            string connString = "";

             //Connection String to Excel Workbook
            if (strFileType.Trim() == ".xls")
            {
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (strFileType.Trim() == ".xlsx")
            {
                //connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";
            }
            string query = "SELECT Code,Name,ContactPerson,Address,Location,Country,State,Zip,Email,Phone,Mobile,WebSite FROM [Customer$]";
            OleDbConnection conn = new OleDbConnection(connString);
            string script1 = string.Empty;
            int i = 1;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                OleDbCommand cmd = new OleDbCommand(query, conn);
                OleDbDataReader odr = cmd.ExecuteReader();
                var accounts = new Accounts();
                while (odr.Read())
                {
                    i++;
                    var code = odr["Code"].ToString().Trim();
                    if (string.IsNullOrEmpty(code))
                    {
                        script1 += " - " + "Code is empty @ line No. : " + i.ToString();                        
                    }
                    if(accounts.Where(x=>x.SName==code).Any())
                    {
                        script1 = code + " Party Code already exist";
                    }
                    QueryArgument queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                    {
                        FilterKey = code,
                        QueryType = Constants.TableAccounts
                    };
                    if (_transactionManager.ValidateKey(queryArgument))
                    {
                        script1 = code + " Party Code already exist";
                    }
                    
                    var name = odr["Name"].ToString().Trim();
                    if (string.IsNullOrEmpty(name))
                    {
                        script1 += " - " + "Code is empty @ line No. : " + i.ToString();
                    }
                    var loc = odr["Location"].ToString().Trim();
                    if (string.IsNullOrEmpty(loc))
                    {
                        script1 += " - " + "Location is empty @ line No. : " + i.ToString();
                    }
                    var country = odr["Country"].ToString().Trim();
                    if (string.IsNullOrEmpty(country))
                    {
                        script1 += " - " + "Country is empty @ line No. : " + i.ToString();
                    }
                    var state = odr["State"].ToString().Trim();
                    if (string.IsNullOrEmpty(state))
                    {
                        script1 += " - " + "State is empty @ line No. : " + i.ToString();                        
                    }

                    if (script1 == string.Empty)
                    {
                        Action = Constants.InsertAction;
                        accounts.Add(new Account
                        {
                            SName = code,
                            Name = name,
                            Type = LType,
                            Created = CreatedDate,
                            Sub = Constants.LedgerSub,
                            LMode = LMode,
                            FGroup = FGroup,
                            ContactPerson = odr["ContactPerson"].ToString().Trim(),
                            Address = new Address
                            {
                                Street = odr["Address"].ToString().Trim(),
                                City = loc,
                                State = state,
                                Country = country,
                                ZipCode = odr["Zip"].ToString().Trim(),
                                Mobile = odr["Mobile"].ToString().Trim(),
                                Email = odr["Email"].ToString().Trim(),
                                Phone = odr["Phone"].ToString().Trim(),
                                WebSite = odr["WebSite"].ToString().Trim(),
                            },
                            Parent = LType == Constants.Customers ? Constants.LabelCustomer : LType == Constants.Vendors ? Constants.LabelVendor : string.Empty,
                            Action = Action,
                            DataBaseInfo = UserContext.DataBaseInfo,
                        });
                    }
                    else
                    {
                        CustomMessageControl.MessageBodyText = script1;
                        CustomMessageControl.MessageType = MessageTypes.Error;
                        CustomMessageControl.ShowMessage();
                        break;
                    }
                }
                if (script1 == string.Empty)
                {
                    if (_controlPanel.SetAccounts(accounts))
                    {
                        CustomMessageControl.MessageBodyText = GlobalCustomResource.PartyMasterSaved;
                        CustomMessageControl.MessageType = MessageTypes.Success;
                        CustomMessageControl.ShowMessage();
                        AuditLog.LogEvent(UserContext, SysEventType.INFO, "PARTYMASTER SAVED",
                            GlobalCustomResource.PartyMasterSaved, true);
                        clearForm();
                        DivAction = false;
                        BindData(BindType.List);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
                    }
                    else
                    {
                        CustomMessageControl.MessageBodyText = GlobalCustomResource.PartyMasterFailed;
                        CustomMessageControl.MessageType = MessageTypes.Error;
                        CustomMessageControl.ShowMessage();
                        AuditLog.LogEvent(UserContext, SysEventType.INFO, "PARTYMASTER UPDATE FAILED",
                            GlobalCustomResource.PartyMasterFailed, true);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageControl.MessageBodyText = ex.Message;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            uplView.Update();


        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Code = txtSearch.Text;
            BindData(BindType.List);
        }       
    }
}