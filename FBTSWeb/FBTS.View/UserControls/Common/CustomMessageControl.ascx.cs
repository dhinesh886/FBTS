using System.Web.UI;
using FBTS.Model.Common;

namespace FBTS.View.UserControls.Common
{
    public partial class CustomMessageControl : UserControl
    {
        private string _messageBodyText = string.Empty;
        private string _messageHeaderText = string.Empty;
        private MessageTypes _messageType = MessageTypes.None;

        public string MessageHeaderText
        {
            get { return string.IsNullOrEmpty(_messageHeaderText) ? Constants.DefaultMessageHeader : _messageHeaderText.Replace(Constants.SpecialCharSinglequote, string.Empty); }
            set { _messageHeaderText = value; }
        }

        public string MessageBodyText
        {
            get { return string.IsNullOrEmpty(_messageBodyText) ? "Oops! No message to display." : _messageBodyText.Replace(Constants.SpecialCharSinglequote, string.Empty); }
            set { _messageBodyText = value; }
        }

        public MessageTypes MessageType
        {
            get { return _messageType; }
            set { _messageType = value; }
        }

        public void ShowMessage()
        {
            ScriptManager.RegisterStartupScript (Page, typeof(Page), "EzyTransactionAlert",
                string.Format("showCustomMessage('{0}','{1}','{2}')", MessageHeaderText, MessageBodyText, MessageType.ToString()),true);
        } 
    };
}