using FBTS.Model.Common;

namespace FBTS.Model.Transaction.Accounts
{
    public class AccountMapping : Operations
    {

        public string TrnType { get; set; }
        public string TrnMode { get; set; }
        public string CType { get; set; }
        public string Payment { get; set; }
        public string Reference { get; set; }
        public string JCode { get; set; }
        public int SlNo { get; set; }
        public string Branch { get; set; }
        public string Label { get; set; }
        public string Validation { get; set; }
        public string JName { get; set; }
        public string TypeName { get; set; }
        public string CTypeName { get; set; }
    }
}
