using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using FBTS.Model.Common;
using FBTS.Model.Control;

namespace FBTS.Model.Transaction
{
    [Serializable]
    [KnownType(typeof(MaterialClass))]
    [KnownType(typeof(MaterialType))]
    [KnownType(typeof(MaterialGroup))]
    [XmlInclude(typeof(MaterialClass))]
    [XmlInclude(typeof(MaterialType))]
    [XmlInclude(typeof(MaterialGroup))]
    public class MaterialHierarchy : Operations
    {
        public MaterialHierarchy()
        {
            CreatedDate = Convert.ToDateTime(Constants.DefaultDate);
            Margin = 0;
        }
        public string Id { get; set; }
        public string Description { get; set; } 
        public MaterialHierarchyType MaterialHierarchyType { get; set; }       
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get { return DateTime.Now; } }
        public decimal Margin { get; set; }
    }
}
