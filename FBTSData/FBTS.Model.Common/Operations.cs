using System;

namespace FBTS.Model.Common
{
    [Serializable]
    public class Operations
    {
        public string Action { get; set; } 
        public DataBaseInfo DataBaseInfo { get; set; }
    }
}