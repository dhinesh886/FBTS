using FBTS.Model.Common;

namespace FBTS.Model.Transaction.Accounts
{
    public class VoucherSetup : Operations  
    {
        public string MultiCurrencySName { get; set; }
        public string TDSSName { get; set; }
        public string TCSSName { get; set; }
        public string MultiCurrencyLName { get; set; }
        public string TDSLName { get; set; }
        public string TCSLName { get; set; }
    }
}
