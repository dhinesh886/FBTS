<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RefrenceControl.ascx.cs" Inherits="FBTS.View.UserControls.ForecastingCommon.RefrenceControl" %>
<script>
    function jsValidateReferance<%=this.ClientID%>(id) {        
        var lbl = $('#<%=lblddlParameter.ClientID%>')
        if (!validateMandatoryField($('#<%=txtParameter.ClientID %>'), lbl[0].innerText + " cant be Blank !!")) return false;    
        return true;
    }
</script>
<div class="col-md-12">
    <div class="form-group">
        <label>
            <asp:Label ID="lblddlParameter" runat="server" Text="Label"></asp:Label></label>
        <div>
            <asp:HiddenField ID="hidParaCode" runat="server" />
            <asp:HiddenField ID="hidIsDate" runat="server" />
            <asp:TextBox ID="txtParameter" runat="server" CssClass="form-control" OnTextChanged="txtParameter_TextChanged" MaxLength="250">
            </asp:TextBox>
        </div>
    </div>
</div>
   