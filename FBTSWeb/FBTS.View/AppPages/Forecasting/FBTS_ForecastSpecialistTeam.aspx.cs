using FBTS.App.Library;
using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.Model.Transaction;
using FBTS.Model.Transaction.Transactions;
using FBTS.View.Resources.ResourceFiles;
using FBTS.View.UserControls.Common;
using FBTS.View.UserControls.ForecastingCommon;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View.AppPages.Forecasting
{
    public partial class FBTS_ForecastSpecialistTeam : System.Web.UI.Page
    {
        public readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        public readonly TransactionManager _transactionManager = new TransactionManager();
        private readonly GenericManager _genericClass = new GenericManager();
        protected void Page_Init(object sender, EventArgs e)
        {
            ForecastingGridViewListControl.chkClick += FillForm;
            ForecastingPart.setPartAutopostBack = true;
            ForecastingPart.ddlPartselectedIndexchanged += SelectCtegories;
            ForecastingPart.addclick += Addclick;
            ForecastingGridViewListControlId.onGVListDataPageIndexChanging += onGridListPageIndexChanges;
            ForecastingGridViewListControlId.onGVListDataSorting += onGVListDataSorting;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Stage"] != null)
                TransactionStageControlId1.ActiveStage = Request.QueryString["Stage"].ToString().Trim();
            if (IsPostBack) return;
            this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));

            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                filter1 = Constants.DvStages,
                filter2 = TransactionStageControlId1.ActiveStage,
                FilterKey = Constants.ReferencesType
            };
            var references = _controlPanel.GetReferences(queryArgument);
            BindReferences(references);
            fillHeaderGrid();
            ForecastingPart.addKeyupText(ForecastingPart.txtqty, "onkeyup");
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
                filter1 = Constants.TRNLogedOFF+","+Constants.TRNInProcessOFF,
                filter2 = Constants.PurchaseRequestTdType,
                filter4 = Constants.RetriveList,
                FilterKey = Constants.TableOrderDetail
            };
            var orderTxn = _transactionManager.GetOrderData(queryArgument);
            ForecastingGridViewListControl.OrderTxns = orderTxn;
            uplForm.Update();
        }
        #region
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        public bool DivAction
        {
            set
            {
                divSave.Visible = value;
                uplActions.Update();
            }
        }
        public TransactionStageControl TransactionStageControlId1
        {
            get { return TransactionStageControlId; }
        }
        public ForecastingGridViewListControl ForecastingGridViewListControl
        {
            get { return ForecastingGridViewListControlId; }
        }

        public ForecastingControl ForecastingHead
        {
            get { return ForecastingControlId; }
        }
        public ForecastingPartControl ForecastingPart
        {
            get { return ForecastingPartControlId; }
        }
        public RefrenceControl RefrenceControl1
        {
            get { return RefrenceControlId1; }
        }
        public RefrenceControl RefrenceControl2
        {
            get { return RefrenceControlId2; }
        }
        public RefrenceControl RefrenceControl3
        {
            get { return RefrenceControlId3; }
        }
        public RefrenceControl RefrenceControl4
        {
            get { return RefrenceControlId4; }
        }
        public RefrenceControl RefrenceControl5
        {
            get { return RefrenceControlId5; }
        }
        public RefrenceControl RefrenceControl6
        {
            get { return RefrenceControlId6; }
        }
        public string Action
        {
            get {return hidAction.Value.Trim(); }
            set { hidAction.Value = value.Trim(); }
        }
        #endregion
        protected void FillForm(object sender, EventArgs e)
        {
            var partNumber = ForecastingGridViewListControl.OrderNumber;
            var amndNo = ForecastingGridViewListControl.Amdno;
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = partNumber,
                filter1 = amndNo,
                filter2 = Constants.TRNLogedOFF + "," + Constants.TRNInProcessOFF,
                filter4 = Constants.RetriveForm,
                FilterKey = Constants.TableOrderDetail
            };
            var orderTxn = _transactionManager.GetOrderData(queryArgument);
            var firstOrDefault=orderTxn.FirstOrDefault();
            if(firstOrDefault==null)return;            
            ForecastingHead.clearData();
            ForecastingHead.SetData(firstOrDefault.orderHead);
            ForecastingHead.IsEnableLocation = false;

            ForecastingPart.clearForm();
            ForecastingPart.IsEnablePartType = false;            
            ForecastingPart.StatusOrderNoDivTrue = true;
            ForecastingPart.GVPartData = new OrderDetails();
            
            var keyValuePairItems = new KeyValuePairItems();
            keyValuePairItems.AddRange(firstOrDefault.orderDetails.Select(row => new KeyValuePairItem(row.PartDetail.PartNumber.Trim().ToUpper() +" "+ Constants.SpecialCharApprox +" "+ row.PartDetail.Description.Trim(),
                                                                                                                                                        row.PartDetail.PartNumber.Trim().ToUpper())));
            ForecastingPart.fillDdl(ForecastingPart.PartControl as DropDownList, keyValuePairItems);

            ForecastingPart.OrderDetailData = firstOrDefault.orderDetails;
            Action = Constants.InsertAction;
            uplView.Update();
            DivAction = true;
        }
        private void BindReferences(WFComponentSubs references)
        {
            int i = 1;
            foreach (var reference in references)
            {
                switch (i)
                {
                    case 1:
                        divref1.Visible = true;
                        setReferenceTextBox(RefrenceControl1, reference);
                        break;
                    case 2:
                        divref2.Visible = true;
                        setReferenceTextBox(RefrenceControl2, reference);
                        break;
                    case 3:
                        divref3.Visible = true;
                        setReferenceTextBox(RefrenceControl3, reference);
                        break;
                    case 4:
                        divref4.Visible = true;
                        setReferenceTextBox(RefrenceControl4, reference);
                        break;
                    case 5:
                        divref5.Visible = true;
                        setReferenceTextBox(RefrenceControl5, reference);
                        break;
                    case 6:
                        divref6.Visible = true;
                        setReferenceTextBox(RefrenceControl6, reference);
                        break;

                }
                i++;
            }
        }
        private void setReferenceTextBox(RefrenceControl referenceControl, WFComponentSub reference)
        {
            referenceControl.ParameterLabel = reference.WFCDesp;
            referenceControl.IsDateRequired = reference.Relation1.Trim() == Constants.TxnRefTypeDate.Trim() ? true : false;
            referenceControl.ParameterCode = reference.WFCSCode;
        }
        private TxnReference getReferenceTextBox(RefrenceControl referenceControl)
        {
            var date = Convert.ToDateTime(Constants.DefaultDate);
            var text = string.Empty;
            if (referenceControl.IsDateRequired)
                date = Dates.ToDateTime(referenceControl.ParameterInput, DateFormat.Format_05);
            else
                text = referenceControl.ParameterInput;
            var reference = new TxnReference
            {
                RefCode = referenceControl.ParameterCode,
                Date = date,
                Text = text
            };
            return reference;
        }
        private TxnReferences getReferences()
        {
            var references = new TxnReferences();
            if (divref1.Visible)
                references.Add(getReferenceTextBox(RefrenceControl1));
            if (divref2.Visible)
                references.Add(getReferenceTextBox(RefrenceControl2));
            if (divref3.Visible)
                references.Add(getReferenceTextBox(RefrenceControl3));
            if (divref4.Visible)
                references.Add(getReferenceTextBox(RefrenceControl4));
            if (divref5.Visible)
                references.Add(getReferenceTextBox(RefrenceControl5));
            if (divref6.Visible)
                references.Add(getReferenceTextBox(RefrenceControl6));
            return references;
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            OrderTransactions orderTransactions = new OrderTransactions();
            orderTransactions.Add(new OrderTransaction
            {
                OrderType = Constants.PurchaseRequestTdType,
                orderHead = ForecastingHead.GetData(),
                orderDetails = ForecastingPart.GVPartData,
                updateOrderDeatils=ForecastingPart.OrderDetailData,
                Action = Action,
                DataBaseInfo = UserContext.DataBaseInfo,
                StageId = TransactionStageControlId.ActiveStage.Trim(),
                Bu = UserContext.UserProfile.Bu,
                Off = Constants.TRNInProcessOFF,
                Branch = UserContext.UserProfile.Branch,
                LogedUser = UserContext.UserId,
            });
            var firstOrDefault = orderTransactions.FirstOrDefault();
            var references = getReferences();
            if (firstOrDefault != null)
                firstOrDefault.References = references;
            if (_transactionManager.SetForcasting(orderTransactions))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.ForecastingSave;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Forecasting Saved",
                    GlobalCustomResource.ForecastingSave, true);

                fillHeaderGrid();
                DivAction = false;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.ForecastingFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Forecasting Update Failed",
                    GlobalCustomResource.ForecastingFailed, true);
            }
        }
        protected void SelectCtegories(object sender, EventArgs e)
        {
            ForecastingPart.RetriveDatabasedonPart();
            uplView.Update();
        }
        protected void Addclick(object sender, EventArgs e)
        {
            ForecastingPart.addSqty();
            ForecastingPart.clearForm();
            //ForecastingPart.changepartddlIndex();
            //ForecastingPart.RetriveDatabasedonPart();
            
            uplView.Update();
        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            DivAction = false;
        }
    }
}