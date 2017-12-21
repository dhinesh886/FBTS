using FBTS.Model.Common;
using FBTS.Model.Control;
using System;

namespace FBTS.Model.Transaction.Accounts
{
    public class PDC:Operations
    {
        public PDC()
        {
            SysDate = DateTime.Now;
            BillDetails = new BillDetails();
            references = new TRNReferences();
            country = new Country();
        }
        public string Type { get; set; }
        public string JournalType { get; set; }
        public string VoucherNO { get; set; }
        public DateTime VoucherDate { get; set; }
        public string Bu { get; set; }
        public string Branch { get; set; }
        public DateTime SysDate { get; set; }
        public string SlnNo { get; set; }
        public string Sname { get; set; }    
        public string Naration { get; set; }
        public string PaymentReceipt { get; set; }
        public string DC { get; set; }
        public string Account { get; set; }
        public string AccountName { get; set; }
        public string ACode { get; set; }
        public decimal Amount
        {
            get;set;
        }
        public decimal BalanceAmount
        {
            get;
            set;
        }
        public decimal CurrAmount
        {
            get
            {
                try
                {
                    return Math.Round(Amount / country.currency.CurrencyRate, 2);
                }
                catch
                {
                    return 0;
                }
            }
        }
        public string OldChequeNo { get; set; }
        public string TrnStatus { get; set; }
        public decimal TDS { get; set; }
        public string TrnOff { get; set; }
        public BillDetails BillDetails { get; set; }
        public TRNReferences references { get; set; }
        public Country country { get; set; }
      
    }
}
