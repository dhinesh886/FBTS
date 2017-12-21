using FBTS.Model.Common;
using System;

namespace FBTS.Model.Transaction
{
    [Serializable]
    public class TxnReference : Operations
    {
        public TxnReference()
        {
            Date = Convert.ToDateTime(Constants.DefaultDate);
            RefType = string.Empty;
            RefCode = string.Empty;
            Text = string.Empty;
        }
        public string RefType { get; set; }
        public string RefCode { get; set; }
        public string Text { get; set; } 
        public DateTime Date { get; set; } 
    }
}
