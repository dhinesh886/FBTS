using FBTS.App.Library;
using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.Model.Transaction;
using FBTS.Model.Transaction.Enum;
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
    public partial class FBTS_Teams : System.Web.UI.Page
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
            ForecastingPart.DeleteEdit += Edit;
            ForecastingEditPart.Save += Save_Click;
            ForecastingGridViewListControl.fillGrid += ddl1_SelectedIndexChanged;
            //ForecastingGridViewListControl.onddlSelect2 += ddl2_SelectedIndexChanged;
            ForecastingEditPart.onselectCurrentStatus += onCurrentStatusSelect;
            ForecastingGridViewListControl.onGVListDataPageIndexChanging += onGridListPageIndexChanges;
            ForecastingGridViewListControl.onGVListDataSorting += onGVListDataSorting;
            ForecastingGridViewListControl.ontext1change += ontxtSearchChange;
            ForecastingEditPart.txtAltPartTextChange += AltPartTextChange;
            ForecastingEditPart.ddlAltPartselectedIndexchanged += SelectAltPartNumber;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Stage"] != null)
                TransactionStageControlId1.ActiveStage = Request.QueryString["Stage"].ToString().Trim();
            if (IsPostBack) return;
            this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            CategoryType = Request.QueryString["CategoryType"].ToString().Trim() == "01" ? "01,03" : Request.QueryString["CategoryType"].ToString().Trim();
            pageTitle.InnerText = Request.QueryString["PageTitle"].ToString().Trim();
            if (Request.QueryString["SubLinkType"] != null)
                StageType = Request.QueryString["SubLinkType"].ToString().Trim();

            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                filter1 = Constants.DvStages,
                filter2 = TransactionStageControlId1.ActiveStage.ToString().Trim(),
                FilterKey = Constants.ReferencesType
            };
            var references = _controlPanel.GetReferences(queryArgument);
            BindReferences(references);

            ForecastingGridViewListControl.SetText1(Constants.LabelSREnter, Constants.ToolTipSrNumberSearch, 12);

            if (StageType == Constants.BillTrackingType)
            {
                DivFilter = true;
                ForecastingEditPart.SetCurrentStatusAutopostBack = true;
                ForecastingGridViewListControl.Ddl1.Items.Add(new ListItem("BackOrder", "BO"));
                ForecastingGridViewListControl.Ddl1.Items.Add(new ListItem("StockTransfer", "ST"));               
                ForecastingGridViewListControl.Ddllbl1 = "Select Status";
                //ForecastingGridViewListControl.Ddl2.Items.Add(new ListItem("Ordering", Constants.OrderingType));
                //ForecastingGridViewListControl.Ddl2.Items.Add(new ListItem("Deviation Order", Constants.DeviationOrder));
                //ForecastingGridViewListControl.Ddllbl2 = "Select Order/Deviation";
            }
            else
                DivFilter = false;
            fillHeaderGrid();
            ForecastingPart.addKeyupText(ForecastingPart.txtqty, "onkeyup");
            ForecastingEditPart.IsHeader = false;
            ForecastingEditPart.IsPanelDiv = false;
        }
        protected void ddl1_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        protected void ontxtSearchChange(object sender, EventArgs e)
        {
            fillHeaderGrid();
        }

        //protected void ddl2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    fillHeaderGrid();
        //}
        public void fillHeaderGrid()
        {
            var orderTxn = new OrderTransactions();
            if (FilterOrder == Constants.FreshOrder)
            {
                var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    Key = StageType == Constants.BillTrackingType ? Constants.SaleRequestTdType : Constants.PurchaseRequestTdType,
                    filter1 = StageType == Constants.BillTrackingType ? Constants.TRNLogedOFF + "," + Constants.TRNInProcessOFF + "," + Constants.TRNCompletedOFF : Constants.TRNLogedOFF,
                    filter2 = CategoryType,
                    filter3 = ForecastingGridViewListControl.Ddl1.SelectedValue.Trim(),
                    filter4 = string.IsNullOrEmpty(ForecastingGridViewListControl.Text1Value) ? Constants.RetriveList :
                                                                                       string.Format("{0}|{1}", ForecastingGridViewListControl.Text1Value, Constants.RetriveList),
                    //filter5 = StageType == Constants.BillTrackingType ? ForecastingGridViewListControl.Ddl2.SelectedValue.Trim() : string.Empty,
                    FilterKey = Constants.TableFolloup
                };
                orderTxn = _transactionManager.GetFollowupData(queryArgument);                
            }
            else
            {
                var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {                    
                    filter2 = CategoryType,
                    filter3 = ForecastingGridViewListControl.Ddl1.SelectedValue.Trim(),
                    filter4 = string.IsNullOrEmpty(ForecastingGridViewListControl.Text1Value) ? Constants.RetriveList :
                                                                                       string.Format("{0}|{1}", ForecastingGridViewListControl.Text1Value, Constants.RetriveList),
                    FilterKey = Constants.FlolloupPendingFollowup
                };
                orderTxn = _transactionManager.GetpPendingFollowupData(queryArgument);
            }

            KeyValuePairItems headers = new KeyValuePairItems();
            headers.Add(new KeyValuePairItem("1", Constants.ForcastingHeader));
            if (StageType == Constants.BillTrackingType)
            {
                headers.Add(new KeyValuePairItem("2", Constants.SRHeader));
                if (FilterOrder == Constants.PendingOrder)
                    headers.Add(new KeyValuePairItem("5", Constants.StatusHeader));
                else
                    headers.Add(new KeyValuePairItem("5", Constants.ReqLocHeader));
            }
            else
                headers.Add(new KeyValuePairItem("2", Constants.FRHeader));
            ForecastingGridViewListControl.GVHeaders = headers;
            ForecastingGridViewListControl.IsVisiableColumn(2, true);

            ForecastingGridViewListControl.OrderTxns = orderTxn;
            uplForm.Update();
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
        public string CategoryType
        {
            get { return hidCategories.Value.Trim(); }
            set { hidCategories.Value = value.Trim(); }
        }
        public string StageType
        {
            get { return hidStageType.Value.Trim(); }
            set { hidStageType.Value = value.Trim(); }
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
        public ForecastingPartControl ForecastingEditPart
        {
            get { return ForecastingPartControlIdEdit; }
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
            get { return hidAction.Value.Trim(); }
            set { hidAction.Value = value.Trim(); }
        }
        public bool DiVSave
        {
            set
            {
                divSave.Visible = value;
                DivFilter = !value;
                uplActions.Update();
            }
        }
        public string FilterOrder
        {
            get { return ddlfilterOrder.SelectedValue.Trim(); }
            set { ddlfilterOrder.SelectedValue = value.Trim(); }
        }
        public OrderTransactions OrderTransactionsData
        {
            get
            {
                return XmlUtilities.ToObject<OrderTransactions>(ViewState["OrderTransactionsData"].ToString());
            }
            set
            {
                ViewState["OrderTransactionsData"] = value.ToXml();
            }
        }
        public string SlNoKey
        {
            get { return hidSlNoKey.Value.Trim(); }
            set { hidSlNoKey.Value = value.Trim(); }
        }
        public bool DivFilter
        {
            set
            {
                if (value && StageType == Constants.BillTrackingType)
                    divFilterOrder.Style.Add("display", "block");
                else
                    divFilterOrder.Style.Add("display", "none");
                uplActions.Update();
            }
        }
        #endregion
        protected void FillForm(object sender, EventArgs e)
        {
            fillForm();
        }

        public void fillForm()
        {
            var partNumber = ForecastingGridViewListControl.OrderNumber;
            var FcDtlNo = ForecastingGridViewListControl.Amdno;
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = StageType == Constants.BillTrackingType && FilterOrder == Constants.PendingOrder ? FcDtlNo : partNumber,
                filter1 = StageType == Constants.BillTrackingType ? Constants.TRNLogedOFF + "," + Constants.TRNInProcessOFF + "," + Constants.TRNCompletedOFF : Constants.TRNLogedOFF,
                filter2 = CategoryType,
                filter3 = ForecastingGridViewListControl.Ddl1.SelectedValue.Trim(),
                filter4 = Constants.RetriveForm,
                FilterKey = StageType == Constants.BillTrackingType && FilterOrder == Constants.PendingOrder ? Constants.FlolloupPendingFollowup : Constants.TableFolloup
            };

            var orderTxn = new OrderTransactions();
            if (StageType == Constants.BillTrackingType && FilterOrder == Constants.PendingOrder)
                orderTxn = _transactionManager.GetpPendingFollowupData(queryArgument);
            else
                orderTxn = _transactionManager.GetFollowupData(queryArgument);

            var firstOrDefault = orderTxn.FirstOrDefault();
            if (firstOrDefault == null)
            {
                fillHeaderGrid();
                DiVSave = false;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
                return;
            }
            OrderTransactionsData = orderTxn;
            ForecastingHead.clearData();
            ForecastingHead.SubIdLabel = StageType == Constants.BillTrackingType ? Constants.SRHeader : Constants.FRHeader;
            ForecastingHead.IsEnableSubId = false;
            ForecastingHead.IdLabel = Constants.ForcastingHeader;
            ForecastingHead.IsVisiableSubId = true;            
            ForecastingHead.SetData(firstOrDefault.orderHead);
            ForecastingHead.IsEnableLocation = false;
            ForecastingPart.clearForm();
            ForecastingPart.InputDivFalse = false;
            ForecastingPart.StatusOrderNoDivTrue = true;
            ForecastingPart.changeActionName = Constants.UpdateAction;
            ForecastingPart.IsVisiableCell(10, true);
            ForecastingPart.GVPartData = firstOrDefault.orderDetails;
            Action = Constants.InsertAction;
            ForecastingEditPart.clearForm();
            
            uplView.Update();
            DiVSave = true;
        }
        protected void onCurrentStatusSelect(object sender, EventArgs e)
        {
            //if (ForecastingEditPart.CurrentStatus == Constants.AlternativePartNeeded)
            //    ForecastingEditPart.IsVisiableAlternativePartText = true;
            //else
            //{
            //    ForecastingEditPart.AlternativePart = string.Empty;
            //    ForecastingEditPart.IsVisiableAlternativePartText = false;
            //    ForecastingEditPart.IsVisiableAlternativePartDdlDiv = false;
            //}
        }
      
        protected void Addclick(object sender, EventArgs e)
        {
            ForecastingPart.addSqty();
            ForecastingPart.clearForm();
           // ForecastingPart.changepartddlIndex();
          //  ForecastingPart.RetriveDatabasedonPart();

            uplView.Update();
        }

        protected void Edit(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            var hidSlNo= row.FindControl("hidSlNo") as HiddenField;
            if(hidSlNo==null)return;
            SlNoKey = hidSlNo.Value.Trim();
            fillPartGrid();
        }
        public void fillPartGrid()
        {
            if (SlNoKey == string.Empty || SlNoKey == null) return;
            var orderDetail = OrderTransactionsData.FirstOrDefault().orderDetails.Where(x => x.SlNo.Trim() == SlNoKey).FirstOrDefault();
            if (orderDetail == null) return;           
            divEdit.Attributes.Add("style", "display:block");
            ForecastingEditPart.divGVvisiable = false;
            ForecastingEditPart.StatusOrderNoDivTrue = true;
            ForecastingEditPart.PartType = orderDetail.MaterialGroup.Id;
            
            var keyValuePairItems = new KeyValuePairItems();            
            keyValuePairItems.Add(new KeyValuePairItem(orderDetail.PartDetail.PartNumber.Trim()+" "+Constants.SpecialCharApprox+" "+ orderDetail.PartDetail.Description.Trim(),orderDetail.PartDetail.PartNumber.Trim()));
            ForecastingEditPart.fillDdl(ForecastingEditPart.PartControl as DropDownList, keyValuePairItems);

            ForecastingEditPart.PartNumber = orderDetail.PartDetail.PartNumber;
            ForecastingEditPart.CategoryCode = orderDetail.MaterialType.Id;
            ForecastingEditPart.RemaingQty = orderDetail.Quantity;
            ForecastingEditPart.BillingLocationCode = orderDetail.WarehouseTo.Id;
            ForecastingEditPart.Modality = orderDetail.Modality;           
          //  ForecastingEditPart.LogistiOrderNumber = orderDetail.LogisticOrderNumber;
            ForecastingEditPart.setVisiablelnkAddNew = false;
            ForecastingEditPart.setVisiablelnkSave = true;
            ForecastingEditPart.setEnablePart = false;
            ForecastingEditPart.IsEnablePartType = false;

            if (StageType == Constants.BillTrackingType)
            {
                //Cahnges CurrentStatuts
                keyValuePairItems.Clear();
                keyValuePairItems.Add(new KeyValuePairItem("Ordering", Constants.OrderingType));
                keyValuePairItems.Add(new KeyValuePairItem("On Hold", "OH"));
                keyValuePairItems.Add(new KeyValuePairItem("Wating for more part", "WP"));
                keyValuePairItems.Add(new KeyValuePairItem("EOL Part", "EP"));
                keyValuePairItems.Add(new KeyValuePairItem("Alternate Part Number Needed", "AN"));
                keyValuePairItems.Add(new KeyValuePairItem("Send Back", "CS"));
                ForecastingEditPart.fillDdl(ForecastingEditPart.DropdownStatus, keyValuePairItems);

                if (ForecastingGridViewListControl.Ddl1.SelectedValue == Constants.TxnStockTransferType)
                {
                    keyValuePairItems.Clear();
                    keyValuePairItems.Add(new KeyValuePairItem("Ordering", Constants.OrderingType));
                    keyValuePairItems.Add(new KeyValuePairItem("On Hold", "OH"));
                    keyValuePairItems.Add(new KeyValuePairItem("EOL Part", "EP"));
                    keyValuePairItems.Add(new KeyValuePairItem("Alternate Part Number Needed", "AN"));
                    keyValuePairItems.Add(new KeyValuePairItem("Send Back", "CS"));
                    ForecastingEditPart.fillDdl(ForecastingEditPart.DropdownStatus, keyValuePairItems);
                }

                if (FilterOrder == Constants.PendingOrder)
                {
                    ForecastingEditPart.DropdownStatus.SelectedIndex = 1;
                    ForecastingEditPart.setEnableCurrentStatus = false;
                }
                else
                    ForecastingEditPart.setEnableCurrentStatus = true;

                
            }
            else
            {
                ForecastingEditPart.CurrentStatus = orderDetail.CurrentStatus;
                ForecastingEditPart.setEnableCurrentStatus = false;
            }
            //  ForecastingEditPart.setEnableLogisticNumber = false;
            ForecastingEditPart.addKeyupText(ForecastingEditPart.txtqty, "onkeyup");
            Action = Constants.InsertAction;
            uplView.Update();
        }
        public void SelectAltPartNumber(object sender, EventArgs e)
        {
            AlternativePartCahnge();
        }
        public void AltPartTextChange(object sender, EventArgs e)
        {
            AlternativePartCahnge();
        }
        private void AlternativePartCahnge()
        {
            if (string.IsNullOrEmpty(ForecastingEditPart.AltPartNumber.ToTrimString()))
            {
                ForecastingEditPart.CategoryCode = string.Empty;
                ForecastingEditPart.PartType = string.Empty;
            }
            else
            {
                //BindPartsDetail(true);
                var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    Key = ForecastingEditPart.AltPartNumber,
                    filter1 = string.Empty,
                    filter4 = Constants.RetriveForm,
                    FilterKey = Constants.TableMaterials
                };
                var parts = _controlPanel.GetMaterials(queryArgument);
                var firstOrDefault = parts.FirstOrDefault();
                if (firstOrDefault == null) return;

                ForecastingEditPart.AltPartCat = firstOrDefault.MaterialType.Id;
                ForecastingEditPart.AltPartType = firstOrDefault.MaterialGroup.Id;
            }
            uplView.Update();
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            OrderTransactions orderTransactions = new OrderTransactions();
            var orderDetails = OrderTransactionsData.FirstOrDefault().orderDetails.Where(x => x.SlNo.Trim() == SlNoKey).ToList();
            var orderDetails1 = OrderTransactionsData.FirstOrDefault().orderDetails.Where(x => x.SlNo.Trim() == SlNoKey).ToList();
            if (orderDetails.Count == 0) return;
            var updateOrderDetails = new OrderDetails();
            foreach (var orderdetail in orderDetails)
            {
                //orderdetail.LogisticOrderNumber = ForecastingEditPart.LogistiOrderNumber.Trim();
                orderdetail.SQuantity += ForecastingEditPart.Quantity;
                if (StageType == Constants.BillTrackingType)
                {
                    if (FilterOrder == Constants.PendingOrder && ForecastingEditPart.Quantity == ForecastingEditPart.RemaingQty)
                        orderdetail.Off = Constants.TRNCompletedOFF;
                    else
                        orderdetail.Off = Constants.TRNInProcessOFF;
                }
                else
                {
                    if (ForecastingEditPart.Quantity == ForecastingEditPart.RemaingQty)
                        orderdetail.Off = Constants.TRNInProcessOFF;
                    else
                        orderdetail.Off = Constants.TRNLogedOFF;
                }
                if (ForecastingEditPart.CurrentStatus == Constants.OrderingType ||
                                ForecastingEditPart.CurrentStatus == Constants.ChangeStatus ||
                                            ForecastingEditPart.CurrentStatus == Constants.AlternativePartNeeded)
                {
                    orderdetail.ShQuantity += ForecastingEditPart.Quantity;
                    if (ForecastingEditPart.CurrentStatus == Constants.ChangeStatus)
                    {
                        orderdetail.BQuantity += ForecastingEditPart.Quantity;
                        orderdetail.DoQuantity += ForecastingEditPart.Quantity;
                    }
                    orderdetail.LogisticOrderNumber = orderdetail.LogisticOrderNumber.Trim() + "," + ForecastingEditPart.LogistiOrderNumber.Trim();
                }
                updateOrderDetails.Add(orderdetail);
            }
            var insertOrderDeatil = new OrderDetails();
            
            foreach (var orderdetail in orderDetails1)
            {
                orderdetail.SQuantity = ForecastingEditPart.CurrentStatus == Constants.AlternativePartNeeded ? ForecastingEditPart.Quantity : 0;
                orderdetail.Quantity = ForecastingEditPart.Quantity;
                orderdetail.CurrentStatus = ForecastingEditPart.CurrentStatus;               
                orderdetail.LogisticOrderNumber = ForecastingEditPart.LogistiOrderNumber.Trim();
                insertOrderDeatil.Add(orderdetail);
            }
            var sqty = orderDetails.FirstOrDefault().Quantity;
            var qty = ForecastingEditPart.Quantity;
            orderTransactions.Add(new OrderTransaction
            {
                orderHead = ForecastingHead.GetData(),
                orderDetails = insertOrderDeatil,
                updateOrderDeatils = updateOrderDetails,
                Action = Action,
                DataBaseInfo = UserContext.DataBaseInfo,
                StageId = TransactionStageControlId.ActiveStage.Trim(),
                Bu = UserContext.UserProfile.Bu,
                Off = StageType == Constants.BillTrackingType ? ForecastingEditPart.CurrentStatus == Constants.OrderingType ? Constants.TRNCompletedOFF : Constants.TRNInProcessOFF :
                                                               qty == ForecastingEditPart.RemaingQty ? Constants.TRNInProcessOFF : Constants.TRNLogedOFF,
                //Off = Constants.TRNInProcessOFF,
                Branch = UserContext.UserProfile.Branch,
                LogedUser = UserContext.UserId,
                FormType = FilterOrder == Constants.PendingOrder ? FormType.Pending : FormType.Fresh
            });
            var firstOrDefault = orderTransactions.FirstOrDefault();
            var references = getReferences();
            if (firstOrDefault != null)
                firstOrDefault.References = references;
            if (_transactionManager.SetTeam(orderTransactions))
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.TeamDataSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Team Data Saved",
                    GlobalCustomResource.TeamDataSaved, true);

                ForecastingEditPart.clearForm();
               // divEdit.Visible = false;
                divEdit.Attributes.Add("style", "display:none");
                ClearReferences();               
                fillForm();

               // ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
            }
            else
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.TeamDataFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Team Data Update Failed",
                    GlobalCustomResource.TeamDataFailed, true);
            }
        }
        private void ClearReferences()
        {
            if (divref1.Visible)
                RefrenceControl1.BindData(string.Empty);
            if (divref2.Visible)
                RefrenceControl2.BindData(string.Empty);
            if (divref3.Visible)
                RefrenceControl3.BindData(string.Empty);
            if (divref4.Visible)
                RefrenceControl4.BindData(string.Empty);
            if (divref5.Visible)
                RefrenceControl5.BindData(string.Empty);
            if (divref6.Visible)
                RefrenceControl6.BindData(string.Empty);
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

        protected void SelectCtegories(object sender, EventArgs e)
        {
            ForecastingPart.RetriveDatabasedonPart();
            uplView.Update();
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            divEdit.Attributes.Add("style", "display:none");
            DiVSave = false;
        }

        protected void ddlfilterOrder_SelectedIndexChanged(object sender, EventArgs e)
        {  
            fillHeaderGrid();
        }
    }
}