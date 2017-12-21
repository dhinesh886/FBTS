using FBTS.Model.Common;
using System;

namespace FBTS.Model.Transaction.Transactions
{
    [Serializable]
    public class BillValidition
    {
        public BillValidition()
        {
            ReferanceId = string.Empty;
            ReferenceValue = string.Empty;
        }
        public string ReferanceId { get; set; }
        public string ReferenceValue { get; set; }
    }
}
