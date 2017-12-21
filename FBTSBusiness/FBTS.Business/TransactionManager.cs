using FBTS.Data.Manager;
using FBTS.Library.EventLogger;
using FBTS.Model.Common;
using FBTS.Model.Transaction;
using FBTS.Model.Transaction.Transactions;
using System;

namespace FBTS.Business.Manager
{
    public class TransactionManager
    {
        public bool SetFieldRequests(OrderTransactions orderTransactions)
        {
            bool result;
            try
            {
                result = TransactionWriteHelper.SetFieldRequests(orderTransactions);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetFieldRequest failed with exception", ex);
                throw;
            }
            return result;
        }
        public OrderTransactions GetOrderData(QueryArgument queryArgument)
        {
            OrderTransactions orderTransactions;
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                orderTransactions = TransactionReadHelper.GetOrderData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "Get Order Data failed with exception", ex);
                throw;
            }
            return orderTransactions;
        }

        public bool SetForcasting(OrderTransactions orderTransactions)
        {
            bool result;
            try
            {
                result = TransactionWriteHelper.SetForcasting(orderTransactions);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "setForecasting failed with exception", ex);
                throw;
            }
            return result;
        }
        public OrderTransactions GetFollowupData(QueryArgument queryArgument)
        {
            OrderTransactions orderTransactions;
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                orderTransactions = TransactionReadHelper.GetFollowupData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "Get Followup failed with exception", ex);
                throw;
            }
            return orderTransactions;
        }
        public bool SetTeam(OrderTransactions orderTransactions)
        {
            bool result;
            try
            {
                result = TransactionWriteHelper.SetTeams(orderTransactions);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "setForecasting failed with exception", ex);
                throw;
            }
            return result;
        }
        public TxnReferences GetTxnReferences(QueryArgument queryArgument)
        {
            TxnReferences txnReferences;
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                txnReferences = TransactionReadHelper.GetTxnReferences();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetTxnReferences failed with exception", ex);
                throw;
            }
            return txnReferences;
        }
        public bool SetVerifications(BillTrackings billTrackings)
        {
            bool result;
            try
            {
                result = TransactionWriteHelper.SetVerifications(billTrackings);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetVerifications failed with exception", ex);
                throw;
            }
            return result;
        }
        public BillTrackings GetTrackingData(QueryArgument queryArgument)
        {
            BillTrackings billTrackings;
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                billTrackings = TransactionReadHelper.GetTrackingData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetTrackingData Data failed with exception", ex);
                throw;
            }
            return billTrackings;
        }
        public BillValiditions GetOrderHeaderReferances(QueryArgument queryArgument)
        {
            BillValiditions billValidations;
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                billValidations = TransactionReadHelper.GetOrderHeaderReferances();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetOrderHeaderReferances Data failed with exception", ex);
                throw;
            }
            return billValidations;
        }
        public bool SetOrderingData(BillTrackings billTrackings)
        {
            bool result;
            try
            {
                result = TransactionWriteHelper.SetOrderingData(billTrackings);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetOrderingData failed with exception", ex);
                throw;
            }
            return result;
        }
        public BillTrackings GetDebriefings(QueryArgument queryArgument)
        {
            BillTrackings billTrackings;
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                billTrackings = TransactionReadHelper.GetDebriefings();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetDebriefings Data failed with exception", ex);
                throw;
            }
            return billTrackings;
        }
        public bool SetDebriefing(BillTrackings billTrackings)
        {
            bool result;
            try
            {
                result = TransactionWriteHelper.SetDebriefing(billTrackings);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetDebriefing failed with exception", ex);
                throw;
            }
            return result;
        }
        public BillTrackings GetInvoiceData(QueryArgument queryArgument)
        {
            BillTrackings billTrackings;
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                billTrackings = TransactionReadHelper.GetInvoiceData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetInvoiceData Data failed with exception", ex);
                throw;
            }
            return billTrackings;
        }
        public bool SetTracking(BillTrackings billTrackings)
        {
            bool result;
            try
            {
                result = TransactionWriteHelper.SetTracking(billTrackings);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetTracking failed with exception", ex);
                throw;
            }
            return result;
        }
        public OrderTransactions GetFollowupDataForApprove(QueryArgument queryArgument)
        {
            OrderTransactions orderTransactions;
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                orderTransactions = TransactionReadHelper.GetFollowupDataForApprove();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetFollowupDataForApprove failed with exception", ex);
                throw;
            }
            return orderTransactions;
        }
        public bool SetFollowupApprove(OrderTransactions orderTransactions)
        {
            bool result;
            try
            {
                result = TransactionWriteHelper.SetFollowupApprove(orderTransactions);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetFollowupApprove failed with exception", ex);
                throw;
            }
            return result;
        }
        public OrderTransactions GetPendingOrderData(QueryArgument queryArgument)
        {
            OrderTransactions orderTransactions;
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                orderTransactions = TransactionReadHelper.GetPendingOrderData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetPendingOrderData Data failed with exception", ex);
                throw;
            }
            return orderTransactions;
        }
        public BillTrackings GetPendingOrderFormData(QueryArgument queryArgument)
        {
            BillTrackings orderTransactions;
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                orderTransactions = TransactionReadHelper.GetPendingOrderFormData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetPendingOrderFormData Data failed with exception", ex);
                throw;
            }
            return orderTransactions;
        }
        public OrderTransactions GetpPendingFollowupData(QueryArgument queryArgument)
        {
            OrderTransactions orderTransactions;
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                orderTransactions = TransactionReadHelper.GetpPendingFollowupData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetpPendingFollowupData failed with exception", ex);
                throw;
            }
            return orderTransactions;
        }
        public OrderTransactions GetChangeStatus_ALPN_Data(QueryArgument queryArgument)
        {
            OrderTransactions orderTransactions;
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                orderTransactions = TransactionReadHelper.GetChangeStatus_ALPN_Data();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetChangeStatus_ALPN_Data failed with exception", ex);
                throw;
            }
            return orderTransactions;
        }
        public BillTrackings GetChangeStatus_ALPN_FromData(QueryArgument queryArgument)
        {
            BillTrackings orderTransactions;
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                orderTransactions = TransactionReadHelper.GetChangeStatus_ALPN_FromData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetChangeStatus_ALPN_FromData Data failed with exception", ex);
                throw;
            }
            return orderTransactions;
        }
        public bool SetChangeStatus_ALPN(BillTrackings billTrackings)
        {
            bool result;
            try
            {
                result = TransactionWriteHelper.SetChangeStatus_ALPN(billTrackings);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetChangeStatus_ALPN failed with exception", ex);
                throw;
            }
            return result;
        }
        public bool SetApproval(BillTrackings billTrackings)
        {
            bool result;
            try
            {
                result = TransactionWriteHelper.SetApproval(billTrackings);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetApproval failed with exception", ex);
                throw;
            }
            return result;
        }
        public bool SetTMData(QueryArgument queryArgument)
        {
            bool result;
            try
            {

                TransactionReadHelper.QueryArgument = queryArgument;
                result = TransactionReadHelper.SetTMData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "SetTMData failed with exception", ex);
                throw;
            }
            return result;
        }
        public bool ValidateKey(QueryArgument queryArgument)
        {
            var result = false;
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                result = TransactionReadHelper.ValidateKey();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetSRNumber failed with exception", ex);
                throw;
            }
            return result;
        }
        public bool UpdateStatus(string SR, DataBaseInfo dataBaseInfo)
        {
            bool result;
            try
            {
                result = TransactionWriteHelper.UpdateSRStatus(SR, dataBaseInfo);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "UpdateStatus failed with exception", ex);
                throw;
            }
            return result;
        }
        public bool UpdateSendBackStatus(QueryArgument queryArgument)
        {
            bool result;
            try
            {
                result = TransactionWriteHelper.UpdateSendBackStatus(queryArgument);
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "UpdateSendBackStatus failed with exception", ex);
                throw;
            }
            return result;
        }

        public BillTrackings GetAlternativeOrdData(QueryArgument queryArgument)
        {
            var billTrackings = new BillTrackings();
            try
            {
                TransactionReadHelper.QueryArgument = queryArgument;
                billTrackings = TransactionReadHelper.GetAlternativeOrdData();
            }
            catch (Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "GetAlternativeOrdData failed with exception", ex);
                throw;
            }
            return billTrackings;
        }
    }
}
