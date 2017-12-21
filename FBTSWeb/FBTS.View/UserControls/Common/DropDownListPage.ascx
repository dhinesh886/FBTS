<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DropDownListPage.ascx.cs" Inherits="FBTS.View.UserControls.Common.DropDownListPage" %>
<script>
    function jsValidateReferance<%=this.ClientID%>(id) {        
        var lbl = $('#<%=lbl.ClientID%>')
        if (!validateMandatoryField($('#<%=ddl.ClientID %>'), lbl[0].innerText + " cant be Blank !!")) return false;    
        return true;
    }
</script>
<div class="col-md-12">
    <div class="form-group">
        <asp:Label ID="lbl" runat="server" Text="Label"></asp:Label>
        <div>            
            <asp:DropDownList ID="ddl" runat="server" CssClass="form-control chosen" OnSelectedIndexChanged="ddl_SelectedIndexChanged">
                         </asp:DropDownList> 
        </div>
    </div>
</div>
