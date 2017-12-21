<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForecastingPartControl.ascx.cs" Inherits="FBTS.View.UserControls.Common.ForecastingPartControl" %>

<style type="text/css">
    .table {
        table-layout:fixed;
    }

    .table td {
      white-space: nowrap;
      overflow: hidden;
      text-overflow: ellipsis;
    }
</style>
<script type="text/javascript">

    function jsValidatePart<%=this.ClientID %>(lnkbutton) {
        if ($("#<%=divPartText.ClientID%>").css('display') == "block") {
            if (!validateMandatoryField($('#<%=txtPart.ClientID %>'), "Please Enter Part Number !!")) return false;
            if (!validateMandatoryField($('#<%=ddlPartType.ClientID %>'), "Entered Part Number Is wrong please check once !!")) return false;
        }
        else {
            if (!validateMandatoryField($('#<%=ddlPartType.ClientID %>'), "Please Select Part Type !!")) return false;
            if (!validateMandatoryField($('#<%=ddlPart.ClientID %>'), "Please Select Part !!")) return false;
        }
        if (!validateMandatoryField($('#<%=txtQuantity.ClientID %>'), "Quantity Cannot be Blank !!")) return false;
        var qty = parseFloat(TrimAll($('#<%=txtQuantity.ClientID %>').val()));
        if (qty == 0)
        {
            showCustomMessage("Qty", "Qty Cannt be enter 0", "Error");
            return false;
        }
        if ($("#<%=divAlternativePartNeeded.ClientID%>").css('display') == "block") {
            if (!validateMandatoryField($('#<%=txtAlternativePartNeeded.ClientID %>'), "Alternative Part Cannot be Blank !!")) return false;
        }
        
        var statusdiv = "<%=divStatus.ClientID%>";
        if (document.getElementById(statusdiv) != null) {
            if (!validateGride<%=this.ClientID%>()) return false;  
        }
        var div = "<%=divBillLocation.ClientID%>";

        if (document.getElementById(div) != null) {
            if (!validateParts()) return false;
            if (!validateMandatoryField($('#<%=ddlBillLocation.ClientID %>'), "Please Select Billing Location!!")) return false;
            if (!validateMandatoryField($('#<%=ddlModality.ClientID %>'), "Please Enter Modality!!")) return false;

        }
        else {                     
            if (!validateMandatoryField($('#<%=ddlCurrentStatus.ClientID %>'), "Please Select Current Status !!")) return false;
            if (TrimAll($('#<%=ddlCurrentStatus.ClientID %>').val()) == 'OR')
                if (!validateMandatoryField($('#<%=txtLogisticOrderNo.ClientID %>'), "Please Enter Order# !!")) return false;
        }
       

        var save = "<%=lnkSave.ClientID%>";
        if (document.getElementById(save) != null) {
            return jsValidate();
        }
        return true;
    }

    function chequeQty<%=this.ClientID%>(id) {
        var qty = $('#<%=txtQuantity.ClientID %>');
        var remainqty = $('#<%=lblRemaingQrt.ClientID %>');
        if (parseFloat(qty[0].value) > parseFloat(remainqty[0].innerText)) {
            qty[0].value = "";
            showCustomMessage("Qty Greater", "Qty Cannt be greater than remaining Qty", "Error");
            return false;
        }
        return true;
    }

    function validateGrid<%=this.ClientID%>(id) {
        var gv = "<%=GVPart.ClientID%>";        
        if (document.getElementById(gv) == null) {
            showCustomMessage("GV Part", "Please Add Part", "Error");
            return false;
        }
        else
        {
            var TabLength;   
            TabLength = document.getElementById(gv).rows.length;
            if(TabLength==1)
            {
                showCustomMessage("GV Part", "Please Add Part", "Error");
                return false;
            }
        }
        return true;
    }
    
    function validateParts() {
        var part = TrimAll($('#<%=ddlPart.ClientID %>').val());
        var gv = "<%=GVPart.ClientID%>";
        var TabLength;
        var jvRowData;
        var jvString;

        if (document.getElementById(gv) != null) {
            TabLength = document.getElementById(gv).rows.length;
        }
        for (var Row = 1; Row < parseInt(TabLength) ; Row++) {
            jvRowData = document.getElementById(gv).rows[Row].cells[1];
            jvString = TrimAll(jvRowData.innerText);
            if (jvString == part) {
                showCustomMessage("Status", part+ " this part Already selected you cannot select again same Part!!", "Error");
                return false;
            }
        }
        return true;
    }

    function validateGride<%=this.ClientID%>()
    {
        
        var divremainqty = "<%=divRemaingQty.ClientID%>";
        if (document.getElementById(divremainqty) == null) {
            showCustomMessage("Qty", "No Remain Qty!!", "Error");
            return false;
        }
        var part = TrimAll($('#<%=ddlPart.ClientID %>').val());
        var status = TrimAll($('#<%=ddlCurrentStatus.ClientID %>').val());
        var gv = "<%=GVPart.ClientID%>";
        var TabLength;
        var jvRowData;
        var jvRowData1;
        var jvString;
        var jvString1;

        if (document.getElementById(gv) != null) {
            TabLength = document.getElementById(gv).rows.length;
        }
        for (var Row = 1; Row < parseInt(TabLength) ; Row++) {
            jvRowData = document.getElementById(gv).rows[Row].cells[1];
            jvString = TrimAll(jvRowData.innerText);
            jvRowData1 = document.getElementById(gv).rows[Row].cells[0].children;
            //  jvString1 = TrimAll(jvRowData1.innerText);       
            jvString1 = TrimAll(jvRowData1[6].value);
            if (jvString == part && jvString1 == status) {
                showCustomMessage("Status", status+ " Status Already selected you cannot select again same status!!", "Error");
                return false;
            }
        }
        return true;
    }

    function jsValidateSave<%=this.ClientID%>(id)
    {       
        if (!validateMandatoryField($('#<%=txtQuantity.ClientID %>'), "Quantity Cannot be Blank !!")) return false;
        if (!validateMandatoryField($('#<%=ddlCurrentStatus.ClientID %>'), "Please select Status !!")) return false;
        if ($("#<%=divAlternativePartNeeded.ClientID%>").css('display') == "block") {
            if (!validateMandatoryField($('#<%=txtAlternativePartNeeded.ClientID %>'), "Alternative Part Cannot be Blank !!")) return false;
        }
        if (TrimAll($('#<%=ddlCurrentStatus.ClientID %>').val()) == 'OR' ||
                    TrimAll($('#<%=ddlCurrentStatus.ClientID %>').val()) == 'BO' ||
                            TrimAll($('#<%=ddlCurrentStatus.ClientID %>').val()) == 'ST') {
            if (!validateMandatoryField($('#<%=txtLogisticOrderNo.ClientID %>'), "Please Enter Order# !!")) return false;
        }
        if (!jsValidateForm()) return false;
        return true;
    }
</script>
<section class="panel pl5 pr5">
    <div class="panel panel-default" id="divPanel" runat="server">
        <header class="panel-heading" runat="server" id="divheader"><b>Part Detail</b></header>
        <div class="panel-body ">
            <div class="row" id="divInput" runat="server">
                <div class="col-md-2">
                    <div class="form-group">
                        <label>Part Type</label>
                        <div>
                            <asp:DropDownList ID="ddlPartType" runat="server" CssClass="form-control chosen" AutoPostBack="true" OnSelectedIndexChanged="ddlPartType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                 <div class="col-md-2" style="display:none">
                    <div class="form-group">
                        <label>Part Type</label>
                        <div >
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control chosen">
                                </asp:DropDownList>
                        </div>
                    </div>
                </div>              
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Part</label>
                        <div runat="server" id="divPartText" style="display:none">
                                <asp:TextBox ID="txtPart" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPart_TextChanged" MaxLength="18"></asp:TextBox>
                            </div>
                        <div runat="server" id="divPartDdl">
                            <asp:DropDownList ID="ddlPart" runat="server" CssClass="form-control chosen" OnSelectedIndexChanged="ddlPart_SelectedIndexChanged">
                            </asp:DropDownList>                                                                                       
                        </div>
                    </div>
                    <div id="divAlternativePartNeeded" runat="server" style="display: none">
                        <div class="form-group">
                            <%--  <label>Alternative Part</label>--%>
                            <div>
                                <div runat="server" id="divAlternatibePartText">
                                    <asp:TextBox ID="txtAlternativePartNeeded" runat="server" CssClass="form-control" placeholder="Alternative Part Needed" MaxLength="18"
                                        AutoPostBack="true" OnTextChanged="txtAlternativePartNeeded_TextChanged"></asp:TextBox>
                                </div>
                                <div runat="server" id="divAlternativePartDdl" style="display: none">
                                    <asp:DropDownList ID="ddlAlternativePart" runat="server" CssClass="form-control chosen" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlAlternativePart_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hidAltPartType" />
                                    <asp:HiddenField runat="server" ID="hidAltPartCat" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-1" id="divOuterQty" runat="server">
                    <div class="col-md-5 form-group" runat="server" id="divRemaingQty" visible="false">
                        <label>FC Qty</label>
                        <div>
                            <asp:Label ID="lblRemaingQrt" runat="server" Text="" Font-Bold="True"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-12 form-group" runat="server" id="divQty">
                        <label>Quantity</label>
                        <div>
                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control text-right" onkeypress="return validentry('N',event);" placeholder="QTY" MaxLength="5"></asp:TextBox>
                        </div>
                    </div>
                </div>                
                <div class="col-md-3" id="divBillLocation" runat="server">
                    <div class="form-group">
                        <label>Billing Location</label>
                        <div>
                            <asp:DropDownList ID="ddlBillLocation" runat="server" CssClass="form-control chosen">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-2" id="divModality" runat="server">
                    <div class="form-group">
                        <label>Modality</label>
                        <div>
                            <asp:DropDownList ID="ddlModality" runat="server" CssClass="form-control chosen">
                            </asp:DropDownList>
                           <%-- <asp:TextBox ID="txtModality" runat="server" CssClass="form-control" placeholder="Modality" MaxLength="50"></asp:TextBox>--%>
                        </div>
                    </div>
                </div>
                <div class="col-md-2" id="divStatus" runat="server" visible="false">
                    <div class="form-group">
                        <label>Current Status</label>
                        <div>
                            <asp:DropDownList ID="ddlCurrentStatus" runat="server" CssClass="form-control chosen" OnSelectedIndexChanged="ddlCurrentStatus_SelectedIndexChanged">
                                <asp:ListItem Value="">Select Status</asp:ListItem>
                                <asp:ListItem Value="OR">Ordering</asp:ListItem>
                                <asp:ListItem Value="BO">Back Order</asp:ListItem>
                                <asp:ListItem Value="ST">Stock Transfer</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-2" id="divOrderNumber" runat="server" visible="false">
                    <div class="form-group">
                        <label>Order #</label>
                        <div>
                            <asp:TextBox ID="txtLogisticOrderNo" runat="server" CssClass="form-control" placeholder="Order#" MaxLength="10"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-md-1 pt25">
                    <div class="pull-right">
                        <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-primary btn-sm " Text="Login" Visible="false" OnClick="lnkSave_Click">
                                            <i class="icon-save mr5"></i> Save </asp:LinkButton>
                        <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-sm btn-success" OnClick="lnkAddNew_Click"><i class="icon-plus"></i>Add</asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="row"  id="divGV" runat="server">
                <div class="col-md-12">
                    <div class="no-more-tables">
                    <asp:HiddenField ID="hidActionName" runat="server" />
                    <asp:GridView ID="GVPart" runat="server" CssClass="table table-striped table-bordered table-hover"
                        FooterStyle="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                        EmptyDataText="No Data is Available" OnRowDataBound="GVPart_RowDataBound">
                        <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Sl#">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowIndex" runat="server" />
                                    <asp:HiddenField ID="hidAction" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidCategoryCode" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidBillLocation" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidSlNo" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidPartTypeCode" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidStatus" runat="server"></asp:HiddenField>
                                     <asp:HiddenField ID="hidModality" runat="server"></asp:HiddenField>
                                </ItemTemplate>
                                <ItemStyle Width="3%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PartDetail.PartNumber" HeaderText="Part #" ItemStyle-Width="10%" ItemStyle-CssClass="Shorter"></asp:BoundField>
                            <asp:BoundField DataField="PartDetail.Description" HeaderText="Description" ItemStyle-Width="30%"></asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n0}"></asp:BoundField>
                            <asp:BoundField DataField="MaterialGroup.Description" HeaderText="Part Type" ItemStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="MaterialType.Description" HeaderText="Categories" ItemStyle-Width="5%" ItemStyle-CssClass="Shorter"></asp:BoundField>
                            <asp:BoundField DataField="WarehouseTo.Description" HeaderText="Billing Location" ItemStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="ModalityDesp" HeaderText="Modality" ItemStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="CurrentStatusDesc" HeaderText="Status" ItemStyle-Width="7%" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="LogisticOrderNumber" HeaderText="Order#" ItemStyle-Width="10%" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="PartDetail.DetailedDescription" HeaderText="Alternative Part" ItemStyle-Width="5%" Visible="false"></asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-xs btn-danger mr5" OnClick="lnkDelete_Click" ToolTip="Delete"><i class="icon-trash"></i></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="2%"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                        </div>
                </div>
            </div>
        </div>
    </div>
</section>
