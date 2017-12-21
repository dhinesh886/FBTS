<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddressControlVertical.ascx.cs" Inherits="FBTS.View.UserControls.Common.AddressControlVertical" %>
<header class="panel-heading"><b>Address Details</b></header>
<script>
function jsValidateAddress() {
        if (!validateMandatoryField($('#<%=txtStreet.ClientID %>'), "Address Cannot be Blank!!")) return false;
        if (!validateMandatoryField($('#<%=txtCity.ClientID %>'), "City Cannot be Blank!!")) return false;
        if (!validateMandatoryField($('#<%=txtPostCode.ClientID %>'), "Zip Code Cannot be Blank!!")) return false;
        if (!validateMandatoryField($('#<%=txtOffPhone.ClientID %>'), "Office Phone Cannot be Blank!!")) return false;
        if (!validateMandatoryField($('#<%=txtEmail.ClientID %>'), "Email Cannot be Blank!!")) return false;
<%--        if (!validateMandatoryField($('#<%=txtMobile.ClientID %>'), "Mobile# Cannot be Blank!!")) return false;--%>
        opentab(1);
        return true;
}
    </script>
    <section class="panel">
       
        <div class="panel-body">
        <div class="form-group">
            <label>Address</label>
            <div>
                    <asp:TextBox ID="txtStreet" runat="server" class="form-control" placeholder="Street" onkeypress="return validentry('H',event);" MaxLength="250"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label>City</label>
            <div>
                <asp:TextBox ID="txtCity" runat="server" class="form-control" placeholder="City" onkeypress="return validentry('NM',event);" MaxLength="30"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label>Country</label>
            <div>
                    <asp:DropDownList ID="ddlCountry" runat="server" class="form-control" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                    </asp:DropDownList> 
            </div>
        </div>
  
        <div class="form-group">
            <label>State</label>
            <div>
                    <asp:DropDownList ID="ddlState" runat="server" class="form-control">
                    </asp:DropDownList>
            </div>
        </div>
            <div class="form-group">
            <label>Zip/Postal Code</label>
            <div>
                    <asp:TextBox ID="txtPostCode" runat="server" class="form-control" placeholder="Post Code" onkeypress="return validentry('N',event);" MaxLength="6"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label>Phone</label>
            <div>
                    <asp:TextBox ID="txtOffPhone" runat="server" class="form-control" placeholder="Office Phone Number" onkeypress="return validentry('P',event);"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label>Email</label>
            <div>
                    <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email Address" onblur="return validateEmail('#<%=txtEmail.ClientID %>');"></asp:TextBox>
            </div>
        </div>
                                  
        <div class="form-group" runat="server" id="divMobile">
            <label>Mobile</label>
            <div>
                    <asp:TextBox ID="txtMobile" runat="server" class="form-control" placeholder="Mobile Number" onkeypress="return validentry('P',event);" MaxLength="10"></asp:TextBox>
            </div>
        </div>  
             <div class="form-group" runat="server" id="divWww">
            <label>Web Site</label>
            <div>
                    <asp:TextBox ID="txtWww" runat="server" class="form-control" placeholder="Web Site"></asp:TextBox>
            </div>
        </div>  
     </div>       
 </section> 