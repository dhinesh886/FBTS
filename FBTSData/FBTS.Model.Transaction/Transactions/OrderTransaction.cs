using FBTS.Model.Common;
using FBTS.Model.Transaction.Enum;
using FBTS.Model.Transaction.Transactions;
using System;

namespace FBTS.Model.Transaction
{
     [Serializable]
    public class OrderTransaction : Operations
    {
        public OrderTransaction()
        {
            OrderType = string.Empty;
            orderHead = new OrderHead();
            orderDetails = new OrderDetails();
            References= new TxnReferences();
            StageId = string.Empty;
            Off = string.Empty;
            Bu = string.Empty;
            Branch = string.Empty;
            FormType = FormType.Fresh;           
        }
        public FormType FormType { get; set; }
        public string OrderType { get; set; }
        public OrderHead orderHead { get; set; }       
        public OrderDetails orderDetails { get; set; }
        public TxnReferences References { get; set; }
        public OrderDetails updateOrderDeatils { get; set; }
        public Guid LogedUser { get; set; }
        public string StageId { get; set; }
        public string Off { get; set; }
        public string Bu { get; set; }
        public string Branch { get; set; }
        public DateTime UpdatedDate { get { return DateTime.Now; } }
        public string LoggedUserName { get; set; }
    }
}
