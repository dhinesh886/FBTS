using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.View.UserControls.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FBTS.App.Library;
using FBTS.Business.Manager;
using FBTS.View.UserControls.ForecastingCommon;
using FBTS.Model.Transaction;
using FBTS.Library.Common;
using FBTS.View.Resources.ResourceFiles;
using FBTS.Model.Transaction.Transactions;
using FBTS.Model.Transaction.Enum;
using System.Data.OleDb;
using System.IO;
using System.Data;

namespace FBTS.View.AppPages.Billing_Tracking
{
    public partial class FBTS_VarificationTeam : System.Web.UI.Page
    {
        public readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        public readonly TransactionManager _transactionManager = new TransactionManager();
        private readonly GenericManager _genericClass = new GenericManager();

        protected void Page_Init(object sender, EventArgs e)
        {
            ForecastingGridViewListControl.chkClick += FillForm;
            ForecastingGridViewListControl.onCancelClick += CancelSR;
            ForecastingGridViewListControl.Ddllbl1 = Constants.LocationLablel;
            ForecastingGridViewListControl.fillGrid += FillGride;            
            BillingPartControl1.setPartAutopostBack = true;
            BillingPartControl1.ddlPartselectedIndexchanged += SelectCategories;
            ForecastingHead.ddlCurrenStatusSelectedIndex += ddlCurrenStatusSelectedIndex;
            BillingPartControl1.txtPartTextChange += txtPartTextChange;
            BillingPartControl1.DeleteEdit += Edit;
            BillingPartControl1.addclick += UpdatePartDetail;
            ConfirmationPopup.YesClicked += ConfirmYes;
            ForecastingGridViewListControl.onGVListDataPageIndexChanging += onGridListPageIndexChanges;
            ForecastingGridViewListControl.ontext1change += ontxtSearchChange;
            ForecastingGridViewListControl.onGVListDataSorting += onGVListDataSorting;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Stage"] != null)
                TransactionStageControlId1.ActiveStage = Request.QueryString["Stage"].ToString();
            if (IsPostBack) return;
            this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            if (Request.QueryString["PageTitle"] != null)
                pageTitle.InnerText = Request.QueryString["PageTitle"].ToString().Trim();
            if (Request.QueryString["FormType"] != null)
                StageType = Request.QueryString["FormType"].ToString().Trim();
            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.WHCType),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.AllType),
                            new KeyValuePairItem(Constants.masterType,Constants.TableAccounts)
                        };
            _genericClass.LoadDropDown(ForecastingGridViewListControl.Ddl1, filter, null, UserContext.DataBaseInfo);

            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                filter1 = Constants.DvStages,
                filter2 = TransactionStageControlId1.ActiveStage,
                FilterKey = Constants.ReferencesType
            };
            var references = _controlPanel.GetReferences(queryArgument);
            BindReferences(references);
            ForecastingHead.IdLabel = Constants.MainSRHeader;
            ForecastingHead.DateLabel = Constants.PrcessingDateHeader;
            ForecastingHead.IsVisiableCDType = true;
            ForecastingHead.IsVisiableBillStatus = true;
            ForecastingHead.IsVisiableCurrentStatus = true;
            ForecastingHead.IsVisiablePONumber = true;
            ForecastingHead.IsVisibleGSTNumber = true;
            ForecastingHead.IsEnableID = true;
            ForecastingHead.IsEnableDate = false;
            ForecastingGridViewListControl.IsVisiableColumn(15, true);
            if (StageType == Constants.DeviationType)
            {
                ForecastingGridViewListControl.IsVisiableColumn(16, true);
                ForecastingGridViewListControl.IsVisiableColumn(17, true);
                ForecastingGridViewListControl.IsVisiableColumn(18, true);
                ForecastingGridViewListControl.IsVisiableColumn(23, true);//Cancel button for Deviation Team               
                IsVisiableStatus = true;
            }
            ForecastingGridViewListControl.SetText1(Constants.LabelSREnter, Constants.ToolTipSrNumberSearch, 12);
            BindData();
            if (StageType == Constants.DeviationType)
                IsVisiableDivAdd = false;
            else
                IsVisiableDivAdd = true;
            ForecastingHead.IsEnableBillStatus = false;
            ForecastingHead.IsCurrentStatusAutopostBack = true;
            ForecastingHead.SetRequestLoctionAutopostBack = true;

            BillingPartControl1.IsVisiablePartText = true;
            BillingPartControl1.IsEnablePartType = false;
            BillingPartControl1.SetGridViewFooter = true;
            ForecastingHead.IsEnableCustomer = true;

            // ScriptManager.GetCurrent(this).RegisterPostBackControl(this.BillingPartControl1.ImportButton);
        }
        protected void FillGride(object sender, EventArgs e)
        {
            BindData();
        }
        protected void onGridListPageIndexChanges(object sender, GridViewPageEventArgs e)
        {
            BindData();
        }
        protected void ontxtSearchChange(object sender,EventArgs e)
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
                Key = FilterLocation,
                //Constants.TRNInProcessOFF
                filter1 = StageType == Constants.DeviationType ? Status : Constants.TRNLogedOFF,
                filter2 = Constants.SaleRequestTdType,
                filter3 = StageType == Constants.DeviationType ? StageType + "," + Constants.DeviationOrder : StageType,
                filter4 = Constants.RetriveList,
                filter6 = ForecastingGridViewListControl.Text1Value,
                FilterKey = Constants.TableOrderDetail
            };
            var orderTxn = _transactionManager.GetOrderData(queryArgument);
            KeyValuePairItems headers = new KeyValuePairItems();
            headers.Add(new KeyValuePairItem("2", Constants.MainSRHeader));
            ForecastingGridViewListControl.GVHeaders = headers;
            ForecastingGridViewListControl.IsVisiableColumn(1, false);
            ForecastingGridViewListControl.IsVisiableColumn(2, true);
            ForecastingGridViewListControl.IsVisiableColumn(16, true);
            if (StageType == Constants.DeviationType)
            {
                ForecastingGridViewListControl.IsVisiableColumn(20, true);
            }
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
                IsVisiableDivAdd = !value;
                uplActions.Update();
            }
        }
        public bool IsVisiableDivAdd
        {
            set
            {
                divAdd.Visible = value;
                divSearch.Visible = value;
                uplActions.Update();
            }
        }
        public string Status
        {
            get
            {
                return IsVisiableStatus ? ddlStatus.SelectedValue : Constants.TRNLogedOFF;//fresh then IP else PN for pendding
            }
            set
            {
                if (IsVisiableStatus)
                    WebControls.SetCurrentComboIndex(ddlStatus, value);
            }
        }
        public string Action
        {
            get { return hidAction.Value.Trim(); }
            set { hidAction.Value = value.Trim(); }
        }
        public string StageType
        {
            get { return hidType.Value.Trim(); }
            set { hidType.Value = value.Trim(); }
        }
        public ForecastingGridViewListControl ForecastingGridViewListControl
        {
            get
            {
                return ForecastingGridViewListControlId;
            }
        }
        public TransactionStageControl TransactionStageControlId1
        {
            get { return TransactionStageControlId; }
        }
        public ForecastingControl ForecastingHead
        {
            get { return ForecastingControlId; }
        }
        public ValidationDdl ValidationDdlId1
        {
            get { return ValidationDdlId; }
        }
        public BillingPartControl BillingPartControl1
        {
            get { return BillingPartControlId; }
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
        public string Ord_No
        {
            get { return hidOrd_No.Value.Trim(); }
            set { hidOrd_No.Value = value.Trim(); }
        }
        public string FilterLocation
        {
            get { return ForecastingGridViewListControl.Ddl1.SelectedValue.ToTrimString(); }
            set { ForecastingGridViewListControl.Ddl1.SelectedValue = value.Trim(); }
        }
        public bool IsVisiableSendTo
        {
            set
            {
                lnkSendTo.Visible = value;
                uplActions.Update();
            }
        }
        public bool IsVisiableSave
        {
            set
            {
                lnkSave.Visible = value;
                uplActions.Update();
            }
        }
        public bool IsVisiableExcelUpload
        {
            set
            {
                if (value)
                    divUploadExcel.Style.Add("display", "block");
                else
                    divUploadExcel.Style.Add("display", "none");
            }
        }
        public bool IsVisiableStatus
        {
            get
            {
                return divStatus.Visible;
            }
            set
            {
                divStatus.Visible = value;
            }
        }
        #endregion
        protected void FillForm(object sender, EventArgs e)
        {            
            var FRNumber = ForecastingGridViewListControl.OrderNumber;
            var amndNo = ForecastingGridViewListControl.Amdno;
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = FRNumber,
                filter1 = amndNo,
                filter2 = StageType == Constants.DeviationType ? Status : Constants.TRNLogedOFF,
                filter3 = StageType,
                filter4 = Constants.RetriveForm,
                FilterKey = Constants.TableOrderDetail
            };
            var tracking = _transactionManager.GetTrackingData(queryArgument);
            var firstOrDefault = tracking.FirstOrDefault();
            if (firstOrDefault == null) return;
            Ord_No = firstOrDefault.OrdNumber;
            ForecastingHead.clearData();
            if (StageType == Constants.DeviationType)
            {
                firstOrDefault.Head.OrderAmendmentNumber = (firstOrDefault.Head.OrderAmendmentNumber.ToInt() + 1).ToString();
            }
            ForecastingHead.SetData(firstOrDefault.Head);

            ValidationDdlId1.clearForm();
            ValidationDdlId1.SetData(firstOrDefault.BillValiditions);

            fillReferences();
            DivAction = true;
            if (StageType == Constants.DeviationType)
            {
                IsVisiableStatus = false;
                IsVisiableSendTo = false;
                BillingPartControl1.changeActionName = Constants.UpdateAction;
                Action = Constants.InsertAction;
                BillingPartControl1.IsVisiableInputDiv = false;
                BillingPartControl1.isVisiableCell(10, false);
                BillingPartControl1.isVisiableCell(17, false);

                ForecastingHead.IsEnableID = false;
                ForecastingHead.IsEnableSubId = false;
                ForecastingHead.IsEnableLocation = false;
                //ForecastingHead.IsEnableDate = false;
                ForecastingHead.IsEnableBillLocation = false;
                ForecastingHead.IsEnableCDType = false;
                BillingPartControl1.PartDetails = firstOrDefault.Details;

            }
            else
            {
                IsVisiableSendTo = true;
                IsVisiableSave = false;
                Action = Constants.UpdateAction;
                BillingPartControl1.IsVisiableInputDiv = true;
            }
            BillingPartControl1.clearForm();
            BillingPartControl1.GVPartData = firstOrDefault.Details;

            uplView.Update();
        }
        protected void CancelSR(object sender, EventArgs e)
        {
            ConfirmationPopup.MessageHeaderText = "CANCEL THIS SR!";
            ConfirmationPopup.MessageBodyText = "Are you sure?";
            if (ForecastingGridViewListControl.BillStatus == Constants.DeviationType)
            {
                ConfirmationPopup.Show();
                uplForm.Update();
            }
            else
            {
                CustomMessageControl.MessageBodyText = "SR already processed, cannot cancel now!!";
                CustomMessageControl.MessageType = MessageTypes.Warning;
                CustomMessageControl.ShowMessage();
                return;
            }
        }
        protected void ConfirmYes(object sender, EventArgs e)
        {
            var result = _transactionManager.UpdateStatus(ForecastingGridViewListControl.OrderNumber, UserContext.DataBaseInfo);
            if (result == true)
            {
                CustomMessageControl.MessageBodyText = "SR has been Cancelled";
                CustomMessageControl.MessageType = MessageTypes.Warning;
                CustomMessageControl.ShowMessage();
                BindData();
            }
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
            var date = Convert.ToDateTime(Constants.DefaultDate);
            var text = string.Empty;
            if (referenceControl.IsDateRequired)
                date = Dates.ToDateTime(referenceControl.ParameterInput, DateFormat.Format_01);
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
        public void clearForm()
        {
            ForecastingHead.clearData();
            BillingPartControl1.clearForm();
            ValidationDdlId1.clearForm();
            BillingPartControl1.GVPartData = new BillTrackingDetails();
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
            IsVisiableSendTo = false;
            IsVisiableSave = true;
        }
        private void setReferenceTextBox(RefrenceControl referenceControl, string str)
        {
            referenceControl.BindData(str);
        }
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            DivAction = true;
            clearForm();
            if (StageType == Constants.DeviationType)
                IsVisiableExcelUpload = false;
            else
                IsVisiableExcelUpload = true;
            uplView.Update();
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            if (StageType != Constants.DeviationType)
            {
                if (ForecastingHead.Id == string.Empty) return;
                QueryArgument queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    FilterKey = Ord_No,
                    filter1 = ForecastingHead.Id,
                    QueryType = Constants.SRValidationType
                };

                var result = _transactionManager.ValidateKey(queryArgument);
                if (result)
                {
                    CustomMessageControl.MessageBodyText = "Duplicate SR";
                    CustomMessageControl.MessageType = MessageTypes.Error;
                    CustomMessageControl.ShowMessage();
                    ForecastingHead.Id = string.Empty;
                    return;
                }
            }
            BillTrackings billTrackings = new BillTrackings();
            OrderHead prevUpdate = ForecastingHead.GetData();
            prevUpdate.Off = Constants.TRNCompletedOFF;
            prevUpdate.OrderAmendmentNumber = (prevUpdate.OrderAmendmentNumber.ToInt() - 1).ToString();
            var head = ForecastingHead.GetData();
            var partdeails = BillingPartControl1.GVPartData;
            if (head.BillStatus == Constants.DeviationOrder)
            {
                foreach (var partdetail in BillingPartControl1.PartDetails)
                {
                    partdeails.Where(x => x.PartDetail.PartNumber.Trim() == partdetail.PartDetail.PartNumber.Trim()).FirstOrDefault().SQuantity = partdetail.SQuantity;
                }
            }
            head.NetValue = partdeails.Sum(x => x.POValue);
            var billValidations = ValidationDdlId1.GetData();
            bool flag = true;
            if (StageType == Constants.DeviationType)
            {
                foreach (var billValidation in billValidations)
                {
                    if (billValidation.ReferenceValue == Constants.StringNotOk)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    if (head.CurrentStatus == Constants.DeviationType)
                        flag = false;
                }
            }

            if (!flag)
                head.OrderAmendmentNumber = "0";
            billTrackings.Add(new BillTracking
            {
                FormType = StageType == Constants.DeviationType ? FormType.Deviation : FormType.Verification,
                OrdNumber = Ord_No,
                OrderType = Constants.SaleRequestTdType,
                Head = head,
                UpdatePrevHead = prevUpdate,
                BillValiditions = ValidationDdlId1.GetData(),
                Details = partdeails,
                StageId = TransactionStageControlId.ActiveStage.Trim(),
                Bu = UserContext.UserProfile.Bu,
                Off = StageType == Constants.DeviationType  &&   head.BillStatus == Constants.DeviationOrder ? Constants.TRNCompletedOFF :
                                                                StageType == head.BillStatus ?  Constants.TRNPendingOFF :
                                                                StageType == Constants.DeviationType ?  Constants.TRNInProcessOFF :
                                                                                                        Constants.TRNLogedOFF,
                Branch = UserContext.UserProfile.Branch,
                LogedUser = UserContext.UserId,
                Action = flag ? Action : Constants.UpdateAction,
                DataBaseInfo = UserContext.DataBaseInfo,
            });
            var firstOrDefault = billTrackings.FirstOrDefault();
            var references = getReferences();
            if (firstOrDefault != null)
                firstOrDefault.References = references;
            if (_transactionManager.SetVerifications(billTrackings))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.VerificationSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Verification Saved",
                    GlobalCustomResource.VerificationSaved, true);
                clearForm();
                DivAction = false;
                if (StageType == Constants.DeviationType)
                {
                    IsVisiableStatus = true;
                    IsVisiableDivAdd = false;
                }
                BindData();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.VerificayionFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Verification Update Failed",
                    GlobalCustomResource.VerificayionFailed, true);
            }
        }
        protected void SelectCategories(object sender, EventArgs e)
        {
            BindPartsDetail();
        }
        public void BindPartsDetail()
        {
            if (BillingPartControl1.PartNumber.Trim() == string.Empty)
            {
                BillingPartControl1.CategoryCode = string.Empty;
                BillingPartControl1.PartType = string.Empty;
            }
            else
            {
                var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                                    {
                                        Key = BillingPartControl1.PartNumber,
                                        filter1 = string.Empty,
                                        filter4 = Constants.RetriveForm,
                                        FilterKey = Constants.TableMaterials
                                    };
                var parts = _controlPanel.GetMaterials(queryArgument);
                var firstOrDefault = parts.FirstOrDefault();
                if (firstOrDefault == null) return;

                BillingPartControl1.CategoryCode = firstOrDefault.MaterialType.Id;
                BillingPartControl1.PartType = firstOrDefault.MaterialGroup.Id;
            }

            uplView.Update();
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            DivAction = false;
            if (StageType == Constants.DeviationType)
            {
                IsVisiableDivAdd = false;
                IsVisiableStatus = true;
            }
        }

        protected void lnkSendTo_Click(object sender, EventArgs e)
        {

            if (ForecastingHead.Id == string.Empty) return;
            QueryArgument queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                FilterKey = Ord_No,
                filter1 = ForecastingHead.Id,
                QueryType = Constants.SRValidationType
            };

            var result = _transactionManager.ValidateKey(queryArgument);
            if (result)
            {
                CustomMessageControl.MessageBodyText = "Duplicate SR";
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                ForecastingHead.Id = string.Empty;
                return;
            }

            var head = ForecastingHead.GetData();
            var partdeails = BillingPartControl1.GVPartData;
            head.NetValue = partdeails.Sum(x => x.POValue);
            BillTrackings billTrackings = new BillTrackings();

            billTrackings.Add(new BillTracking
            {
                OrdNumber = Ord_No,
                OrderType = Constants.SaleRequestTdType,
                Head = head,
                BillValiditions = ValidationDdlId1.GetData(),
                Details = BillingPartControl1.GVPartData,
                StageId = TransactionStageControlId.ActiveStage.Trim(),
                Bu = UserContext.UserProfile.Bu,
                Off = Constants.TRNInProcessOFF,
                Branch = UserContext.UserProfile.Branch,
                LogedUser = UserContext.UserId,
                Action = Action,
                DataBaseInfo = UserContext.DataBaseInfo,
            });
            var firstOrDefault = billTrackings.FirstOrDefault();
            var references = getReferences();
            if (firstOrDefault != null)
                firstOrDefault.References = references;
            if (_transactionManager.SetVerifications(billTrackings))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.NextStageSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "SetNextStage Saved",
                    GlobalCustomResource.NextStageSaved, true);
                clearForm();
                DivAction = false;
                BindData();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.NextStageFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "SetNextStage Update Failed",
                    GlobalCustomResource.NextStageFailed, true);
            }
        }
        protected void ddlCurrenStatusSelectedIndex(object sender, EventArgs e)
        {
            if (ForecastingHead.BillStatus == Constants.DeviationOrder) return;
            ForecastingHead.BillStatus = ForecastingHead.CurrentStatus;
        }
        protected void txtPartTextChange(object sender, EventArgs e)
        {
            BindPartsDetail();
        }
        protected void Edit(object sender, EventArgs e)
        {
            if (StageType == Constants.DeviationType && ForecastingHead.BillStatus == Constants.DeviationType)
            {
                var lnkedit = sender as Control;
                var row = lnkedit.NamingContainer as GridViewRow;
                var partnumber = row.Cells[1].Text.Trim();

                var griddata = BillingPartControl1.GVPartData;
                var rowdatas = griddata.Where(x => x.PartDetail.PartNumber.Trim() == partnumber);
                if (!rowdatas.Any()) return;
                var rowdata = rowdatas.FirstOrDefault();
                if (rowdata == null) return;
                BillingPartControl1.IsVisiableInputDiv = true;
                BillingPartControl1.PartType = rowdata.MaterialGroup.Id;
                BillingPartControl1.CategoryCode = rowdata.MaterialType.Id;

                BillingPartControl1.PartNumberText = rowdata.PartDetail.PartNumber;

                var filter = new KeyValuePairItems
                        {                            
                            new KeyValuePairItem(Constants.filter3, BillingPartControl1.PartType),
                            new KeyValuePairItem(Constants.filter4, BillingPartControl1.PartNumberText),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextPartNo),
                            new KeyValuePairItem(Constants.masterType,Constants.TableMaterials)
                        };
                _genericClass.LoadDropDownIfMorereturnFalse(BillingPartControl1.PartControl, filter, null, UserContext.DataBaseInfo);

                BillingPartControl1.PrevSLNo = rowdata.SlNo;
                BillingPartControl1.PartNumber = rowdata.PartDetail.PartNumber;
                BillingPartControl1.Unit = rowdata.Unit.Id;
                BillingPartControl1.POQuantity = rowdata.POQuantity;
                BillingPartControl1.UnitValue = rowdata.UnitValue;
                BillingPartControl1.Modality = rowdata.Modality;

                //BillingPartControl1.IsEnablePartText = false;
                //BillingPartControl1.IsEnablePOQty = false;
                BillingPartControl1.IsEnableModality = false;
                //BillingPartControl1.IsEnableUnit = false;
                uplView.Update();
            }
            else if (StageType == Constants.DeviationType)
            {
                CustomMessageControl.MessageBodyText = "Cannot Edit Now!!";
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
            }
        }
        protected void UpdatePartDetail(object sender, EventArgs e)
        {
            if (StageType == Constants.DeviationType)
            {
                var griddata = BillingPartControl1.GVPartData;
                var rowdatas = griddata.Where(x => x.SlNo.Trim() == BillingPartControl1.PrevSLNo);
                if (!rowdatas.Any()) return;
                var rowdata = rowdatas.FirstOrDefault();
                if (rowdata == null) return;

                rowdata.PartDetail.PartNumber = BillingPartControl1.PartNumber;
                rowdata.PartDetail.Description = BillingPartControl1.PartDesp;
                rowdata.MaterialGroup.Id = BillingPartControl1.PartType;
                rowdata.MaterialGroup.Description = BillingPartControl1.PartTypeDesp;
                rowdata.MaterialType.Id = BillingPartControl1.CategoryCode;
                rowdata.MaterialType.Description = BillingPartControl1.CategoryDesp;
                rowdata.Unit.Id = BillingPartControl1.Unit;
                rowdata.UnitValue = BillingPartControl1.UnitValue;
                rowdata.POQuantity = BillingPartControl1.POQuantity;

                BillingPartControl1.GVPartData = griddata;
                BillingPartControl1.IsVisiableInputDiv = false;
            }
        }
        public bool checkItemisThereInddl(DropDownList ddl, string value)
        {
            if (ddl.Items.FindByValue(value.Trim()) == null)
                return false;
            return true;
        }

        protected void fileExcelUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            if (fileExcelUpload.PostedFile == null || fileExcelUpload.PostedFile.ContentLength == 0)
                return;
            try
            {
                var msPath1 = Path.GetPathRoot(fileExcelUpload.PostedFile.FileName);

                var fileName = (Path.GetFileName(fileExcelUpload.PostedFile.FileName));
                string strFileType = Path.GetExtension(fileName).ToLower();
                if (msPath1 == string.Empty)
                {
                    msPath1 = Server.MapPath(Constants.UserExcelFileDirectory);
                    new DirectoryInfo(msPath1).CreateDirectory();

                    fileExcelUpload.PostedFile.SaveAs(Path.Combine(msPath1, fileName));
                    return;
                }


                string path = Path.Combine(msPath1);

                string connString = "";

                //Connection String to Excel Workbook
                if (strFileType.Trim() == ".xls")
                {
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (strFileType.Trim() == ".xlsx")
                {
                    //connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";
                }
                string query = "SELECT Cat_Type,Part_Type,Part_No,Unit,PO_Qty,Unit_Value,Modality FROM [Sheet1$]";
                OleDbConnection conn = new OleDbConnection(connString);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                OleDbCommand cmd = new OleDbCommand(query, conn);
                OleDbDataReader odr = cmd.ExecuteReader();
                string script = string.Empty;
                string script1 = string.Empty;
                string modality = string.Empty;
                int i = 1;
                while (odr.Read())
                {
                    i++;
                    if (odr["Part_Type"].ToString().Trim() == string.Empty)
                        script1 += " - " + "Part Type is empty @ line No. " + i.ToString();
                    else if (odr["Part_No"].ToString().Trim() == string.Empty)
                        script1 += " - " + "Part Number is empty @ line No. " + i.ToString();
                    else if (odr["Unit"].ToString().Trim() == string.Empty)
                        script1 += " - " + "Unit is empty @ line No. " + i.ToString();
                    else if (odr["PO_Qty"].ToString().Trim() == string.Empty)
                        script1 += " - " + "PO Qty is empty @ line No. " + i.ToString();
                    else if (odr["Unit_Value"].ToString().Trim() == string.Empty)
                        script1 += " - " + "Unit Value is empty @ line No. " + i.ToString();
                    else if (odr["Modality"].ToString().Trim() == string.Empty)
                        script1 += " - " + "Modality is empty @ line No. " + i.ToString();
                    else
                    {
                        bool flag = true;
                        foreach (var gvRow in BillingPartControl1.GVPartData)
                        {
                            if (gvRow.PartDetail.PartNumber.Trim() == odr["Part_No"].ToString().Trim())
                            {
                                script = odr["Part_No"].ToString().Trim() + " This Part Already added @ line No." + i.ToString();
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            if (checkItemisThereInddl(BillingPartControl1.PartTypeControl, odr["Part_Type"].ToString().Trim()))
                            {
                                var filter = new KeyValuePairItems
                            {                            
                                new KeyValuePairItem(Constants.filter3, odr["Part_Type"].ToString().Trim()),
                                new KeyValuePairItem(Constants.filter4, odr["Part_No"].ToString().Trim()),
                                new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextPartNo),
                                new KeyValuePairItem(Constants.masterType,Constants.TableMaterials)
                            };

                                _genericClass.LoadDropDownIfMorereturnFalse(BillingPartControl1.PartControl, filter, null, UserContext.DataBaseInfo);
                                if (BillingPartControl1.PartControl.Items.Count > 1)
                                    BillingPartControl1.PartControl.SelectedIndex = 1;
                                if (BillingPartControl1.PartNumber.Trim() == string.Empty)
                                    script += " - " + odr["Part_No"].ToString().Trim() + " This Part is wrong, if it is new then add in Master @ line No. " + i.ToString();
                                else
                                    BindPartsDetail();
                            }
                            else
                            {
                                script += " - " + odr["Part_Type"].ToString().Trim() + " This Part Type is wrong, if it is new then add in Master @ line No. " + i.ToString();
                            }

                            if (!checkItemisThereInddl(BillingPartControl1.UnitControl, odr["Unit"].ToString().Trim()))
                            {
                                script += " - " + odr["Unit"].ToString().Trim() + " This Unit is wrong, if it is new then add in Master @ line No. " + i.ToString();
                            }

                            if (!checkItemisThereInddl(BillingPartControl1.ModalityControl, odr["Modality"].ToString().Trim()))
                            {
                                script += " - " + odr["Modality"].ToString().Trim() + " This Modality is wrong, if it is new then add in Master @ line No. " + i.ToString();
                            }
                        }
                        if (script == string.Empty && script1 == string.Empty)
                        {
                            BillingPartControl1.CategoryCode = odr[0].ToString().Trim();
                            BillingPartControl1.PartType = odr[1].ToString().Trim();
                            BillingPartControl1.PartNumber = odr[2].ToString().Trim();
                            BillingPartControl1.Unit = odr[3].ToString().Trim();
                            BillingPartControl1.POQuantity = odr[4].ToString().Trim().ToDecimal(0);
                            BillingPartControl1.UnitValue = odr[5].ToString().Trim().ToDecimal(2);
                            BillingPartControl1.Modality = odr[6].ToString().Trim();
                            if (i == 2)
                                modality = odr[6].ToString().Trim();
                            else if (modality != odr[6].ToString().Trim())
                            {
                                script = "Check Modality of part Number :" + odr[2].ToString().Trim() + " in Excel file";
                                break;
                            }
                            BillingPartControl1.addMethod();
                        }
                    }
                    BillingPartControl1.clearForm();
                }
                odr.Close();
                if (script != string.Empty || script1 != string.Empty)
                {
                    BillingPartControl1.GVPartData = new BillTrackingDetails();
                    CustomMessageControl.MessageBodyText = script + script1;
                    CustomMessageControl.MessageType = MessageTypes.Error;
                    CustomMessageControl.ShowMessage();
                }
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                BillingPartControl1.GVPartData = new BillTrackingDetails();
                CustomMessageControl.MessageBodyText = ex.Message;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
            }
            finally
            {
                
            }
            uplView.Update();
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            string fileName = hidFileName.Value.Trim();

            if (fileName == string.Empty) return;
            try
            {
                var msPath1 = Server.MapPath(Constants.UserExcelFileDirectory);
                new DirectoryInfo(msPath1).CreateDirectory();
                fileName = fileName.Replace(@"C:\fakepath\", string.Empty);

                string strFileType = Path.GetExtension(fileName).ToLower();

                // ExcelfileUpload.SaveAs(Path.Combine(msPath1, fileName));
                //fileExcelUpload.PostedFile.SaveAs(Path.Combine(msPath1, fileName));

                string path = Path.Combine(msPath1, fileName);

                string connString = "";


                //Connection String to Excel Workbook
                if (strFileType.Trim() == ".xls")
                {
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (strFileType.Trim() == ".xlsx")
                {
                    //connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";
                }
                string query = "SELECT Part_No,Unit,PO_Qty,Unit_Value,Modality FROM [Sheet1$]";
                OleDbConnection conn = new OleDbConnection(connString);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                OleDbCommand cmd = new OleDbCommand(query, conn);
                OleDbDataReader odr = cmd.ExecuteReader();
                string script = string.Empty;
                string script1 = string.Empty;
                int i = 1;
                string modality = string.Empty;
                while (odr.Read())
                {
                    i++;

                    if (odr["Part_No"].ToString().Trim() == string.Empty)
                        script1 += " - " + "Part Number is empty @ line No. " + i.ToString();
                    else if (odr["Unit"].ToString().Trim() == string.Empty)
                        script1 += " - " + "Unit is empty @ line No. " + i.ToString();
                    else if (odr["PO_Qty"].ToString().Trim() == string.Empty)
                        script1 += " - " + "PO Qty is empty @ line No. " + i.ToString();
                    else if (odr["Unit_Value"].ToString().Trim() == string.Empty)
                        script1 += " - " + "Unit Value is empty @ line No. " + i.ToString();
                    else if (odr["Modality"].ToString().Trim() == string.Empty)
                        script1 += " - " + "Modality is empty @ line No. " + i.ToString();
                    else
                    {
                        bool flag = true;
                        foreach (var gvRow in BillingPartControl1.GVPartData)
                        {
                            if (gvRow.PartDetail.PartNumber.Trim() == odr["Part_No"].ToString().Trim())
                            {
                                script = odr["Part_No"].ToString().Trim() + " This Part Already add @ line No." + i.ToString();
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            //if (checkItemisThereInddl(BillingPartControl1.PartTypeControl, odr["Part_Type"].ToString().Trim()))
                            //{
                            var filter = new KeyValuePairItems
                                {
                                    new KeyValuePairItem(Constants.filter4, odr["Part_No"].ToString().Trim()),
                                    new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextPartNo),
                                    new KeyValuePairItem(Constants.masterType,Constants.TableMaterials)
                                };

                            _genericClass.LoadDropDownIfMorereturnFalse(BillingPartControl1.PartControl, filter, null, UserContext.DataBaseInfo);

                            if (BillingPartControl1.PartControl.Items.Count > 1)
                                BillingPartControl1.PartControl.SelectedIndex = 1;
                            if (BillingPartControl1.PartNumber.Trim() == string.Empty)
                                script += " - " + odr["Part_No"].ToString().Trim() + " This Part is wrong, if it is new then add in Master";
                            else
                                BindPartsDetail();
                            //}
                            //else
                            //{
                            //    script += " - " + odr["Part_Type"].ToString().Trim() + " This Part Type is wrong, if it is new then add in Master";
                            //}

                            if (!checkItemisThereInddl(BillingPartControl1.UnitControl, odr["Unit"].ToString().Trim()))
                            {
                                script += " - " + odr["Unit"].ToString().Trim() + " This Unit is wrong, if it is new then add in Master";
                            }

                            if (!checkItemisThereInddl(BillingPartControl1.ModalityControl, odr["Modality"].ToString().Trim()))
                            {
                                script += " - " + odr["Modality"].ToString().Trim() + " This Modality is wrong, if it is new then add in Master";
                            }
                        }
                        if (script == string.Empty && script1 == string.Empty)
                        {
                            BillingPartControl1.CategoryCode = BillingPartControl1.CategoryCode;
                            BillingPartControl1.PartType = BillingPartControl1.PartType;
                            BillingPartControl1.PartNumber = odr["Part_No"].ToString().Trim();
                            BillingPartControl1.Unit = odr["Unit"].ToString().Trim();
                            BillingPartControl1.POQuantity = odr["PO_Qty"].ToString().Trim().ToDecimal(0);
                            BillingPartControl1.UnitValue = odr["Unit_Value"].ToString().Trim().ToDecimal(2);
                            BillingPartControl1.Modality = odr["Modality"].ToString().Trim();
                            if (i == 2)
                                modality = odr["Modality"].ToString().Trim();
                            else if (modality != odr["Modality"].ToString().Trim())
                            {
                                script = "Check Modality of part Number :" + odr[2].ToString().Trim() + " in Excel file";
                                break;
                            }
                            BillingPartControl1.addMethod();
                        }
                    }
                    BillingPartControl1.clearForm();
                }
                odr.Close();
                if (script != string.Empty || script1 != string.Empty)
                {
                    BillingPartControl1.GVPartData = new BillTrackingDetails();
                    CustomMessageControl.MessageBodyText = script + script1;
                    CustomMessageControl.MessageType = MessageTypes.Error;
                    CustomMessageControl.ShowMessage();
                }
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                BillingPartControl1.GVPartData = new BillTrackingDetails();
                CustomMessageControl.MessageBodyText = ex.Message.ToString();
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
            }
            finally
            {
                
            }
            hidFileName.Value = string.Empty;
            uplView.Update();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}