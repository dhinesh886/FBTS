using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Transaction.Accounts
{
    public class Contact : Operations
    {
        public Contact()
        {
            Created = Convert.ToDateTime(Constants.DefaultDate);
            Updated = Convert.ToDateTime(Constants.DefaultDate);
        }
        public string ContactType { get; set; }
        public string ContactCode { get; set; }
        public string ContactName { get; set; }
        public string ContactAddress { get; set; }
        public string ContactCity { get; set; }
        public string ContactPin { get; set; }
        public string ContactCountry { get; set; }
        public string ContactState { get; set; }
        public string ContactContact1 { get; set; }
        public string ContactContact2 { get; set; }
        public string ContactItno { get; set; }
        public string ContactDesign { get; set; }
        public string ContactMail { get; set; }
        public DateTime ContactValid_From { get; set; }
        public DateTime ContactValid_To { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
    }
}
