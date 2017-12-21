using FBTS.Library.Common;
using FBTS.Model.Common.CustomEventArgs;
using System;

namespace FBTS.View.UserControls.ForecastingCommon
{
    public partial class RefrenceControl : System.Web.UI.UserControl
    {
        public event EventHandler ParameterValueChanged;
        protected void Page_Load(object sender, EventArgs e)
        {
            
           
        }
        public bool AutoPostback
        {
            set
            {
                txtParameter.AutoPostBack = value;
            }
        }      
        public string ParameterCode { set { hidParaCode.Value = value; } get { return hidParaCode.Value.Trim(); } }
        public object ParameterInputObject
        {
            get { return txtParameter; }
        }
        public string ParameterInput
        {
            get 
            {
                return txtParameter.Text.Trim();
            }
        }
        public string ParameterLabel
        {
            set { lblddlParameter.Text = value.Trim(); }
        }
        public bool IsDateRequired 
        {
            get { return hidIsDate.Value.ToBool(); }
            set 
            {
                hidIsDate.Value = value ? "1" : "0";
                if (value)
                    setCssClass(); 
            }             
        }
        private void setCssClass()
        {
            if (IsDateRequired)
                txtParameter.CssClass = "form-control datepicker";
        }
        public void BindData(string value)
        {
            txtParameter.Text = value.Trim();
        }
        public void txtParameter_TextChanged(object sender, EventArgs e)
        {
            if (ParameterValueChanged == null) return;
            var eventArgs = new CommonEventArgs { Key = txtParameter.Text };
            ParameterValueChanged(sender, eventArgs);
        }
    }
}