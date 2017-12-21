using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Transaction.Accounts
{
    public class Bank : Operations
    {
        public string BankType { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankCity { get; set; }
        public string BankPin { get; set; }
        public string BankCountry { get; set; }
        public string BankState { get; set; }
        public string BankContact1 { get; set; }
        public string BankContact2 { get; set; }
        public string BankItno { get; set; }
        public string BankIFSC { get; set; }
        public string BankWebSite { get; set; }
        public string BankMail { get; set; }
        public DateTime BankValid_From { get; set; }
        public DateTime BankValid_To { get; set; } 
    }
}
