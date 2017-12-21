using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FBTS.Library.Common;

namespace FBTS.Model.Transaction.HR_Payroll
{
    public class PayrollExpression : Operations
    {
        public PayrollExpression()
        {
            FromDate = Dates.ToDateTime(Constants.DefaultDate, DateFormat.None);
            ToDate = Dates.ToDateTime(Constants.DefaultDate, DateFormat.None);
        }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Expression { get; set; }
        public string Parameter { get; set; }
        public decimal Value { get; set; }
        public decimal Percentage { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Grade { get; set; }
        //public Grades Grade { get; set; }
        public bool Payslip { get; set; }
        public string DetailHeader { get; set; }
        public string Slno { get; set; }
        public Int32 CodeSeq { get; set; }
        public bool? PayslipEntry { get; set; }
        public bool EnableValue { get; set; }
        public bool EnableEntry { get; set; }
    }
}
