using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FBTS.Library.Common;
using System.Text;
using FBTS.View.Resources.ResourceFiles;
using FBTS.Business.Manager;

namespace FBTS.View.UserControls.Common
{
    public partial class DeleteAndBanPopup : UserControl
    {
        private string _messageBodyText = string.Empty;
        private string _messageHeaderText = string.Empty;
        private readonly GenericManager _genericClass = new GenericManager();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region Session
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        #endregion

        #region Hiddan field
        public string Type
        {
            get { return hdnType.Value; }
            set { hdnType.Value = value; }
        }
        public string Action
        {
            get { return hdnAction.Value; }
            set { hdnAction.Value = value; }
        }
        public string Key1
        {
            get { return hdnKey1.Value; }
            set { hdnKey1.Value = value; }
        }
        public string Key2
        {
            get { return hdnKey2.Value; }
            set { hdnKey2.Value = value; }
        }
        public string Key3
        {
            get { return hdnKey3.Value; }
            set { hdnKey3.Value = value; }
        }
        #endregion

        #region Proprties
        public string MessageHeaderText
        {
            get { return string.IsNullOrEmpty(_messageHeaderText) ? "Confirmation" : _messageHeaderText; }
            set { _messageHeaderText = value; Refresh(); }
        }
        public string QueryType
        {
            get { return hdnQueryType.Value.ToTrimString(); }
            set { hdnQueryType.Value = value; }
        }
        public string MessageBodyText
        {
            get { return _messageBodyText; }
            set { _messageBodyText = value; }
        }

        public MessageTypes MessageType
        {
            get;
            set;
        }
        public KeyValuePairItem Status
        {
            get;
            set;
        }
        public KeyValuePairItems DataSource { get; set; }
        #endregion

        private void Refresh()
        {
            uplConfMsg.Update();
        }

        public void Show()
        {
            BindData();
            Refresh();
            ShowMessage();
        }
        public void ShowMessage()
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "FBTSTransactionDeleteAlert", "showConfirmation()", true);
        }
        private void setProperties(string msgHeadding, string msgBodyText, MessageTypes messageType)
        {
            MessageHeaderText = msgHeadding;
            MessageBodyText = msgBodyText;
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
                divMessageBodyText.InnerHtml = body.ToString();
            }
            else
            {
                var message = MessageBodyText;
                if (string.IsNullOrEmpty(message))
                {
                    var firstOrDefault = Constants.ConfirmMessages.Where(msg => msg.Key == QueryType).FirstOrDefault();
                    if (firstOrDefault != null)
                    {
                        message = string.Format(firstOrDefault.Value, Action == Constants.DeleteAction ? Constants.DeleteText :
                                                                      Action == Constants.UnBanAction ? Constants.UnBanText : Constants.BanText);
                    }
                }                

                divMessageBodyText.InnerHtml = message;
            }
            switch (MessageType)
            {
                case MessageTypes.Confirm:
                    {
                        Header.InnerHtml = "<i class='fa fa-question'></i>&nbsp;&nbsp;" + MessageHeaderText;
                        break;
                    }
                default:
                    {
                        Header.InnerHtml = "<i class='fa fa-question'></i>&nbsp;&nbsp;" + MessageHeaderText;
                        break;
                    }
            }
            Refresh();
        }
        public void Show(string msgHeadding, string msgBodyText, MessageTypes messageType = MessageTypes.None)
        {
            setProperties(msgHeadding, msgBodyText, messageType);
            Show();
        }

        // Delegate declaration
        public delegate void OnButtonClick(object sender, EventArgs e);
        // Event declaration
        public event OnButtonClick YesClicked;
        protected void btnYes_Click(object sender, EventArgs e)
        {          
            var Status = _genericClass.Delete(Key1, Key2, Key3, Action, QueryType, Type, UserContext.DataBaseInfo);
            var customMessageControl = (CustomMessageControl)Parent.FindControl("CustomMessageControl");
            if (customMessageControl == null) return;

            if (Status != null)
            {
                customMessageControl.MessageBodyText = Status.Value;
                customMessageControl.MessageType = (Status.Key == "1" ? MessageTypes.Success : MessageTypes.Error);
            }
            else
            {
                customMessageControl.MessageBodyText = "Could not complete your request. Please try again.";
                customMessageControl.MessageType = MessageTypes.Error;
            }
            customMessageControl.ShowMessage();
            if (YesClicked != null)
            {
                YesClicked(sender, e);
            }
        }
    }
}