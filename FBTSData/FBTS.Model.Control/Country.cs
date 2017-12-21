using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Control
{
    public class Country : Operations
    {
        public Country()
        {
            States = new States();
            currency = new Currency();
        }
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public string Denomination { get; set; }
        public States States { get; set; }
        public Currency currency { get; set; }
    }
}
