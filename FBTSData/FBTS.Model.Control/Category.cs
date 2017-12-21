
using FBTS.Model.Common;
using System;
namespace FBTS.Model.Control
{
    [Serializable]
    public class Category : Operations
    {
        public Category()
        {
            CreatedDate = Convert.ToDateTime(Constants.DefaultDate);
            CatType = string.Empty;
            CatCode = string.Empty;
            ID = string.Empty;
            Description = string.Empty;
            Suspend = false;
        }

        public string CatType { get; set; }
        public string CatCode { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get { return DateTime.Now; } }
        public bool Suspend { get; set; }

    }
}
