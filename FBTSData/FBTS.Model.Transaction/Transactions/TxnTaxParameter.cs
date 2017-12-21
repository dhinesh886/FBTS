using System;
using FBTS.Model.Common;

namespace FBTS.Model.Transaction
{
    [Serializable] 
    public class TxnTaxParameter : Operations
    {
        public string ParaCode { get; set; }
        public string Description { get; set; } 
        public Decimal Percentage { get; set; }
        public Decimal Amount { get; set; }
    }
}
