using FBTS.Model.Common;
using FBTS.Model.Control;
using System;

namespace FBTS.Model.Transaction.Transactions
{

    [Serializable]
    public class BillTrackingDetail 
    {
        public BillTrackingDetail()
        {
            SlNo = string.Empty;
            PrevSlNo = string.Empty;
            PartDetail = new Material();
            MaterialType = new MaterialType();
            MaterialGroup = new MaterialGroup();
            Unit = new Unit();
            POQuantity = 0;
            SQuantity = 0;
            ShQuantity = 0;
            UnitValue = 0;
            Tax = string.Empty;
            BillStatus = string.Empty;
            CurrentStatus = string.Empty;
            BillLocation = new Location();
            Off = string.Empty;
            OrdType = string.Empty;
            FCNumber = string.Empty;
            FCSlNo = string.Empty;
            BillQuantity = 0;
            TaxRate = 0;
            DocketNumber = string.Empty;
            InvoiceNumber = string.Empty;
            EndMileDelivery = Convert.ToDateTime(Constants.DefaultDate);
            Remark = string.Empty;
            RelatedSR = string.Empty;
        }
        public string OrdType { get; set; }
        public string Ord_no { get; set; }
        public string RelatedSR { get; set; }
        public string SlNo { get; set; }
        public string PrevSlNo { get; set; }
        public Material PartDetail { get; set; }
        public MaterialType MaterialType { get; set; }
        public MaterialGroup MaterialGroup { get; set; }
        public Unit Unit { get; set; }
        public decimal POQuantity { get; set; }
        public decimal SQuantity { get; set; }
        public decimal ShQuantity { get; set; }
        public decimal RemainQty { get { return BillQuantity - SQuantity; } }
        public decimal BQuantity { get; set; }
        public decimal UnitValue { get; set; }
        public decimal POValue { get { return Math.Round(POQuantity * UnitValue, 2); } }
        public decimal FCBillQuantity { get; set; }
        public string Tax { get; set; }
        public string Modality { get; set; }
        public string ModalityDesp { get; set; }
        public string FCNumber { get; set; }
        public string FCSlNo { get; set; }
        public decimal BillQuantity { get; set; }
        public decimal TaxRate { get; set; }
        public string TaxType { get; set; }
        public decimal BillValue { get { return TaxType == "EXC" ? (BillQuantity * UnitValue) + (TaxRate / 100 * BillQuantity * UnitValue) : BillQuantity * UnitValue; } }
        public string BillStatus { get; set; }
        public string CurrentStatus { get; set; }
        public string BillStatusDesc
        {
            get { return StatesDescripation(BillStatus.Trim()); }
        }
        public Location BillLocation { get; set; }
        public string LogisticOrderNumber { get; set; }
        public string DocketNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime EndMileDelivery { get; set; }
        public string Remark { get; set; }
        public string Remark1 { get; set; }
        public string PartAging { get; set; }
        public string Off { get; set; }
        public bool Calcel
        {
            get
            {
                if (Off.Trim() == Constants.TRNCancelOFF)
                    return true;
                else
                    return false;
            }
        }

        private string StatesDescripation(string status)
        {
            string str = string.Empty;
            if (status == Constants.OrderingType) return "Ordering";
            if (status == Constants.TxnBackOrderType) return "Back Order";
            if (status == Constants.TxnStockTransferType) return "Stock Transfer";
            if (status == Constants.AlternativePartNeeded) return "Alternate Part Number Needed";
            if (status == Constants.TRNCancelOFF) return "Cancelled";
            if (status == "WP") return "Wating for more part";
            if (status == "EP") return "EOL Part";
            if (status == "OH") return "On Hold";
            if (status == "IS") return "Part Issue - System";
            if (status == "IG") return "Part  issue – GPRS";
            if (status == "PS") return "Partially Billed - Stock Transfer";
            if (status == "PB") return "Partially Billed - Backorder";
            if (status == "EA") return "Export Parts arrangement";
            if (status == "ES") return "Export Parts shipment";
            if (status == Constants.TxnDebriefingType) return "Debriefing";
            if (status == Constants.Billed) return "Billed";
            if (status == Constants.BackToOrder) return "Back To Order";
            if (status == Constants.PartReserved) return "Part Reserved";
            if (status == Constants.SystemIssue) return "System Issue";
            return str;
        }
    }
}
