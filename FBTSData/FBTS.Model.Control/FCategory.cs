using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Control
{
    public class FCategory : Operations
    {
        public string CategoryCode { get; set; }
        public string CategoryDesp { get; set; }
        public string CategoryType { get; set; }
        public bool Suspend { get; set; }

        public string PurchaseSales { get; set; }
    }
}
