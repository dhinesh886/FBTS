using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Transaction.HR_Payroll 
{
    public class EmployeeEarningDeduction : Operations
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string ParameterId { get; set; }
        public string ParameterDescription { get; set; }
        public string ParameterMode { get; set; }
        public decimal Amount { get; set; }
        public bool Applicable { get; set; }
        public bool EnableValue { get; set; }
        public string EmployeeGrade { get; set; }
        public decimal Value { get; set; }
    }
}
