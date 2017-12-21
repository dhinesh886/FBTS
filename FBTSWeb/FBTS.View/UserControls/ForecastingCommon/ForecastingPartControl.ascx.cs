using FBTS.Business.Manager;
using FBTS.Library.Common;
using FBTS.Library.Statemanagement;
using FBTS.Model.Common;
using FBTS.Model.Control;
using FBTS.Model.Transaction;
using FBTS.Model.Transaction.Transactions;
using FBTS.View.Resources.ResourceFiles;
using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FBTS.View.UserControls.Common
{
    public partial class ForecastingPartControl : System.Web.UI.UserControl
    {
        private readonly GenericManager _genericClass = new GenericManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            lnkAddNew.Attributes.Add("onclick", "return jsValidatePart" + this.ClientID + "(this);");
            lnkSave.Attributes.Add("onclick", "return jsValidateSave" + this.ClientID + "(this);");
            if (IsPostBack) return;            
            var filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter4, Constants.DdlFilterTypes),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextMaterialType),
                            new KeyValuePairItem(Constants.masterType, Constants.DdlMATCategories)
                        };
            _genericClass.LoadDropDown(ddlCategory, filter, null, UserContext.DataBaseInfo);
             filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter4, Constants.DdlFilterGroups),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextMaterialType),
                            new KeyValuePairItem(Constants.masterType, Constants.DdlMATCategories)
                        };
            _genericClass.LoadDropDown(ddlPartType, filter, null, UserContext.DataBaseInfo);
            filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.WHCType),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextLocation),
                            new KeyValuePairItem(Constants.masterType,Constants.TableAccounts)
                        };
            _genericClass.LoadDropDown(ddlBillLocation, filter, null, UserContext.DataBaseInfo);
            filter = new KeyValuePairItems
                        {
                            new KeyValuePairItem(Constants.filter1, Constants.PartType),
                            new KeyValuePairItem(Constants.filter1, Constants.DvMode),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextModality),
                            new KeyValuePairItem(Constants.masterType,Constants.DdlCatHeaderData)
                        };
            _genericClass.LoadDropDown(ddlModality, filter, null, UserContext.DataBaseInfo);
        }
        #region Genaral
        public UserContext UserContext
        {
            get { return SessionManagement<UserContext>.GetValue(Constants.UserContextSessionKey); }
        }
        #endregion
        #region Properti
        public string PartNumber
        {
            get { return ddlPart.SelectedValue.ToTrimString(); }
            set { WebControls.SetCurrentComboIndex(ddlPart, value); }
        }
        public string PartDesp
        {
            get
            {
                string[] arr = ddlPart.SelectedItem.Text.Trim().Split('~');
                if (arr.Length > 1)
                    return arr[1].Trim();
                return arr[0].Trim();
            }
        }
        public string CategoryCode
        {
            get { return ddlCategory.SelectedValue.Trim(); }
            set { ddlCategory.SelectedValue = value.Trim(); }
        }
        public string CategoryDesp
        {
            get
            {
                return ddlCategory.SelectedItem.Text.Trim();
            }
        }
        public string PartType
        {
            get { return ddlPartType.SelectedValue.Trim(); }
            set { ddlPartType.SelectedValue = value.Trim(); }
        }
        public string PartTypeDesp
        {
            get { return ddlPartType.SelectedItem.Text.Trim(); }
        }
       
        public decimal Quantity
        {
            get { return txtQuantity.Text.Trim().ToDecimal(0); }
            set
            {
                if (value == 0)
                    txtQuantity.Text = string.Empty;
                else
                    txtQuantity.Text = value.ToString("0");
            }
        }       
        public string BillingLocationCode
        {
            get { return ddlBillLocation.SelectedValue.Trim(); }
            set { ddlBillLocation.SelectedValue = value.Trim(); }
        }
        public string BillingLocationDesp
        {
            get { return ddlBillLocation.SelectedItem.Text.Trim(); }
        }
        public string Modality
        {
            get { return ddlModality.SelectedValue.Trim(); }
            set { ddlModality.SelectedValue = value.Trim(); }
        }
        public string ModalityDesp
        {
            get { return ddlModality.SelectedItem.Text.Trim(); }
        }
        public string CurrentStatus
        {
            get { return ddlCurrentStatus.SelectedValue.Trim(); }
            set { ddlCurrentStatus.SelectedValue = value.Trim(); }
        }
        public string LogistiOrderNumber
        {
            get { return txtLogisticOrderNo.Text.Trim(); }
            set { txtLogisticOrderNo.Text = value.Trim(); }
        }
        public string changeActionName
        {
            get { return hidActionName.Value.Trim(); }
            set { hidActionName.Value = value.Trim(); }
        }
        public string AlternativePart
        {
            get { return txtAlternativePartNeeded.Text.ToTrimString(); }
            set { txtAlternativePartNeeded.Text = value.Trim(); }
        }
        public decimal RemaingQty
        {
            get { return lblRemaingQrt.Text.ToDecimal(2); }
            set
            {
                lblRemaingQrt.Text = value.ToString(Constants.DecimalFormate);
                if (value <= 0)
                {
                    divRemaingQty.Visible = false;
                    divQty.Attributes.Add("class", "col-md-12 form-group");
                }
                else
                {
                    divRemaingQty.Visible = true;
                    divQty.Attributes.Add("class", "col-md-7 form-group");
                }
            }
        }
        public string AltPartNumber
        {
            get { return ddlAlternativePart.SelectedValue.ToTrimString(); }
            set
            {
                WebControls.SetCurrentComboIndex(ddlAlternativePart, value);
            }
        }
        public string AltPartType
        {
            get { return hidAltPartType.Value.ToTrimString(); }
            set { hidAltPartType.Value = value; }
        }
        public string AltPartCat
        {
            get { return hidAltPartCat.Value.ToTrimString(); }
            set { hidAltPartCat.Value = value; }
        }
        #endregion
        #region Visibality
        public bool StatusOrderNoDivTrue
        {
            get { return divStatus.Visible; }
            set
            {
                divStatus.Visible = value;
                divOrderNumber.Visible = value;
                divBillLocation.Visible = !value;
                divModality.Visible = !value;

                divOuterQty.Attributes.Add("class", "col-md-2");
                GVPart.Columns[8].Visible = value;
               // GVPart.Columns[9].Visible = value;

            }
        }       
        public bool InputDivFalse
        {
            set { divInput.Visible = value; }
        }
        public bool divGVvisiable
        {
            set { divGV.Visible = value; }
        }
        public bool setVisiablelnkSave
        {
            set { lnkSave.Visible = value; }
        }
        public bool setVisiablelnkAddNew
        {
            set { lnkAddNew.Visible = value; }
        }
        public bool IsHeader
        {
            set { divheader.Visible = value; }
        }
        public bool IsVisiablePartText
        {
            set
            {
                if (value)
                {
                    divPartText.Style.Add("display", "block");
                    divPartDdl.Style.Add("display", "none");
                }
                else
                {
                    divPartText.Style.Add("display", "none");
                    divPartDdl.Style.Add("display", "block");
                }
            }
        }
        public bool IsVisiableAlternativePartText
        {
            set
            {
                if (value)
                {
                    divAlternativePartNeeded.Style.Add("display", "block");
                }
                else
                {
                    divAlternativePartNeeded.Style.Add("display", "none");
                }
            }
        }
        public bool IsVisiableAlternativePartDdlDiv
        {
            set
            {
                if (value)
                {
                    divAlternativePartDdl.Style.Add("display", "block");
                    divAlternatibePartText.Style.Add("display", "none");
                }
                else
                {
                    divAlternativePartDdl.Style.Add("display", "none");
                    divAlternatibePartText.Style.Add("display", "block");
                }
            }
        }
        public bool checkIsVisiableCell(int cellIndex)
        {
            return GVPart.Columns[cellIndex].Visible;
        }
        public void IsVisiableCell(int cellIndex, bool value)
        {
            GVPart.Columns[cellIndex].Visible = value;
        }
        #endregion
        #region Enabled
        public bool IsEnablePartType
        {
            set
            {
                ddlPartType.Enabled = value;
                if (value)
                    ddlPartType.CssClass = "form-control chosen";
                else
                    ddlPartType.CssClass = "form-control";
            }
        }
        public bool setEnablePart
        {
            set { 
                ddlPart.Enabled = value;
                if (value)
                    ddlPart.CssClass = "form-control chosen";
                else
                    ddlPart.CssClass = "form-control";
            }
        }
        public bool setEnableCurrentStatus
        {
            set { 
                ddlCurrentStatus.Enabled = value;
                if (value)
                    ddlCurrentStatus.CssClass = "form-control chosen";
                else
                    ddlCurrentStatus.CssClass = "form-control";
            }
        }
        public bool setEnableLogisticNumber
        {
            set { txtLogisticOrderNo.Enabled = value; }
        }
        #endregion
        #region Compont
        public bool IsPanelDiv
        {
            set {
                if (value)
                    divPanel.Attributes.Add("class", "panel panel-default");
                else
                    divPanel.Attributes.Add("class", "");
            }
        }
        public Boolean setPartAutopostBack
        {
            get { return ddlPart.AutoPostBack; }
            set
            {
                ddlPart.AutoPostBack = value;
            }
        }
        public TextBox txtqty
        {
            get { return txtQuantity; }
        }
        public void addKeyupText(TextBox txt, string eventStr)
        {
            txt.Attributes.Add(eventStr, "return chequeQty" + this.ClientID + "(this);");
        }
        public DropDownList DropdownStatus
        {
            get { return ddlCurrentStatus; }
        }
        public Boolean SetCurrentStatusAutopostBack
        {
            get { return ddlCurrentStatus.AutoPostBack; }
            set
            {
                ddlCurrentStatus.AutoPostBack = value;
            }
        }
        #endregion
        
        public OrderDetails OrderDetailData
        {
            get
            {
                if (ViewState["OrderDetailData"] == null) return null;
                return XmlUtilities.ToObject<OrderDetails>(ViewState["OrderDetailData"].ToString());
            }
            set
            {
                ViewState["OrderDetailData"] = value.ToXml();                
            }
        }
        public void fillDdl(DropDownList ddl,KeyValuePairItems datasource)
        {
            KeyValuePairItems dt;
            if (datasource == null) return;

            dt = datasource;            
            WebControls.ClearDdl(ddl, Constants.DdlDefaultTextPartNo);
            foreach (var dr in dt)
            {
                ddl.Items.Add(new ListItem(dr.Key, dr.Value));
            }
        }
        public OrderDetails GVPartData
        {
            get
            {
                OrderDetails orderDetails = new OrderDetails();
                foreach (GridViewRow gvRow in GVPart.Rows)
                {
                    var CurrentStatus = gvRow.Cells.Count > 8 ? ((HiddenField)gvRow.FindControl("hidStatus")).Value.Trim() : string.Empty;
                    orderDetails.Add(new OrderDetail
                    {
                        SlNo = ((Label)gvRow.FindControl("lblRowIndex")).Text.Trim().ToInt() < 10 ? "0000" + ((Label)gvRow.FindControl("lblRowIndex")).Text.Trim() :
                              ((Label)gvRow.FindControl("lblRowIndex")).Text.Trim().ToInt() < 100 ? "000" + ((Label)gvRow.FindControl("lblRowIndex")).Text.Trim() :
                              ((Label)gvRow.FindControl("lblRowIndex")).Text.Trim().ToInt() < 1000 ? "00" + ((Label)gvRow.FindControl("lblRowIndex")).Text.Trim() :
                              ((Label)gvRow.FindControl("lblRowIndex")).Text.Trim().ToInt() < 10000 ? "0" + ((Label)gvRow.FindControl("lblRowIndex")).Text.Trim() :
                                                                                        ((Label)gvRow.FindControl("lblRowIndex")).Text.Trim(),
                        PartDetail = new Material
                        {
                            PartNumber = gvRow.Cells[1].Text.Trim().Replace(Constants.SpaceFromHTML, string.Empty),
                            Description = gvRow.Cells[2].Text.Trim().Replace(Constants.SpaceFromHTML, string.Empty).Replace(Constants.DoubleQuetsHTML, Constants.DoubleQuets),
                            DetailedDescription = checkIsVisiableCell(10) ? gvRow.Cells[10].Text.Trim().Replace("&nbsp;", string.Empty) : string.Empty,
                        },
                        Quantity = gvRow.Cells[3].Text.Trim().ToDecimal(2),
                        MaterialGroup = new MaterialGroup
                        {
                            Id = ((HiddenField)gvRow.FindControl("hidPartTypeCode")).Value.Trim().Replace(Constants.SpaceFromHTML, string.Empty),
                            Description = gvRow.Cells[4].Text.Trim().Replace(Constants.SpaceFromHTML, string.Empty)
                        },
                        MaterialType = new MaterialType
                        {
                            Id = ((HiddenField)gvRow.FindControl("hidCategoryCode")).Value.Trim().Replace(Constants.SpaceFromHTML, string.Empty),
                            Description = gvRow.Cells[5].Text.Trim().Replace(Constants.SpaceFromHTML, string.Empty)
                        },
                        WarehouseTo = new Location
                        {
                            Id = ((HiddenField)gvRow.FindControl("hidBillLocation")).Value.Trim().Replace(Constants.SpaceFromHTML, string.Empty),
                            Description = gvRow.Cells[6].Text.Trim().Replace(Constants.SpaceFromHTML, string.Empty),
                        },
                        Modality = ((HiddenField)gvRow.FindControl("hidModality")).Value.Trim().Replace(Constants.SpaceFromHTML, string.Empty),
                        ModalityDesp = gvRow.Cells[7].Text.Trim().Replace(Constants.SpaceFromHTML, string.Empty),
                        CurrentStatus = gvRow.Cells.Count > 8 ? ((HiddenField)gvRow.FindControl("hidStatus")).Value.Trim().Replace(Constants.SpaceFromHTML, string.Empty) : string.Empty,
                        LogisticOrderNumber = gvRow.Cells.Count > 9 ? gvRow.Cells[9].Text.Trim().Replace(Constants.SpaceFromHTML, string.Empty) : string.Empty,
                        SQuantity = CurrentStatus == Constants.TxnOrderType ? gvRow.Cells[3].Text.Trim().ToDecimal(2) : 0,
                        Off = CurrentStatus == Constants.TxnOrderType ? Constants.TRNInProcessOFF :
                                CurrentStatus == Constants.TxnBackOrderType || CurrentStatus == Constants.TxnStockTransferType ? Constants.TRNLogedOFF
                                                                        : Constants.TRNLogedOFF,
                        
                        // Off = CurrentStatus == Constants.TxnOrderType ? Constants.TRNCompletedOFF :
                        //Off = CurrentStatus == Constants.TxnOrderType || CurrentStatus == Constants.TxnBackOrderType || CurrentStatus == Constants.TxnStockTransferType ? Constants.TRNInProcessOFF
                        //                                                : Constants.TRNLogedOFF,
                    });
                }
                return orderDetails;
            }
            set
            {
                GVPart.DataSource = value.ToList();
                GVPart.DataBind();
            }
        }
        public Control PartControl
        {
            get { return ddlPart; }
        }
        public void clearForm()
        {
            PartType = string.Empty;
            PartNumber = string.Empty;
            CategoryCode = string.Empty;
            Quantity = 0;
            BillingLocationCode = string.Empty;
            Modality = string.Empty;
            CurrentStatus = string.Empty;
            LogistiOrderNumber = string.Empty;
            RemaingQty = 0;
            AlternativePart = string.Empty;
            IsVisiableAlternativePartText = false;
            txtPart.Text = string.Empty;
            IsVisiableAlternativePartDdlDiv = false;
        }
        public delegate void onclick(object sender, EventArgs e);
        public event onclick addclick;
        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            var orderDetails = GVPartData;

            orderDetails.Add(new OrderDetail
            {
                PartDetail = new Material
                {
                    PartNumber = PartNumber,
                    Description = PartDesp,
                    DetailedDescription = AlternativePart
                },
                MaterialGroup = new MaterialGroup
                {
                    Id = PartType,
                    Description = PartTypeDesp
                },
                MaterialType = new MaterialType
                {
                    Id = CategoryCode,
                    Description = CategoryDesp
                },
                Quantity = Quantity,
                WarehouseTo = new Location
                {
                    Id = BillingLocationCode,
                    Description = BillingLocationDesp,
                },
                ModalityDesp = ModalityDesp,
                Modality = Modality,
                CurrentStatus = CurrentStatus,
                LogisticOrderNumber = LogistiOrderNumber
            });
            GVPartData = orderDetails;
           
            if (addclick != null)
                addclick(sender, e);            
        }

        protected void GVPart_RowDataBound(object sender, GridViewRowEventArgs e)
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

            var orderDetail = e.Row.DataItem as OrderDetail;
            if (orderDetail == null) return;
            var hidPartTypeCode = e.Row.Cells[0].FindControl("hidPartTypeCode") as HiddenField;
            if (hidPartTypeCode != null)
                hidPartTypeCode.Value = orderDetail.MaterialGroup.Id.Trim();

            var hidCategoryCode = e.Row.Cells[0].FindControl("hidCategoryCode") as HiddenField;
            if (hidCategoryCode != null)
                hidCategoryCode.Value = orderDetail.MaterialType.Id.Trim();

            var hidBillLocation = e.Row.Cells[0].FindControl("hidBillLocation") as HiddenField;
            if (hidBillLocation != null)
                hidBillLocation.Value = orderDetail.WarehouseTo.Id.Trim();
            var hidSlNo = e.Row.Cells[0].FindControl("hidSlNo") as HiddenField;
            if (hidSlNo != null)
                hidSlNo.Value = orderDetail.SlNo;
            var hidStatus = e.Row.Cells[0].FindControl("hidStatus") as HiddenField;
            if (hidStatus != null)
                hidStatus.Value = orderDetail.CurrentStatus;

            var hidModality = e.Row.Cells[0].FindControl("hidModality") as HiddenField;
            if (hidModality != null)
                hidModality.Value = orderDetail.Modality;
            
            var lnkaction = e.Row.Cells[0].FindControl("lnkDelete") as LinkButton;
            if (lnkaction != null)
            {
                if (changeActionName == Constants.UpdateAction)
                {
                    //lnkaction.Text = "Edit";
                    lnkaction.ToolTip = "Edit";
                    lnkaction.CssClass = "btn btn-xs btn-success";
                    lnkaction.Text = "<i class=" + @"""icon-edit""" + "></i>";
                }
                else if (changeActionName == Constants.InsertAction)
                {
                 //   lnkaction.Text = "Add Row";
                    lnkaction.ToolTip = "Add Row";
                    lnkaction.CssClass = "btn btn-xs btn-success";
                }
            }                
        }
        public delegate void OnSelectedIndexChanged(object sender, EventArgs e);
        public event OnSelectedIndexChanged ddlPartselectedIndexchanged;
        protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPartselectedIndexchanged != null)
                ddlPartselectedIndexchanged(sender, e);
            Quantity = 0;            
        }
        public void addSqty()
        {
            if (PartNumber == string.Empty) { clearForm(); return; }
            var orderDetailData = OrderDetailData;
            var orderDetail = orderDetailData.Where(x => x.PartDetail.PartNumber.Trim() == PartNumber).FirstOrDefault();
            orderDetail.SQuantity = orderDetail.SQuantity + Quantity;
            OrderDetailData = orderDetailData;
        }
        public void RetriveDatabasedonPart()
        {
            if (PartNumber == string.Empty) { clearForm(); return; }
            var orderDetail = OrderDetailData.Where(x => x.PartDetail.PartNumber.Trim() == PartNumber).FirstOrDefault();           
            if (orderDetail == null) return;
            CategoryCode = orderDetail.MaterialType.Id;
            PartType = orderDetail.MaterialGroup.Id;
            BillingLocationCode = orderDetail.WarehouseTo.Id;
            Modality = orderDetail.Modality;
            RemaingQty = orderDetail.Quantity - orderDetail.SQuantity;
        }

        public void changepartddlIndex()
        {
            var selectedIndex = ddlPart.SelectedIndex;
            if (ddlPart.Items.Count > selectedIndex + 1)
                ddlPart.SelectedIndex = selectedIndex + 1;
        }
       
        public event onclick DeleteEdit;
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            var lnkbtn = sender as LinkButton;
            if (lnkbtn == null) return;
            if (changeActionName == string.Empty)
            {               
                var closeLink = (Control)sender;
                GridViewRow row = (GridViewRow)closeLink.NamingContainer;
                var slno = ((Label)row.FindControl("lblRowIndex")).Text.Trim().ToInt();
                var partNo = row.Cells[1].Text.Trim();
                var qty = row.Cells[3].Text.Trim().ToDecimal(0);

                var orderDetails=new OrderDetails();
                foreach(var part in GVPartData )
                {
                    if (part.SlNo.Trim().ToInt() == slno)
                        minSqty(partNo, qty);
                    else
                        orderDetails.Add(part);                        
                }

                GVPartData = orderDetails;
            }
            else
            {
                if (DeleteEdit == null) return;
                DeleteEdit(sender, e);
            }
            clearForm();
        }
        public void minSqty(string partNo,decimal qty)
        {
            if (partNo == string.Empty) return;
            if (OrderDetailData == null) return;
            var orderDetailData = OrderDetailData;            
            var orderDetail = orderDetailData.Where(x => x.PartDetail.PartNumber.Trim() == partNo).FirstOrDefault();
            orderDetail.SQuantity = orderDetail.SQuantity - qty;
            OrderDetailData = orderDetailData;
        }
        public event onclick Save;
        protected void lnkSave_Click(object sender, EventArgs e)
        {
            if (Save == null) return;
            Save(sender, e);
        }

        protected void ddlPartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PartType == string.Empty)
            {
                ddlPart.Items.Clear();
                return;
            }
            var filter = new KeyValuePairItems
                        {                            
                             new KeyValuePairItem(Constants.filter3, PartType),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextPartNo),
                            new KeyValuePairItem(Constants.masterType,Constants.TableMaterials)
                        };
           // _genericClass.LoadDropDown(ddlPart, filter, null, UserContext.DataBaseInfo);
            IsVisiablePartText = _genericClass.LoadDropDownIfMorereturnFalse(ddlPart, filter, null, UserContext.DataBaseInfo);
        }

        public event OnSelectedIndexChanged onselectCurrentStatus;
        protected void ddlCurrentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (onselectCurrentStatus == null) return;
            onselectCurrentStatus(sender, e);
        }

        public delegate void OnTextChanged(object sender, EventArgs e);
        public event OnTextChanged txtPartTextChange;
        protected void txtPart_TextChanged(object sender, EventArgs e)
        {
            if (txtPart.Text.Trim() == string.Empty)
                ddlPart.Items.Clear();
            else
            {
                var filter = new KeyValuePairItems
                        {                            
                            new KeyValuePairItem(Constants.filter3, PartType),
                            new KeyValuePairItem(Constants.filter4, txtPart.Text.Trim()),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextPartNo),
                            new KeyValuePairItem(Constants.masterType,Constants.TableMaterials)
                        };
                //_genericClass.LoadDropDown(ddlPart, filter, null, UserContext.DataBaseInfo);
                _genericClass.LoadDropDownIfMorereturnFalse(ddlPart, filter, null, UserContext.DataBaseInfo);
                if (ddlPart.Items.Count > 1)
                    ddlPart.SelectedIndex = 1;
            }
            if (txtPartTextChange != null)
                txtPartTextChange(sender, e);
            //txtPart.Text = string.Empty;
        }
        public event OnTextChanged txtAltPartTextChange;
        protected void txtAlternativePartNeeded_TextChanged(object sender, EventArgs e)
        {
            if (AlternativePart == string.Empty)
                ddlAlternativePart.Items.Clear();
            else
            {
                if (AlternativePart == PartNumber)
                {
                    var CustomMessageControl = Parent.FindControl("CustomMessageControl") as CustomMessageControl;
                    CustomMessageControl.MessageBodyText = GlobalCustomResource.AlternativePartValidation;
                    CustomMessageControl.MessageType = MessageTypes.Warning;
                    CustomMessageControl.ShowMessage();
                    AlternativePart = string.Empty;
                    return;
                }
                var filter = new KeyValuePairItems
                        {                            
                           // new KeyValuePairItem(Constants.filter3, PartType),
                            new KeyValuePairItem(Constants.filter4, AlternativePart),
                            new KeyValuePairItem(Constants.DdldefaultText, Constants.DdlDefaultTextPartNo),
                            new KeyValuePairItem(Constants.masterType,Constants.TableMaterials)
                        };
                //_genericClass.LoadDropDown(ddlPart, filter, null, UserContext.DataBaseInfo);
                var more = _genericClass.LoadDropDownIfMorereturnFalse(ddlAlternativePart, filter, null, UserContext.DataBaseInfo);
                if (more)
                {
                    var customMessageControl = (CustomMessageControl)Parent.FindControl("CustomMessageControl");
                    if (customMessageControl == null)
                        return;
                    customMessageControl.MessageBodyText = GlobalCustomResource.PartDdlFillValFaildMoreNoofRow;
                    customMessageControl.MessageType = MessageTypes.Error;
                    customMessageControl.ShowMessage();
                    return;
                }
                if (ddlAlternativePart.Items.Count > 1)
                {
                    AlternativePart = string.Empty;
                    IsVisiableAlternativePartDdlDiv = true;
                    ddlAlternativePart.SelectedIndex = 1;
                    AlternativePart = AltPartNumber;
                    if (txtAltPartTextChange != null)
                        txtAltPartTextChange(sender, e);
                }
                else
                {
                    var customMessageControl = (CustomMessageControl)Parent.FindControl("CustomMessageControl");
                    if (customMessageControl == null)
                        return;
                    customMessageControl.MessageBodyText = GlobalCustomResource.PartNotFoundFaild;
                    customMessageControl.MessageType = MessageTypes.Error;
                    customMessageControl.ShowMessage();
                }
            }
        }

        public event OnSelectedIndexChanged ddlAltPartselectedIndexchanged;
        protected void ddlAlternativePart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AltPartNumber))
            {
                IsVisiableAlternativePartDdlDiv = false;
                return;
            }
            if (ddlAltPartselectedIndexchanged != null)
                ddlAltPartselectedIndexchanged(sender, e);
            AlternativePart = AltPartNumber;
        }
    }
}