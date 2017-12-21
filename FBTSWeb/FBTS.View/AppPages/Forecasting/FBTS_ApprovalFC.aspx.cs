using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.View.UserControls.Common;
using FBTS.View.UserControls.ForecastingCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using FBTS.App.Library;
using System.Web.UI.WebControls;
using FBTS.Business.Manager;
using FBTS.Model.Transaction.Transactions;
using FBTS.Model.Transaction;
using FBTS.View.Resources.ResourceFiles;

namespace FBTS.View.AppPages.Forecasting
{
    public partial class FBTS_ApprovalFC : System.Web.UI.Page
    {
        public readonly TransactionManager _transactionManager = new TransactionManager();
        protected void Page_Init(object sender, EventArgs e)
        {
            ForecastingGridViewListControl.chkClick += Approve;
            ForecastingGridViewListControl.onGVListDataPageIndexChanging += onGridListPageIndexChanges;
            ForecastingGridViewListControl.onGVListDataSorting += onGVListDataSorting;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Stage"] != null)
                TransactionStageControlId1.ActiveStage = Request.QueryString["Stage"].ToString().Trim();
            if (IsPostBack) return;
            this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            fillHeaderGrid();
        }
        protected void onGridListPageIndexChanges(object sender, GridViewPageEventArgs e)
        {
            fillHeaderGrid();
        }
        protected void onGVListDataSorting(object sender, GridViewSortEventArgs e)
        {
            fillHeaderGrid();
        }
        public void fillHeaderGrid()
        {
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {                
                filter1 = Constants.TRNInProcessOFF,
                filter2 = Constants.PurchaseRequestTdType,               
                FilterKey = Constants.TableFolloupApprove
            };
            var orderTxn = _transactionManager.GetFollowupDataForApprove(queryArgument);
            KeyValuePairItems headers = new KeyValuePairItems();
            headers.Add(new KeyValuePairItem("1", Constants.ForcastingHeader));
            headers.Add(new KeyValuePairItem("2", Constants.FRHeader));
            ForecastingGridViewListControl.GVHeaders = headers;
            ForecastingGridViewListControl.IsVisiableColumn(2, true);
            ForecastingGridViewListControl.IsVisiableColumn(6, true);
            ForecastingGridViewListControl.IsVisiableColumn(7, true);
            ForecastingGridViewListControl.IsVisiableColumn(8, true);
            ForecastingGridViewListControl.IsVisiableColumn(9, true);
            ForecastingGridViewListControl.IsVisiableColumn(10, true);
            ForecastingGridViewListControl.changeActionName = Constants.ApproveAction;

            ForecastingGridViewListControl.OrderTxns = orderTxn;
            uplForm.Update();
        }
        #region Property
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        public TransactionStageControl TransactionStageControlId1
        {
            get { return TransactionStageControlId; }
        }
        public ForecastingGridViewListControl ForecastingGridViewListControl
        {
            get { return ForecastingGridViewListControlId; }
        }
        #endregion
        protected void Approve(object sender, EventArgs e)
        {
            var orderTransactions = new OrderTransactions();
            var orderDeatils=new OrderDetails();
            orderDeatils.Add(new OrderDetail { SlNo = ForecastingGridViewListControl.Amdno.Trim() });
            orderTransactions.Add(new OrderTransaction
             {
                 orderHead = new OrderHead
                 {
                     OrderNumber = ForecastingGridViewListControl.OrderNumber.Trim(),
                 },
                 orderDetails = orderDeatils,
                 Action = Constants.UpdateAction,
                 DataBaseInfo = UserContext.DataBaseInfo,
                 Off = Constants.TRNCompletedOFF,
             });
            if (_transactionManager.SetFollowupApprove(orderTransactions))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.ApprovedSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Approved Data Saved",
                    GlobalCustomResource.ApprovedSaved, true);
                fillHeaderGrid();
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.ApprovedFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Approved Data Update Failed",
                    GlobalCustomResource.ApprovedFailed, true);
            }
        }
    }
}