using System;
using System.Collections.Generic;
using FBTS.Model.Common;

namespace FBTS.Model.Transaction.Accounts
{
    [Serializable]
    public class TxnAccount : Operations
    {
        public TxnAccount()
        {
            Addresses = new List<TxnAcccountAddress>(); 
        }
        public string SName { get; set; }
        public string ACode { get; set; } 
        public string Type { get; set; }
        public string Mode { get; set; }
        public string LRep { get; set; }
        public string LSub { get; set; }

        public List<TxnAcccountAddress> Addresses { get; set; }
    }
}
