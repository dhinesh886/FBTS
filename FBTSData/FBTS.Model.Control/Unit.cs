using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Control
{
    [Serializable]
    public class Unit : Operations
    {
        public Unit()
        {
            Id = string.Empty;
            Description = string.Empty;
            Date = Convert.ToDateTime(Constants.DefaultDate);
            Suspend = false;
        }
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool Suspend { get; set; }

    }
}
