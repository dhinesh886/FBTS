using FBTS.Model.Common;
using FBTS.Model.Transaction.Accounts;
using System;

namespace FBTS.Model.Transaction.Transactions
{
    [Serializable]
    public class OrderHead:Operations
    {
        public OrderHead()
        {
            
            OrderNumber = string.Empty;
            RelatedSR = string.Empty;
            OrderAmendmentNumber = "0";
            OrderDate = Convert.ToDateTime(Constants.DefaultDate);
            ProcessingDate = Convert.ToDateTime(Constants.DefaultDate);
            Customer = new Account();
            WarehouseFrom = string.Empty;
            WarehouseTo = string.Empty;
            BillStatus = string.Empty;
            CurrentStatus = string.Empty;
            NetValue = 0;
            Off = string.Empty;
            PONumber = string.Empty;
            NotOkStatus = string.Empty;
        }

       
        public string OrderNumber { get; set; }
        public string RelatedSR { get; set; }        
        public string OrderAmendmentNumber { get; set; } 
        public DateTime OrderDate { get; set; }
        public DateTime ProcessingDate { get; set; }
        public double Aging { get { return (DateTime.Now.Date - ProcessingDate).TotalDays; } }
        public string PONumber { get; set; }
        public string WarehouseFrom { get; set; }
        public string WarehouseTo { get; set; }
        public string BillStatus { get; set; }
        public string CurrentStatus { get; set; }
        public Account Customer { get; set; }       
        public decimal NetValue { get; set; }
        public string NotOkStatus { get; set; }
        public string VrfRemark { get; set; }
        public string Off { get; set; }
        public string ModalityDesp { get; set; }
        public string OrdRemark { get; set; }
    }
}
