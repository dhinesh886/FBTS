<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidationDdl.ascx.cs" Inherits="FBTS.View.UserControls.ForecastingCommon.ValidationDdl" %>
<script>
    function jsValidate<%=this.ClientID%>(id) {

        var lbl = $('#<%=lblPONumber.ClientID%>')
        var poNumber = TrimAll($('#<%=ddlPONumber.ClientID %>').val());
        if (poNumber == "Not Ok") {
            showCustomMessage("Validation", "Please Check " + lbl[0].innerText, "Error");
            return false;
        }
        var lbl = $('#<%=lblValidity.ClientID%>')
        var poNumber = TrimAll($('#<%=ddlValidity.ClientID %>').val());
        if (poNumber == "Not Ok") {
            showCustomMessage("Validation", "Please Check " + lbl[0].innerText, "Error");
            return false;
        }
        var lbl = $('#<%=lblBillToAddress.ClientID%>')
        var poNumber = TrimAll($('#<%=ddlBillToAddress.ClientID %>').val());
        if (poNumber == "Not Ok") {
            showCustomMessage("Validation", "Please Check " + lbl[0].innerText, "Error");
            return false;
        }
        var lbl = $('#<%=lblShipToAddress.ClientID%>')
        var poNumber = TrimAll($('#<%=ddlShipToAddress.ClientID %>').val());
        if (poNumber == "Not Ok") {
            showCustomMessage("Validation", "Please Check " + lbl[0].innerText, "Error");
            return false;
        }
        var lbl = $('#<%=lblSeal_Sign.ClientID%>')
        var poNumber = TrimAll($('#<%=ddlSeal_Sign.ClientID %>').val());
        if (poNumber == "Not Ok") {
            showCustomMessage("Validation", "Please Check " + lbl[0].innerText, "Error");
            return false;
        }
        var lbl = $('#<%=lblMargin.ClientID%>')
        var poNumber = TrimAll($('#<%=ddlMargin.ClientID %>').val());
        if (poNumber == "Not Ok") {
            showCustomMessage("Validation", "Please Check " + lbl[0].innerText, "Error");
            return false;
        }
        var lbl = $('#<%=lblAccountReceivableStatus.ClientID%>')
        var poNumber = TrimAll($('#<%=ddlAccountReceivableStatus.ClientID %>').val());
        if (poNumber == "Not Ok") {
            showCustomMessage("Validation", "Please Check " + lbl[0].innerText, "Error");
            return false;
        }
        var lbl = $('#<%=lblPaymentStatus.ClientID%>')
        var poNumber = TrimAll($('#<%=ddlPaymentStatus.ClientID %>').val());
        if (poNumber == "Not Ok") {
            showCustomMessage("Validation", "Please Check " + lbl[0].innerText, "Error");
            return false;
        }
        var lbl = $('#<%=lblPaymentTerms.ClientID%>')
        var poNumber = TrimAll($('#<%=ddlPaymentTerms.ClientID %>').val());
        if (poNumber == "Not Ok") {
            showCustomMessage("Validation", "Please Check " + lbl[0].innerText, "Error");
            return false;
        }
        var lbl = $('#<%=lblPaymentDetails.ClientID%>')
        var poNumber = TrimAll($('#<%=ddlPaymentDetails.ClientID %>').val());
        if (poNumber == "Not Ok") {
            showCustomMessage("Validation", "Please Check " + lbl[0].innerText, "Error");
            return false;
        }
        var lbl = $('#<%=lblGST.ClientID%>')
        var poNumber = TrimAll($('#<%=ddlGST.ClientID %>').val());
        if (poNumber == "Not Ok") {
            showCustomMessage("Validation", "Please Check " + lbl[0].innerText, "Error");
            return false;
        }
        return true;
    }

</script>
<div class="panel panel-default">
    <header class="panel-heading"><b>Validations</b></header>
    <div class="panel-body ">
        <div class="row">
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblPONumber" runat="server" Text="PO #"></asp:Label>
                    <div>
                        <asp:DropDownList ID="ddlPONumber" runat="server" CssClass="form-control chosen">
                            <asp:ListItem>Ok</asp:ListItem>
                            <asp:ListItem>Not Ok</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblValidity" runat="server" Text="Validity"></asp:Label>
                    <div>
                        <asp:DropDownList ID="ddlValidity" runat="server" CssClass="form-control chosen">
                            <asp:ListItem>Ok</asp:ListItem>
                            <asp:ListItem>Not Ok</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblBillToAddress" runat="server" Text="Bill To Address"></asp:Label>
                    <div>
                        <asp:DropDownList ID="ddlBillToAddress" runat="server" CssClass="form-control chosen">
                            <asp:ListItem>Ok</asp:ListItem>
                            <asp:ListItem>Not Ok</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblShipToAddress" runat="server" Text="Ship To Address"></asp:Label>
                    <div>
                        <asp:DropDownList ID="ddlShipToAddress" runat="server" CssClass="form-control chosen">
                            <asp:ListItem>Ok</asp:ListItem>
                            <asp:ListItem>Not Ok</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblSeal_Sign" runat="server" Text="Seal & Sign"></asp:Label>
                    <div>
                        <asp:DropDownList ID="ddlSeal_Sign" runat="server" CssClass="form-control chosen">
                            <asp:ListItem>Ok</asp:ListItem>
                            <asp:ListItem>Not Ok</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblMargin" runat="server" Text="Margin"></asp:Label>
                    <div>
                        <asp:DropDownList ID="ddlMargin" runat="server" CssClass="form-control chosen">
                            <asp:ListItem>Ok</asp:ListItem>
                            <asp:ListItem>Not Ok</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblAccountReceivableStatus" runat="server" Text="Account Receivable Status"></asp:Label>
                    <div>
                        <asp:DropDownList ID="ddlAccountReceivableStatus" runat="server" CssClass="form-control chosen">
                            <asp:ListItem>Ok</asp:ListItem>
                            <asp:ListItem>Not Ok</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblPaymentStatus" runat="server" Text="Payment Status"></asp:Label>
                    <div>
                        <asp:DropDownList ID="ddlPaymentStatus" runat="server" CssClass="form-control chosen">
                            <asp:ListItem>Ok</asp:ListItem>
                            <asp:ListItem>Not Ok</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblPaymentTerms" runat="server" Text="Payment Terms"></asp:Label>
                    <div>
                        <asp:DropDownList ID="ddlPaymentTerms" runat="server" CssClass="form-control chosen">
                            <asp:ListItem>Ok</asp:ListItem>
                            <asp:ListItem>Not Ok</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblPaymentDetails" runat="server" Text="Payment Details"></asp:Label>
                    <div>
                        <asp:DropDownList ID="ddlPaymentDetails" runat="server" CssClass="form-control chosen">
                            <asp:ListItem>Ok</asp:ListItem>
                            <asp:ListItem>Not Ok</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblGST" runat="server" Text="GST#"></asp:Label>
                    <div>
                        <asp:DropDownList ID="ddlGST" runat="server" CssClass="form-control chosen">
                            <asp:ListItem>Ok</asp:ListItem>
                            <asp:ListItem>Not Ok</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
