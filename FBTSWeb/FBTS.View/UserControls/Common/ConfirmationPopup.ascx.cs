using System;
using System.Text;
using System.Web.UI;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.Library.Statemanagement;
using FBTS.View.Resources.ResourceFiles;
using FBTS.Model.Common.CustomEventArgs;
using FBTS.Library.Common;

namespace Ezy.ERP.View.UserControls.Common
{
    public partial class ConfirmationPopup : UserControl//, IConfirmationPopup
    {
        private string _messageBodyText = string.Empty;
        private string _messageHeaderText = string.Empty;

        public event EventHandler YesClicked = null;
        public event EventHandler NoClicked = null;
        public event EventHandler OkClicked = null;

        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        public string Key
        {
            get { return hdnKey1.Value; }
            set { hdnKey1.Value = value; }
        }
        public string SecondaryKey
        {
            get { return hdnKey2.Value; }
            set { hdnKey2.Value = value; }
        }
        public string MessageHeaderText
        {
            get { return string.IsNullOrEmpty(_messageHeaderText) ? "Confirmation" : _messageHeaderText; }
            set { _messageHeaderText = value; uplConfMsg.Update(); }
        }

        public string MessageBodyText
        {
            get { return _messageBodyText; }
            set { _messageBodyText = value; }
        }
        public string Remark
        {
            get { return IsVisiableTextRemark ? txtRemark.Text.ToTrimString() : string.Empty; }
            set
            {
                if (IsVisiableTextRemark)
                    txtRemark.Text = value.ToTrimString();
            }
        }

        public MessageTypes MessageType
        {
            get;
            set;
        }

        public string SetCancelControlId
        {
            set
            {
                mpeMessageBox.CancelControlID = value;
            }
        }
        public bool visiableOK
        {
            get;
            set;
        }
        public bool IsVisiableOkButton
        {
            set
            {
                if (value)
                {
                    divActionOk.Style.Add("display", "block");
                    divActionsYesNo.Style.Add("display", "none");
                }
                else
                {
                    divActionOk.Style.Add("display", "none");
                    divActionsYesNo.Style.Add("display", "block");
                }
            }
        }
        public bool IsVisiableTextRemark
        {
            get { return divRemark.Visible; }
            set { divRemark.Visible = value; }
        }

        public KeyValuePairItems DataSource { get; set; }

        public void Show()
        {
            BindData();
            mpeMessageBox.Show();
            uplConfMsg.Update();
        }
        private void setProperties(string msgHeadding, string msgBodyText, bool isVisiableOkButton, MessageTypes messageType)
        {
            MessageHeaderText = msgHeadding;
            MessageBodyText = msgBodyText;
            IsVisiableOkButton = isVisiableOkButton;
            MessageType = messageType;
        }
        private void BindData()
        {
            if (DataSource != null)
            {
                var body = new StringBuilder();
                foreach (var item in DataSource)
                {
                    body.AppendFormat(GlobalCustomResource.DataRow, item.Key, item.Value);
                }
                divPopupBody.InnerHtml = body.ToString();
            }
            switch (MessageType)
            {
                case MessageTypes.Confirm:
                    {
                        secHeader.InnerHtml = "<i class='fa fa-question'></i>&nbsp;&nbsp;" + MessageHeaderText;
                        break;
                    }
                default:
                    {
                        secHeader.InnerHtml = MessageHeaderText;
                        break;
                    }
            }
            divMessageBodyText.InnerHtml = MessageBodyText;
        }
        public void Show(string msgHeadding, string msgBodyText, bool isVisiableOkButton, MessageTypes messageType = MessageTypes.None)
        {
            setProperties(msgHeadding, msgBodyText, isVisiableOkButton, messageType);
            Show();
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            if (YesClicked != null)
            {
                var eventArgs = new CommonEventArgs { Key = Key, SecondaryKey = SecondaryKey };
                YesClicked(sender, eventArgs);
                mpeMessageBox.Hide();
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            if (NoClicked != null)
            {
                var eventArgs = new CommonEventArgs { Key = Key, SecondaryKey = SecondaryKey };
                NoClicked(sender, eventArgs);
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (OkClicked != null)
            {
                var eventArgs = new CommonEventArgs { Key = Key, SecondaryKey = SecondaryKey };
                OkClicked(sender, eventArgs);
            }
        }
    }
}