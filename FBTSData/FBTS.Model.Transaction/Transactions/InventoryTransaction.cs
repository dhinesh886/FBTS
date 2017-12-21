using FBTS.Model.Common;
using FBTS.Model.Transaction.Accounts;

namespace FBTS.Model.Transaction
{
    public class InventoryTransaction : Operations
    {
        public InventoryTransaction()
        {
            TxnDetails = new TxnDetails();
            StaticReferences = new KeyValuePairItems();
            PartyInfo = new TxnAccount();
            Definition = new TxnDefinition();
            References = new TxnReferences();
            TaxParameters = new TxnTaxParameters();
            Materials = new TxnMaterials();
        }
        public TxnDetails TxnDetails { set; get; } 
        public KeyValuePairItems StaticReferences { set; get; }
        public TxnAccount PartyInfo { get; set; }
        public TxnDefinition Definition { get; set; }
        public TxnReferences References { get; set; }
        public TxnTaxParameters TaxParameters { get; set; }
        public TxnMaterials Materials { get; set; }
    }
}
