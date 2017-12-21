using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Transaction.HR_Payroll
{
    public class EmployeeDesignation : Operations
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Grade { get; set; }
    }
}
