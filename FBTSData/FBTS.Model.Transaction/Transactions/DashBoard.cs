
namespace FBTS.Model.Transaction.Transactions
{
    public class DashBoard
    {
        public string Description {get;set;}
        public string Status{get;set;}
        public int LessThen7days{get;set;}
        public int LessThen7To15days { get; set; }
        public int LessThen15To30days { get; set; }
        public int LessThen30To60days { get; set; }
        public int MoreThen60days { get; set; }
        public int GrandTotal { get; set; }

    }
}
