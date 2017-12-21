using System;
using FBTS.Model.Common;

namespace FBTS.Model.Transaction.Accounts
{
    public class AccountAddress : Operations
{
        public AccountAddress()
        {
            Address = new Address();
            credit = new Credit();
            contacts = new Contacts();
            banks = new Banks();
            Created = Convert.ToDateTime(Constants.DefaultDate);
            Updated = Convert.ToDateTime(Constants.DefaultDate);
        }
        public string Sname { get; set; }
        public string Name { get; set; }
        public string Acode { get; set; }
        public Address Address { get; set; }  
        public string Www { get; set; } 
        public string Pan { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public string TaxNo1 { get; set; }
        public string TaxNo2 { get; set; }
        public string TaxNo3 { get; set; }
        public string TaxNo4 { get; set; }
        public string CDetail1 { get; set; }
        public string CDetail2 { get; set; }
        public string CDetail3 { get; set; }
        public bool Contact { get; set; }
        public string Docthru { get; set; }
        public string Carrier { get; set; }
        public string SalesRep { get; set; }
        public string SalesArea { get; set; }
        public decimal SalesComPer { get; set; }       
        public bool OrderRequired { get; set; }
        public bool PayTermsReq { get; set; }
        public string PayTerms { get; set; }
        public string DocPrefix { get; set; }
        public string TaxId { get; set; }       
        public string Remarks1 { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
        public bool Suspend { get; set; }
        public string Off { get; set; }        
        public string ExciseNo { get; set; }
        public string ExciseRange { get; set; }
        public string ExciseDivision { get; set; }
        public string ExciseCommissinerate { get; set; }        
        public AccountBalances Balances { get; set; }
        public Credit credit { get; set; }
        public Contacts contacts { get; set; }
        public Banks banks { get; set; }

    }
}
