using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Transaction.HR_Payroll
{
    public class EmployeeRef : Operations
    {
        public string EmpId { get; set; }
        public string ESI { get; set; }
        public string PF { get; set; }
    }
}
