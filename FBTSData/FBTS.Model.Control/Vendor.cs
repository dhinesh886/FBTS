using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Control
{
    public class Vendor : Operations
    {
        public string Code { get; set; }
        public string LType { get; set; }
        public string VendorName { get; set;}
        public string VAddress { get; set; }
        public string VCity { get; set;}
        public string VState { get; set; }
        public string VCountry { get; set; }
        public string VEmail { get; set; }
        public string VPhone { get; set; }
        public string Zip { get; set; }
        public string VTin { get; set; }
        public string VExcise { get; set; }
        public string VCST { get; set; }
        public string CreditLimit { get; set; }
        public string Off { get; set; }
        public string Fax { get; set; }
        public string Pan { get; set; }
        public string Currency { get; set; }
               

        public Vendors MultiVendors { get; set; }
         public Vendor()
        {
            Code = string.Empty;
            VendorName = string.Empty;
            VAddress = string.Empty;
            VCity = string.Empty;
            VState = string.Empty;
            VCountry = string.Empty;
            VEmail = string.Empty;
            VPhone = string.Empty;
            Zip = string.Empty;
            VTin = string.Empty;
            VExcise = string.Empty;
            VCST = string.Empty;
            CreditLimit = string.Empty;
            Fax = string.Empty;
            Pan = string.Empty;
            Currency = string.Empty;
            LType = string.Empty;
        
        
            Off = string.Empty;
        }

        public string ControlDbServer { get; set; }
        public string DbServer { get; set; }
        public string DbName { get; set; }
        public string DbUserid { get; set; }
        public string DbPassword { get; set; }
    }
}