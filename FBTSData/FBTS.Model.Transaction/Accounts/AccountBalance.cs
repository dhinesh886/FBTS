using FBTS.Model.Common;
using FBTS.Model.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Transaction.Accounts
{
    public class AccountBalance : Operations
    {

        public AccountBalance()
        {
            billDetails = new BillDetails();
        }
        public string Sl { set; get; }
        public string Category { set; get; }
        public string AccountType { set; get; }
        public string AccountName { set; get; }
        public string BU { get; set; }
        public string Branch { get; set; }
        public string AddressCode { get; set; }
        public string Credit { set; get; }
        public string Debit { set; get; }
        public BillDetails billDetails { set; get; }
    }
}
