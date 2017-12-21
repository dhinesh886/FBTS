
using FBTS.Model.Common;
namespace FBTS.Model.Control
{
    public class MSp:Operations 
    {
        public MSp()
        {
            Module = new Module();
        }
        public string SpType { get; set; }
        public Module Module { get; set; }
        public string SpName { get; set; }
        public string SpDescripation { get; set; }

    }
}
