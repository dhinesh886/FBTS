using FBTS.Model.Common;

namespace FBTS.Model.Control
{
    public class UOM : Operations
    {      
        public string PrimaryUom { get; set; }
        public string SecondryUom { get; set; }
        public string UomConversion { get; set; }
        public string Description { get; set; }       
        public bool Suspend { get; set; }

    }
}
