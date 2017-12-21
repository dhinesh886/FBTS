using FBTS.Model.Common;
using FBTS.Model.Transaction.Transactions;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace FBTS.View.UserControls.ForecastingCommon
{
    public partial class ValidationDdl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region
        public string PONumber
        {
            set { ddlPONumber.SelectedValue = value.Trim(); }
            get { return ddlPONumber.SelectedValue.Trim(); }
        }
        public string Validity
        {
            set { ddlValidity.SelectedValue = value.Trim(); }
            get { return ddlValidity.SelectedValue.Trim(); }
        }
        public string BillToAddress
        {
            set { ddlBillToAddress.SelectedValue = value.Trim(); }
            get { return ddlBillToAddress.SelectedValue.Trim(); }
        }
        public string ShipToAddress
        {
            set { ddlShipToAddress.SelectedValue = value.Trim(); }
            get { return ddlShipToAddress.SelectedValue.Trim(); }
        }
        public string SealAndSign
        {
            set { ddlSeal_Sign.SelectedValue = value.Trim(); }
            get { return ddlSeal_Sign.SelectedValue.Trim(); }
        }
        public string Margin
        {
            set { ddlMargin.SelectedValue = value.Trim(); }
            get { return ddlMargin.SelectedValue.Trim(); }
        }
        public string AccountReceivableStatus
        {
            set { ddlAccountReceivableStatus.SelectedValue = value.Trim(); }
            get { return ddlAccountReceivableStatus.SelectedValue.Trim(); }
        }
        public string PaymentStatus
        {
            set { ddlPaymentStatus.SelectedValue = value.Trim(); }
            get { return ddlPaymentStatus.SelectedValue.Trim(); }
        }
        public string PaymentTerms
        {
            set { ddlPaymentTerms.SelectedValue = value.Trim(); }
            get { return ddlPaymentTerms.SelectedValue.Trim(); }
        }
        public string PaymentDetails
        {
            set { ddlPaymentDetails.SelectedValue = value.Trim(); }
            get { return ddlPaymentDetails.SelectedValue.Trim(); }
        }
        public string GSTNumber
        {
            set { ddlGST.SelectedValue = value.Trim(); }
            get { return ddlGST.SelectedValue.Trim(); }
        }
        #endregion
        public void makeEnable(DropDownList ddl, bool value)
        {
            ddl.Enabled = value;
            if(value)
                ddl.CssClass = "form-control chosen";
            else
                ddl.CssClass = "form-control";
        }
        
        public bool IsEnableAll
        {
            set
            {
                makeEnable(ddlPONumber, value);
                makeEnable(ddlValidity, value);
                makeEnable(ddlBillToAddress, value);
                makeEnable(ddlShipToAddress, value);
                makeEnable(ddlSeal_Sign, value);
                makeEnable(ddlMargin, value);
                makeEnable(ddlAccountReceivableStatus, value);
                makeEnable(ddlPaymentStatus, value);
                makeEnable(ddlPaymentTerms, value);
                makeEnable(ddlPaymentDetails, value);
                makeEnable(ddlGST, value);
            }
        }
        public void clearForm()
        {
            PONumber = Constants.StringOk;
            Validity = Constants.StringOk;
            BillToAddress = Constants.StringOk;
            ShipToAddress = Constants.StringOk;
            SealAndSign = Constants.StringOk;
            Margin = Constants.StringOk;
            AccountReceivableStatus = Constants.StringOk;
            PaymentStatus = Constants.StringOk;
            PaymentTerms = Constants.StringOk;
            PaymentDetails = Constants.StringOk;
            GSTNumber = Constants.StringOk;
        }
        public BillValiditions GetData()
        {
            var billValiditions = new BillValiditions();

            billValiditions.Add(AddData(PONumber, "01"));
            billValiditions.Add(AddData(Validity, "02"));
            billValiditions.Add(AddData(BillToAddress, "03"));
            billValiditions.Add(AddData(ShipToAddress, "04"));
            billValiditions.Add(AddData(SealAndSign, "05"));
            billValiditions.Add(AddData(Margin, "06"));
            billValiditions.Add(AddData(AccountReceivableStatus, "07"));
            billValiditions.Add(AddData(PaymentStatus, "08"));
            billValiditions.Add(AddData(PaymentTerms, "09"));
            billValiditions.Add(AddData(PaymentDetails, "10"));
            billValiditions.Add(AddData(GSTNumber, "11"));

            return billValiditions;
        }
        public BillValidition AddData(string value,string id)
        {
            var billValidation = new BillValidition();
            billValidation.ReferanceId = id;
            billValidation.ReferenceValue = value;
            return billValidation;
        }
        public string getValue(BillValiditions billValiditions,string id)
        {
            string str = string.Empty;
            str = billValiditions.Where(x => x.ReferanceId.Trim() == id.Trim()).Any() ? billValiditions.Where(x => x.ReferanceId.Trim() == id.Trim()).FirstOrDefault().ReferenceValue.Trim() :
                                                                                           string.Empty;
            return str;
        }
       
        public void SetData(BillValiditions billValiditions)
        {
            PONumber = getValue(billValiditions, "01");
            Validity = getValue(billValiditions, "02");
            BillToAddress = getValue(billValiditions, "03");
            ShipToAddress = getValue(billValiditions, "04");
            SealAndSign = getValue(billValiditions, "05");
            Margin = getValue(billValiditions, "06");
            AccountReceivableStatus = getValue(billValiditions, "07");
            PaymentStatus = getValue(billValiditions, "08");
            PaymentTerms = getValue(billValiditions, "09");
            PaymentDetails = getValue(billValiditions, "10");
            GSTNumber = getValue(billValiditions, "11");
        }
    }
}