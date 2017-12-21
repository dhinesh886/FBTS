using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Transaction.Accounts
{
    [Serializable]
    public class BankCheque : Operations
    {
        public string Sname { get; set; }
        public string Chqslno { get; set; }
        public string JCode { get; set; }
        public string Chq_Book_RctDt { get; set; }
        public string Chq_Book_From { get; set; }
        public string Chq_Book_To { get; set; }
        public string TotalChq { get; set; }
        public string UsedChq { get; set; }
        public string AvailableChq { get; set; }
        public string CancelledChq { get; set; }
        public string IssuedPartyName { get; set; }
    }
}
