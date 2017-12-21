using FBTS.Model.Common;
using System;


namespace FBTS.Model.Transaction.Accounts
{
    public class TRNReference
    {
        public TRNReference()
        {
            ReferenceDate = Convert.ToDateTime(Constants.DefaultDate);
        }
        public string ReferenceNo { get; set; }
        public DateTime ReferenceDate { get; set; }
    }
}
