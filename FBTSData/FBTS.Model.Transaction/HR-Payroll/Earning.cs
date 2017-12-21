using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Transaction.HR_Payroll
{
    public class Earning : Operations
    { 
        public string Code { get; set; }
        public string Grade { get; set; }
        public string Name { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool Select { get; set; }
    }
}
