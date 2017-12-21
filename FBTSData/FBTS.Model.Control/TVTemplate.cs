
using FBTS.Model.Common;
namespace FBTS.Model.Control
{
    public class TVTemplate : Operations
    {
        public TVTemplate()
        {
            ColumnsCriteria = new WFComponentSubs();
            TVType = string.Empty;
            TVID = string.Empty;
            TVDESP = string.Empty;
            TrnType = string.Empty;
            SlNo = 0;
            TypeData = string.Empty;
            StageTage = string.Empty;
            Field = string.Empty;
            SPName = string.Empty;
            FieldDesp = string.Empty;
            Link = string.Empty;
            width = 0;
        }
        public string TVType { get; set; }
        public string TVID { get; set; }
        public string TVDESP { get; set; }
        public string TrnType { get; set; }
        public int SlNo { get; set; }
        public string TypeData { get; set; }
        public string StageTage { get; set; }
        public string Field { get; set; }
        public string SPName { get; set; }
        public string FieldDesp { get; set; }
        public string Link { get; set; }
        public decimal width { get; set; }
        public WFComponentSubs ColumnsCriteria { get; set; }

    }
}
