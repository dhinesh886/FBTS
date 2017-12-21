using FBTS.Library.Common;
using FBTS.Model.Common;
using System;

namespace FBTS.Model.Transaction.HR_Payroll
{
    public class OperationalParameter : Operations
    {
        public OperationalParameter()
        {
            FromDate = Dates.ToDateTime(Constants.DefaultDate, DateFormat.None);
            ToDate = Dates.ToDateTime(Constants.DefaultDate, DateFormat.None);
        }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Parameter { get; set; }
        public decimal Value { get; set; }
        public string Slno { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Percentage { get;set; }
        public string Expression { get; set; }
        public bool EnableValue { get; set; }
        public bool EnableEntry { get; set; }
        public string Account { get; set; }
        public string InsertUpdateHeader { get; set; }
        public string LoginId { get; set; }
        public string AccountType { get; set; }
        public string PType { get; set; }
    }
}
