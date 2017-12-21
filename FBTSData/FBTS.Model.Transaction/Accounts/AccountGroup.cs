using FBTS.Model.Common;
using FBTS.Model.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Transaction.Accounts
{
    [Serializable]
    public class AccountGroup : Operations
    {
        public string GroupID { set; get; }
        public string GroupName { set; get; }
        public string FinancialGroup { set; get; }
        public string Type { set; get; }
        public string Mode { get; set; }
        public string Sub { get; set; }
        public string Category { set; get; }
        public AccountType AccTrnType { get; set; }
        public string Parent { get; set; }
        public string Rgroup { set; get; }       

        public AnAttributes Attributes { get; set; }
    }
}
