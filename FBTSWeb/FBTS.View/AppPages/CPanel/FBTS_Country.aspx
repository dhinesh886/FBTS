<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_Country.aspx.cs" Inherits="Ezy.ERP.View.AppPages.FBTS_Country" %>

<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="ezyuc" TagName="CustomMessageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>

        function validate() {
            if (!validateMandatoryField($('#<%=txtId.ClientID %>'), "ID Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtName.ClientID %>'), "Name Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtCurrency.ClientID %>'), "Currency Code Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtCName.ClientID %>'), "Currency Name Cannot be Blank!!")) return false;
        }

    </script>
    <section class="panel">
        <!-- BEGIN PAGE HEADER-->
        <div class="row">
            <div class="col-md-4">
                <header class="panel-heading no-b">
                    <h4>Country</h4>
                </header>
            </div>
            <div class="col-md-8">
                <asp:UpdatePanel ID="uplActions" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="pull-right pt10" style="width: 100%;" id="divActionAdd" runat="server">
                            <div class="col-md-3 pull-right">
                                <div class="pull-right">
                                    <%--<div class="btn-group">
                                        <a class="btn btn-success  btn-sm dropdown-toggle mr5" href="#" data-toggle="dropdown"><i class="icon-cogs"></i>Tools
                                            <i class="icon-angle-down"></i></a>
                                        <ul class="dropdown-menu" role="menu">
                                            <li><a href="#"><i class="icon-pencil"></i>Save as PDF</a></li>
                                            <li><a href="#"><i class="icon-trash"></i>Save as Excel</a></li>
                                            <li><a href="#"><i class="icon-print"></i>Print</a></li>
                                            <li class="divider"></li>
                                            <li><a href="#"><i class="icon-refresh"></i>Refresh</a></li>
                                        </ul>
                                    </div>--%>
                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-sm btn-success mr5" OnClientClick="openTab(2);" OnClick="lnkAddNew_Click"><i class="icon-plus"></i> Add</asp:LinkButton>                                  
                                </div>
                            </div>
                           <%-- <div class="col-md-3 pull-right">
                                <div class="input-group mb15">
                                    <span class="input-group-addon"><i class="icon-search"></i></span>
                                    <asp:TextBox ID="txtSearch" runat="server" class="form-control" placeholder="Enter a keyword to search"></asp:TextBox>
                                </div>
                            </div>--%>
                        </div>
                        <div class="pull-right panel-body" runat="server" id="divActionSave" style="display: none">
                            <div class="pull-right">
                                <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-sm btn-primary mr5" OnClick="lnkSave_Click" OnClientClick="return validate();"><i class="icon-save mr5"></i>Save</asp:LinkButton>                                
                                <asp:LinkButton ID="lnkBack" runat="server" CssClass="btn btn-sm btn-default" OnClientClick="openTab(1);" OnClick="lnkBack_Click"><i class="icon-backward mr5"></i>Back</asp:LinkButton>
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
                    <div class="tab-pane active" id="tab1">
                        <ezyuc:CustomMessageControl runat="server" ID="CustomMessageControl" />
                        <asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="responsive" data-tablet="span12 fix-offset" data-desktop="span6">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="GridViewTable" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                AllowPaging="true" FooterStyle="table table-striped table-bordered table-hover" PageSize="15" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                                EmptyDataText="No Data is Available" OnRowDataBound="GridViewTable_OnRowDataBoound" OnPageIndexChanging="GridViewTable_PageIndexChanging">
                                                <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowIndex" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CountryId" HeaderText="Country ID"></asp:BoundField>
                                                    <asp:BoundField DataField="CountryName" HeaderText="Country Name"></asp:BoundField>
                                                     <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.CountryId") %>' CommandName='<%#FBTS.Model.Common.Constants.UpdateAction %>' CssClass="btn btn-xs btn-success"
                                                            ToolTip="Click here to Edit" OnClientClick=" openTab(2); " runat="server" OnClick="LoadDetails"><i class="icon-edit"></i> Edit</asp:LinkButton>                                                        
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%"></ItemStyle>
                                                </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane" id="tab2">
                        <asp:UpdatePanel ID="uplForm" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:HiddenField ID="hidKey" runat="server" />
                                <asp:HiddenField ID="hidAction" runat="server" />
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <header class="panel-heading"><b>Country & Currency Details</b></header>
                                            <div class="col-md-6 pt10">
                                                <div class="form-group">
                                                    <label>Country ID</label>
                                                    <div>
                                                        <asp:TextBox ID="txtId" runat="server" CssClass="form-control" MaxLength="8" onkeypress="return validentry('H',event);"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group pt10">
                                                    <label>Country Name</label>
                                                    <div>
                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" onkeypress="return validentry('zz',event);" MaxLength="50"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Currency Code</label>
                                                    <div>
                                                        <asp:TextBox ID="txtCurrency" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Name</label>
                                                    <div>
                                                        <asp:TextBox ID="txtCName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Symbol</label>
                                                    <div>
                                                        <asp:TextBox ID="txtSymbol" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Denomination</label>
                                                    <div>
                                                        <asp:TextBox ID="txtDenomination" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6" id="divStateDetails" runat="server" visible="false">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <header class="panel-heading"><b>State Details</b></header>
                                                </div>
                                                <div class="col-md-6 pull-right">
                                                    <div class="pull-right">
                                                        <asp:LinkButton ID="lnkSaveState" runat="server" CssClass="btn btn-sm btn-success mr5" OnClick="lnkSaveState_Click"><i class="icon-save mr5"></i>Save State</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkAddState" runat="server" CssClass="btn btn-sm btn-primary mr5" OnClick="lnkAddState_Click"><i class="icon-save mr5"></i>Add State</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="panel-body">
                                                <asp:GridView ID="GridViewState" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                    AllowPaging="true" FooterStyle="table table-striped table-bordered table-hover" PageSize="10" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                                    EmptyDataText="No Data is Available" OnRowDataBound="GridViewState_RowDataBound" OnPageIndexChanging="GridViewState_PageIndexChanging">
                                                    <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowIndex" runat="server" />
                                                                <asp:HiddenField ID="hdnAction" runat="server"></asp:HiddenField>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="3%"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="State ID">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtStateId" runat="server" CssClass="form-control" MaxLength="8"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="15%"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="State Name">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtStateName" runat="server" CssClass="form-control" MaxLength="230"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="15%"></ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
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
        <script type="text/javascript">
            function openTab(tabId) {
                if (tabId == 1) {
                    $("#tabhd1").addClass("active");
                    $("#tab1").addClass("active");

                    $("#tabhd2").removeClass("active");
                    $("#tab2").removeClass("active");
                } else {
                    $("#tabhd2").addClass("active");
                    $("#tab2").addClass("active");

                    $("#tabhd_1").removeClass("active");
                    $("#tab1").removeClass("active");
                }
            }
        </script>
    </section>
</asp:Content>
