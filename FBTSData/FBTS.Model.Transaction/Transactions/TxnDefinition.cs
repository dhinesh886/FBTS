using System;
using FBTS.Model.Common;

namespace FBTS.Model.Transaction
{
    [Serializable] 
    public class TxnDefinition : Operations
    {
        public TxnDefinition()
        {
            StockCategory = new StockCategory();
        }
        public string TdCode { get; set; }
        public string TdType { get; set; } 
        public string TdPost { get; set; }
        public string TdCType { get; set; }
        public string TdJType { get; set; }
        public string TdMode { get; set; } 
        public string TdGross { get; set; } 
        public string TdClass { get; set; }
        public string TdBno { get; set; }
        public string TdBno1 { get; set; }
        public string TdStockCatg { get; set; }
        public string TdCode1 { get; set; }
        public StockCategory StockCategory { get; set; }
    }
}
