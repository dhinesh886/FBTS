using System;
using System.Collections.Generic;

namespace FBTS.Model.Transaction
{
    public class TxnMaterials : List<TxnMaterial>
    {
        public DateTime ExpairyDate { get; set; }
    }
}