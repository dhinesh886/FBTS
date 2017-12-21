using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Control
{
    public class Transaction : Operations
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Module { get; set; }
        public string AnType { get; set; }
        public bool Suspend { get; set; }
        public Transactions SubTransaction { get; set; }
    }
}
