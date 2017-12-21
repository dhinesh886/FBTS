using System;
using FBTS.Model.Control;

namespace FBTS.Model.Transaction
{
    public class TxnMaterial : Material
    {
        public string Vendor { get; set; }
        public string VendorName { get; set; }
        public string VendorACode { get; set; }
        public string VendorCategory { get; set; }

        public string RequiredUom { get; set; }
        public decimal Quantity { get; set; }
        public decimal PendingQuantity { get; set; }
        public decimal UtilizedQuantity { get; set; } 
        public decimal GrossValue
        {
            get { return Quantity; }//*CostPrice; }
        }
        public StockCategory StockCategory { get; set; } 
    }
}