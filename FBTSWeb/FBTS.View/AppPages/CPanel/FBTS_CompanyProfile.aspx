<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_CompanyProfile.aspx.cs" Inherits="FBTS.View.AppPages.CPanel.FBTS_CompanyProfile" %>

<%@ Import Namespace="FBTS.Model.Common" %> 
<%@ Register Src="~/UserControls/Common/AddressControlHorizontal2Col.ascx" TagPrefix="ezyuc" TagName="AddressControl" %>
<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="ezyuc" TagName="CustomMessageControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <script>
        // Retains the calender after Postback
        function pageLoad() {
            $('.datepicker').datepicker();

        }

        function uploadComplete() {
        }

        function uploadError() {
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
            if (!validateMandatoryField($('#<%=txtId.ClientID %>'), "ID Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtName.ClientID %>'), "Name Cannot be Blank!!")) return false;
            if (!jsValidateAddress()) return false;
            if (!validateMandatoryField($('#<%=txtFPFrom.ClientID %>'), "From Date Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtFPTo.ClientID %>'), "To Date Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtTin.ClientID %>'), "Tin# Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtCst.ClientID %>'), "CST# Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtExcise.ClientID %>'), "Excise# Cannot be Blank!!")) return false;
            if (!validateMandatoryField($('#<%=txtEXValidTill.ClientID %>'), "Validity Date Cannot be Blank!!")) return false;
            opentab(1);
            return true;
        }
    </script> 
     <ezyuc:CustomMessageControl runat="server" id="CustomMessageControl" />
    <section class="panel ">
    <!-- BEGIN PAGE HEADER-->
    <header class="panel-heading no-b">
        <h4 runat="server" id="pageTitle">Manage Users</h4>
    </header>
    <!-- END PAGE HEADER-->
    <div class="panel-body"> 
    <!-- BEGIN PAGE CONTENT-->
        <div class="tabbable tabbable-custom boxless">             
            <div class="tab-content">
                <div class="tab-pane active animated fadeIn" id="tab_1">
                    <div class="responsive" data-tablet="span12 fix-offset" data-desktop="span6">
                        <!-- BEGIN EXAMPLE TABLE PORTLET--> 
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="pull-right" style="width:100%;"> 
                                    <div class="col-md-2 pull-right">                                      
                                        <div class="btn-group">
                                            <a class="btn btn-success  btn-sm dropdown-toggle"  href="#" data-toggle="dropdown"><i class="icon-cogs"></i> Tools
                                                <i class="icon-angle-down"></i></a>
                                            <ul class="dropdown-menu" role="menu">
                                                <li><a href="#"><i class="icon-pencil"></i>Save as PDF</a></li>
                                                <li><a href="#"><i class="icon-trash"></i>Save as Excel</a></li>
                                                <li><a href="#"><i class="icon-print"></i>Print</a></li>
                                                <li class="divider"></li>
                                                <li><a href="#"><i class="icon-refresh"></i>Refresh</a></li>
                                            </ul>
                                        </div>
                                        <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="openTab(2);" OnClick="lnkAddNew_Click"><i class="icon-plus"></i> Add</asp:LinkButton>
                                    </div>                                    
                                    <div class="col-md-4 pull-right" style="padding-left: 2px; padding-right: 2px;">
                                        <div class="input-group mb15">
                                            <span class="input-group-addon"> <i class="icon-search"></i></span>
                                            <asp:TextBox ID="txtSearch" runat="server" class="form-control" placeholder="Enter a keyword to search"></asp:TextBox>               
                                        </div> 
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate> 
                                    <asp:GridView ID="GridViewTable" runat="server" CssClass="table table-striped table-bordered table-hover"
                                        AllowPaging="true" FooterStyle="table table-striped table-bordered table-hover" PageSize="5" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                        EmptyDataText="No Data is Available" OnPageIndexChanging="GridViewTable_PageIndexChanging"                                                         
                                        OnRowDataBound="GridViewTable_RowDataBound">
                                        <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowIndex" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Id" SortExpression="U_NAME">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkView" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Id") %>' CommandName='<%#Constants.ViewAction %>'
                                                        ToolTip="Click here to View Detail" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Id") %>'
                                                        OnClick="LoadDetails" OnClientClick=" openTab(2); "></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                                            <asp:BoundField DataField="FinancialYearStart" HeaderText="Financial Period Start" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField> 
                                            <asp:BoundField DataField="FinancialYearEnd" HeaderText="Financial Period End" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>                                           
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>                                                        
                                                    <asp:LinkButton ID="lnkEdit" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Id") %>' CommandName='<%#Constants.UpdateAction %>' CssClass="btn btn-xs btn-success"
                                                        ToolTip="Click here to Edit" OnClientClick=" openTab(2); " runat="server" OnClick="LoadDetails"><i class="icon-edit"></i> Edit</asp:LinkButton>
                                                    <asp:LinkButton ID="lnkDelete" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Id") %>' CommandName='<%#Constants.DeleteAction %>' CssClass="btn btn-xs btn-danger"
                                                        ToolTip="Click here to Delete" OnClientClick=" openTab(2); " runat="server" OnClick="LoadDetails"><i class="icon-trash"></i> Delete</asp:LinkButton>
                                                    <asp:LinkButton ID="lnkBan" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Id") %>' CommandName='<%#Constants.UpdateAction %>'
                                                        ToolTip="Click here to Ban" OnClientClick=" openTab(2); " runat="server" OnClick="LoadDetails" CssClass="btn btn-xs btn-default"><i class="icon-ban-circle"></i> Ban</asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView> 
                                </ContentTemplate>
                            </asp:UpdatePanel> 
                        </div>
                        <!-- END EXAMPLE TABLE PORTLET-->
                    </div>
                </div>
                <div class="tab-pane animated fadeIn" id="tab_2"> 
                    <asp:UpdatePanel ID="uplForm" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="pull-right">
                                        <asp:LinkButton ID="lnkBack" runat="server" CssClass="btn btn-sm btn-default" Text="Login"  OnClientClick="openTab(1);"><i class="icon-backward mr5"></i> Back </asp:LinkButton> 
                                        <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-sm btn-success" Text="Login" OnClick="lnkSave_Click"
                                            OnClientClick=" return jsValidateForm(); "><i class="icon-save"></i> Save </asp:LinkButton>                                     
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8">
                                     <section class="panel pt10 pl5 pr5">
                                        <div class="panel panel-default ">
                                            <header class="panel-heading"><b>Basic Details</b></header>   
                                            <div class="panel-body">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Id</label>
                                                        <div>
                                                            <asp:HiddenField ID="hidKey" runat="server" />
                                                            <asp:HiddenField ID="hidAction" runat="server" /> 
                                                            <asp:HiddenField ID="hidCType" runat="server" /> 
                                                            <asp:HiddenField ID="hidLogo" runat="server" />
                                                            <asp:TextBox ID="txtId" runat="server" class="form-control" placeholder="Id" onkeypress="return validentry('H',event);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-8">
                                                    <div class="form-group">
                                                        <label>Name</label>
                                                        <div>
                                                            <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="Name" onkeypress="return validentry('NM',event);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </section>
                                </div>
                                <div class="col-md-4">
                                    <section class="panel pt10 pl5 pr5" runat="server" id="secFPeriod">
                                        <div class="panel panel-default ">
                                            <header class="panel-heading"><b>Financial Period</b></header>
                                            <div class="panel-body">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>From</label>
                                                        <div>
                                                            <asp:TextBox ID="txtFPFrom" runat="server" class="form-control datepicker" placeholder="From Date" onkeypress="return validentry('N',event);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>To</label>
                                                        <div>
                                                            <asp:TextBox ID="txtFPTo" runat="server" class="form-control datepicker" placeholder="To Date" onkeypress="return validentry('N',event);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </section>
                                </div>
                            </div>  
                            <div class="row">
                                    <ezyuc:AddressControl runat="server" id="AddressControl"/>
                            </div>      
                                <div class="col-md-6">
                                    

                                    <section class="panel" runat="server" id="secTaxDetails">
                                        <header class="panel-heading"><b>Tax Details</b></header>
                                        <div class="panel-body">                                     
                                            <div class="col-md-6">
                                    <div class="form-group">
                                        <label>TIN#</label>
                                        <div>
                                            <asp:TextBox ID="txtTin" runat="server" class="form-control" placeholder="TIN Number" onkeypress="return validentry('H',event);" MaxLength="30"></asp:TextBox>
                                        </div>
                                        </div></div>
                                            <div class="col-md-6">
                                    <div class="form-group">
                                        <label>CST#</label>
                                        <div>
                                            <asp:TextBox ID="txtCst" runat="server" class="form-control" placeholder="CST Number" onkeypress="return validentry('H',event);" MaxLength="30"></asp:TextBox>
                                        </div>
                                        </div></div>
                                           <div class="col-md-6">
                                        <div class="form-group">
                                        <label>Excise#</label>
                                        <div>
                                            <asp:TextBox ID="txtExcise" runat="server" class="form-control" placeholder="Excise Number" onkeypress="return validentry('H',event);" MaxLength="30"></asp:TextBox>
                                        </div>
                                    </div></div>
                                            <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Excise# Valid Till</label>
                                        <div>
                                            <asp:TextBox ID="txtEXValidTill" runat="server" class="form-control datepicker" placeholder="Excise Validity Date" onkeypress="return validentry('N',event);" MaxLength="30"></asp:TextBox>
                                        </div>
                                    </div></div>
                                            <div class="col-md-6">
                                            <div class="form-group">
                                        <label>Transaction Currency</label>
                                        <div>
                                             <asp:DropDownList ID="ddlTrnCurrency" runat="server" class="form-control">
                                                </asp:DropDownList> 
                                        </div>
                                    </div></div>
                                        </div>
                                    </section>
                                     
                                    <section class="panel" runat="server" id="secContactDetails">
                                        <header class="panel-heading"><b>Contact Details</b></header>
                                        <div class="panel-body">
                                    <div class="form-group">
                                        <label>Contact Person Name 1</label>
                                        <div>
                                            <asp:TextBox ID="txtContactName1" runat="server" class="form-control" placeholder="Contact Name"></asp:TextBox>
                                        </div>
                                        </div>
                                    <div class="form-group">
                                        <label>Contact Phone/Mobile 1</label>
                                        <div>
                                            <asp:TextBox ID="txtContactPhone1" runat="server" class="form-control" placeholder="Contact Phone/Mobile"></asp:TextBox>
                                        </div>
                                        </div>
                                      <div class="form-group">
                                        <label>Contact Person Name 2</label>
                                        <div>
                                            <asp:TextBox ID="txtContactName2" runat="server" class="form-control" placeholder="Contact Name"></asp:TextBox>
                                        </div>
                                        </div>
                                    <div class="form-group">
                                        <label>Contact Phone/Mobile 2</label>
                                        <div>
                                            <asp:TextBox ID="txtContactPhone2" runat="server" class="form-control" placeholder="Contact Phone/Mobile"></asp:TextBox>
                                        </div>
                                        </div>
                                        </div>
                                    </section>
                                         
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
        </section>
</asp:Content>
