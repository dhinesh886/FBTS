using FBTS.Model.Common;

namespace FBTS.Model.Transaction.Accounts
{
    public class Credit
    {
        public Credit()
        {
            currency = new Currency();
        }
        public int CrLimit1 { set; get; }
        public int CrLimit2 { set; get; }
        public int CrLimit3 { set; get; }
        public int CrLimit4 { set; get; }
        public int CrLimit5 { set; get; }
        public int CrDay1 { set; get; }
        public int CrDay2 { set; get; }
        public int CrDay3 { set; get; }
        public int CrDay4 { set; get; }
        public int CrDay5 { set; get; }
        public int Grace_days { set; get; }
        public decimal Pay_int1 { set; get; }
        public decimal Pay_int2 { set; get; }
        public int Pay_day1 { set; get; }
        public int Pay_day2 { set; get; }
        public int Cash_PayDays { set; get; }
        public decimal Addl_disper { set; get; }
        public int Rcpt_days { set; get; }
        public bool Disc_Allowed { set; get; }
        public bool Override_terms { set; get; }
        public Currency currency { set; get; }
        public string Remarks { set; get; }
        public bool Int_Cal_Allowed { get; set; }
        public decimal Tds { get; set; }
    }
}
