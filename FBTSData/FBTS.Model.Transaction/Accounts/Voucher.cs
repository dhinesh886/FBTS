using FBTS.Model.Common;
using System;

namespace FBTS.Model.Transaction.Accounts
{
    public class Voucher:Operations
    {
        public Voucher()
        {
            SysDate = DateTime.Now;
            BillDetails = new BillDetails();
            Accounts = new Account();
        }
        public string Type { get; set; }
        public string BankJournal { get; set; }
        public string BU { get; set; }
        public string Branch { get; set; }
              
        public string VoucherNo { get; set; }
        public DateTime VoucherCDate { get; set; }
        public DateTime SysDate { get; set; }
        public string SLN_No { get; set; }
        public string Naration { get; set; }

        public string PaymentReceipt { get; set; } // Enum
        public string DC { get; set; } // Enum
        public Account Accounts { get; set; }

        //public string Account { get; set; } // TxnAccount Model
        //public string ACode { get; set; }// TxnAccount Model
        

        //public string Country { get; set; }

        //public string TrnCurrency { get; set; }
        //public decimal CurRate { get; set; }

        //public decimal Amount { get; set; }
        public decimal Amount
        {
            get;
            set;
            //{
            //    return Accounts.credits.currency.CurrencyRate * CurAmount;
            //}
        }
        public decimal CurAmount
        {
            get;
            set; 
        }
        public BillDetails BillDetails { get; set; }
        public TRNReferences references { get; set; }      
    }
}
