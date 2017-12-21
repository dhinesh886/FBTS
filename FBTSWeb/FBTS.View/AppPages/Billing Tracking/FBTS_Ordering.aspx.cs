using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.Model.Transaction;
using FBTS.View.UserControls.Common;
using FBTS.View.UserControls.ForecastingCommon;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using FBTS.App.Library;
using FBTS.Model.Transaction.Transactions;
using FBTS.View.Resources.ResourceFiles;
using FBTS.Model.Transaction.Enum;
using System.Globalization;
using Ezy.ERP.View.UserControls.Common;

namespace FBTS.View.AppPages.Billing_Tracking
{
    public partial class FBTS_Ordering : System.Web.UI.Page
    {
        public readonly ControlPanelManager _controlPanel = new ControlPanelManager();
        public readonly TransactionManager _transactionManager = new TransactionManager();
        private readonly GenericManager _genericClass = new GenericManager();
        private int _newPageIndex = -1;
        public readonly ReportManager _reportManager = new ReportManager();


        protected void Page_Init(object sender, EventArgs e)
        {
            ForecastingGridViewListControl.Ddllbl0 = Constants.RegionLable;
            ForecastingGridViewListControl.Ddllbl1 = Constants.LocationLablel;
            ForecastingGridViewListControl.Ddllbl3 = Constants.ModalityLabel;
            ForecastingGridViewListControl.chkClick += FillForm;
            ForecastingGridViewListControl.fillGrid += FillGride;
            ForecastingGridViewListControl.fillLocation += fillLocation;
            BillingPartControl1.setPartAutopostBack = true;
            BillingPartControl1.ddlPartselectedIndexchanged += SelectPartNumber;
            BillingPartControl1.addKeyupText(BillingPartControl1.TextBillQuantity, "onkeyup");
            BillingPartControl1.addonchangeDll(BillingPartControl1.FCNumberControl, "onchange", "return checkFcQty" + BillingPartControl1.ClientID + "(this);");
            BillingPartControl1.onselectBillStatus += onselectBillStatus;            
            BillingPartControl1.addclick += Add_Click;
            BillingPartControl1.DeleteEdit += Delete_Click;
            BillingPartControlPending.DeleteEdit += EditPart;
            BillingPartControl1.GVPartChange += GVPartChange;
            BillingPartControlPending.onShow_Click += onShow_click;
            ForecastingGridViewListControl.onViewClick += lnkViewDetails_Click;
            ForecastingGridViewListControl.onGVListDataPageIndexChanging += onGridListPageIndexChanges;
            ForecastingGridViewListControl.onGVListDataSorting += onGVListDataSorting;
            ForecastingGridViewListControl.ontext1change += ontxtSearchChange;
            //BillingPartControl1.ddlAltPartselectedIndexchanged += SelectAltPartNumber;
           // BillingPartControl1.txtAltPartTextChange += AltPartTextChange;
            ForecastingGridViewListControl.onSendBackClick += onSendBackClick;
            ConfirmationPopup.YesClicked += ConfirmYes;
            BillingPartControl1.txtPartTextChange += SelectCategories;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Stage"] != null)
                TransactionStageControlId1.ActiveStage = Request.QueryString["Stage"].ToString();
            if (IsPostBack) return;
            this.ValidatePage(UserContext, QueryStringManagement.GetValue(Constants.MenuCodeQsKey, Guid.Empty.ToString()));
            if (Request.QueryString["PageTitle"] != null)
                pageTitle.InnerText = Request.QueryString["PageTitle"].ToString().ToTrimString();
            if (Request.QueryString["FormType"] != null)
                StageType = Request.QueryString["FormType"].ToString().ToTrimString();

            ForecastingGridViewListControl.SetText1(Constants.LabelSREnter, Constants.ToolTipSrNumberSearch, 12);
            fillRegion();

            WebControls.ClearList(ForecastingGridViewListControl.Ddl3, string.Empty);
            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.PartType),
                            new KeyValuePairItem(Constants.filter1, Constants.DvMode),                            
                            new KeyValuePairItem(Constants.masterType,Constants.DdlCatHeaderData)
                        };
            var modality = _genericClass.LoadDropDown(null, filter, null, UserContext.DataBaseInfo);

            foreach (var mod in modality)
            {
                ForecastingGridViewListControl.Ddl3.Items.Add(new ListItem(mod.Value, mod.Key));
            }
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                filter1 = Constants.DvStages,
                filter2 = TransactionStageControlId1.ActiveStage,
                FilterKey = Constants.ReferencesType
            };
            var references = _controlPanel.GetReferences(queryArgument);
            BindReferences(references);
            ForecastingHead.IdLabel = Constants.MainSRHeader;
            ForecastingHead.SubIdLabel = Constants.RelatedSRHeader;
            if (StageType == Constants.TxnOrderType || StageType == Constants.TxnTrackingType)
                DivFilter = true;
            else
                DivFilter = false;
            if (StageType == Constants.TxnTrackingType)
            {
                var datasource = new KeyValuePairItems{
                    new KeyValuePairItem(Constants.TRNInProcessOFF,"Fresh"),
                    new KeyValuePairItem(Constants.TRNPendingOFF,"Pending")
                };
                _genericClass.LoadDropDown(ddlfilterOrder, null, null, null, datasource);
            }
            BindData();
        }

        public void fillRegion()
        {
            var filter = new KeyValuePairItems
                        {                           
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextRegion),
                            new KeyValuePairItem(Constants.masterType, Constants.DdlRegion)
                        };
            _genericClass.LoadDropDown(ForecastingGridViewListControlId.Ddl0, filter, null, UserContext.DataBaseInfo);
           // fillLocation();
        }

        public void fillLocation(object sender, EventArgs e)
        {
            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.WHCType),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.AllType),
                            new KeyValuePairItem(Constants.masterType,Constants.TableAccounts),
                            new KeyValuePairItem(Constants.key,ForecastingGridViewListControlId.Ddl0.SelectedValue.ToTrimString())
                        };
            _genericClass.LoadDropDown(ForecastingGridViewListControl.Ddl1, filter, null, UserContext.DataBaseInfo);
            BindData();
        }

        protected void FillGride(object sender, EventArgs e)
        {
            BindData();
        }
        protected void onGridListPageIndexChanges(object sender, GridViewPageEventArgs e)
        {
            BindData();
        }
        protected void onGVListDataSorting(object sender, GridViewSortEventArgs e)
        {
            BindData();
        }
        protected void ontxtSearchChange(object sender, EventArgs e)
        {
            BindData();
        }
        protected void onSendBackClick(object sender, EventArgs e)
        {

            var lnk = sender as LinkButton;
                if (lnk == null) return;
            var gvRow = lnk.NamingContainer as GridViewRow;
                     
            if(gvRow==null)
            {
                return;
            }
            var hidOrderNo = gvRow.FindControl("hidOrderNo") as HiddenField;
            var hidAmdno = gvRow.FindControl("hidAmdno") as HiddenField;
            var srNo = gvRow.Cells[2].Text.ToTrimString();

            QueryArgument queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                FilterKey = srNo,
                QueryType = Constants.SendBackValidation
            };

            var result = _transactionManager.ValidateKey(queryArgument);
            if (result)
            {
                CustomMessageControl.MessageBodyText = "This SR cannot be sent back to Deviation";
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                ForecastingHead.Id = string.Empty;
                return;
            }

            ConfirmationPopup.Key = hidOrderNo == null ? string.Empty : hidOrderNo.Value.ToTrimString();
            ConfirmationPopup.SecondaryKey = hidAmdno == null ? string.Empty : hidAmdno.Value.ToTrimString();
            ConfirmationPopup.MessageHeaderText = "Confirmation";
            ConfirmationPopup.MessageBodyText = "This action will send the current SR to Deviation, Are you sure to proceed?";
            ConfirmationPopup.IsVisiableTextRemark = true;
            ConfirmationPopup.Show();
            uplForm.Update();

            
        }
        protected void ConfirmYes(object sender, EventArgs e)
        {
            //ConfirmationPopup.MessageHeaderText = "Selected SR Send back to Deviation?";
            //ConfirmationPopup.MessageBodyText = "Are you sure?";
            //ConfirmationPopup.Show();

            var queryArgument = new QueryArgument()
            {
                Key = ConfirmationPopup.Key,
                filter1 = ConfirmationPopup.SecondaryKey,
                filter2 = Constants.DeviationType,
                filter3 = Constants.DeviationType,
                filter4 = ConfirmationPopup.Remark,
                filter5 = TransactionStageControlId1.ActiveStage,
                UserId = UserContext.UserId,
                DataBaseInfo = UserContext.DataBaseInfo
            };

            ConfirmationPopup.Remark = string.Empty;
            ConfirmationPopup.IsVisiableTextRemark = false;
            var result = _transactionManager.UpdateSendBackStatus(queryArgument);
            
            if (result == true)
            {
                CustomMessageControl.MessageBodyText = "SR has been sent back to Deviation";
                CustomMessageControl.MessageType = MessageTypes.Warning;
                CustomMessageControl.ShowMessage();
                BindData();
            }
        }
        public void BindData()
        {
            ForecastingGridViewListControl.IsVisiableColumn(22, false);
            ForecastingGridViewListControl.IsVisiableColumn(18, false);
            ForecastingGridViewListControl.IsVisiableColumn(16, true);
            ForecastingGridViewListControl.IsVisiableColumn(19, true);
            if (StageType == Constants.TxnDebriefingType)
            {
                var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    Key = Constants.SalesOrderTdType,
                    filter1 = Constants.TRNInProcessOFF,
                    //filter2 = FilterModality,
                    filter5 = FilterLocation,
                    filter6 = FilterModality,
                    filter4 = string.IsNullOrEmpty(ForecastingGridViewListControl.Text1Value) ? Constants.RetriveList :
                                                                                       string.Format("{0}|{1}", ForecastingGridViewListControl.Text1Value, Constants.RetriveList),
                    FilterKey = Constants.TableFolloup
                };
                var orderTxns = _transactionManager.GetFollowupData(queryArgument);                
                KeyValuePairItems headers = new KeyValuePairItems();
                headers.Add(new KeyValuePairItem("1", Constants.OrderingNuberHeader));
                headers.Add(new KeyValuePairItem("2", Constants.MainSRHeader));
                ForecastingGridViewListControl.GVHeaders = headers;
                ForecastingGridViewListControl.IsVisiableColumn(2, true);
                ForecastingGridViewListControl.IsVisiableColumn(15, false);
                foreach (var orderTxn in orderTxns)
                {
                    orderTxn.orderHead.RelatedSR = orderTxn.orderHead.OrderAmendmentNumber;
                }               

                ForecastingGridViewListControl.OrderTxns = orderTxns;
                uplForm.Update();
            }
            else if (StageType == Constants.TxnTrackingType)
            {
                var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    filter1 = FilterOrder,
                    filter3 = FilterLocation,
                    filter4 = Constants.RetriveList,
                    filter5 = FilterModality,
                    filter6 = ForecastingGridViewListControl.Text1Value,
                    FilterKey = Constants.TableInvoice
                };
                var bills = _transactionManager.GetInvoiceData(queryArgument);

                ForecastingGridViewListControl.IsVisiableColumn(2, true);
                ForecastingGridViewListControl.IsVisiableColumn(15, false);

                KeyValuePairItems headers = new KeyValuePairItems();
                headers.Add(new KeyValuePairItem("1", Constants.InvoiceNuberHeader));
                headers.Add(new KeyValuePairItem("2", Constants.MainSRHeader));
                ForecastingGridViewListControl.GVHeaders = headers;
               
                OrderTransactions orderTxns = new OrderTransactions();
                orderTxns.AddRange(bills.Select(bill => new OrderTransaction
                {
                    orderHead = bill.Head
                }));

                ForecastingGridViewListControl.OrderTxns = orderTxns;
                uplForm.Update();
            }
            else
            {
                ForecastingGridViewListControl.IsVisiableColumn(15, true);
                //ForecastingGridViewListControl.IsVisiableColumn(16, true);
                ForecastingGridViewListControl.IsVisiableColumn(22, true);
                ForecastingGridViewListControl.IsVisiableColumn(24, false);
                var orderTxn = new OrderTransactions();
                if (FilterOrder == Constants.FreshOrder)
                {
                    ForecastingGridViewListControl.IsVisiableColumn(24, true);
                    var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                    {
                        Key = FilterLocation,
                        filter1 = Constants.TRNInProcessOFF,
                        filter2 = Constants.SaleRequestTdType,
                        filter3 = Constants.OrderingType,
                        filter4 = Constants.RetriveList,
                        filter5 = FilterModality,
                        filter6 = ForecastingGridViewListControl.Text1Value,
                        FilterKey = Constants.TableOrderDetail,
                    };
                    orderTxn = _transactionManager.GetOrderData(queryArgument);                    
                }
                else if (FilterOrder == Constants.PendingOrder || FilterOrder == Constants.PendingDeviationOrder || FilterOrder == Constants.FreshDeviationOrder)
                {
                    var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                    {
                        Key = FilterLocation,
                       // filter1 = Constants.TxnBackOrderType + "," + Constants.TxnStockTransferType,
                        filter1 = ForecastingGridViewListControl.Text1Value,
                      //  filter2 = "WP,EP,OH,AN,PR,SI",
                        filter3 = FilterOrder == Constants.PendingOrder ? "FO" : FilterOrder == Constants.FreshDeviationOrder ? "DO" : FilterOrder,
                        filter5 = FilterOrder == Constants.PendingOrder || FilterOrder == Constants.FreshDeviationOrder ? Constants.TRNCompletedOFF : string.Format("{0},{1}", Constants.TRNInProcessOFF, Constants.TRNPendingOFF),
                        filter4 = Constants.RetriveList,
                        filter6 = FilterModality,
                        FilterKey = Constants.FlolloupPendingOrder
                    };
                    orderTxn = _transactionManager.GetPendingOrderData(queryArgument);
                    if (FilterOrder == Constants.PendingDeviationOrder)
                        ForecastingGridViewListControl.IsVisiableColumn(18, true);                       
                }
                else if (FilterOrder == Constants.DeviationOrder)
                {
                    var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                    {
                        Key = FilterLocation,
                        filter1 = string.Format("{0},{1}", Constants.TRNInProcessOFF, Constants.TRNPendingOFF),
                        filter2 = Constants.SaleRequestTdType,
                        filter3 = Constants.DeviationType,
                        filter4 = Constants.RetriveList,
                        filter5 = FilterModality,
                        filter6 = ForecastingGridViewListControl.Text1Value,
                        FilterKey = Constants.TableOrderDetail
                    };
                    orderTxn = _transactionManager.GetOrderData(queryArgument);                    
                }
                else if (FilterOrder == Constants.GSPOType || FilterOrder == Constants.C09Type)
                {
                    ForecastingGridViewListControl.IsVisiableColumn(22, false);
                    var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                    {
                        Key = FilterLocation,
                        filter1 = Constants.ChangeStatus,
                        filter2 = FilterOrder == Constants.GSPOType ? "01,03" : "02",
                        filter4 = Constants.RetriveList,
                        filter5 = FilterModality,
                        filter6 = ForecastingGridViewListControl.Text1Value,
                        FilterKey = Constants.FlolloupChangeStatusOrAlternativepart
                    };
                    orderTxn = _transactionManager.GetChangeStatus_ALPN_Data(queryArgument);

                }
                else if (FilterOrder == Constants.AlternativePartNeeded)
                {
                    var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                    {
                        filter1 = ForecastingGridViewListControl.Text1Value,
                        filter2 = FilterLocation,
                        filter3 = FilterModality,
                        filter4 = Constants.RetriveList,
                        FilterKey = Constants.AlternativePart
                    };
                    var billData = _transactionManager.GetAlternativeOrdData(queryArgument);
                    orderTxn.AddRange(billData.Select(bill => new OrderTransaction
                    {
                        orderHead = bill.Head
                    }));
                }



                setGVHeader();
                ForecastingGridViewListControl.IsVisiableColumn(2, true);
               
                ForecastingGridViewListControl.OrderTxns = orderTxn;
                uplForm.Update();
            }
        }
        private void setGVHeader()
        {
            KeyValuePairItems headers = new KeyValuePairItems();
            if (FilterOrder == Constants.GSPOType || FilterOrder == Constants.C09Type || FilterOrder == Constants.AlternativePartNeeded)
            {
                ForecastingGridViewListControl.IsVisiableColumn(1, true);
                headers.Add(new KeyValuePairItem("1", Constants.ForcastingHeader));
            }
            else
                ForecastingGridViewListControl.IsVisiableColumn(1, false);

            headers.Add(new KeyValuePairItem("2", Constants.MainSRHeader));
            headers.Add(new KeyValuePairItem("5", FilterOrder == Constants.FreshOrder ||
                                                    FilterOrder == Constants.DeviationOrder ||
                                                    FilterOrder == Constants.AlternativePartNeeded ? Constants.ReqLocHeader : Constants.TxnRefTypeRemarks));
            ForecastingGridViewListControl.GVHeaders = headers;
        }
        #region

        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        public string Changedstatus
        {
            get { return ddlChangestatus.SelectedValue.ToTrimString(); }
            set { ddlChangestatus.SelectedValue = value.ToTrimString(); }
        }
        public bool IsVisiableStatusChange
        {
            set
            {
                if (value)
                    divstatuschange.Style.Add("display", "block");
                else
                    divstatuschange.Style.Add("display", "none");
            }
        }

        public bool DivAction
        {
            set
            {
                divSave.Visible = value;
                DivFilter = !value;
                uplActions.Update();
            }
        }
        public bool DivFilter
        {
            set
            {
                if (value && (StageType == Constants.TxnOrderType || StageType == Constants.TxnTrackingType))
                    divFilterOrder.Style.Add("display", "block");
                else
                    divFilterOrder.Style.Add("display", "none");
                uplActions.Update();
            }
        }
        public string Action
        {
            get { return hidAction.Value.ToTrimString(); }
            set { hidAction.Value = value.ToTrimString(); }
        }
        public string StageType
        {
            get { return hidType.Value.ToTrimString(); }
            set { hidType.Value = value.ToTrimString(); }
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
        public BillingPartControl BillingPartControlPending
        {
            get { return BillingPartControlIdPending; }
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
            get { return hidOrd_No.Value.ToTrimString(); }
            set { hidOrd_No.Value = value.ToTrimString(); }
        }
        public string Ord_No1
        {
            get { return hidOrd_No1.Value.ToTrimString(); }
            set { hidOrd_No1.Value = value.ToTrimString(); }
        }
        public decimal POValue
        {
            get { return hidPOValue.Value.ToDecimal(2); }
            set { hidPOValue.Value = value.ToString(); }
        }
        public bool IsVisiableValidationDiv
        {
            set
            {
                if (value)
                    divValidation.Style.Add("display", "block");
                else
                    divValidation.Style.Add("display", "none");
            }
        }
        public string FilterOrder
        {
            get { return ddlfilterOrder.SelectedValue.ToTrimString(); }
            set { ddlfilterOrder.SelectedValue = value.ToTrimString(); }
        }
        public string FilterLocation
        {
            get
            {
                var loc = ForecastingGridViewListControl.Ddl1.SelectedValue.ToTrimString();
                if (loc == string.Empty)
                {
                    foreach (ListItem l in ForecastingGridViewListControl.Ddl1.Items)
                    {
                        if (l.Value == string.Empty)
                            continue;
                        loc = loc + "," + l.Value;
                    }
                    if (loc.Contains(","))
                        loc = loc.Substring(1, loc.Length - 1);
                }
                return loc;
            }
            set { WebControls.SetCurrentComboIndex(ForecastingGridViewListControl.Ddl1, value); }
        }
        public string FilterModality
        {
            get
            {
                var modality = string.Empty;
                foreach (ListItem t in ForecastingGridViewListControl.Ddl3.Items)
                {
                    if (t.Selected)
                        modality = modality + "," + t.Value.ToTrimString();
                }
                if (modality != string.Empty)
                    modality = modality.Remove(0, 1);
                //ForecastingGridViewListControl.Ddl3.SelectedValue.ToTrimString();
                return modality;
            }
           
        }
        public bool IsvisiableDivFreshPart
        {
            set
            {
                if (value)
                    divPartforFresh.Style.Add("display", "block");
                else
                    divPartforFresh.Style.Add("display", "none");
            }
        }
        public bool IsvisiableDivPendingPart
        {
            set
            {
                if (value)
                    divPartforPending.Style.Add("display", "block");
                else
                    divPartforPending.Style.Add("display", "none");
            }
        }
        public string TMStage
        {
            set { hidTMStage.Value = value.ToTrimString(); }
            get { return hidTMStage.Value.ToTrimString(); }
        }
        public string TMOrdNo
        {
            set { hidTMOrd_no.Value = value.ToTrimString(); }
            get { return hidTMOrd_no.Value.ToTrimString(); }
        }
        #endregion
        protected void FillForm(object sender, EventArgs e)
        {
            var FRNumber = ForecastingGridViewListControl.OrderNumber;
            var amndNo = ForecastingGridViewListControl.Amdno;
            if (StageType == Constants.TxnDebriefingType)
            {
                #region Debreifing
                var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    Key = FRNumber,
                    FilterKey = Constants.TxnDebriefingType
                };
                var tracking = _transactionManager.GetDebriefings(queryArgument);
                var firstOrDefault = tracking.FirstOrDefault();
                if (firstOrDefault == null) return;


                Ord_No = firstOrDefault.OrdNumber;
                ForecastingHead.clearData();
                ForecastingHead.SetData(firstOrDefault.Head);
                ForecastingHead.IsEnableID = false;
                ForecastingHead.IsVisiableSubId = false;
                ForecastingHead.IsEnableLocation = false;
                ForecastingHead.IsVisiablePONumber = true;
                ForecastingHead.IsEnablePONumber = false;

                //Validation Form
                ValidationDdlId1.clearForm();
                IsVisiableValidationDiv = false;

                //Part Detail Fill
                BillingPartControl1.clearForm();
                IsvisiableDivFreshPart = false;
                //BillingPartControl1.isVisiableCell(17, false);
                BillingPartControl1.GVPartData = new BillTrackingDetails();

                BillingPartControlPending.changeActionName = Constants.UpdateAction;
                IsvisiableDivPendingPart = true;
                BillingPartControlPending.IsVisiableInputDiv = false;
                BillingPartControlPending.isVisiableCell(11, true);
                BillingPartControlPending.isVisiableCell(12, true);
                BillingPartControlPending.isVisiableCell(13, true);
                BillingPartControlPending.isVisiableCell(14, true);
                //BillingPartControlPending.isVisiableCell(15, true);
                BillingPartControlPending.isVisiableCell(16, true);
                BillingPartControlPending.isVisiableCell(17, true);
                BillingPartControlPending.isVisiableCell(18, true);
                BillingPartControlPending.isVisiableCell(19, true);
                BillingPartControlPending.isVisiableCell(28, true);

                BillingPartControlPending.GVPartData = firstOrDefault.Details;
                BillingPartControlPending.PartDetails = firstOrDefault.Details;
                int[] listHideCell = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                BillingPartControlPending.HideDuplicateRow(1, listHideCell);

                int[] listofcell_true = { 0, 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14, 16, 17,28, 29 };
                BillingPartControl1.IsVisiableListCell(listofcell_true, true);
                #endregion
            }
            else if (StageType == Constants.TxnTrackingType)
            {
                #region Tracking
                var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    Key = FRNumber,                    
                    filter4 = Constants.RetriveForm,
                    FilterKey = Constants.TableInvoice
                };
                var tracking = _transactionManager.GetInvoiceData(queryArgument);
                var firstOrDefault = tracking.FirstOrDefault();
                if (firstOrDefault == null) return;
                Ord_No = firstOrDefault.OrdNumber;
                //header
                ForecastingHead.clearData();
                ForecastingHead.SetData(firstOrDefault.Head);

                ForecastingHead.IsEnableID = false;
                ForecastingHead.IsEnableSubId = false;
                ForecastingHead.IsEnableLocation = false;
                ForecastingHead.IsVisiableBillLocation = true;
                ForecastingHead.IsEnableBillLocation = false;
                ForecastingHead.IsEnableBillStatus = false;
                ForecastingHead.IsVisiablePONumber = true;
                ForecastingHead.IsEnablePONumber = false;

                //Validation Form
                ValidationDdlId1.clearForm();
                IsVisiableValidationDiv = false;

                //Part Detail Fill
                BillingPartControl1.clearForm();
                BillingPartControl1.IsVisiableInputDiv = false;
                BillingPartControl1.isVisiableCell(3, false);
                BillingPartControl1.isVisiableCell(4, false);
                BillingPartControl1.isVisiableCell(5, false);
                BillingPartControl1.isVisiableCell(6, false);
                BillingPartControl1.isVisiableCell(8, false);
                BillingPartControl1.isVisiableCell(9, false);

                BillingPartControl1.isVisiableCell(11, true);
                BillingPartControl1.isVisiableCell(12, true);
                BillingPartControl1.isVisiableCell(13, true);
                BillingPartControl1.isVisiableCell(14, true);
                BillingPartControl1.isVisiableCell(16, true);
                BillingPartControl1.isVisiableCell(27, false);
                BillingPartControl1.isVisiableCell(20, true);
                BillingPartControl1.isVisiableCell(21, true);
                BillingPartControl1.isVisiableCell(22, true);
                BillingPartControl1.isVisiableCell(23, true);
                BillingPartControl1.isVisiableCell(28, true);
                BillingPartControl1.isVisiableCell(29, false);
                BillingPartControl1.GVPartData = new BillTrackingDetails();
                BillingPartControl1.GVPartData = firstOrDefault.Details;
                #endregion
            }
            else
            {
                var tracking = new BillTrackings();
                if (FilterOrder == Constants.FreshOrder)
                {
                    var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                    {
                        Key = FRNumber,
                        filter1 = amndNo,
                        filter2 = Constants.TRNInProcessOFF,
                        filter4 = Constants.RetriveForm,
                        FilterKey = Constants.TableOrderDetail
                    };
                    tracking = _transactionManager.GetTrackingData(queryArgument);

                }
                else if (FilterOrder == Constants.PendingOrder || FilterOrder == Constants.PendingDeviationOrder || FilterOrder == Constants.FreshDeviationOrder)
                {
                    var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                    {
                        Key = FRNumber,
                        filter1 = Constants.TxnBackOrderType + "," + Constants.TxnStockTransferType,
                        filter2 = "WP,EP,OH,AN,PR,SI",
                        filter3 = FilterOrder,//Constants.OrderingType + "," + Constants.AlternativePartNeeded,
                        filter4 = Constants.RetriveForm,
                        FilterKey = Constants.FlolloupPendingOrder
                    };
                    tracking = _transactionManager.GetPendingOrderFormData(queryArgument);        
                    if (FilterOrder == Constants.PendingDeviationOrder)
                    {
                        if (tracking.Any())
                        {
                            var first = tracking.FirstOrDefault();
                            if (first == null)
                            {
                                clearForm();
                                DivAction = false;
                                BindData();
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
                                return;
                            }
                        }
                        else
                        {
                            clearForm();
                            DivAction = false;
                            BindData();
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
                            return;
                        }
                    }
                }
                if (FilterOrder == Constants.DeviationOrder)
                {
                    var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                    {
                        Key = FRNumber,
                        filter1 = amndNo,
                        filter2 = string.Format("{0},{1}", Constants.TRNInProcessOFF, Constants.TRNPendingOFF),
                        filter4 = Constants.RetriveForm,
                        FilterKey = Constants.TableOrderDetail
                    };
                    tracking = _transactionManager.GetTrackingData(queryArgument);
                }
                else if (FilterOrder == Constants.GSPOType || FilterOrder == Constants.C09Type)
                {
                    var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                    {
                        Key = amndNo,
                        filter1 = Constants.TxnBackOrderType + "," + Constants.TxnStockTransferType,
                        filter3 = Constants.ChangeStatus,
                        filter4 = Constants.RetriveForm,
                        FilterKey = Constants.FlolloupChangeStatusOrAlternativepart
                    };
                    tracking = _transactionManager.GetChangeStatus_ALPN_FromData(queryArgument);

                }
                else if (FilterOrder == Constants.AlternativePartNeeded)
                {
                    var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                    {
                        Key = FRNumber,
                        filter4 = Constants.RetriveForm,
                        FilterKey = Constants.AlternativePart
                    };
                    tracking = _transactionManager.GetAlternativeOrdData(queryArgument);
                }

                var firstOrDefault = tracking.FirstOrDefault();
                if (firstOrDefault == null) return;

                if (FilterOrder == Constants.GSPOType || FilterOrder == Constants.C09Type)
                {
                    fillChangestatusForm(tracking);
                }
                else
                {
                    if (FilterOrder == Constants.FreshOrder || FilterOrder == Constants.DeviationOrder)
                    {
                        ValidationDdlId1.SetData(firstOrDefault.BillValiditions);
                        BillingPartControl1.IsVisiableFCNumber = true;
                        BillingPartControl1.IsEnableBillStatus = true;
                        //if (FilterOrder == Constants.FreshOrder)
                        //    ForecastingHead.IsVisiableSubId = true;
                        //else
                        //    ForecastingHead.IsVisiableSubId = false;
                    }
                    else
                    {
                        BillingPartControl1.IsVisiableFCNumber = false;
                        BillingPartControl1.IsEnableBillStatus = false;
                        if (FilterOrder == Constants.AlternativePartNeeded)
                            BillingPartControl1.IsEnableBillStatus = true;
                        else
                            BillingPartControl1.IsEnableBillStatus = false;

                        if (FilterOrder == Constants.PendingDeviationOrder)
                            ForecastingHead.IsVisiableSubId = false;
                        //else
                        //    ForecastingHead.IsVisiableSubId = true;
                    }

                    ValidationDdlId1.clearForm();
                    IsVisiableValidationDiv = false;

                    //Header
                    Ord_No = firstOrDefault.OrdNumber;
                    POValue = firstOrDefault.POValue;
                    ForecastingHead.clearData();
                    ForecastingHead.SetData(firstOrDefault.Head);
                    ForecastingHead.IsEnableID = false;
                    ForecastingHead.IsEnableLocation = false;
                    ForecastingHead.IsEnableDate = false;

                    //Part Detail Form
                    BillingPartControl1.clearForm();
                    BillingPartControl1.setBillStatusAutopostBack = true;
                    ///Grid View cell Visiable 
                    if (FilterOrder == Constants.FreshOrder || FilterOrder == Constants.DeviationOrder)
                    {
                        int[] listofcell = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 27, 28 };
                        BillingPartControl1.IsVisiableListCell(listofcell, true);
                    }
                    else if (FilterOrder == Constants.PendingDeviationOrder)
                    {
                        int[] listofcell_false = { 5, 10 };
                        BillingPartControlPending.IsVisiableListCell(listofcell_false, false);

                        int[] listofcell_true = { 0, 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14, 15, 16, 24, 26, 29 };
                        BillingPartControlPending.IsVisiableListCell(listofcell_true, true);
                    }
                    else if (FilterOrder == Constants.PendingOrder || FilterOrder == Constants.FreshDeviationOrder)
                    {
                        int[] Peddinglistofcell_false = { 5, 10 };
                        BillingPartControlPending.IsVisiableListCell(Peddinglistofcell_false, false);

                        int[] Peddinglistofcell_True = { 0, 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14, 15, 16, 17, 18, 19, 25, 27, 28, 29, 30 };
                        BillingPartControlPending.IsVisiableListCell(Peddinglistofcell_True, true);

                        int[] listofcell_true = { 0, 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14, 15, 16, 17, 18, 27, 28, 29 };
                        BillingPartControl1.IsVisiableListCell(listofcell_true, true);
                    }
                    else if (FilterOrder==Constants.AlternativePartNeeded)
                    {
                        int[] Peddinglistofcell_false = { 5, 10, 16, 17 };
                        BillingPartControlPending.IsVisiableListCell(Peddinglistofcell_false, false);

                        int[] Peddinglistofcell_True = { 0, 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14, 15, 19, 25, 27, 28, 29 };
                        BillingPartControlPending.IsVisiableListCell(Peddinglistofcell_True, true);

                        int[] listofcell_true = { 0, 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14, 15, 16, 17, 18, 27, 28, 29 };
                        BillingPartControl1.IsVisiableListCell(listofcell_true, true);
                    }
                    //Changing Action of link Button in Grid
                    if (FilterOrder == Constants.PendingDeviationOrder)
                    {
                        BillingPartControlPending.changeActionName = Constants.InsertAction;
                        BillingPartControlPending.LinkButtonText = "Reserve";
                    }
                    else if (FilterOrder == Constants.PendingOrder || FilterOrder == Constants.FreshDeviationOrder || FilterOrder == Constants.AlternativePartNeeded)
                    {
                        BillingPartControlPending.changeActionName = Constants.UpdateAction;
                    }

                    //Fill Status
                    if (FilterOrder == Constants.DeviationOrder)
                    {
                        ListItem itemToRemove = BillingPartControl1.BillStatusControl.Items.FindByValue(Constants.TxnDebriefingType);
                        if (itemToRemove != null)
                        {
                            BillingPartControl1.BillStatusControl.Items.Remove(itemToRemove);
                        }
                        itemToRemove = BillingPartControl1.BillStatusControl.Items.FindByValue(Constants.AlternativePartNeeded);
                        if (itemToRemove != null)
                        {
                            BillingPartControl1.BillStatusControl.Items.Remove(itemToRemove);
                        }
                        itemToRemove = BillingPartControl1.BillStatusControl.Items.FindByValue(Constants.TRNCancelOFF);
                        if (itemToRemove != null)
                        {
                            BillingPartControl1.BillStatusControl.Items.Remove(itemToRemove);
                        }
                        if (FilterOrder == Constants.DeviationOrder)
                        {
                            itemToRemove = BillingPartControl1.BillStatusControl.Items.FindByValue(Constants.PartReserved);
                            if (itemToRemove == null)
                                BillingPartControl1.BillStatusControl.Items.Add(new ListItem("Part Reserved", Constants.PartReserved));
                        }
                        else
                            BillingPartControl1.IsVisiableBillLocation = false;
                    }


                    //Binding Grid Data
                    if (FilterOrder == Constants.FreshOrder || FilterOrder == Constants.DeviationOrder)
                    {
                        var keyValuePairItems = new KeyValuePairItems();
                        keyValuePairItems.AddRange(firstOrDefault.Details.Select(row => new KeyValuePairItem(row.PartDetail.PartNumber.ToTrimString().ToUpper(), row.PartDetail.PartNumber.ToTrimString().ToUpper() + Constants.SpecialCharApprox + row.PartDetail.Description.ToTrimString())));
                        _genericClass.LoadDropDown(BillingPartControl1.PartControl, null, null, null, keyValuePairItems);
                        BillingPartControl1.PartControl.Items.Insert(0, new ListItem(Constants.DdlDefaultTextPartNo, string.Empty));
                        BillingPartControl1.PartDetails = firstOrDefault.Details;
                        BillingPartControl1.IsVisiableBillLocation = true;
                        BillingPartControl1.IsVisiableLogisticNumber = true;
                    }
                    else
                    {
                        Ord_No1 = firstOrDefault.OrderNumber1;
                        IsvisiableDivFreshPart = false;
                        IsvisiableDivPendingPart = true;
                        BillingPartControlPending.IsVisiableInputDiv = false;
                        BillingPartControlPending.PartDetails = firstOrDefault.Details;
                        BillingPartControlPending.GVPartData = firstOrDefault.Details;
                        
                        int[] listHideCell = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                        BillingPartControlPending.HideDuplicateRow(1, listHideCell);
                    }
                }

                BillingPartControl1.IsVisiableUnit = false;
                BillingPartControl1.IsVisiablePoQuantity = false;
                BillingPartControl1.IsVisiableUnitValue = false;
                BillingPartControl1.IsVisiableTax = false;

                BillingPartControl1.IsVisiableBillQuantity = true;
                BillingPartControl1.IsVisiableTaxRate = true;
                BillingPartControl1.IsVisiableTaxType = true;
                BillingPartControl1.IsVisiableBillStatus = true;
                BillingPartControl1.IsEnablePartType = false;
                BillingPartControl1.IsVisiableRemark = true;
                BillingPartControl1.IsVisiableRealtedSR = true;
            }

            if (FilterOrder == Constants.PendingDeviationOrder)
                lnkSave.Visible = false;
            else
            {
                lnkSave.Visible = true;
                fillReferences();
            }
            DivAction = true;
            uplView.Update();
            Action = Constants.InsertAction;
        }

        public void fillChangestatusForm(BillTrackings tracking)
        {
            var firstOrDefault = tracking.FirstOrDefault();
            if (firstOrDefault == null) return;

            ValidationDdlId1.clearForm();
            IsVisiableValidationDiv = false;

            //Header
            Ord_No = firstOrDefault.OrdNumber;
            Ord_No1 = firstOrDefault.OrderNumber1;
            POValue = firstOrDefault.POValue;
           // ForecastingHead.IsVisiableSubId = true;
            ForecastingHead.clearData();
            ForecastingHead.IsVisiableSubId = true;
            ForecastingHead.IsEnableSubId = false;
            ForecastingHead.SetData(firstOrDefault.Head);
            ForecastingHead.IsEnableID = false;
            ForecastingHead.IsEnableSubId = false;
            ForecastingHead.IsEnableLocation = false;
            ForecastingHead.IsEnableDate = false;
            BillingPartControl1.IsVisiableInputDiv = false;
            BillingPartControl1.IsVisiableBillLocation = false;
            BillingPartControl1.IsVisiableLogisticNumber = false;

            int[] listofcell_false = { 5, 10, 29 };
            BillingPartControl1.IsVisiableListCell(listofcell_false, false);

            int[] listofcell_true = { 0, 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14, 15 };
            BillingPartControl1.IsVisiableListCell(listofcell_true, true);

            IsVisiableStatusChange = true;
            BillingPartControl1.PartDetails = firstOrDefault.Details;
            BillingPartControl1.GVPartData = firstOrDefault.Details;
        }
        protected void SelectPartNumber(object sender, EventArgs e)
        {
            if (BillingPartControl1.PartNumber == string.Empty) return;
            BindPartsDetail();
            fillFCNumberQuantity();
            var PartDetail = BillingPartControl1.PartDetails.Where(x => x.PartDetail.PartNumber.ToTrimString() == BillingPartControl1.PartNumber).FirstOrDefault();
            if (PartDetail == null) return;
            decimal prevQty = FilterOrder == Constants.ChangeStatus ? PartDetail.BillQuantity - PartDetail.SQuantity : PartDetail.POQuantity - PartDetail.SQuantity;
            if (prevQty == 0)
            {
                CustomMessageControl.MessageBodyText = PartDetail.PartDetail.PartNumber + " this part qty finished";
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                BillingPartControl1.PartNumber = string.Empty;
                return;
            }
            BillingPartControl1.Unit = PartDetail.Unit.Id;
            BillingPartControl1.POQuantity = FilterOrder == Constants.ChangeStatus ? PartDetail.BillQuantity : PartDetail.POQuantity;
            BillingPartControl1.UnitValue = PartDetail.UnitValue;
            BillingPartControl1.PrevQty = FilterOrder == Constants.ChangeStatus ? PartDetail.BillQuantity - PartDetail.SQuantity : PartDetail.POQuantity - PartDetail.SQuantity;
            BillingPartControl1.BillQuantity = 0;
            BillingPartControl1.TaxRate = 0;
            BillingPartControl1.Modality = PartDetail.Modality;
            BillingPartControl1.IsVisiablePOValue = true;
            BillingPartControl1.POValue = PartDetail.POValue;
            
            if (BillingPartControl1.FCNumberControl.Items.Count > 0)
                BillingPartControl1.FCNumberControl.SelectedIndex = 0;
            if (FilterOrder == Constants.PendingOrder)
                BillingPartControl1.BillStatus = Constants.TxnDebriefingType;            
            uplView.Update();
        }
        //public void SelectAltPartNumber(object sender, EventArgs e)
        //{
        //    AlternativePartCahnge();
        //}
        //public void AltPartTextChange(object sender, EventArgs e)
        //{
        //    AlternativePartCahnge();
        //}
        protected void SelectCategories(object sender, EventArgs e)
        {
            BindPartsDetail();
        }
        //private void AlternativePartCahnge()
        //{
        //    if (string.IsNullOrEmpty(BillingPartControl1.AltPartNumber.ToTrimString()))
        //    {
        //        BillingPartControl1.CategoryCode = string.Empty;
        //        BillingPartControl1.PartType = string.Empty;
        //    }
        //    else
        //    {
        //        BindPartsDetail(true);
        //    }

        //    uplView.Update();
        //}
        protected void onShow_click(object sender, EventArgs e)
        {
            
            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            if (FilterOrder == Constants.PendingOrder || FilterOrder == Constants.FreshDeviationOrder)
            {
                var hidBillStatus = ((HiddenField)row.FindControl("hidBillStatus"));
                if (hidBillStatus == null) return;
                if (hidBillStatus.Value.ToTrimString() == Constants.TxnBackOrderType || hidBillStatus.Value.ToTrimString() == Constants.TxnStockTransferType)
                {
                    TMOrdNo = ((HiddenField)row.FindControl("hidOrd_No")).Value.ToTrimString();
                    var hidCategoryCode = ((HiddenField)row.FindControl("hidCategoryCode")).Value.ToTrimString();

                    TMStage = hidCategoryCode == "02" ? "13" : "12";
                    var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                    {
                        filter1 = Constants.DvStages,
                        filter2 = TMStage,
                        FilterKey = Constants.ReferencesType
                    };
                    var references = _controlPanel.GetReferences(queryArgument);
                    if (references.Any())
                    {
                        divTmReferences.Visible = true;
                        divPart.InnerText = "Part Number :" + row.Cells[1].Text.ToTrimString();
                    }
                    BindTMReferences(references);
                    fillTMReferences();
                }
                else
                {
                    clearTmData();
                }
            }
        }
        protected void EditPart(object sender, EventArgs e)
        {
            var closeLink = (Control)sender;
            GridViewRow row = (GridViewRow)closeLink.NamingContainer;
            var PartNumber = row.Cells[1].Text.ToTrimString();
            var slno = ((HiddenField)row.FindControl("hidSlNo")).Value.ToTrimString();
            
            if (PartNumber == string.Empty) return;


            if (FilterOrder == Constants.PendingDeviationOrder)
            {
                if (slno == string.Empty) return;
                var billTrackings = new BillTrackings();
                var billTrackingDeatils = BillingPartControlPending.PartDetails.Where(x => x.SlNo == slno);
                if (!billTrackingDeatils.Any()) return;
                var sqty = billTrackingDeatils.FirstOrDefault().SQuantity;
                var hidBillStatus = ((HiddenField)row.FindControl("hidBillStatus")).Value.ToTrimString();
                var PartQty = row.Cells[11].Text.ToTrimString().ToDecimal(2);
                if (hidBillStatus == Constants.TxnBackOrderType || hidBillStatus == Constants.TxnStockTransferType)
                    PartQty = row.Cells[26].Text.ToTrimString().ToDecimal(2);
                else
                {
                    CustomMessageControl.MessageBodyText = "Clear Deviation to Bill";
                    CustomMessageControl.MessageType = MessageTypes.Error;
                    CustomMessageControl.ShowMessage();
                    return;
                }
                if (PartQty == 0)
                {
                    CustomMessageControl.MessageBodyText = "part qty 0 still not send back from Team";
                    CustomMessageControl.MessageType = MessageTypes.Error;
                    CustomMessageControl.ShowMessage();
                    return;
                }
                sqty += PartQty;
                var hidOrd_No = ((HiddenField)row.FindControl("hidOrd_No")).Value.ToTrimString();
                var partDeatil = new BillTrackingDetails();
                //foreach()
                partDeatil.Add(new BillTrackingDetail
                {
                    Ord_no = hidOrd_No,
                    PrevSlNo = slno,
                    BillQuantity = sqty,
                    SQuantity = PartQty
                });
                billTrackings.Add(new BillTracking
                    {
                        OrdNumber = Ord_No,
                        Details = partDeatil,
                        DataBaseInfo = UserContext.DataBaseInfo
                    });
                _transactionManager.SetApproval(billTrackings);
                FillForm(sender, e);
            }
            else
            {
                var PartDetail = getPartDetailFromPendingOrder(PartNumber);
                if (StageType == Constants.TxnDebriefingType)
                    PartDetail = getPartDetailFromPendingDebriefing(slno);

                if (PartDetail == null) return;
                if (PartDetail.BillQuantity - PartDetail.SQuantity == 0)
                {
                    CustomMessageControl.MessageBodyText = PartDetail.PartDetail.PartNumber + "  part qty finished";
                    CustomMessageControl.MessageType = MessageTypes.Error;
                    CustomMessageControl.ShowMessage();
                    return;
                }
                BillingPartControl1.IsVisiableInputDiv = true;
                IsvisiableDivFreshPart = true;
               
                //Add Part 
                var keyValuePairItems = new KeyValuePairItems();
                keyValuePairItems.Add(new KeyValuePairItem(PartDetail.PartDetail.PartNumber.ToTrimString().ToUpper(), PartDetail.PartDetail.Description.ToTrimString()));

                _genericClass.LoadDropDown(BillingPartControl1.PartControl, null, null, null, keyValuePairItems);
                if (FilterOrder == Constants.AlternativePartNeeded)
                {
                    BillingPartControl1.IsVisiablePartText = true;
                    BillingPartControl1.PartNumberText = string.Empty;
                    BillingPartControl1.PartType = string.Empty;
                    BillingPartControl1.CategoryCode = string.Empty;
                }
                else
                {
                    BillingPartControl1.PartNumber = PartDetail.PartDetail.PartNumber;
                    BillingPartControl1.PartType = PartDetail.MaterialGroup.Id;
                    BillingPartControl1.CategoryCode = PartDetail.MaterialType.Id;
                }
                BillingPartControl1.PrevSLNo = PartDetail.SlNo;
                BillingPartControl1.Unit = PartDetail.Unit.Id;
                BillingPartControl1.POQuantity = PartDetail.POQuantity;
                BillingPartControl1.UnitValue = PartDetail.UnitValue;
                BillingPartControl1.PrevQty = PartDetail.BillQuantity - PartDetail.SQuantity;
                BillingPartControl1.BillQuantity = PartDetail.BillQuantity - PartDetail.SQuantity;
                BillingPartControl1.TaxRate = PartDetail.TaxRate;
                BillingPartControl1.TaxType = PartDetail.TaxType;
                BillingPartControl1.Modality = PartDetail.Modality;
                BillingPartControl1.AlternativePart = FilterOrder == Constants.AlternativePartNeeded ? PartDetail.PartDetail.PartNumber : PartDetail.PartDetail.DetailedDescription;
                BillingPartControl1.IsVisiableBillLocation = true;
                BillingPartControl1.BillLocation = PartDetail.BillLocation.Id;
                BillingPartControl1.RelatedSR = PartDetail.RelatedSR;
                BillingPartControl1.IsVisiableLogisticNumber = true;
                BillingPartControl1.IsEnableLogisticNumber = true;
                BillingPartControl1.LogisticOrderNumber = PartDetail.LogisticOrderNumber;
                //  BillingPartControl1.changeAddRemQtyDivClass("col-md-3", "col-md-6", "col-md-6 pt20");
                BillingPartControl1.isVisiableCell(14, true);
                BillingPartControl1.isVisiableCell(15, true);
                BillingPartControl1.isVisiableCell(16, true);
                if (StageType == Constants.TxnDebriefingType)
                {
                    keyValuePairItems.Clear();
                    keyValuePairItems = new KeyValuePairItems();
                    keyValuePairItems.Add(new KeyValuePairItem(string.Empty, "Select Bill Status"));
                    keyValuePairItems.Add(new KeyValuePairItem(Constants.Billed, "Bill"));
                    keyValuePairItems.Add(new KeyValuePairItem(Constants.BackToOrder, "Back To Order"));
                    keyValuePairItems.Add(new KeyValuePairItem(Constants.SystemIssue, "System Issue"));

                    _genericClass.LoadDropDown(BillingPartControl1.BillStatusControl, null, null, null, keyValuePairItems);

                    BillingPartControl1.IsEnableBillLocation = false;
                    BillingPartControl1.IsEnableLogisticNumber = false;
                    BillingPartControl1.IsEnableModality = false;
                    BillingPartControl1.IsEnableTaxRate = false;
                    BillingPartControl1.IsEnableTaxType = false;
                    BillingPartControl1.IsEnablePartType = false;
                    BillingPartControl1.IsEnablePOQty = false;
                    BillingPartControl1.IsEnableUnitValue = false;
                    BillingPartControl1.IsVisiableBillQuantity = true;
                    BillingPartControl1.IsVisiableUnit = false;
                    BillingPartControl1.IsVisiableBillStatus = true;
                    BillingPartControl1.IsVisiableBillLocation = true;
                    BillingPartControl1.IsVisiableTax = false;
                }
                else if (FilterOrder == Constants.AlternativePartNeeded)
                {
                    BillingPartControl1.IsVisiablePOValue = true;
                    BillingPartControl1.POValue = PartDetail.POValue;
                    BillingPartControl1.BillStatusControl.Items.Remove(new ListItem("Alternate Part Number Needed", Constants.AlternativePartNeeded));                    
                }
                else
                {
                    BillingPartControl1.BillStatus = Constants.TxnDebriefingType;
                    BillingPartControl1.IsEnableBillStatus = false;
                    BillingPartControl1.IsVisiableTax = true;
                    BillingPartControl1.IsEnableModality = false;
                }
            }
            uplView.Update();
        }
        private BillTrackingDetail getPartDetailFromPendingOrder(string PartNumber)
        {
            var PartDetail = (from part in BillingPartControlIdPending.PartDetails
                              where part.PartDetail.PartNumber.ToTrimString().Equals(PartNumber)
                              group part by part.PartDetail.PartNumber into g
                              select new BillTrackingDetail
                              {
                                  PartDetail = new Material
                                  {
                                      PartNumber = g.Key,
                                      Description = g.FirstOrDefault().PartDetail.Description,
                                      DetailedDescription = g.Where(x => x.PartDetail.DetailedDescription != string.Empty).Any() ?
                                                                g.Where(x => x.PartDetail.DetailedDescription != string.Empty).FirstOrDefault().PartDetail.DetailedDescription :
                                                                string.Empty,
                                  },
                                  MaterialGroup = new MaterialGroup
                                  {
                                      Id = g.FirstOrDefault().MaterialGroup.Id,
                                  },
                                  MaterialType = new MaterialType
                                  {
                                      Id = g.FirstOrDefault().MaterialType.Id,
                                  },
                                  Unit = new Model.Control.Unit
                                  {
                                      Id = g.FirstOrDefault().Unit.Id,
                                  },
                                  POQuantity = g.FirstOrDefault().POQuantity,
                                  UnitValue = g.FirstOrDefault().UnitValue,
                                  SQuantity = g.Sum(x => x.SQuantity),
                                  BillQuantity = g.Sum(x => x.BillQuantity),
                                  TaxRate = g.FirstOrDefault().TaxRate,
                                  TaxType = g.FirstOrDefault().TaxType,
                                  Modality = g.FirstOrDefault().Modality,
                                  BillLocation = g.FirstOrDefault().BillLocation,
                                  LogisticOrderNumber = string.Join(Constants.SpecialCharComma, g.Select(i => i.LogisticOrderNumber == null ? string.Empty : i.LogisticOrderNumber.ToTrimString()))
                              }).ToList().FirstOrDefault();
            return PartDetail;
        }
        private BillTrackingDetail getPartDetailFromPendingDebriefing(string SLNO)
        {
            var PartDetail = BillingPartControlIdPending.PartDetails.Where(x => x.SlNo.ToTrimString() == SLNO).FirstOrDefault();
            return PartDetail;
        }
        public void BindPartsDetail(bool IsAlternativePart = false)
        {
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = IsAlternativePart ? BillingPartControl1.AlternativePart : BillingPartControl1.PartNumber,
                filter1 = string.Empty,
                filter4 = Constants.RetriveForm,
                FilterKey = Constants.TableMaterials
            };
            var parts = _controlPanel.GetMaterials(queryArgument);
            var firstOrDefault = parts.FirstOrDefault();
            if (firstOrDefault == null) return;
            //if (IsAlternativePart)
            //{
            //    BillingPartControl1.AltPartCat = firstOrDefault.MaterialType.Id;
            //    BillingPartControl1.AltPartType = firstOrDefault.MaterialGroup.Id;
            //}
            //else
            //{
                BillingPartControl1.CategoryCode = firstOrDefault.MaterialType.Id;
                BillingPartControl1.PartType = firstOrDefault.MaterialGroup.Id;
           // }
            uplView.Update();
        }
        private void fillFCNumberQuantity()
        {
            var filter = new KeyValuePairItems
                        {                            
                             new KeyValuePairItem(Constants.filter1, ForecastingHead.DealerCustomer=="AE"?ForecastingHead.Customer:string.Empty),
                             new KeyValuePairItem(Constants.filter2, BillingPartControl1.PartNumber),
                             new KeyValuePairItem(Constants.filter3, Constants.TRNCompletedOFF),
                             new KeyValuePairItem(Constants.filter4,ForecastingHead.DealerCustomer=="AI"?ForecastingHead.RequestorLocation:string.Empty),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextFCNo),
                            new KeyValuePairItem(Constants.masterType,Constants.FCNumberAndQuantity)
                        };
            _genericClass.LoadDropDown(BillingPartControl1.FCNumberControl, filter, null, UserContext.DataBaseInfo);
        }
        protected void onselectBillStatus(object sender, EventArgs e)
        {
            //if (BillingPartControl1.BillStatus == Constants.AlternativePartNeeded)
            //{
            //    BillingPartControl1.IsVisiableAlternativePartText = true;
            //}
            //else
            if (BillingPartControl1.BillStatus == Constants.TRNCancelOFF)
            {
                // BillingPartControl1.BillQuantity = BillingPartControl1.TaxRate = 0;
                if (BillingPartControl1.FCNumberControl.Items.Count > 0)
                    BillingPartControl1.FCNumberControl.SelectedIndex = 0;
                // BillingPartControl1.IsEnableBillQty = BillingPartControl1.IsEnableTaxRate = BillingPartControl1.IsEnableFCNumber = false;
                BillingPartControl1.AlternativePart = string.Empty;
                BillingPartControl1.IsVisiableAlternativePartText = false;
            }
            else
            {
                BillingPartControl1.IsEnableBillQty = BillingPartControl1.IsEnableTaxRate = BillingPartControl1.IsEnableFCNumber = BillingPartControl1.IsEnableTaxType = true;
                //BillingPartControl1.AlternativePart = string.Empty;
                //BillingPartControl1.IsVisiableAlternativePartText = false;
            }
            if (BillingPartControl1.BillStatus != Constants.AlternativePartNeeded && FilterOrder != Constants.AlternativePartNeeded)
            {
                BillingPartControl1.AlternativePart = string.Empty;
                BillingPartControl1.IsVisiableAlternativePartText = false;
                BillingPartControl1.IsVisiableAlternativePartDdlDiv = false;
            }
            
            if (BillingPartControl1.BillStatus == Constants.TRNCancelOFF ||
                BillingPartControl1.BillStatus == Constants.TxnBackOrderType ||
                BillingPartControl1.BillStatus == Constants.TxnStockTransferType)
                BillingPartControl1.IsEnableLogisticNumber = false;
            else
                BillingPartControl1.IsEnableLogisticNumber = true;

            if (BillingPartControl1.BillStatus == Constants.TxnDebriefingType ||
                                                BillingPartControl1.BillStatus == Constants.TxnBackOrderType ||
                                                BillingPartControl1.BillStatus == Constants.TxnStockTransferType)
                BillingPartControl1.IsEnableBillLocation = true;
            else
            {
                //BillingPartControl1.IsEnableBillLocation = false;
                if (BillingPartControl1.BillStatus != Constants.Billed &&
                    BillingPartControl1.BillStatus != Constants.BackToOrder &&
                    BillingPartControl1.BillStatus != Constants.SystemIssue)
                    BillingPartControl1.BillLocation = string.Empty;
            }           
        }
        
         
        protected void Add_Click(object sender, EventArgs e)
        {
            
            if (StageType == Constants.TxnDebriefingType)
            {
                var partDetails = BillingPartControlIdPending.PartDetails;
                var PartDetail = partDetails.Where(x => x.SlNo.ToTrimString() == BillingPartControl1.PrevSLNo).FirstOrDefault();

                if (PartDetail == null) return;
                PartDetail.SQuantity += BillingPartControl1.BillQuantity;               
                BillingPartControlIdPending.PartDetails = partDetails;
                if (PartDetail.SQuantity == PartDetail.BillQuantity)
                {
                    ListItem partlist = BillingPartControl1.PartControl.Items.FindByValue(BillingPartControl1.PartNumber.ToTrimString());
                    if (partlist != null)
                        BillingPartControl1.PartControl.Items.Remove(partlist);
                }
            }
            else
            {
                if (FilterOrder == Constants.PendingOrder || FilterOrder == Constants.FreshDeviationOrder
                                                            || FilterOrder == Constants.AlternativePartNeeded)
                {
                    if (BillingPartControlIdPending.PartDetails == null) return;
                    var partDetails = BillingPartControlIdPending.PartDetails;

                    var filterPartNumber = FilterOrder == Constants.AlternativePartNeeded ? BillingPartControl1.AlternativePart : BillingPartControl1.PartNumber;

                    var PartDetail = (from part in partDetails
                                      where part.PartDetail.PartNumber.ToTrimString().Equals(filterPartNumber)
                                      group part by part.PartDetail.PartNumber into g
                                      select new BillTrackingDetail
                                      {
                                          SQuantity = g.FirstOrDefault().SQuantity,
                                          BillQuantity = g.Sum(x => x.BillQuantity),
                                          BQuantity = g.FirstOrDefault().BQuantity,
                                      }).ToList().FirstOrDefault();

                    if (PartDetail == null) return;
                    PartDetail.SQuantity += BillingPartControl1.BillQuantity;
                  

                    var qty = BillingPartControl1.BillQuantity;
                    foreach (var billedPart in partDetails.Where(x => x.PartDetail.PartNumber.ToTrimString() == filterPartNumber))
                    {
                        
                        if ((billedPart.BillQuantity - billedPart.SQuantity) > 0)
                        {
                            if (qty > (billedPart.BillQuantity - billedPart.SQuantity))
                            {
                                qty -= (billedPart.BillQuantity - billedPart.SQuantity);
                                billedPart.ShQuantity = (billedPart.BillQuantity - billedPart.SQuantity);
                                billedPart.SQuantity += (billedPart.BillQuantity - billedPart.SQuantity);
                                billedPart.Off = Constants.TRNCompletedOFF;                                
                            }
                            else
                            {
                                billedPart.SQuantity += qty;
                                billedPart.Off = billedPart.SQuantity == billedPart.BillQuantity ? Constants.TRNCompletedOFF : Constants.TRNInProcessOFF;
                                billedPart.ShQuantity = qty;
                                break;
                            }                            
                        }
                    }

                    foreach (var billpart in partDetails.Where(x => x.PartDetail.PartNumber.ToTrimString() == filterPartNumber))
                    {
                        billpart.BQuantity += partDetails.Where(x => x.SlNo.ToTrimString() == billpart.SlNo.ToTrimString()).Sum(x => x.ShQuantity);
                        //previus part coverting to alternative part
                        if (FilterOrder == Constants.AlternativePartNeeded)
                            billpart.PartDetail.DetailedDescription = BillingPartControl1.PartNumber;
                    }

                    BillingPartControlIdPending.PartDetails = partDetails;
                }
                else
                {
                    if (BillingPartControl1.PartDetails == null) return;
                    var partDetails = BillingPartControl1.PartDetails;
                    var PartDetail = partDetails.Where(x => x.PartDetail.PartNumber.ToTrimString() == BillingPartControl1.PartNumber).FirstOrDefault();
                    if (PartDetail == null) return;
                    //PartDetail.POQuantity -= BillingPartControl1.BillQuantity;
                    PartDetail.SQuantity += BillingPartControl1.BillQuantity;
                    BillingPartControl1.PartDetails = partDetails;
                    if (PartDetail.SQuantity == PartDetail.POQuantity)
                    {
                        ListItem partlist = BillingPartControl1.PartControl.Items.FindByValue(BillingPartControl1.PartNumber.ToTrimString());
                        if (partlist != null)
                            BillingPartControl1.PartControl.Items.Remove(partlist);
                    }
                }

            }
        }
       
        protected void Delete_Click(object sender, EventArgs e)
        {
            if (StageType == Constants.TxnDebriefingType)
            {
                var partDetails = BillingPartControlIdPending.PartDetails;
                var PartDetail = partDetails.Where(x => x.SlNo.ToTrimString() == BillingPartControl1.PrevSLNo).FirstOrDefault();

                if (PartDetail == null) return;
                PartDetail.SQuantity -= BillingPartControl1.BillQuantity;               
                BillingPartControlIdPending.PartDetails = partDetails;               
            }
            else
            {
                if (FilterOrder == Constants.PendingOrder || FilterOrder == Constants.FreshDeviationOrder || FilterOrder == Constants.AlternativePartNeeded)
                {
                    var partDetails = BillingPartControlIdPending.PartDetails;
                    var filterPartNumber = FilterOrder == Constants.AlternativePartNeeded ? BillingPartControl1.AlternativePart : BillingPartControl1.PartNumber;

                    var PartDetail = (from part in partDetails
                                      where part.PartDetail.PartNumber.ToTrimString().Equals(filterPartNumber)
                                      group part by part.PartDetail.PartNumber into g
                                      select new BillTrackingDetail
                                      {
                                          SQuantity = g.FirstOrDefault().SQuantity,
                                          BillQuantity = g.Sum(x => x.BillQuantity),
                                      }).ToList().FirstOrDefault();

                    if (PartDetail == null) return;
                    PartDetail.SQuantity -= BillingPartControl1.BillQuantity;
                    var PartDetail1 = partDetails.Where(x => x.PartDetail.PartNumber.ToTrimString() == filterPartNumber).FirstOrDefault();
                    PartDetail1.SQuantity = PartDetail.SQuantity;
                    
                    PartDetail1.BQuantity -= BillingPartControl1.BillQuantity;
                    PartDetail1.Off = PartDetail1.SQuantity == PartDetail1.BillQuantity ? Constants.TRNCompletedOFF : Constants.TRNInProcessOFF;
                    
                    BillingPartControlIdPending.PartDetails = partDetails;
                }
                else
                {
                    var partDetails = BillingPartControl1.PartDetails;
                    if (partDetails == null) return;
                    var PartDetail = partDetails.Where(x => x.PartDetail.PartNumber.ToTrimString() == BillingPartControl1.PartNumber).FirstOrDefault();
                    if (PartDetail == null) return;
                    // PartDetail.POQuantity += BillingPartControl1.BillQuantity;
                    PartDetail.SQuantity -= BillingPartControl1.BillQuantity;
                    BillingPartControl1.PartDetails = partDetails;
                }
            }
        }
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            var result = false;
            if (StageType == Constants.TxnDebriefingType)
                result = SaveDebriefing();
            else if (StageType == Constants.TxnTrackingType)
                result = SaveTracking();
            else
            {
                if (FilterOrder == Constants.FreshOrder || FilterOrder == Constants.DeviationOrder)
                    result = SaveFreshOrdering();
                else if (FilterOrder == Constants.GSPOType || FilterOrder == Constants.C09Type)
                    result = SaveChangestatus_APN();
                else if (FilterOrder == Constants.AlternativePartNeeded)
                {
                    if (BillingPartControlIdPending.PartDetails.Where(x => x.SQuantity != x.BillQuantity).Any())
                    {
                        CustomMessageControl.MessageBodyText = GlobalCustomResource.ValidationAllSelect;
                        CustomMessageControl.MessageType = MessageTypes.Error;
                        CustomMessageControl.ShowMessage();
                        return;
                    }
                    result = SaveAlternativePartForm();
                }
                else
                    result = SavePendingOrdering();
            }
            if (result)
            {
                if (StageType == Constants.TxnDebriefingType)
                    CustomMessageControl.MessageBodyText = GlobalCustomResource.DebriefingSaved;
                else if (StageType == Constants.TxnTrackingType)
                    CustomMessageControl.MessageBodyText = GlobalCustomResource.TrackingSaved;
                else
                    CustomMessageControl.MessageBodyText = GlobalCustomResource.OrderingSave;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                if (StageType == Constants.TxnDebriefingType)
                    AuditLog.LogEvent(UserContext, SysEventType.INFO, "Debriefing Saved",
                        GlobalCustomResource.DebriefingSaved, true);
                else if (StageType == Constants.TxnTrackingType)
                    AuditLog.LogEvent(UserContext, SysEventType.INFO, "Tracking Saved",
                        GlobalCustomResource.TrackingSaved, true);
                else
                    AuditLog.LogEvent(UserContext, SysEventType.INFO, "Ordering Saved",
                  GlobalCustomResource.OrderingSave, true);
                clearForm();
                DivAction = false;
                BindData();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "openTabFunctionCall", "openTab(1)", true);
            }
            else
            {
                if (StageType == Constants.TxnDebriefingType)
                    CustomMessageControl.MessageBodyText = GlobalCustomResource.DebriefingFailed;
                else if (StageType == Constants.TxnTrackingType)
                    CustomMessageControl.MessageBodyText = GlobalCustomResource.TrackingFailed;
                else
                    CustomMessageControl.MessageBodyText = GlobalCustomResource.OrderingFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                if (StageType == Constants.TxnDebriefingType)
                    AuditLog.LogEvent(UserContext, SysEventType.INFO, "Debriefing Update Failed",
                        GlobalCustomResource.DebriefingFailed, true);
                else if (StageType == Constants.TxnTrackingType)
                    AuditLog.LogEvent(UserContext, SysEventType.INFO, "Tracking Update Failed",
                        GlobalCustomResource.TrackingFailed, true);
                else
                    AuditLog.LogEvent(UserContext, SysEventType.INFO, "Ordering Update Failed",
                    GlobalCustomResource.OrderingFailed, true);
            }
        }
        public bool SaveTracking()
        {
            BillTrackings billTrackings = new BillTrackings();
            var partdetails = BillingPartControl1.GVPartData;
            var off = Constants.TRNCompletedOFF;
            foreach (var partdetail in partdetails)
            {
                if (partdetail.Remark.ToTrimString() == string.Empty || partdetail.DocketNumber.ToTrimString() == string.Empty || partdetail.InvoiceNumber.ToTrimString() == string.Empty)
                {
                    off = Constants.TRNPendingOFF;
                    break;
                }
            }

            billTrackings.Add(new BillTracking
            {
                Details = BillingPartControl1.GVPartData,
                OrdNumber = Ord_No,
                OrderType = Constants.SalesInvoice,
                StageId = TransactionStageControlId.ActiveStage.ToTrimString(),
                Bu = UserContext.UserProfile.Bu,
                Branch = UserContext.UserProfile.Branch,
                LogedUser = UserContext.UserId,
                Action = Action,
                DataBaseInfo = UserContext.DataBaseInfo,
                Off = off
            });
            var firstOrDefault = billTrackings.FirstOrDefault();
            var references = getReferences();
            if (firstOrDefault != null)
                firstOrDefault.References = references;


            return _transactionManager.SetTracking(billTrackings);
        }
        public bool SaveDebriefing()
        {
            BillTrackingDetails PrevParteDtails = new BillTrackingDetails();

            var partDetails = BillingPartControl1.GVPartData;

            foreach (var prevPart in BillingPartControlPending.PartDetails.Where(x => x.SQuantity > 0))
            {
                var partDetail = partDetails.Where(x => x.PartDetail.PartNumber.ToTrimString() == prevPart.PartDetail.PartNumber.ToTrimString() &&
                                                                                   x.BillStatus.ToTrimString() == Constants.SystemIssue);
                if (partDetail.Any())
                {
                    var firstOrdefaultPart = partDetail.FirstOrDefault();
                    if (firstOrdefaultPart != null)
                        prevPart.SQuantity -= firstOrdefaultPart.BillQuantity;
                }
                prevPart.Off = prevPart.SQuantity == prevPart.BillQuantity ? Constants.TRNCompletedOFF : Constants.TRNInProcessOFF;
                PrevParteDtails.Add(prevPart);
            }

            var header = ForecastingHead.GetData();
            header.OrderAmendmentNumber = "0";

            header.NetValue = BillingPartControl1.GVPartData.Sum(x => x.POValue);
            BillTrackings billTrackings = new BillTrackings();
            ValidationDdlId1.clearForm();

            billTrackings.Add(new BillTracking
            {
                OrdNumber = Ord_No,
                OrderType = Constants.SalesInvoice,
                Head = header,
                BillValiditions = ValidationDdlId1.GetData(),
                Details = BillingPartControl1.GVPartData,
                UpdatePrevDetails = PrevParteDtails,
                StageId = TransactionStageControlId.ActiveStage.ToTrimString(),
                Bu = UserContext.UserProfile.Bu,
                Branch = UserContext.UserProfile.Branch,
                LogedUser = UserContext.UserId,
                Action = Action,
                DataBaseInfo = UserContext.DataBaseInfo,
                Off = Constants.TRNInProcessOFF
            });
            var firstOrDefault = billTrackings.FirstOrDefault();
            var references = getReferences();
            if (firstOrDefault != null)
                firstOrDefault.References = references;


            return _transactionManager.SetDebriefing(billTrackings);
        }
        public bool SavePendingOrdering()
        {

            BillTrackingDetails PrevDetails = new BillTrackingDetails();
            BillTrackingDetails ParteDtails = new BillTrackingDetails();

            foreach (var billedPart in BillingPartControlIdPending.PartDetails.Where(x => x.SQuantity > 0))
            {
                PrevDetails.Add(billedPart);
            }
            foreach (var gvdata in BillingPartControl1.GVPartData)
            {
                var qty = gvdata.BillQuantity;
                
                foreach (var billedPart in BillingPartControlIdPending.PartDetails.Where(x => x.PartDetail.PartNumber.ToTrimString() == gvdata.PartDetail.PartNumber.ToTrimString()))
                {
                    gvdata.FCSlNo = billedPart.FCSlNo;
                   // PrevDetails.Add(billedPart);
                }
                #region
                //foreach (var billedPart in BillingPartControlIdPending.PartDetails.Where(x => x.PartDetail.PartNumber.ToTrimString() == gvdata.PartDetail.PartNumber.ToTrimString()))
                //{
                //    gvdata.FCSlNo = billedPart.FCSlNo;
                //    if (qty > (billedPart.BillQuantity - billedPart.SQuantity))
                //    {
                //        billedPart.SQuantity = (billedPart.BillQuantity - billedPart.SQuantity);
                //        qty -= billedPart.SQuantity;
                //        billedPart.Off = Constants.TRNCompletedOFF;
                //        PrevDetails.Add(billedPart);
                //    }
                //    else
                //    {

                //        billedPart.SQuantity += qty;
                //        billedPart.Off = billedPart.SQuantity == billedPart.BillQuantity ? Constants.TRNCompletedOFF : Constants.TRNInProcessOFF;
                //        PrevDetails.Add(billedPart);
                //        break;
                //    }
                //}
                #endregion
                if (gvdata.BillQuantity > 0)
                    ParteDtails.Add(gvdata);
            }

            var head = ForecastingHead.GetData();
            head.NetValue = ParteDtails.Sum(x => x.POValue);
            BillTrackings billTrackings = new BillTrackings();
            billTrackings.Add(new BillTracking
            {
                OrdNumber = Ord_No,
                OrderNumber1 = Ord_No1,
                OrderType = Constants.SalesOrderTdType,
                Head = head,
                BillValiditions = ValidationDdlId1.GetData(),
                Details = ParteDtails,
                UpdatePrevDetails = PrevDetails,
                StageId = TransactionStageControlId.ActiveStage.ToTrimString(),
                Bu = UserContext.UserProfile.Bu,
                Branch = UserContext.UserProfile.Branch,
                LogedUser = UserContext.UserId,
                Action = Action,
                DataBaseInfo = UserContext.DataBaseInfo,
                FormType = FormType.Pending,
            });
            var firstOrDefault = billTrackings.FirstOrDefault();
            var references = getReferences();
            if (firstOrDefault != null)
                firstOrDefault.References = references;

            return _transactionManager.SetOrderingData(billTrackings);
        }
        public bool SaveFreshOrdering()
        {
            foreach (var completedPart in BillingPartControl1.PartDetails)
            {
                var firstOrDefaultPart = (from x in BillingPartControl1.GVPartData
                                          where x.PartDetail.PartNumber.ToTrimString().Equals(completedPart.PartDetail.PartNumber.ToTrimString())
                                          group x by x.PartDetail.PartNumber into g
                                          select new BillTrackingDetail
                                          {
                                              PartDetail = new Material
                                              {
                                                  PartNumber = g.Key,
                                                  Description = g.FirstOrDefault().PartDetail.Description
                                              },
                                              BillQuantity = g.Sum(s => s.BillQuantity)
                                          }).ToList().FirstOrDefault();

                if (firstOrDefaultPart == null)
                {
                    CustomMessageControl.MessageBodyText = "Complete All Part and Quantity of " + completedPart.PartDetail.Description + " this Part";
                    CustomMessageControl.MessageType = MessageTypes.Error;
                    CustomMessageControl.ShowMessage();
                    return false;
                }
                if (firstOrDefaultPart.BillQuantity != completedPart.POQuantity)
                {
                    CustomMessageControl.MessageBodyText = "Complete All Part and Quantity of " + completedPart.PartDetail.Description + " this Part";
                    CustomMessageControl.MessageType = MessageTypes.Error;
                    CustomMessageControl.ShowMessage();
                    return false;
                }
            }


            BillTrackingDetails PrevDetails = new BillTrackingDetails();
            BillTrackingDetails ParteDtails = new BillTrackingDetails();
            OrderHead PrevHead = new OrderHead();
            var off = Constants.TRNCompletedOFF;
            foreach (var gvdata in BillingPartControl1.GVPartData)
            {
                var firstOrDefaultPart = BillingPartControl1.PartDetails.Where(x => x.PartDetail.PartNumber.ToTrimString() == gvdata.PartDetail.PartNumber.ToTrimString()).FirstOrDefault();

                firstOrDefaultPart.Off = FilterOrder == Constants.DeviationOrder ? Constants.TRNInProcessOFF : 
                                                        firstOrDefaultPart.POQuantity == firstOrDefaultPart.SQuantity ? Constants.TRNCompletedOFF : Constants.TRNInProcessOFF;
                

                firstOrDefaultPart.CurrentStatus = gvdata.BillStatus == Constants.TxnBackOrderType ||
                                                    gvdata.BillStatus == Constants.TxnStockTransferType ||
                                                    gvdata.BillStatus == Constants.TxnDebriefingType ? string.Empty : gvdata.BillStatus;

                //firstOrDefaultPart.CurrentStatus = gvdata.BillStatus;
                gvdata.FCBillQuantity = gvdata.SQuantity;
                gvdata.SQuantity = 0;
                gvdata.Off = gvdata.BillStatus == Constants.TxnBackOrderType || gvdata.BillStatus == Constants.TxnStockTransferType ? Constants.TRNLogedOFF : Constants.TRNInProcessOFF;
                if (gvdata.BillQuantity > 0)
                    ParteDtails.Add(gvdata);
                PrevDetails.Add(firstOrDefaultPart);

            }

            var prevcount = BillingPartControl1.PartDetails.Count;
            var count = BillingPartControl1.GVPartData.GroupBy(x => x.PartDetail.PartNumber).ToList().Count;
            if (prevcount == count)
            {
                foreach (var PrevDetail in PrevDetails)
                {
                    if (PrevDetail.Off == Constants.TRNInProcessOFF)
                    {
                        off = Constants.TRNInProcessOFF;
                        break;
                    }
                }
            }
            else
                off = Constants.TRNInProcessOFF;

            if (FilterOrder == Constants.DeviationOrder)
            {
                PrevHead = ForecastingHead.GetData();
                PrevHead.OrderNumber = Ord_No;
                PrevHead.Off = Constants.TRNInProcessOFF;
                PrevHead.BillStatus = FilterOrder;
            }
            else if (off == Constants.TRNCompletedOFF)
            {
                PrevHead = ForecastingHead.GetData();
                PrevHead.OrderNumber = Ord_No;
                PrevHead.Off = FilterOrder == Constants.DeviationOrder ? Constants.TRNInProcessOFF : off;
                PrevHead.BillStatus = FilterOrder;
            }

            BillTrackings billTrackings = new BillTrackings();
            var head = ForecastingHead.GetData();
            head.NetValue = POValue;
            billTrackings.Add(new BillTracking
            {
                OrdNumber = Ord_No,
                OrderType = Constants.SalesOrderTdType,
                Head = head,
                BillValiditions = ValidationDdlId1.GetData(),
                Details = ParteDtails,
                UpdatePrevHead = PrevHead,
                UpdatePrevDetails = PrevDetails,
                StageId = TransactionStageControlId.ActiveStage.ToTrimString(),
                Bu = UserContext.UserProfile.Bu,
                Branch = UserContext.UserProfile.Branch,
                LogedUser = UserContext.UserId,
                Action = Action,
                DataBaseInfo = UserContext.DataBaseInfo,
                FormType = FilterOrder == Constants.DeviationOrder ? FormType.DeviationOrder : FormType.Fresh,

            });
            var firstOrDefault = billTrackings.FirstOrDefault();
            var references = getReferences();
            if (firstOrDefault != null)
                firstOrDefault.References = references;

            return _transactionManager.SetOrderingData(billTrackings);
        }

        public bool SaveChangestatus_APN()
        {
            BillTrackings billTrackings = new BillTrackings();
            var partdetails = BillingPartControl1.PartDetails;
            var prevpartdetails = BillingPartControl1.PartDetails;
            var off = Constants.TRNCompletedOFF;
           
            foreach (var partdetail in prevpartdetails)
            {                
               partdetail.Off = Constants.TRNCompletedOFF;                
              //  prevpartdetails.Add(partdetail);               
            }

            foreach (var partdetail in partdetails)
            {
                partdetail.Off = Changedstatus == Constants.TRNCancelOFF ? Constants.TRNCompletedOFF : Constants.TRNInProcessOFF;
                partdetail.BillStatus = Changedstatus;
                if (Changedstatus == Constants.TxnBackOrderType || Changedstatus == Constants.TxnStockTransferType)
                    partdetail.ShQuantity = 0;
                else
                    partdetail.ShQuantity = partdetail.BillQuantity;
            }
            var head = ForecastingHead.GetData();
            head.NetValue = POValue;
            billTrackings.Add(new BillTracking
            {
                OrdNumber = Ord_No,
                OrderNumber1 = Ord_No1,
                POValue = POValue,
                OrderType = Constants.SaleRequestTdType,
                Head = head,
                Details = partdetails,
                UpdatePrevDetails = prevpartdetails,
                StageId = TransactionStageControlId.ActiveStage.ToTrimString(),
                Bu = UserContext.UserProfile.Bu,
                Branch = UserContext.UserProfile.Branch,
                LogedUser = UserContext.UserId,
                Action = Action,
                DataBaseInfo = UserContext.DataBaseInfo,
                Off = off
            });
            var firstOrDefault = billTrackings.FirstOrDefault();
            var references = getReferences();
            if (firstOrDefault != null)
                firstOrDefault.References = references;


            return _transactionManager.SetChangeStatus_ALPN(billTrackings);
        }

        public bool SaveAlternativePartForm()
        {
            
            BillTrackingDetails ParteDtails = new BillTrackingDetails();
            BillTrackingDetails PrevDetails = new BillTrackingDetails();

            foreach (var billedPart in BillingPartControlIdPending.PartDetails.Where(x => x.SQuantity > 0))
            {
                PrevDetails.Add(billedPart);
            }
            foreach (var gvdata in BillingPartControl1.GVPartData)
            {
                var qty = gvdata.BillQuantity;

                foreach (var billedPart in BillingPartControlIdPending.PartDetails.Where(x => x.PartDetail.PartNumber.ToTrimString() == gvdata.PartDetail.DetailedDescription.ToTrimString()))
                {
                    gvdata.FCSlNo = billedPart.FCSlNo;                    
                }
               
                if (gvdata.BillQuantity > 0)
                    ParteDtails.Add(gvdata);
            }
            var head = ForecastingHead.GetData();
            head.NetValue = POValue;

            BillTrackings billTrackings = new BillTrackings();
            billTrackings.Add(new BillTracking
            {
                OrdNumber = Ord_No,
                OrderNumber1 = Ord_No1,
                OrderType = Constants.SalesOrderTdType,
                Head = head,
                BillValiditions = ValidationDdlId1.GetData(),
                Details = ParteDtails,
                UpdatePrevDetails = PrevDetails,
                StageId = TransactionStageControlId.ActiveStage.ToTrimString(),
                Bu = UserContext.UserProfile.Bu,
                Branch = UserContext.UserProfile.Branch,
                LogedUser = UserContext.UserId,
                Action = Action,
                DataBaseInfo = UserContext.DataBaseInfo,
                FormType = FormType.AlternativePart,
            });
            var firstOrDefault = billTrackings.FirstOrDefault();
            var references = getReferences();
            if (firstOrDefault != null)
                firstOrDefault.References = references;

            return _transactionManager.SetOrderingData(billTrackings);           
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            clearForm();
            DivAction = false;
            BindData();
        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            DivAction = true;
            clearForm();
            uplView.Update();
        }
        public void clearForm()
        {           
            //Header
            ForecastingHead.clearData();
           //Validation
            ValidationDdlId1.clearForm();

            //Part Detail
            int[] listofcell = { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26,28, 30 };

            //Pending Grid Means First Grid for Pendding Order,Fresh Deviation Order,Pendding Deviation
            BillingPartControlPending.clearForm();
            BillingPartControlPending.PartDetails = new BillTrackingDetails();
            BillingPartControlPending.GVPartData = new BillTrackingDetails();
            BillingPartControlPending.IsVisiableListCell(listofcell, false);
            BillingPartControlPending.changeActionName = string.Empty;

            //Part Control For all form
            BillingPartControl1.clearForm();
            BillingPartControl1.IsVisiableInputDiv = true;
            BillingPartControl1.GVPartData = new BillTrackingDetails();
            BillingPartControl1.PartDetails = new BillTrackingDetails();
            BillingPartControl1.IsVisiableListCell(listofcell, false);
            BillingPartControl1.changeActionName = string.Empty;
           

            IsvisiableDivPendingPart = false;
            IsvisiableDivFreshPart = true;

            //References
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
            //This only change status form
            Changedstatus = string.Empty;
            IsVisiableStatusChange = false;
            
            //Fresh Order And Pendding Order
            clearTmData();
            uplView.Update();

            //filling Status Ddl
            if (BillingPartControl1.BillStatusControl.Items.FindByValue(Constants.TxnDebriefingType) == null)
                BillingPartControl1.BillStatusControl.Items.Insert(1, new ListItem("Debriefing", Constants.TxnDebriefingType));
            if (BillingPartControl1.BillStatusControl.Items.FindByValue(Constants.TRNCancelOFF) == null)
            {
                var maxCount = BillingPartControl1.BillStatusControl.Items.Count;
                BillingPartControl1.BillStatusControl.Items.Insert(maxCount, new ListItem("Cancelled", Constants.TRNCancelOFF));
            }
            if (BillingPartControl1.BillStatusControl.Items.FindByValue(Constants.AlternativePartNeeded) == null)
            {
                var maxCount = BillingPartControl1.BillStatusControl.Items.Count;
                BillingPartControl1.BillStatusControl.Items.Insert(maxCount, new ListItem("Alternate Part Number Needed", Constants.AlternativePartNeeded));
            }
            var statusList = BillingPartControl1.BillStatusControl.Items.FindByValue(Constants.PartReserved);
            if (statusList != null)
                BillingPartControl1.BillStatusControl.Items.Remove(statusList);

            BillingPartControl1.IsVisiablePartText = false;
            Action = Constants.InsertAction;
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
                    if (txnPageRef.RefCode.ToTrimString() == txnref.RefCode.ToTrimString())
                    {
                        if (RefrenceControl1.IsDateRequired)
                            setReferenceTextBox(RefrenceControl1, Dates.FormatDate(txnref.Date, Constants.Format02));
                        else
                            setReferenceTextBox(RefrenceControl1, txnref.Text);
                        flag = false;
                        break;
                    }
                    txnPageRef = getReferenceTextBox(RefrenceControl2);
                    if (txnPageRef.RefCode.ToTrimString() == txnref.RefCode.ToTrimString())
                    {
                        if (RefrenceControl2.IsDateRequired)
                            setReferenceTextBox(RefrenceControl2, Dates.FormatDate(txnref.Date, Constants.Format02));
                        else
                            setReferenceTextBox(RefrenceControl2, txnref.Text);
                        flag = false;
                        break;
                    }
                    txnPageRef = getReferenceTextBox(RefrenceControl3);
                    if (txnPageRef.RefCode.ToTrimString() == txnref.RefCode.ToTrimString())
                    {
                        if (RefrenceControl3.IsDateRequired)
                            setReferenceTextBox(RefrenceControl3, Dates.FormatDate(txnref.Date, Constants.Format02));
                        else
                            setReferenceTextBox(RefrenceControl3, txnref.Text);
                        flag = false;
                        break;
                    }
                    txnPageRef = getReferenceTextBox(RefrenceControl4);
                    if (txnPageRef.RefCode.ToTrimString() == txnref.RefCode.ToTrimString())
                    {
                        if (RefrenceControl4.IsDateRequired)
                            setReferenceTextBox(RefrenceControl4, Dates.FormatDate(txnref.Date, Constants.Format02));
                        else
                            setReferenceTextBox(RefrenceControl4, txnref.Text);
                        flag = false;
                        break;
                    }
                    txnPageRef = getReferenceTextBox(RefrenceControl5);
                    if (txnPageRef.RefCode.ToTrimString() == txnref.RefCode.ToTrimString())
                    {
                        if (RefrenceControl5.IsDateRequired)
                            setReferenceTextBox(RefrenceControl5, Dates.FormatDate(txnref.Date, Constants.Format02));
                        else
                            setReferenceTextBox(RefrenceControl5, txnref.Text);
                        flag = false;
                        break;
                    }
                    txnPageRef = getReferenceTextBox(RefrenceControl6);
                    if (txnPageRef.RefCode.ToTrimString() == txnref.RefCode.ToTrimString())
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
        private void BindTMReferences(WFComponentSubs references)
        {
            int i = 1;
            foreach (var reference in references)
            {
                switch (i)
                {
                    case 1:
                        divTmref1.Visible = true;
                        setReferenceTextBox(RefrenceControlTM1, reference);
                        break;
                    case 2:
                        divTmref2.Visible = true;
                        setReferenceTextBox(RefrenceControlTM2, reference);
                        break;
                    case 3:
                        divTmref3.Visible = true;
                        setReferenceTextBox(RefrenceControlTM3, reference);
                        break;
                    case 4:
                        divTmref4.Visible = true;
                        setReferenceTextBox(RefrenceControlTM4, reference);
                        break;
                    case 5:
                        divTmref5.Visible = true;
                        setReferenceTextBox(RefrenceControlTM5, reference);
                        break;
                    case 6:
                        divTmref6.Visible = true;
                        setReferenceTextBox(RefrenceControlTM6, reference);
                        break;

                }
                i++;
            }
        }
        private void fillTMReferences()
        {
            var FRNumber = ForecastingGridViewListControl.OrderNumber;
            var amndNo = ForecastingGridViewListControl.Amdno;
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                Key = TMOrdNo,
                filter1 = string.Empty,
                filter2 = TMStage,//TransactionStageControlId1.ActiveStage,
                FilterKey = Constants.FieldRequestReference
            };
            var txnReferences = _transactionManager.GetTxnReferences(queryArgument);
            bool flag = true;
            foreach (var txnref in txnReferences)
            {
                flag = true;
                while (flag)
                {
                    var txnPageRef = getReferenceTextBox(RefrenceControlTM1);
                    if (txnPageRef.RefCode.ToTrimString() == txnref.RefCode.ToTrimString())
                    {
                        if (RefrenceControlTM1.IsDateRequired)
                        {
                            string str = string.Empty;
                            if (txnref.Date != Convert.ToDateTime(Constants.DefaultDate))
                                str = Dates.FormatDate(txnref.Date, Constants.Format02);
                            setReferenceTextBox(RefrenceControlTM1, str);
                        }
                        else
                            setReferenceTextBox(RefrenceControlTM1, txnref.Text);
                        flag = false;
                        break;
                    }
                    txnPageRef = getReferenceTextBox(RefrenceControlTM2);
                    if (txnPageRef.RefCode.ToTrimString() == txnref.RefCode.ToTrimString())
                    {
                        if (RefrenceControlTM2.IsDateRequired)
                        {
                            string str = string.Empty;
                            if (txnref.Date != Convert.ToDateTime(Constants.DefaultDate))
                                str = Dates.FormatDate(txnref.Date, Constants.Format02);
                            setReferenceTextBox(RefrenceControlTM2, str);
                        }
                        else
                            setReferenceTextBox(RefrenceControlTM2, txnref.Text);
                        flag = false;
                        break;
                    }
                    txnPageRef = getReferenceTextBox(RefrenceControlTM3);
                    if (txnPageRef.RefCode.ToTrimString() == txnref.RefCode.ToTrimString())
                    {
                        if (RefrenceControlTM3.IsDateRequired)
                        {
                            string str = string.Empty;
                            if (txnref.Date != Convert.ToDateTime(Constants.DefaultDate))
                                str = Dates.FormatDate(txnref.Date, Constants.Format02);
                            setReferenceTextBox(RefrenceControlTM3, str);
                        }
                        else
                            setReferenceTextBox(RefrenceControlTM3, txnref.Text);
                        flag = false;
                        break;
                    }
                    txnPageRef = getReferenceTextBox(RefrenceControlTM4);
                    if (txnPageRef.RefCode.ToTrimString() == txnref.RefCode.ToTrimString())
                    {
                        if (RefrenceControlTM4.IsDateRequired)
                        {
                            string str = string.Empty;
                            if (txnref.Date != Convert.ToDateTime(Constants.DefaultDate))
                                str = Dates.FormatDate(txnref.Date, Constants.Format02);
                            setReferenceTextBox(RefrenceControlTM4, str);
                        }
                        else
                            setReferenceTextBox(RefrenceControlTM4, txnref.Text);
                        flag = false;
                        break;
                    }
                    txnPageRef = getReferenceTextBox(RefrenceControlTM5);
                    if (txnPageRef.RefCode.ToTrimString() == txnref.RefCode.ToTrimString())
                    {
                        if (RefrenceControlTM5.IsDateRequired)
                        {
                            string str = string.Empty;
                            if (txnref.Date != Convert.ToDateTime(Constants.DefaultDate))
                                str = Dates.FormatDate(txnref.Date, Constants.Format02);
                            setReferenceTextBox(RefrenceControlTM5, str);
                        }
                        else
                            setReferenceTextBox(RefrenceControlTM5, txnref.Text);
                        flag = false;
                        break;
                    }
                    txnPageRef = getReferenceTextBox(RefrenceControlTM6);
                    if (txnPageRef.RefCode.ToTrimString() == txnref.RefCode.ToTrimString())
                    {
                        if (RefrenceControlTM6.IsDateRequired)
                        {
                            string str = string.Empty;
                            if (txnref.Date != Convert.ToDateTime(Constants.DefaultDate))
                                str = Dates.FormatDate(txnref.Date, Constants.Format02);
                            setReferenceTextBox(RefrenceControlTM6, str);
                        }
                        else
                            setReferenceTextBox(RefrenceControlTM6, txnref.Text);
                        flag = false;
                        break;
                    }
                }
            }
        }
        private void setReferenceTextBox(RefrenceControl referenceControl, WFComponentSub reference)
        {
            referenceControl.ParameterLabel = reference.WFCDesp;
            referenceControl.IsDateRequired = reference.Relation1.ToTrimString() == Constants.TxnRefTypeDate.ToTrimString() ? true : false;
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

        private void setReferenceTextBox(RefrenceControl referenceControl, string str)
        {
            referenceControl.BindData(str);
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

        protected void ddlfilterOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
       

        protected void GVPartChange(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Width = new System.Web.UI.WebControls.Unit(3, UnitType.Percentage);
            e.Row.Cells[1].Width = new System.Web.UI.WebControls.Unit(10, UnitType.Percentage);
            e.Row.Cells[2].Width = new System.Web.UI.WebControls.Unit(10, UnitType.Percentage);
            e.Row.Cells[7].Width = new System.Web.UI.WebControls.Unit(5, UnitType.Percentage);
            e.Row.Cells[11].Width = new System.Web.UI.WebControls.Unit(5, UnitType.Percentage);
            e.Row.Cells[12].Width = new System.Web.UI.WebControls.Unit(5, UnitType.Percentage);
            e.Row.Cells[13].Width = new System.Web.UI.WebControls.Unit(5, UnitType.Percentage);
            e.Row.Cells[15].Width = new System.Web.UI.WebControls.Unit(5, UnitType.Percentage);
            e.Row.Cells[17].Width = new System.Web.UI.WebControls.Unit(7, UnitType.Percentage);
            e.Row.Cells[19].Width = new System.Web.UI.WebControls.Unit(10, UnitType.Percentage);
            e.Row.Cells[20].Width = new System.Web.UI.WebControls.Unit(10, UnitType.Percentage);
            e.Row.Cells[21].Width = new System.Web.UI.WebControls.Unit(12, UnitType.Percentage);
            e.Row.Cells[22].Width = new System.Web.UI.WebControls.Unit(15, UnitType.Percentage);
            //BillingPartControl1.setCellWidth(10, 1);
            //BillingPartControl1.setCellWidth(10, 2);
            //BillingPartControl1.setCellWidth(5, 7);
            //BillingPartControl1.setCellWidth(5, 11);
            //BillingPartControl1.setCellWidth(5, 12);
            //BillingPartControl1.setCellWidth(5, 13);
            //BillingPartControl1.setCellWidth(5, 15);
            //BillingPartControl1.setCellWidth(7, 17);
            //BillingPartControl1.setCellWidth(10, 19);
            //BillingPartControl1.setCellWidth(10, 20);
            //BillingPartControl1.setCellWidth(12, 21);
            //BillingPartControl1.setCellWidth(15, 22);
        }

        protected void lnkSaveTmData_Click(object sender, EventArgs e)
        {
            var billingTrackings = new BillTrackings();
            billingTrackings.Add(new BillTracking
            {
                OrdNumber = TMOrdNo,
                StageId = TMStage
            });

            var firstOrDefault = billingTrackings.FirstOrDefault();
            var references = getTmReferences();
            if (firstOrDefault != null)
                firstOrDefault.References = references;
            bool falge = true;
            foreach (var refe in references)
            {
                var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
                {
                    Key = TMOrdNo,
                    filter1 = string.Empty,
                    filter2 = TMStage,
                    filter3 = refe.RefCode,
                    filter4 = refe.Text,
                    filterDate = refe.Date,
                };
                falge = _transactionManager.SetTMData(queryArgument);
            }
            if (falge)
            {
                CustomMessageControl.MessageBodyText = GlobalCustomResource.TeamDataSaved;
                CustomMessageControl.MessageType = MessageTypes.Success;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "TM Data Saved",
                    GlobalCustomResource.TeamDataSaved, true);
                clearTmData();
            }
            else
            {
                if (StageType == Constants.TxnDebriefingType)
                    CustomMessageControl.MessageBodyText = GlobalCustomResource.TeamDataFailed;
                CustomMessageControl.MessageType = MessageTypes.Error;
                CustomMessageControl.ShowMessage();
                AuditLog.LogEvent(UserContext, SysEventType.INFO, "Team Data Update Failed",
                GlobalCustomResource.TeamDataFailed, true);
            }
        }
        public void clearTmData()
        {
            if (divTmref1.Visible)
                setReferenceTextBox(RefrenceControlTM1, string.Empty);
            if (divTmref2.Visible)
                setReferenceTextBox(RefrenceControlTM2, string.Empty);
            if (divTmref3.Visible)
                setReferenceTextBox(RefrenceControlTM3, string.Empty);
            if (divTmref4.Visible)
                setReferenceTextBox(RefrenceControlTM4, string.Empty);
            if (divTmref5.Visible)
                setReferenceTextBox(RefrenceControlTM5, string.Empty);
            if (divTmref6.Visible)
                setReferenceTextBox(RefrenceControlTM6, string.Empty);
            divTmReferences.Visible = false;
        }
        private TxnReferences getTmReferences()
        {
            var references = new TxnReferences();
            if (divTmref1.Visible)
                references.Add(getReferenceTextBox(RefrenceControlTM1));
            if (divTmref2.Visible)
                references.Add(getReferenceTextBox(RefrenceControlTM2));
            if (divTmref3.Visible)
                references.Add(getReferenceTextBox(RefrenceControlTM3));
            if (divTmref4.Visible)
                references.Add(getReferenceTextBox(RefrenceControlTM4));
            if (divTmref5.Visible)
                references.Add(getReferenceTextBox(RefrenceControlTM5));
            if (divTmref6.Visible)
                references.Add(getReferenceTextBox(RefrenceControlTM6));
            return references;
        }
        public string SR
        {
            get { return txtSRSearch.Text.ToTrimString(); }
            set { txtSRSearch.Text = value; }
        }
        protected void lnkViewDetails_Click(object sender, EventArgs e)
        {
           DivFilter = false;
           var lnkbtn = sender as LinkButton;
           if (lnkbtn == null) return;
           var closeLink = (Control)sender;
           GridViewRow row = (GridViewRow)closeLink.NamingContainer;
           SR = lnkbtn.CommandArgument;
           var hidOrderNo = row.FindControl("hidOrderNo") as HiddenField;
           var fcNumber = string.Empty;
           if (hidOrderNo != null)
               fcNumber = hidOrderNo.Value.ToTrimString();

           txtSRSearch.Text = SR;
           
           if (FilterOrder == Constants.PendingOrder || FilterOrder == Constants.PendingDeviationOrder || FilterOrder == Constants.FreshDeviationOrder)
           {
               var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
               {
                   Key = fcNumber,
                   filter1 = Constants.TxnBackOrderType + "," + Constants.TxnStockTransferType,
                   filter2 = "WP,EP,OH,AN,PR,SI",
                   filter3 = FilterOrder,//Constants.OrderingType + "," + Constants.AlternativePartNeeded,
                   filter4 = Constants.RetriveForm,
                   FilterKey = Constants.FlolloupPendingOrder
               };
               var ordDetails = _reportManager.GetPendingOrderDetailView(queryArgument);

               GVListData2.DataSource = ordDetails;
               GVListData2.DataBind();                             
           }
           else
           {
               var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
               {
                   filter5 = SR,
                   filter6 = FilterOrder == Constants.FreshOrder ? Constants.TRNInProcessOFF :
                                FilterOrder == Constants.DeviationOrder ? string.Format("{0},{1}", Constants.TRNInProcessOFF, Constants.TRNPendingOFF) :
                                FilterOrder == Constants.PendingDeviationOrder ? string.Format("{0},{1},{2}", Constants.TRNCompletedOFF, Constants.TRNInProcessOFF, Constants.TRNPendingOFF) :
                                string.Format("{0},{1}", Constants.TRNCompletedOFF, Constants.TRNInProcessOFF),

                   filter4 = FilterOrder == Constants.PendingOrder ? Constants.RetriveTempType :
                                FilterOrder == Constants.AlternativePartNeeded ? Constants.RetriveAlternativeView : "O",
                   FilterKey = Constants.TableOrderDetail
               };
               var ordDetails = _reportManager.GetOrderDetailViewData(queryArgument);

               GVListData2.DataSource = ordDetails;
               GVListData2.DataBind();
           }

           GVListData2.Columns[12].Visible = false;
           GVListData2.Columns[13].Visible = false;
           GVListData2.Columns[14].Visible = false;
           GVListData2.Columns[15].Visible = false;
           GVListData2.Columns[16].Visible = false;
           GVListData2.Columns[17].Visible = false;
           GVListData2.Columns[18].Visible = true;
           uplViewDetails.Update();
          
        }

        protected void GVListData2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].ToolTip = e.Row.Cells[i].Text;
            }
            var gv = sender as GridView;
            var lbl = e.Row.Cells[0].FindControl("lblRowIndex") as Label;
            if (gv != null && lbl != null)
                lbl.Text = ((e.Row.RowIndex + 1) + (gv.PageIndex * gv.PageSize)).ToString(CultureInfo.InvariantCulture);
        }
       
        protected void lnkViewBack_Click(object sender, EventArgs e)
        {
            DivFilter = true;
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            BindViewData();
        } 
      
        public void BindViewData()
        {
            var queryArgument = new QueryArgument(UserContext.DataBaseInfo)
            {
                filter5 = SR,
                filter4 = Constants.RetriveTempType,
                FilterKey = Constants.TableOrderDetail
            };
            var ordDetails = _reportManager.GetOrderDetailViewData(queryArgument);
            GVListData2.DataSource = ordDetails;
            GVListData2.DataBind();
            uplViewDetails.Update();
        }

        protected void lnkCancelTmData_Click(object sender, EventArgs e)
        {
            clearTmData();
        }
    }
}