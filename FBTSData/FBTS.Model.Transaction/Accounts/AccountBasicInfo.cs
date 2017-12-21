using System;
using FBTS.Model.Common;

namespace FBTS.Model.Transaction.Accounts
{
    public class AccountBasicInfo :Operations
    {
        public string Parent { get; set; }
        public string SName { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Mode { get; set; }
        public string Sub { get; set; }
        public string Catg1 { get; set; }
        public string Catg2 { get; set; }
        public string Catg3 { get; set; }
        public string Catg4 { get; set; }
        public string Catg5 { get; set; }
        public string FGroup { get; set; }
        public string RGroup { get; set; }
        public string Budgeted { get; set; }
        public string ControlAcc { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Suspend { get; set; }
        public string Reconcile { get; set; }
    }
}
