using System;
using FBTS.Model.Common;

namespace FBTS.Model.Transaction
{
    public class Bom : Operations
    {
        public Bom()
        {
           ParentPart = new Material();
           ChildParts =new Materials();
        }
        public string BomId { get; set; }
        public int RevisionNumber { get; set; }
        public DateTime RevisedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Remarks { get; set; }

        public Material ParentPart { get; set; }

        public Materials ChildParts { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalCostPrice { get; set; }
        public decimal WastagePersentage { get; set; }
        public decimal GrandTotal { get; set; }
        public bool SubBOM { get; set; }

        public decimal MarketingCost { get; set; }
        public decimal EnggAndDocCost { get; set; }
        public decimal InfraCost { get; set; }
        public decimal QualityCost { get; set; }
        public decimal MarginAndOverheadCost { get; set; }
        public decimal TotalProcessCost { get; set; }
      

        
    }
}