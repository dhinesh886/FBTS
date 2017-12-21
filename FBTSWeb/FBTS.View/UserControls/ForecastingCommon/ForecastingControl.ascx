<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForecastingControl.ascx.cs" Inherits="FBTS.View.UserControls.Common.ForecastingControl" %>

<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="uc1" TagName="CustomMessageControl" %>

<script>  
    function pageLoad() {
      
        $('.datepicker').datepicker({
            format: "dd/mm/yyyy"
        });
        $('.chosen').chosen();
        $('.chosen-container').css('width', '100%');
    }
    function jsValidateHeader<%=this.ClientID %>() {
        
        var lbl = TrimAll($('#<%=lblId.ClientID %>')[0].innerText);
        if (!validateMandatoryField($('#<%=txtId.ClientID %>'), lbl+" Cannot be Blank!!")) return false;
        if (!validateMandatoryField($('#<%=txtDate.ClientID %>'), "Date Cannot be Blank!!")) return false;
        if (!validateMandatoryField($('#<%=ddlRequestorLocation.ClientID %>'), "Please Select Requestor Location !!")) return false;
        
        var divCDType = $('#<%=divCDType.ClientID%>');
        if (divCDType.css('display') == 'block') {
            if (!validateMandatoryField($('#<%=ddlCDType.ClientID %>'), "Please Customer/Dealer Type !!")) return false;
        }
        if (!validateMandatoryField($('#<%=ddlCustomer.ClientID %>'), "Please Select Customer !!")) return false;              
        var divBillLoc = $('#<%=divBillLocation.ClientID%>');
        if (divBillLoc.css('display') == 'block') {
            if (!validateMandatoryField($('#<%=ddlBillLocation.ClientID %>'), "Please Select Bill Location !!")) return false;
        }
        var divPonumber = $('#<%=divPonumber.ClientID%>');
        if (divPonumber.css('display') == 'block' && TrimAll($('#<%=ddlCurrentStatus.ClientID %>').val()) == 'OR') {
            if (!validateMandatoryField($('#<%=txtPoNumber.ClientID %>'), "Please Enter PO Number !!")) return false;
        }
        return true;
    }
    function jsValidateStatus<%=this.ClientID %>() {    
        
        var divbillstatus = $('#<%=divBillStatus.ClientID%>');
        if (divbillstatus.css('display') == 'block') {
            if (TrimAll($('#<%=ddlBillStatus.ClientID %>').val()) == 'DV') {
                showCustomMessage("Validation", "Please Check Bill Status", "Error");
                return false;
            }
            if (TrimAll($('#<%=ddlBillStatus.ClientID %>').val()) == '') {
                var divbillstatus = $('#lblbillstatus');
                showCustomMessage("Validation", "Please Check " + divbillstatus[0].innerText, "Error");
                return false;
            }
        }
        var divcurrentStatus = $('#<%=divCurrentStatus.ClientID%>');
        if (divcurrentStatus.css('display') == 'block') {
            if (TrimAll($('#<%=ddlCurrentStatus.ClientID %>').val()) == 'DV') {
                showCustomMessage("Validation", "Please Check Current Status", "Error");
                return false;
            }
        }
        return true;
    }
</script>
    <section class="panel pt10 pl5 pr5"> 
        <div class="panel panel-default ">
            <header class="panel-heading"><b>Basic Detail</b></header>
            <uc1:CustomMessageControl runat="server" id="CustomMessageControl" />
            <div class="panel-body ">               
                <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                            <label runat="server" id="lblId">FR#</label>
                            <div>
                                <asp:HiddenField ID="hidAmdno" runat="server" />
                                <asp:HiddenField ID="hidProcessingDate" runat="server" />
                                <asp:TextBox ID="txtId" runat="server" CssClass="form-control" placeholder="FR #" MaxLength="12" Enabled="false" onkeypress="return validentry('P',event);" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2" style="display:none" runat="server" id="divSubId">
                        <div class="form-group">
                            <label runat="server" id="lblSubId">Related SR</label>
                            <div>
                                <asp:TextBox ID="txtSubId" runat="server" CssClass="form-control" placeholder="Related SR" MaxLength="12" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label runat="server" id="lblDate">Date</label>
                            <div>
                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control datepicker" placeholder="Date" Enabled="false"></asp:TextBox>                                 
                            </div>
                        </div>
                    </div>  
                    <div class="col-md-3" runat="server" id="divLocation">
                        <div class="form-group">
                            <label>Requestor Location</label>
                            <div>
                                <asp:DropDownList ID="ddlRequestorLocation" runat="server" CssClass="form-control chosen" OnSelectedIndexChanged="ddlRequestorLocation_SelectedIndexChanged">
                                </asp:DropDownList> 
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2" runat="server" id="divCDType" style="display:none">
                        <div class="from-group">
                            <label>Customer/Dealer Type</label>
                            <div>
                                <asp:DropDownList ID="ddlCDType" runat="server" CssClass="form-control chosen" AutoPostBack="true" OnSelectedIndexChanged="ddlCDType_SelectedIndexChanged">  
                                    <asp:ListItem Value="">Select Dealer/Customer</asp:ListItem>
                                    <asp:ListItem Value="AE">Customer</asp:ListItem>
                                    <asp:ListItem Value="AI">Dealer</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>  
                    <div class="col-md-5" runat="server" id="divCustomer">
                        <div class="form-group">
                            <label>Dealer/Customer Name</label>
                            <div>
                                <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control chosen" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>                   
                </div> 
                <div class="row" runat="server" id="divBillDetail">                   
                    <div class="col-md-3" runat="server" id="divCurrentStatus" style="display:none">
                        <div class="from-group">
                            <label>Current Status</label>
                            <div>
                                <asp:DropDownList ID="ddlCurrentStatus" runat="server" CssClass="form-control chosen" OnSelectedIndexChanged="ddlCurrentStatus_SelectedIndexChanged">                                                              
                                    <asp:ListItem Value="DV">Deviation</asp:ListItem>
                                    <asp:ListItem Value="OR">Ordering</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3" runat="server" id="divBillStatus" style="display:none">
                        <div class="from-group">
                            <label id="lblbillstatus">Bill Status</label>
                            <div>
                                <asp:DropDownList ID="ddlBillStatus" runat="server" CssClass="form-control chosen">  
                                    <asp:ListItem Value="DV">Deviation</asp:ListItem>
                                    <asp:ListItem Value="OR">Ordering</asp:ListItem>
                                     <asp:ListItem Value="DO">Deviation Order</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>                    
                    <div class="col-md-4"  runat="server" id="divBillLocation" style="display:none">
                        <div class="from-group">
                            <label>Bill Location</label>
                            <div>
                                <asp:DropDownList ID="ddlBillLocation" runat="server" CssClass="form-control chosen">  
                                    </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2" runat="server" id="divPonumber" style="display:none">
                        <div class="form-group">
                            <label runat="server" id="lblPONo">PO Number</label>
                            <div>
                                <asp:TextBox ID="txtPoNumber" runat="server" CssClass="form-control" placeholder="PO Number" MaxLength="30"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2" runat="server" id="divGST" style="display:none">
                        <div class="form-group">
                            <label runat="server" id="lblGST">GST#</label>
                            <div>
                                <label runat="server" id="lblGSTValue"></label>
                            </div>                            
                        </div>
                    </div>    
                </div>              
            </div>
        </div>      
    </section> 