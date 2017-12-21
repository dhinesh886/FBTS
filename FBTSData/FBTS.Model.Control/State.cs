using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Control
{
    public class State : Operations
    {
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string Type { get; set; }
        public string CCode { get; set; }
    }
}
