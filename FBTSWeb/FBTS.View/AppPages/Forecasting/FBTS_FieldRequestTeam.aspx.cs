using FBTS.App.Library;
using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.EventLogger;
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
    public partial class FBTS_FieldRequestTeam : System.Web.UI.Page
    {
        public readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        public readonly TransactionManager _transactionManager = new TransactionManager();
        private readonly GenericManager _genericClass = new GenericManager();
        protected void Page_Init(object sender, EventArgs e)
        {
            ForecastingGridViewListControl.chkClick += FillForm;
            ForecastingPart.setPartAutopostBack = true;
            ForecastingPart.ddlPartselectedIndexchanged += SelectCategories;
            ForecastingPart.addclick += SelectCategories;
            ForecastingPart.addclick += Addclick;
            ForecastingPart.txtPartTextChange += txtPartTextChange;
            ForecastingGridViewListControlId.onGVListDataPageIndexChanging += onGridListPageIndexChanges;
            ForecastingGridViewListControl.onGVListDataSorting += onGVListDataSorting;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Stage"] != null)
                    TransactionStageControlId1.ActiveStage = Request.QueryString["Stage"].ToString();
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
                ForecastingPart.IsVisiablePartText = true;
                ForecastingPart.IsEnablePartType = false;
                ForecastingHead.SetRequestLoctionAutopostBack = true;
                //ForecastingHead.IsEnableLocation = false;
                ForecastingHead.IsEnableCustomer = true;
                BindData();
            }
            catch(Exception ex)
            {
                EventLogger.LogEvent(SysEventType.ERROR.ToString(), "Error", "FieldRequest failed in page load with exception ", ex);
            }
        }
        #region
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        public TransactionStageControl TransactionStageControlId1
        {
            get { return TransactionStageControlId; }
        }
        public bool DivAction
        {
            set
            {
                divSave.Visible = value;
                divAdd.Visible = !value;
                uplActions.Update();
            }
        }
        public string Action
        {
            get { return hidAction.Value.Trim(); }
            set { hidAction.Value = value.Trim(); }
        }
        public ForecastingGridViewListControl ForecastingGridViewListControl
        {
            get
            {
                return ForecastingGridViewListControlId;
                uplForm.Update();
            }
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
        #endregion
        protected void FillForm(object sender, EventArgs e)
        {
            var FRNumber = ForecastingGridViewListControl.OrderNumber;
            var amndNo = ForecastingGridViewListControl.Amdno;
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = FRNumber,
                filter1 =amndNo,
                filter2 = Constants.TRNLogedOFF,
                filter4 = Constants.RetriveForm,
                FilterKey = Constants.TableOrderDetail
            };
            var orderTxn = _transactionManager.GetOrderData(queryArgument);
            var firstOrDefault = orderTxn.FirstOrDefault();
            if (firstOrDefault == null) return;
            ForecastingHead.clearData();
            firstOrDefault.orderHead.OrderAmendmentNumber = (firstOrDefault.orderHead.OrderAmendmentNumber.ToInt() + 1).ToString();
            ForecastingHead.SetData(firstOrDefault.orderHead);
            ForecastingPart.clearForm();          
            ForecastingPart.GVPartData = firstOrDefault.orderDetails;
            Action = Constants.InsertAction;
            fillReferences();
            uplView.Update();
            DivAction = true;
        }
        private void fillReferences()
        {
            var FRNumber = ForecastingGridViewListControl.OrderNumber;
            var amndNo = ForecastingGridViewListControl.Amdno;
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = FRNumber,
                filter1 = amndNo,
                filter2 = TransactionStageControlId1.ActiveStage,
                FilterKey = Constants.FieldRequestReference
            };
            var txnReferences = _transactionManager.GetTxnReferences(queryArgument);
            bool flag = true;
            foreach (var txnref in txnReferences)
            {
                flag = true;
                while (flag)
                {
                    var txnPageRef = getReferenceTextBox(RefrenceControl1);
                    if (txnPageRef.RefCode.Trim() == txnref.RefCode.Trim())
                    {
                        if (RefrenceControl1.IsDateRequired)
                            setReferenceTextBox(RefrenceControl1, Dates.FormatDate(txnref.Date, Constants.Format02));
                        else
                            setReferenceTextBox(RefrenceControl1, txnref.Text);
                        flag = false;
                        break;
                    }
                     txnPageRef = getReferenceTextBox(RefrenceControl2);
                    if (txnPageRef.RefCode.Trim() == txnref.RefCode.Trim())
                    {
                        if (RefrenceControl2.IsDateRequired)
                            setReferenceTextBox(RefrenceControl2, Dates.FormatDate(txnref.Date, Constants.Format02));
                        else
                            setReferenceTextBox(RefrenceControl2, txnref.Text);
                        flag = false;
                        break;
                    }
                    txnPageRef = getReferenceTextBox(RefrenceControl3);
                    if (txnPageRef.RefCode.Trim() == txnref.RefCode.Trim())
                    {
                        if (RefrenceControl3.IsDateRequired)
                            setReferenceTextBox(RefrenceControl3, Dates.FormatDate(txnref.Date, Constants.Format02));
                        else
                            setReferenceTextBox(RefrenceControl3, txnref.Text);
                        flag = false;
                        break;
                    }
                    txnPageRef = getReferenceTextBox(RefrenceControl4);
                    if (txnPageRef.RefCode.Trim() == txnref.RefCode.Trim())
                    {
                        if (RefrenceControl4.IsDateRequired)
                            setReferenceTextBox(RefrenceControl4, Dates.FormatDate(txnref.Date, Constants.Format02));
                        else
                            setReferenceTextBox(RefrenceControl4, txnref.Text);
                        flag = false;
                        break;
                    }
                    txnPageRef = getReferenceTextBox(RefrenceControl5);
                    if (txnPageRef.RefCode.Trim() == txnref.RefCode.Trim())
                    {
                        if (RefrenceControl5.IsDateRequired)
                            setReferenceTextBox(RefrenceControl5, Dates.FormatDate(txnref.Date, Constants.Format02));
                        else
                            setReferenceTextBox(RefrenceControl5, txnref.Text);
                        flag = false;
                        break;
                    }
                    txnPageRef = getReferenceTextBox(RefrenceControl6);
                    if (txnPageRef.RefCode.Trim() == txnref.RefCode.Trim())
                    {
                        if (RefrenceControl6.IsDateRequired)
                            setReferenceTextBox(RefrenceControl6, Dates.FormatDate(txnref.Date, Constants.Format02));
                        else
                            setReferenceTextBox(RefrenceControl6, txnref.Text);
                        flag = false;
                        break;
                    }
                }
            }
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
            var date=Convert.ToDateTime(Constants.DefaultDate);
            var text=string.Empty;
            if(referenceControl.IsDateRequired)
                date=Dates.ToDateTime(referenceControl.ParameterInput,DateFormat.Format_01);
            else
                text=referenceControl.ParameterInput;
            var reference = new TxnReference
            {
                RefCode = referenceControl.ParameterCode,
                Date=date,
                Text=text
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
                    Action = Action,
                    DataBaseInfo = UserContext.DataBaseInfo,
                    StageId = TransactionStageControlId.ActiveStage.Trim(),
                    Bu = UserContext.UserProfile.Bu,
                    Off = Constants.TRNLogedOFF,
                    Branch = UserContext.UserProfile.Branch,
                    LogedUser = UserContext.UserId,
                });
            var firstOrDefault = orderTransactions.FirstOrDefault();
            var references = getReferences();
            if (firstOrDefault != null)
                firstOrDefault.References = references;
            if (_transactionManager.SetFieldRequests(orderTransactions))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.FieldRequestSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Field Request Saved",
                    GlobalCustomResource.FieldRequestSaved, true);
                clearForm();
                DivAction = false;
                BindData();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.FieldRequestFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Field Request Update Failed",
                    GlobalCustomResource.FieldRequestFailed, true);
            }
        }
        protected void SelectCategories(object sender, EventArgs e)
        {
            BindPartsDetail();
        }
        public void BindPartsDetail()
        {
            if (ForecastingPart.PartNumber.Trim() == string.Empty)
            {
                ForecastingPart.CategoryCode = string.Empty;
                ForecastingPart.PartType = string.Empty;
            }
            else
            {
                var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    Key = ForecastingPart.PartNumber,
                    filter1 = string.Empty,
                    filter4 = Constants.RetriveForm,
                    FilterKey = Constants.TableMaterials
                };
                var parts = _controlPanel.GetMaterials(queryArgument);
                var firstOrDefault = parts.FirstOrDefault();
                if (firstOrDefault == null) return;

                ForecastingPart.CategoryCode = firstOrDefault.MaterialType.Id;
                ForecastingPart.PartType = firstOrDefault.MaterialGroup.Id;
            }
            uplView.Update();
        }
        public void clearForm()
        {
            ForecastingHead.clearData();
            ForecastingPart.clearForm();
            ForecastingPart.GVPartData = new OrderDetails();
            if (divref1.Visible)
            setReferenceTextBox(RefrenceControl1, string.Empty);
            if (divref2.Visible)
                setReferenceTextBox(RefrenceControl2, string.Empty);
            if (divref3.Visible)
                setReferenceTextBox(RefrenceControl3, string.Empty);
            if (divref4.Visible)
                setReferenceTextBox(RefrenceControl4, string.Empty);
            if (divref5.Visible)
                setReferenceTextBox(RefrenceControl5, string.Empty);
            if (divref6.Visible)
                setReferenceTextBox(RefrenceControl6, string.Empty);
            Action = Constants.InsertAction;
        }
        private void setReferenceTextBox(RefrenceControl referenceControl,string str)
        {
            referenceControl.BindData(str);
        }
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            DivAction = true;
            clearForm();
            ForecastingHead.Id = _genericClass.GetNewMasterNumber(Constants.FieldRequestDefaultNumber, Constants.FieldRequestGenrateNumberType, UserContext.DataBaseInfo);
            uplView.Update();
        }
        protected void onGridListPageIndexChanges(object sender, GridViewPageEventArgs e)
        {
            BindData();
        }
        protected void onGVListDataSorting(object sender, GridViewSortEventArgs e)
        {
            BindData();
        }
        public void BindData()
        {
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                filter1 = Constants.TRNLogedOFF,
                filter2=Constants.PurchaseRequestTdType,
                filter4 = Constants.RetriveList,
                FilterKey = Constants.TableOrderDetail
            };
            var orderTxn = _transactionManager.GetOrderData(queryArgument);
            ForecastingGridViewListControl.OrderTxns = orderTxn;
            uplForm.Update();
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            DivAction = false;
        }
        protected void Addclick(object sender, EventArgs e)
        {
            ForecastingPart.clearForm();
            //ForecastingPart.changepartddlIndex();
            //BindPartsDetail();
        }
        protected void txtPartTextChange(object sender, EventArgs e)
        {
            BindPartsDetail();
        }
    }
}