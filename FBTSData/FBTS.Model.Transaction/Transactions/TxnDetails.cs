using System;
using FBTS.Model.Common;

namespace FBTS.Model.Transaction
{
    [Serializable]
    public class TxnDetails 
    {
        public string TransactionId { get; set; }
      
        public DateTime TransactionDate { get; set; }
        public string UserId { get; set; }
        public string Bu { get; set; }
        public string Branch { get; set; }
        public string WhFrom { get; set; }
        public string WhTo { get; set; }
        public string Stage { get; set; }
    }
}
