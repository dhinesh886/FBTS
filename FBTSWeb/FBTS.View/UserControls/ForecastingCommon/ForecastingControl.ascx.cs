using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.Model.Transaction.Accounts;
using FBTS.Model.Transaction.Transactions;
using System;
using System.Web.UI.WebControls;
using System.Linq;

namespace FBTS.View.UserControls.Common
{
    public partial class ForecastingControl : System.Web.UI.UserControl
    {
        private readonly GenericManager _genericClass = new GenericManager();
        public readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (IsPostBack) return;
            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.WHCType),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextLocation),
                            new KeyValuePairItem(Constants.masterType,Constants.TableAccounts)
                        };
            _genericClass.LoadDropDown(ddlRequestorLocation, filter, null, UserContext.DataBaseInfo);
           
            filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.WHCType),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextLocation),
                            new KeyValuePairItem(Constants.masterType,Constants.TableAccounts)
                        };
            _genericClass.LoadDropDown(ddlBillLocation, filter, null, UserContext.DataBaseInfo);

            fillCustomerDealer();
        }
        public void fillCustomerDealer()
        {
            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.Customers),
                            new KeyValuePairItem(Constants.filter2, DealerCustomer),
                            new KeyValuePairItem(Constants.filter3, RequestorLocation),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextCustomer),
                            new KeyValuePairItem(Constants.masterType,Constants.TableAccounts)
                        };
            _genericClass.LoadDropDown(ddlCustomer, filter, null, UserContext.DataBaseInfo);
        }
        #region Genaral
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        #endregion
        #region Properties     

        public string Id
        {
            get { return txtId.Text.Trim(); }
            set { txtId.Text = value.Trim(); }
        }       
        public string SubID
        {
            get { return txtSubId.Text.Trim(); }
            set { txtSubId.Text = value.Trim(); }
        }
        
        public DateTime Date
        {
            get { return Dates.ToDateTime(txtDate.Text.Trim(), DateFormat.Format_01); }
            set
            {
                if (value == Convert.ToDateTime(Constants.DefaultDate))
                    txtDate.Text = Dates.FormatDate(DateTime.Now, Constants.Format02);
                else
                    txtDate.Text = Dates.FormatDate(value, Constants.Format02);               
            }
        }
        public DateTime ProcessingDate
        {
            get { return Dates.ToDateTime(hidProcessingDate.Value.ToTrimString(), DateFormat.Format_01); }
            set
            {
                if (value == Convert.ToDateTime(Constants.DefaultDate))
                    hidProcessingDate.Value = Dates.FormatDate(DateTime.Now, Constants.Format02);
                else
                    hidProcessingDate.Value = Dates.FormatDate(value, Constants.Format02);
            }
        }
        public string RequestorLocation
        {
            get { return ddlRequestorLocation.SelectedValue.ToTrimString(); }
            set { ddlRequestorLocation.SelectedValue = value.ToTrimString(); }
        }
        public string DealerCustomer
        {
            get { return ddlCDType.SelectedValue.ToTrimString(); }
            set { ddlCDType.SelectedValue = value.ToTrimString(); }
        }
        public string Customer
        {
            get { return ddlCustomer.SelectedValue.ToTrimString(); }
            set { ddlCustomer.SelectedValue = value.ToTrimString(); }
        }
        public string Amdno
        {
            get { return hidAmdno.Value.ToTrimString() == string.Empty ? "0" : hidAmdno.Value.ToTrimString(); }
            set { hidAmdno.Value = value.ToTrimString(); }
        }
        public string BillStatus
        {
            get { return ddlBillStatus.SelectedValue.ToTrimString(); }
            set { WebControls.SetCurrentComboIndex(ddlBillStatus, value); }
        }
        public string CurrentStatus
        {
            get { return ddlCurrentStatus.SelectedValue.ToTrimString(); }
            set {
                WebControls.SetCurrentComboIndex(ddlCurrentStatus, value);
                //if (value.Trim() == string.Empty) return;
                //ddlCurrentStatus.SelectedValue = value.Trim(); 
            }
        }
        public string BillLocation { get { return ddlBillLocation.SelectedValue.ToTrimString(); } set { ddlBillLocation.SelectedValue = value.ToTrimString(); } }
        public string PONumber
        {
            get { return txtPoNumber.Text.ToTrimString(); }
            set { txtPoNumber.Text = value.ToTrimString(); }
        }
        public string GSTValue
        {
            get { return lblGSTValue.InnerText.ToTrimString(); }
            set { lblGSTValue.InnerText = value.ToTrimString(); }
        }
        #endregion
        #region Change Label
        public string SubIdLabel
        {
            set
            {
                lblSubId.InnerText = value.Trim();
                txtSubId.Attributes.Add("placeholder", value.Trim());
            }
        }
        public string IdLabel
        {
            set
            {
                lblId.InnerText = value.Trim();
                txtId.Attributes.Add("placeholder", value.Trim());
            }
        }
        public string DateLabel
        {
            set { lblDate.InnerText = value.Trim(); }
        }
        #endregion
        #region Visibality
        public bool IsVisiableBillStatus
        {
            get
            {
                if (divBillStatus.Style["display"] == "block")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    divBillStatus.Style.Add("display", "block");
                }
                else
                {
                    divBillStatus.Style.Add("display", "none");
                }
            }
        }
        public bool IsVisiableCurrentStatus
        {
            get
            {
                if (divCurrentStatus.Style["display"] == "block")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    divCurrentStatus.Style.Add("display", "block");
                }
                else
                {
                    divCurrentStatus.Style.Add("display", "none");
                }
            }
        }
        public bool IsVisiableBillLocation
        {
            get
            {
                if (divBillLocation.Style["display"] == "block")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    divBillLocation.Style.Add("display", "block");
                }
                else
                {
                    divBillLocation.Style.Add("display", "none");
                }
            }
        }
        public bool IsVisiableSubId
        {
            get
            {
                if (divSubId.Style["display"] == "block")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    divSubId.Style.Add("display", "block");
                    divCustomer.Attributes.Add("class", "col-md-4");
                    divLocation.Attributes.Add("class", "col-md-2");
                }
                else
                {
                    divSubId.Style.Add("display", "none");
                    divCustomer.Attributes.Add("class", "col-md-5");
                    divLocation.Attributes.Add("class", "col-md-3");
                }
            }
        }
        public bool IsVisiableCDType
        {
            get
            {
                if (divCDType.Style["display"] == "block")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    divCDType.Style.Add("display", "block");
                    divCustomer.Attributes.Add("class", "col-md-4");
                    divLocation.Attributes.Add("class", "col-md-2");
                }
                else
                {
                    divCDType.Style.Add("display", "none");
                    divCustomer.Attributes.Add("class", "col-md-5");
                    divLocation.Attributes.Add("class", "col-md-3");
                }
            }
        }
        public bool IsVisiablePONumber
        {
            get
            {
                if (divPonumber.Style["display"] == "block")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    divPonumber.Style.Add("display", "block");
                }
                else
                {
                    divPonumber.Style.Add("display", "none");
                }
            }
        }
        public bool IsVisibleGSTNumber
        {
            get
            {
                if (divGST.Style["display"] == "block")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    divGST.Style.Add("display", "block");
                }
                else
                {
                    divGST.Style.Add("display", "none");
                }
            }
        }


        #endregion
        #region Enabaling
        public bool IsEnableID
        {
            set { txtId.Enabled = value; }
        }
        public bool IsEnableSubId
        {
            set { txtSubId.Enabled = value; }
            get {return txtSubId.Enabled; }
        }
        public bool IsEnableDate
        {
            set { txtDate.Enabled = value; }
        }
        public bool IsEnableCustomer
        {
            set
            {                
                ddlCustomer.Enabled = value;
                if (value)
                {                   
                    ddlCustomer.CssClass = "form-control chosen";
                }
                else
                {                    
                    ddlCustomer.CssClass = "form-control";
                }
            }
        }
        public bool IsEnableLocation
        {
            set
            {
                ddlRequestorLocation.Enabled = value;
                ddlCustomer.Enabled = value;
                if (value)
                {
                    ddlRequestorLocation.CssClass = "form-control chosen";
                    ddlCustomer.CssClass = "form-control chosen";
                }
                else
                {
                    ddlRequestorLocation.CssClass = "form-control";
                    ddlCustomer.CssClass = "form-control";
                }
            }
        }
        public bool IsEnableBillLocation
        {
            set
            {
                ddlBillLocation.Enabled = value;
                if (value)
                    ddlBillLocation.CssClass = "form-control chosen";
                else
                    ddlBillLocation.CssClass = "form-control";
            }
        }
        public bool IsEnableBillStatus
        {
            set {
                ddlBillStatus.Enabled = value;
                if (value)
                {
                    ddlBillStatus.CssClass = "form-control chosen";
                }
                else
                {
                    ddlBillStatus.CssClass = "form-control";
                }
            }
        }
        public bool IsEnablePONumber
        {
            set { txtPoNumber.Enabled = value; }
        }
        public bool IsEnableCDType
        {
            set { ddlCDType.Enabled = value; }
        }
        #endregion

        #region AutopostBox Set
        public bool IsCurrentStatusAutopostBack
        {
            set { ddlCurrentStatus.AutoPostBack = value; }
        }
        public bool SetRequestLoctionAutopostBack
        {
            set { ddlRequestorLocation.AutoPostBack = value; }
        }
        #endregion
        #region Compont
        public DropDownList BillStatusDdl
        {
            get { return ddlBillStatus; }
        }
        public DropDownList CurrentStatusDdl
        {
            get { return ddlCurrentStatus; }
        }
        #endregion
        public OrderHead GetData()
        {
            var orderHead = new OrderHead
            {
                OrderNumber = Id,
                RelatedSR = IsVisiableSubId ? SubID : string.Empty,
                OrderAmendmentNumber = Amdno,
                PONumber=PONumber,
                OrderDate = Date,
                ProcessingDate=ProcessingDate,
                WarehouseFrom = RequestorLocation,
                Customer = new Account
                {
                    SName = Customer,
                    LMode = DealerCustomer,
                },
                BillStatus = BillStatus,
                CurrentStatus = CurrentStatus,
                WarehouseTo = BillLocation,

            };
            return orderHead;
        }
        public void SetData(OrderHead orderHead)
        {
            Id = orderHead.OrderNumber;
            SubID = orderHead.RelatedSR;
            Amdno = orderHead.OrderAmendmentNumber;
            PONumber = orderHead.PONumber;
            Date = orderHead.OrderDate;
            ProcessingDate = orderHead.ProcessingDate;
            RequestorLocation = orderHead.WarehouseFrom;
            DealerCustomer = orderHead.Customer.LMode;
            fillCustomerDealer();
            Customer = orderHead.Customer.SName;
            loadGST();
            BillStatus = orderHead.BillStatus;
            CurrentStatus = orderHead.CurrentStatus;
            BillLocation = orderHead.WarehouseTo;            
        }
        public void clearData()
        {
            Id = string.Empty;
            SubID = string.Empty;
            Date = DateTime.Now;
            ProcessingDate = DateTime.Now;
            RequestorLocation = string.Empty;
            fillCustomerDealer();
            Customer = string.Empty;
            Amdno = "0";
            PONumber = string.Empty;
            ddlCurrentStatus.SelectedIndex = 0;
            ddlBillStatus.SelectedIndex = 0;            
            BillLocation = string.Empty;
            GSTValue = string.Empty;
        }
        public delegate void OnSelectedIndexChanged(object sender, EventArgs e);
        public event OnSelectedIndexChanged ddlCurrenStatusSelectedIndex;
        protected void ddlCurrentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCurrenStatusSelectedIndex == null) return;
            ddlCurrenStatusSelectedIndex(sender, e);
        }

        protected void ddlCDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCustomerDealer();
        }

        protected void ddlRequestorLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCustomerDealer();
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadGST();
        }

        public void loadGST()
        {
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = Customer,
                filter4 = Constants.RetriveForm,
                FilterKey = Constants.TableAccounts
            };
            var accounts = _controlPanel.GetAccounts(queryArgument);
            var firstOrDefault = accounts.FirstOrDefault();
            if (firstOrDefault == null) return;
            if (string.IsNullOrEmpty(firstOrDefault.Address.GST))
            {
                lblGSTValue.InnerText = firstOrDefault.Address.GSTNAReason;
            }
            else
            {
                lblGSTValue.InnerText = firstOrDefault.Address.GST;
            }
        }
    }
}