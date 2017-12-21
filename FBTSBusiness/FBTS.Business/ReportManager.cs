using FBTS.Data.Manager;
using FBTS.Library.EventLogger;
using FBTS.Model.Common;
using FBTS.Model.Transaction.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.Business.Manager
{
    public class ReportManager
    {
        public DashBoards GetDashBoards(QueryArgument queryArgument)
        {
            var dashBoards = new DashBoards();            
            try
            {
                ReportReadHelper.QueryArgument = queryArgument;
                dashBoards = ReportReadHelper.GetDashBoards();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetDashBoards failed with exception", ex);
                throw;
            }
            return dashBoards;
        }
        public OrderTransactions GetForecatingViewData(QueryArgument queryArgument)
        {
            var orderViewData = new OrderTransactions();
            try
            {
                ReportReadHelper.QueryArgument = queryArgument;
                orderViewData = ReportReadHelper.GetForecatingViewData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetForecatingViewData failed with exception", ex);
                throw;
            }
            return orderViewData;
        }
        public OrderTransactions GetOrderDetailViewData(QueryArgument queryArgument)
        {
            var orderViewData = new OrderTransactions();
            try
            {
                ReportReadHelper.QueryArgument = queryArgument;
                orderViewData = ReportReadHelper.GetOrderDetailViewData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetOrderDetailViewData failed with exception", ex);
                throw;
            }
            return orderViewData;
        }
        public OrderTransactions GetPendingOrderDetailView(QueryArgument queryArgument)
        {
            var orderViewData = new OrderTransactions();
            try
            {
                ReportReadHelper.QueryArgument = queryArgument;
                orderViewData = ReportReadHelper.GetPendingOrderDetailView();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetPendingOrderDetailView failed with exception", ex);
                throw;
            }
            return orderViewData;
        }
        public OrderTransactions GetAuditLogData(QueryArgument queryArgument)
        {
            var orderViewData = new OrderTransactions();
            try
            {
                ReportReadHelper.QueryArgument = queryArgument;
                orderViewData = ReportReadHelper.GetAuditLogData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetAuditLogData failed with exception", ex);
                throw;
            }
            return orderViewData;
        }
        public OrderTransactions GetBilledPartData(QueryArgument queryArgument)
        {
            var orderViewData = new OrderTransactions();
            try
            {
                ReportReadHelper.QueryArgument = queryArgument;
                orderViewData = ReportReadHelper.GetBilledPartData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetBilledPartData failed with exception", ex);
                throw;
            }
            return orderViewData;
        }
        public RawDataContainer GetReportData(ObjectContainer parameters)
        {
            RawDataContainer rawDataContainer;
            try
            {
               // ReportReadHelper.QueryArgument = queryArgument;
                rawDataContainer = ReportReadHelper.GetReportData(parameters);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetForecatingViewData failed with exception", ex);
                throw;
            }
            return rawDataContainer;
        }
    }

}
