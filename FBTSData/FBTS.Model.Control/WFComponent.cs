using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Control
{
   public class WFComponent : Operations
    {
       public WFComponent()
       {
           WFCType = WFCComponentType.WorkflowComponentType;
           wfComponentSubs = new WFComponentSubs();
       }
        public string ComponentId { get; set; }
        public string ComponentDesp { get; set; } 
        public string ComponentType { get; set; }
        public string Relation1 { get; set; }
        public string Relation2 { get; set; }       
        public bool Suspend { get; set; }
        public WFCComponentType WFCType { get; set; }
        public WFComponentSubs wfComponentSubs { get; set; }
    }
}
