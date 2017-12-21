using System;
using FBTS.Model.Common;

namespace FBTS.Model.Transaction.Accounts
{
    [Serializable]
    public class Account:Operations
    {     
        public Account()
        {            
            Created = Convert.ToDateTime(Constants.DefaultDate);
            Address = new Address();
            Parent = string.Empty;
            SName = string.Empty;
            Name = string.Empty;
            Type = string.Empty;
            Sub = string.Empty;
            FGroup = string.Empty;
            LMode = string.Empty;
            ContactPerson=string.Empty;
            Suspend = false;
        }

        public string Parent { get; set; }
        public string SName { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Sub { get; set; }
        public string FGroup { get; set; }
        public string LMode { get; set; }
        public string ContactPerson { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get { return DateTime.Now; } }
        public bool Suspend { get; set; }
        public Address Address { get; set; }   
    }
}
