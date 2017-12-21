<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddressControlVertical.ascx.cs" Inherits="FBTS.View.UserControls.Common.AddressControlVertical" %>


<script>
    function jsValidateAddress() {
        if (!validateMandatoryField($('#<%=txtStreet.ClientID %>'), "Address Cannot be Blank!!")) return false;
        if (!validateMandatoryField($('#<%=txtCity.ClientID %>'), "City Cannot be Blank!!")) return false;
        if (!validateMandatoryField($('#<%=txtPostCode.ClientID %>'), "Zip Code Cannot be Blank!!")) return false;
        if (!validateMandatoryField($('#<%=txtOffPhone.ClientID %>'), "Office Phone Cannot be Blank!!")) return false;
        if (!validateMandatoryField($('#<%=txtEmail.ClientID %>'), "Email Cannot be Blank!!")) return false;
        if (!validateMandatoryField($('#<%=txtMobile.ClientID %>'), "Mobile# Cannot be Blank!!")) return false;
        opentab(1);
        return true;
    }
</script>
<section class="panel pt10 pl5 pr5">
    <div class="panel panel-default ">
        <header class="panel-heading"><b>Address Details</b></header>
        <div class="panel-body ">
            <asp:UpdatePanel ID="uplAddress" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>Address</label>
                                <div>
                                    <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control" placeholder="Street" MaxLength="250"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label runat="server" id="lblCity">City</label>
                                <div>
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" placeholder="City" MaxLength="50"></asp:TextBox>
                                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control chosen" Visible="false"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Country</label>
                                <div>
                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control chosen" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>State</label>
                                <div>
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control chosen">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Zip/Postal Code</label>
                                <div>
                                    <asp:TextBox ID="txtPostCode" runat="server" CssClass="form-control" placeholder="Post Code" MaxLength="6" onkeypress="return validentry('N',event);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Email</label>
                                <div>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email Address" MaxLength="256"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Phone</label>
                                <div>
                                    <asp:TextBox ID="txtOffPhone" runat="server" CssClass="form-control" placeholder="Office Phone Number" MaxLength="30" onkeypress="return validentry('N',event);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2" runat="server" id="divMobile">
                            <div class="form-group">
                                <label>Mobile</label>
                                <div>
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Mobile Number" MaxLength="10" onkeypress="return validentry('N',event);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3" runat="server" id="divWww">
                            <div class="form-group">
                                <label>Web Site</label>
                                <div>
                                    <asp:TextBox ID="txtWww" runat="server" CssClass="form-control" placeholder="Web Site" MaxLength="256"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group-lg">
                                <label>GST# Available</label>
                                <div>
                                    <asp:DropDownList ID="ddlGST" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGST_SelectedIndexChanged">                                        
                                        <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                        <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2" runat="server" id="divGST">
                            <div class="form-group-lg">
                                <label>GST#</label>
                                <div>
                                    <asp:TextBox ID="txtGST" runat="server" CssClass="form-control" placeholder="GST#"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2" runat="server" id="divGSTReason" style="display:none">
                            <div class="form-group-lg">
                                <label>GST NA Reason</label>
                                <div>
                                    <asp:DropDownList ID="ddlGSTReason" runat="server" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="" Text="Pick the reason"></asp:ListItem>
                                        <asp:ListItem Value="Not Eligible" Text="Not Eligible"></asp:ListItem>
                                        <asp:ListItem Value="Yet to Receive" Text="Yet to Receive"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</section>
