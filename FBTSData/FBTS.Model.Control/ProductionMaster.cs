using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Control
{
    public class ProductionMaster : Operations
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slno { get; set; }

    }
}