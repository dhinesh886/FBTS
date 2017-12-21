using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FBTS.Model.Transaction
{
    public class ScheduleTransaction : InventoryTransaction
    {
        public ScheduleTransaction()
        {
            OrderDetails =  new OrderTransaction();
            Schedules = new TxnSchedules();
        }
        public OrderTransaction OrderDetails { get; set; }
        public TxnSchedules Schedules { get; set; }
    }

    public class TxnSchedule : Operations
    {
        public TxnSchedule()
        {
           Materials = new TxnMaterials(); 
        }
        public string ScheduleId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public DateTime ShipingDate { get; set; }
        public string WarehouseId { get; set; }

        public TxnMaterials Materials { get; set; }
    }
     public class TxnSchedules : List<TxnSchedule>
     {
         public List<string> ScheduleIds
         {
             get { return this.Select(x => x.ScheduleId).ToList(); }
         }
     }
}
