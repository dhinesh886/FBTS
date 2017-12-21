using FBTS.Model.Control;

namespace FBTS.Model.Transaction
{
    public class MaterialGroup : MaterialHierarchy
    { 
        public MaterialClass MaterialClass { get; set; } 
        public MaterialType MaterialType { get; set; }
    }
}