using System;
using FBTS.Model.Common;
using FBTS.Model.Control;

namespace FBTS.Model.Transaction
{
     [Serializable]
    public class Material : Operations
    {
       
        public Material()
        {
            MaterialType = new MaterialType();  
            MaterialGroup=new MaterialGroup();
            Created = Convert.ToDateTime(Constants.DefaultDate);
            LastTransacted = Convert.ToDateTime(Constants.DefaultDate);
            PartNumber = string.Empty;
            Description = string.Empty;
            DetailedDescription = string.Empty;
            Unit = string.Empty;
            SalesPrice = 0;
            PriceValidDate = Convert.ToDateTime(Constants.DefaultDate);
            Suspend = false;
        }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public string DetailedDescription { get; set; }//This Property used as Alternative Part in BillingPartControl Form
        public MaterialType MaterialType { get; set; }
        public MaterialGroup MaterialGroup { get; set; }              
        public string Unit { get; set; }
        public decimal SalesPrice { get; set; }
        public DateTime PriceValidDate { get; set; }
        public DateTime LastTransacted { get; set; }
        public DateTime Created { get; set; }
        public DateTime UpdateDate { get { return DateTime.Now; } }
        public bool Suspend { get; set; }

    }
}