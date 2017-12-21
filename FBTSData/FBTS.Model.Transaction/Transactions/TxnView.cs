using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Transaction.Transactions
{
    public class TxnView
    {
        public OrderHead OrderHeader{get;set;}
        public string Ord_No { get; set; }
        public string Ord_Amd { get; set; }
        public string Ord_Date { get; set; }
        public string Customer { get; set; }
        public string Req_Loc { get; set; }
        //public string 

    }
}
