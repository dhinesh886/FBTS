using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Model.Transaction.Accounts
{
   // [Serializable]
    public class CashBankJournal:Operations
    {
        public string TYPE { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string BNO { get; set; }//I am using BNO as Values In Recurring Journal
        public string SNO { get; set; }//I am using SNO as Code In Recurring Journal 
        public string BNO1 { get; set; }//I am using BNO1 as Expression In Recurring Journal 
        public string SNO1 { get; set; }//I am using SNo1 as table name in Recurring Journal
        public string PER_UNIT { get; set; }
        public string ACC_NO { get; set; }        
        public string CREATED { get; set; }
        public string UPDATED { get; set; }
        public string TRANSACTED { get; set; }
        public string SNAME { get; set; }
        public string PDCRECBNO { get; set; }
        public string PDCRECSNO { get; set; }
        public string PDCPAYBNO { get; set; }
        public string PDCPAYSNO { get; set; }
        public string SLNO { get; set; }
        public string CD { get; set; }
        public string DSLNO { get; set; }//I am Using for RJ_ID in Recurring Journal
        public string COLHEAD { get; set; }//I am Using as Narration in Recurring Journal form
        public string MODE { get; set; }//I am Using for Payment or receipt in Recurring Journal
        
    }
}
