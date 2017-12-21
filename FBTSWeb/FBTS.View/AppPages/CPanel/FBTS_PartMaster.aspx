<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_PartMaster.aspx.cs" Inherits="FBTS.View.AppPages.CPanel.FBTS_PartMaster" %>
<%@ Import Namespace="FBTS.Model.Common" %>
<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="ezyuc" TagName="CustomMessageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <script>
        // Retains the calender after Postback
            function pageLoad() {
                $('.datepicker').datepicker({
                    format: "dd/mm/yyyy"
                });
                $('.chosen').chosen();
                $('.chosen-container').css('width', '100%');
            } 
            $(document).ready(function () {
                //tagTabLinkAttributes();
                Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            });
            function BeginRequestHandler(sender, args) {
                var elem = args.get_postBackElement();
                ShowPreLoader();
            }

            function EndRequestHandler(sender, args) {
                HidePreLoader(); 
            }
            function jsValidateForm() {
                if (!validateMandatoryField($('#<%=txtPartNo.ClientID %>'), "Part # Cannot be Blank!!")) return false;
                if (!validateMandatoryField($('#<%=txtDesp.ClientID %>'), "Description Cannot be Blank!!")) return false;
                if (!validateMandatoryField($('#<%=ddlPartType.ClientID %>'), "Please Select Part Type!!")) return false;
                if (!validateMandatoryField($('#<%=ddlPartGroup.ClientID %>'), "Please Select Part Group!!")) return false;
                if (!validateMandatoryField($('#<%=ddlUnit.ClientID %>'), "Please Select Part Unit!!")) return false;
                if (!validateMandatoryField($('#<%=txtSalesPrice.ClientID %>'), "Sale Price cannt be blank!!")) return false;
                if (!validateMandatoryField($('#<%=txtPriceValidDate.ClientID %>'), "Price valid Date cannt be blank!!")) return false;

                return true;
            }     
    </script> 
    <ezyuc:CustomMessageControl runat="server" id="CustomMessageControl" />
    <section class="panel">
    <!-- BEGIN PAGE HEADER-->
       <div class="row">
            <div class="col-md-4">
                <header class="panel-heading no-b">
                    <h4 runat="server" id="pageTitle">Manage Users</h4>
                </header>
            </div>
            <div class="col-md-8">
                <asp:UpdatePanel ID="uplActions" runat="server" UpdateMode="Conditional">
                    <ContentTemplate> 
                        <div class="pull-right panel-body" runat="server" id="divForm" visible="false"> 
                            <label><b>Created Date :</b></label>
                            <asp:Label ID="lblCreadet" runat="server" Text="" CssClass="control-label"></asp:Label>&nbsp&nbsp&nbsp&nbsp 
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-primary btn-sm  " Text="Login" OnClick="lnkSave_Click" OnClientClick="return jsValidateForm();">
                                <i class="icon-save mr5"></i> Save </asp:LinkButton>
                            <asp:LinkButton ID="lnkBack" runat="server" CssClass="btn btn-sm btn-default" Text="Login"  OnClientClick="openTab(1);" OnClick="lnkBack_Click">
                                <i class="icon-backward mr5"></i> Back </asp:LinkButton>
                        </div>
                        <div class="pt15" runat="server" id="divView">                             
                            <div class="col-md-12">                                      
                                <div class="pull-right">
                                    <div class="col-md-9">
                                <div class="pull-right">
                                    <div class="input-group mb15">
                                        <span class="input-group-addon"> <i class="icon-search"></i></span>
                                        <asp:TextBox ID="txtSearch" runat="server" class="form-control" placeholder="Enter Part# To Search" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>               
                                    </div> 
                                </div>
                            </div>
                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="openTab(2);" OnClick="lnkAddNew_Click"><i class="icon-plus"></i> Add</asp:LinkButton>
                                </div>
                            </div>                                                                
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <!-- END PAGE HEADER-->
        <div class="panel-body"> 
        <!-- BEGIN PAGE CONTENT-->
            <div class="tabbable tabbable-custom boxless">             
                <div class="tab-content">
                    <div class="tab-pane active" id="tab_1">
                        <div class="responsive" data-tablet="span12 fix-offset" data-desktop="span6">
                            <!-- BEGIN EXAMPLE TABLE PORTLET-->                            
                            <div class="row"> 
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate> 
                                            <asp:GridView ID="GridViewTable" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                AllowPaging="true" FooterStyle="table table-striped table-bordered table-hover" PageSize="15" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                                EmptyDataText="No Data is Available" OnPageIndexChanging="GridViewTable_PageIndexChanging"
                                                OnRowDataBound="GridViewTable_RowDataBound">
                                                <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowIndex" runat="server" />
                                                            <asp:HiddenField ID="hidSuspent" runat="server"></asp:HiddenField>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PartNumber" HeaderText="Part#"></asp:BoundField>
                                                    <%--<asp:TemplateField HeaderText="Part#" >
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.PartNumber") %>' CommandName='<%#Constants.UpdateAction %>'
                                                                        ToolTip="Click here to View Detail" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.PartNumber") %>'
                                                                        OnClick="LoadDetails" OnClientClick=" openTab(2); "></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%"></ItemStyle>
                                                    </asp:TemplateField> --%>
                                                    <asp:BoundField DataField="Description" HeaderText="Part Name"></asp:BoundField>                                       
                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>                                                        
                                                            <asp:LinkButton ID="lnkEdit" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.PartNumber") %>' CommandName='<%#Constants.UpdateAction %>' CssClass="btn btn-xs btn-success"
                                                                            ToolTip="Click here to Edit" OnClientClick=" openTab(2); " runat="server" OnClick="LoadDetails"><i class="icon-edit"></i> Edit</asp:LinkButton>
                                                            <%--<asp:LinkButton ID="lnkDelete" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.PartNumber") %>' CommandName='<%#Constants.DeleteAction %>' CssClass="btn btn-xs btn-danger"
                                                                            ToolTip="Click here to Delete" OnClientClick=" openTab(2); " runat="server" OnClick="LoadDetails"><i class="icon-trash"></i> Delete</asp:LinkButton>--%>
                                                            <asp:LinkButton ID="lnkBan" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.PartNumber") %>' CommandName='<%#Constants.CancelAction %>'
                                                                            ToolTip="Click here to Ban"  runat="server" OnClick="lnkBan_Click" CssClass="btn btn-xs btn-default"><i class="icon-ban-circle"></i> Ban</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView> 
                                        </ContentTemplate>
                                    </asp:UpdatePanel> 
                                </div>
                            </div>
                            <!-- END EXAMPLE TABLE PORTLET-->
                        </div>
                    </div>
                    <div class="tab-pane" id="tab_2"> 
                        <asp:UpdatePanel ID="uplForm" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>    
                                <div class="panel panel-default">
                                    <div class="panel-body"> 
                                        <div class="row"> 
                                            <asp:HiddenField ID="hidAction" runat="server" />
                                            <asp:HiddenField ID="hidType" runat="server" />
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Part #</label>
                                                    <div>
                                                        <asp:TextBox ID="txtPartNo" runat="server" CssClass="form-control text-uppercase" placeholder="Part Number" MaxLength="18"></asp:TextBox>
                                                    </div>
                                                </div>                                
                                            </div> 
                                            <div class="col-md-4">    
                                                <div class="form-group">                                         
                                                    <label>Description</label>
                                                    <div>
                                                        <asp:TextBox ID="txtDesp" runat="server" CssClass="form-control" placeholder="Description" MaxLength="45"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Detailed Description</label>
                                                    <div>
                                                        <asp:TextBox ID="txtDDesp"  CssClass="form-control" runat="server" MaxLength="140"></asp:TextBox>
                                                    </div>
                                                </div> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    <label>Part Type</label>
                                                    <div>
                                                        <asp:DropDownList ID="ddlPartType" runat="server" CssClass="form-control chosen" AutoPostBack="true" OnSelectedIndexChanged="ddlPartType_SelectedIndexChanged">
                                                            </asp:DropDownList> 
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4">
                                                <div class="form-group">
                                                    <label>Part Group</label>
                                                    <div>
                                                        <asp:DropDownList ID="ddlPartGroup" runat="server" CssClass="form-control chosen">
                                                            </asp:DropDownList> 
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    <label>Unit</label>
                                                    <div>
                                                        <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control chosen">
                                                            </asp:DropDownList> 
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Sales Price</label>
                                                    <div>
                                                        <asp:TextBox ID="txtSalesPrice" runat="server" CssClass="form-control text-right" placeholder="Sales Price" onkeypress="return validentry('D',event);"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Part Validity Date</label>
                                                    <div>
                                                        <asp:TextBox ID="txtPriceValidDate" runat="server" CssClass="form-control datepicker" placeholder="Part Valid Date"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel> 
                    </div>
                </div>
            </div> 
        </div>         
        <!-- END PAGE CONTENT-->
    </section>
    <script>
        function openTab(tabId) {
            if (tabId == 1) {
                $("#tabhd_1").addClass("active");
                $("#tab_1").addClass("active");

                $("#tabhd_2").removeClass("active");
                $("#tab_2").removeClass("active");
            } else {

                $("#tabhd_2").addClass("active");
                $("#tab_2").addClass("active");

                $("#tabhd_1").removeClass("active");
                $("#tab_1").removeClass("active");

            }
        }
    </script>
</asp:Content>
