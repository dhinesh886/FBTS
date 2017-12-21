
namespace FBTS.Model.Control
{
    public class WFComponentSub
    {
        public WFComponentSub()
        {
            WFCType = string.Empty;
            WFCCode = string.Empty;
            WFCSCode = string.Empty;
            WFCDesp = string.Empty;
            Relation1 = string.Empty;
        }
        public string WFCType { get; set; }
        public string WFCCode { get; set; }
        public string WFCSCode { get; set; }
        public string WFCDesp { get; set; }
        public string Relation1 { get; set; }
    }
}
