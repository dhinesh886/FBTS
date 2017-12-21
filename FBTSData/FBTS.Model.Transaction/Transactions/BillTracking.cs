using FBTS.Model.Common;
using FBTS.Model.Transaction.Enum;
using System;
using System.Linq;

namespace FBTS.Model.Transaction.Transactions
{
    [Serializable]
    public class BillTracking : Operations
    {
        public BillTracking()
        {
            OrderType = string.Empty;
            OrdNumber = string.Empty;
            Head = new OrderHead();
            BillValiditions = new Transactions.BillValiditions();
            Details = new BillTrackingDetails();
            References = new TxnReferences();
            StageId = string.Empty;
            Off = string.Empty;
            Bu = string.Empty;
            Branch = string.Empty;
            FormType = FormType.Verification;
            UpdatePrevHead = new OrderHead();
        }
        public FormType FormType { get; set; }
        public string OrderType { get; set; }
        public string OrdNumber { get; set; }
        public string OrderNumber1 { get; set; }
        public OrderHead Head { get; set; }
        public BillValiditions BillValiditions { get; set; }
        public BillTrackingDetails Details { get; set; }
        public TxnReferences References { get; set; }
        public OrderHead UpdatePrevHead { get; set; }
        public BillTrackingDetails UpdatePrevDetails { get; set; }
        public string StageId { get; set; }
        public string Off { get; set; }
        public string Bu { get; set; }
        public string Branch { get; set; }
        public Guid LogedUser { get; set; }
        public string LoggedUserName { get; set; }
        public DateTime UpdatedDate { get { return DateTime.Now; } }
        public decimal GrossValue { get { return Details.Sum(x => x.BillValue); } }
        public decimal POValue { get; set; }
    }
}
