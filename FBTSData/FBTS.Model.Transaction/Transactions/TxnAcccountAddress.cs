using System;
using FBTS.Model.Common;

namespace FBTS.Model.Transaction.Accounts
{
    [Serializable]
    public class TxnAcccountAddress : Operations
    {
        public TxnAcccountAddress()
        {
            Address= new Address(); 
            Currency = new Currency();
        }
        public string SName { get; set; }
        public string ACode { get; set; }  
        public Address Address { get; set; } 
        public Currency Currency { get; set; }  
    }
}
