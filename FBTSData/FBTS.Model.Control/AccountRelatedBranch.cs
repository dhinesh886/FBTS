using FBTS.Model.Common;

namespace FBTS.Model.Control
{
    public class AccountRelatedBranch : Operations
    {
        public string AccountName { get; set; }
        public string SName { get; set; }
        public string AddressName { get; set; }
        public string ACode { get; set; }
        public string Branch { get; set; }
        public bool IsInBranch { get; set; }
        public string Bu { get; set; }
        public bool Transacted { get; set; }
    }
}
