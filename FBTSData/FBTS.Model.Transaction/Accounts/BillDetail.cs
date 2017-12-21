using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Transaction.Accounts
{
    public class BillDetail : Operations
    {
        public string Sl { get; set; }
        public string TRN_Type { get; set; }
        public string Bill_No { get; set; }
        public DateTime Bill_Date { get; set; }
        public decimal Bill_Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime Due_Date { get; set; }
        public decimal ADJ_Amount { get; set; }
       
        public string TRN_AMDNo { get; set; }
        public string TRN_BU { get; set; }
        public string TRN_Branch { get; set; }
        public string JCode { get; set; }
        public string TRNArea { get; set; }
        public DateTime TRNEDate { get; set; }
        public string SName { get; set; }
        public string TRNACode { get; set; }
        public string TRNRef1No { get; set; }
        public DateTime TRNRef1Date { get; set; }
        public string TRNCurrency { get; set; }
        public string TRNCountry { get; set; }
        public decimal TRNCurRate { get; set; }
        public string TRNNarration { get; set; }
        public string TRNCD { get; set; }
        public decimal TRNCurAmount { get; set; }
        public string TRNOff { get; set; }
        public DateTime PostDate { get; set; }
        public decimal TDSTCSAmount { get; set; }
    }
}
