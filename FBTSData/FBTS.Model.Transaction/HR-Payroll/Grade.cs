using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Transaction.HR_Payroll
{
    public class Grade : Operations
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string MinBasic { get; set; }
        public string MaxBasic { get; set; }
        public bool Suspend { get; set; }
    }
}
