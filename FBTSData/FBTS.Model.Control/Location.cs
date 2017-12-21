using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Control
{
    [Serializable]
    public class Location:Operations 
    {
        public Location()
        {
            Created = Convert.ToDateTime(Constants.DefaultDate);
            Id = string.Empty;
            Type = string.Empty;
            Description = string.Empty;
            Parent = string.Empty;
            Suspend = false;
        }
        
        public string Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string LSub { get { return Constants.LedgerSub; } }
        public string Parent { get; set; }
        public string Catg1 { get; set; }
        public string Catg2 { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get { return DateTime.Now; } }
        public bool Suspend { get; set; }
    }
}
