using FBTS.Model.Control;
using System;

namespace FBTS.Model.Transaction.Transactions
{
    [Serializable]
    public class  OrderDetail
    {
        public OrderDetail()
        {
            PartDetail = new Material();            
            Quantity = 0;
            SQuantity = 0;
            ShQuantity = 0;
            WarehouseTo = new Location();         
            Rate = 0;
            SlNo = string.Empty;
            PrevSlNo = string.Empty;
            Modality = string.Empty;
            CurrentStatus =string.Empty;
            LogisticOrderNumber = string.Empty;
            Off = string.Empty;
            MaterialType = new MaterialType();
            MaterialGroup = new MaterialGroup();
        }
       
        public string SlNo { get; set; }
        public string PrevSlNo { get; set; }
        public Material PartDetail { get; set; }
        public MaterialType MaterialType { get; set; }
        public MaterialGroup MaterialGroup { get; set; }   
        public decimal Quantity { get; set; }
        public decimal SQuantity { get; set; }
        public decimal ShQuantity { get; set; }
        public decimal BQuantity { get; set; }
        public decimal DoQuantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Value { get { return Quantity * Rate; } }
        public Location WarehouseTo { get; set; }       
        public string Modality { get; set; }
        public string ModalityDesp { get; set; }
        public string CurrentStatus { get; set; }
        public string CurrentStatusDesc
        {
            get { return StatesDescripation();}
        }
        public string LogisticOrderNumber { get; set; }
        public string Off { get; set; }

        public string GPRSStatus { get; set; }
     
        
        
        private string StatesDescripation()
        {
            string str = string.Empty;            
            if (CurrentStatus == "OR") str = "Ordering";
            if (CurrentStatus == "BO") str = "Back Order";
            if (CurrentStatus == "ST") str = "Stock Transfer";
            if (CurrentStatus == "FR") str = "Field Request";
            if (CurrentStatus == "FC") str = "Forecastinng";
            if (CurrentStatus == "AP") str = "Approval";
            if (CurrentStatus == "CL") str = "Close";
            return str;
        }
    }   
}
