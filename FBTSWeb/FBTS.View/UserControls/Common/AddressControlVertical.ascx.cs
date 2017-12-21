using FBTS.Business.Manager;
using FBTS.Library.Statemanagement;
using FBTS.Library.WebControls;
using FBTS.Model.Common;
using FBTS.Model.Control;
using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using FBTS.Library.Common;

namespace FBTS.View.UserControls.Common
{
    public partial class AddressControlVertical : System.Web.UI.UserControl
    {
        private readonly GenericManager _genericClass = new GenericManager();
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        public bool HideMobileField
        {
            set
            {
                divMobile.Style.Add("display", "none");
            }
        }
        public bool HideWebSite
        {
            set
            {
                divWww.Style.Add("display", "none");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.CountryType),
                            new KeyValuePairItem(Constants.DdldefaultText,Constants.DdlDefaultTextCountry),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlCountry)
                        };
            _genericClass.LoadDropDown(ddlCountry, filter, null, UserContext.DataBaseInfo);
        }
        public string Address { get { return txtStreet.Text; } set { txtStreet.Text = value; } }
        public string City
        {
            get
            {
                if (ddlLocation.Visible)
                    return ddlLocation.SelectedValue.Trim();
                else
                    return txtCity.Text.Trim();
            }
            set
            {
                if (ddlLocation.Visible)
                    FBTS.Library.WebControls.WebControls.SetCurrentComboIndex(ddlLocation, value);
                else
                    txtCity.Text = value;
            }
        }
        public string Country { get { return ddlCountry.SelectedValue.Trim(); } set { ddlCountry.SelectedValue = value.Trim(); } }
        public string Website { get { return txtWww.Text; } set { txtWww.Text = value; } }
        public string State
        {
            get
            {
                return ddlState.SelectedValue.Trim();
            }
            set
            {
                var filter = new KeyValuePairItems
                        {
                           new KeyValuePairItem(Constants.filter1, Constants.CountryType),
                            new KeyValuePairItem(Constants.filter2, Country),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextState),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlCatHeaderData)
                        };
                _genericClass.LoadDropDown(ddlState, filter, null, UserContext.DataBaseInfo);
                if (ddlState.Items.Count > 1)
                    ddlState.SelectedValue = value.Trim();
            }
        }

        public string ZipCode { get { return txtPostCode.Text; } set { txtPostCode.Text = value; } }
        public string Mobile { get { return txtMobile.Text; } set { txtMobile.Text = value; } }
        public string Email { get { return txtEmail.Text; } set { txtEmail.Text = value; } }
        public string Phone { get { return txtOffPhone.Text; } set { txtOffPhone.Text = value; } }
        public Address DataSource
        {
            get
            {
                return GetData();
            }
            set
            {
                BindData(value);
            }
        }
        public bool IsVisiableLocationDdl
        {
            set
            {
                ddlLocation.Visible = value;
                txtCity.Visible = !value;
            }
        }
        public bool IsVisiableGSTDiv
        {
            set
            {
                if (value)
                    divGST.Style.Add("display", "block");
                else
                    divGST.Style.Add("display", "none");
            }
        }
        public bool IsVisiableGSTReasonDiv
        {
            set
            {
                if (value)
                    divGSTReason.Style.Add("display", "block");
                else
                    divGSTReason.Style.Add("display", "none");
            }
        }
        public string SetCityLabel
        {
            set { lblCity.InnerText = value.Trim(); }
        }
        public DropDownList LocationControl
        {
            get { return ddlLocation; }
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
        public string GSTAvailability
        {
            get { return ddlGST.SelectedValue.ToTrimString(); }
            set { FBTS.Library.WebControls.WebControls.SetCurrentComboIndex(ddlGST, value); }
        }
        public string GST
        {
            get { return txtGST.Text.ToTrimString(); }
            set { txtGST.Text = value.ToTrimString(); }
        }
        public string GSTNAReason
        {
            get { return ddlGSTReason.SelectedValue.ToTrimString(); }
            set { FBTS.Library.WebControls.WebControls.SetCurrentComboIndex(ddlGSTReason, value); }
        }
        private void BindData(Address address)
        {
            Address = address.Street;
            City = address.City;
            Country = address.Country;
            State = address.State;
            ZipCode = address.ZipCode;
            Mobile = address.Mobile;
            Email = address.Email;
            Phone = address.Phone;
            Website = address.WebSite;
            if (string.IsNullOrEmpty(address.GST))
            {
                GSTAvailability = "N";                
                IsVisiableGSTDiv = false;
                IsVisiableGSTReasonDiv = true;               
            }
            else
            {
                GSTAvailability = "Y";
                IsVisiableGSTReasonDiv = false;
                IsVisiableGSTDiv = true;
            }
            GST = address.GST;
            GSTNAReason = address.GSTNAReason;
            uplAddress.Update();
        }
        private Address GetData()
        {
            var address = new Address
            {
                Street = Address,
                City = City,
                State = State,
                Country = Country,
                ZipCode = ZipCode,
                Mobile = Mobile,
                Email = Email,
                Phone = Phone,
                WebSite = Website,
                GST = GST,
                GSTNAReason = GSTNAReason
            };
            return address;
        }
        public void Clear()
        {
            Address = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Country = string.Empty;
            ZipCode = string.Empty;
            Mobile = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Website = string.Empty;
            GSTAvailability = "Y";
            GST = string.Empty;
            GSTNAReason = string.Empty;
            IsVisiableGSTReasonDiv = false;
            IsVisiableGSTDiv = true;
            uplAddress.Update();
        }
        protected void ddlGST_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GSTAvailability == "Y")
            {                
                IsVisiableGSTReasonDiv = false;
                IsVisiableGSTDiv = true;
            }
            else if (ddlGST.SelectedValue == "N")
            {
                IsVisiableGSTDiv = false;
                IsVisiableGSTReasonDiv = true;   
            }
            else
            {
                IsVisiableGSTDiv = false;
                IsVisiableGSTReasonDiv = false;
            }
        }
    }
}