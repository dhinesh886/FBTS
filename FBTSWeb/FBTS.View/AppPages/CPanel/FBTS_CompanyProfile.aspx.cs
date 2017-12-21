using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FBTS.Model.Common;
using FBTS.Business.Manager;
using FBTS.Library.Statemanagement;
using FBTS.Model.Control;
using FBTS.App.Library;
using FBTS.View.UserControls.Common;
using FBTS.Library.Common;
using System.Globalization;


namespace FBTS.View.AppPages.CPanel
{
    public partial class FBTS_CompanyProfile : System.Web.UI.Page
    {
        public readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        private readonly GenericManager _genericClass = new GenericManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            SetPageProperties();
            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.CountryType),
                            new KeyValuePairItem(Constants.DdldefaultText,Constants.DdlDefaultTextTrnCurrency),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlCountry)
                        };
            _genericClass.LoadDropDown(ddlTrnCurrency, filter, null, UserContext.DataBaseInfo);

            SessionManagement<UploadDetail>.SetValue(Constants.ImportDataSessionKey, new UploadDetail { IsReady = false });
            //AddressControl.HideMobileField = false;
            if (CType == Constants.CompanyCType || CType == Constants.BuCType)
            {
                secContactDetails.Style.Add("display", "none");
            }
            else
            {
                secTaxDetails.Style.Add("display", "none");
                secFPeriod.Style.Add("display", "none");
                GridViewTable.Columns[3].Visible = false;

                GridViewTable.Columns[4].Visible = false;

            }
            BindData(BindType.List);
        }
        private int _newPageIndex = -1;
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        #region Form Fields
        public string Id { get { return txtId.Text; } set { txtId.Text = value; } }
        public string Name { get { return txtName.Text; } set { txtName.Text = value; } }
        public AddressControlVertical Address
        {
            get { return AddressControl; }
        }
        public string Tin { get { return txtTin.Text; } set { txtTin.Text = value; } }
        public string Cst { get { return txtCst.Text; } set { txtCst.Text = value; } }
        public string Excise { get { return txtExcise.Text; } set { txtExcise.Text = value; } }
        public string ExciseActiveTill { get { return txtEXValidTill.Text; } set { txtEXValidTill.Text = value; } }
        public string TransactionCurrency { get { return ddlTrnCurrency.SelectedValue.Trim(); } set { WebControls.SetCurrentComboIndex(ddlTrnCurrency, value); } }
        public string CreatedDate { get; set; }
        public string FinancialPeriodFrom { get { return txtFPFrom.Text; } set { txtFPFrom.Text = value; } }
        public string FinancialPeriodTo { get { return txtFPTo.Text; } set { txtFPTo.Text = value; } }
        public string ContactPersonName1 { get { return txtContactName1.Text; } set { txtContactName1.Text = value; } }
        public string ContactPersonName2 { get { return txtContactName2.Text; } set { txtContactName2.Text = value; } }
        public string ContactPersonPhone1 { get { return txtContactPhone1.Text; } set { txtContactPhone1.Text = value; } }
        public string ContactPersonPhone2 { get { return txtContactPhone2.Text; } set { txtContactPhone2.Text = value; } }
        public string Logo { get { return hidLogo.Value; } set { hidLogo.Value = value; } }

        public string CType { get { return hidCType.Value; } set { hidCType.Value = value; } }
        public string Key { get { return hidKey.Value; } set { hidKey.Value = value; } }
        public string Action { get { return hidAction.Value; } set { hidAction.Value = value; } }
        public string Keyword { get { return txtSearch.Text; } set { txtSearch.Text = value; } }
        #endregion

        public void BindData(BindType bindType)
        {
            var queryargument = new QueryArgument(UserContext.DataBaseInfo)
            {
                FilterKey = Id,
                BindType = bindType,
                Keyword = Keyword,
                SubFilterKey = CType
            };

            var profiles = _controlPanel.GetCompanyProfiles(queryargument);
            BindData(profiles, bindType);
        }
        public void BindData(CompanyProfiles companyProfiles, BindType bindType)
        {
            if (companyProfiles == null) return;
            if (bindType == BindType.Form)
            {
                var companyProfile = companyProfiles.FirstOrDefault();

                if (companyProfile != null)
                {
                    Id = companyProfile.Id;
                    Name = companyProfile.Name.Trim();

                    Address.Address = companyProfile.Address.Trim();
                    Address.City = companyProfile.City.Trim();
                    Address.Country = companyProfile.Country.Trim();
                    Address.State = companyProfile.State.Trim();
                    Address.ZipCode = companyProfile.Zip.Trim();
                    Address.Email = companyProfile.Email.Trim();
                    //Address.Mobile = companyProfile.Mobile.Trim();
                    Address.Phone = companyProfile.Phone.Trim();
                    Logo = companyProfile.Logo;
                    if (CType == Constants.CompanyCType || CType == Constants.BuCType)
                    {
                        Cst = companyProfile.Tax1.Trim();
                        Tin = companyProfile.Tax2.Trim();
                        Excise = companyProfile.Tax3.Trim();
                        ExciseActiveTill = companyProfile.TaxValidity3.Trim();
                        FinancialPeriodFrom =
                            companyProfile.FinancialYearStart.GetValueOrDefault().ToString(Constants.Format02);
                        FinancialPeriodTo =
                            companyProfile.FinancialYearEnd.GetValueOrDefault().ToString(Constants.Format02);
                        TransactionCurrency = companyProfile.TrnCurrency;
                    }
                    else
                    {
                        ContactPersonName1 = companyProfile.CName;
                        ContactPersonPhone1 = companyProfile.CPhone;
                    }

                }
                lnkSave.Visible = hidAction.Value != Constants.ViewAction;
                uplForm.Update();
            }
            else
            {
                GridViewTable.DataSource = companyProfiles;
                if (_newPageIndex >= 0)
                {
                    GridViewTable.PageIndex = _newPageIndex;
                }
                GridViewTable.DataSource = companyProfiles;
                GridViewTable.DataBind();
                uplView.Update();
            }
        }
        protected void LoadDetails(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            Action = lnkbtn.CommandName;
            Id = lnkbtn.CommandArgument;
            BindData(BindType.Form);
        }
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
        private void SetPageProperties()
        {
            var menuSettings = GeneralUtilities.GetCurrentMenuSettings(UserContext,
                QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            if (menuSettings == null) return;
            pageTitle.InnerText = menuSettings.PageTitle;
            CType = menuSettings.Type;
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {

        }

        protected void GridViewTable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            _newPageIndex = e.NewPageIndex;
            ClearForm();
            BindData(BindType.List);
        }
        private void ClearForm()
        {
            Id = string.Empty;
            Name = string.Empty;
            Address.Clear();
            ExciseActiveTill = string.Empty;
            CreatedDate = string.Empty;
            ExciseActiveTill = string.Empty;
            Excise = string.Empty;
            Tin = string.Empty;
            Cst = string.Empty;
            FinancialPeriodFrom = string.Empty;
            FinancialPeriodTo = string.Empty;
            TransactionCurrency = string.Empty;
            hidAction.Value = Constants.InsertAction;
            uplForm.Update();
        }

        protected void GridViewTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            var gv = sender as GridView;
            var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
            if (gv != null && lbl != null)
                lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);
        }
    }
}