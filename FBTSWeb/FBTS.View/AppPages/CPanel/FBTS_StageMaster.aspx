<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_StageMaster.aspx.cs" Inherits="FBTS.View.AppPages.CPanel.FBTS_StageMaster" %>

<%@ Import Namespace="FBTS.Model.Common" %>  
<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="ezyuc" TagName="CustomMessageControl" %>
<%@ Register Src="~/UserControls/Common/Categorie.ascx" TagPrefix="ezyuc" TagName="CategoriesControl" %>
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
                if (!validateMandatoryField($('#<%=txtStageId.ClientID %>'), "Please Enter Team Id")) return false;
                if (!validateMandatoryField($('#<%=txtStageName.ClientID %>'), "Please Enter Team Name")) return false;
                if (!IsSelected<%=TeamId.ClientID%>()) {
                    showCustomMessage("Team", "Please Select at list one Team", "Error");
                    return false;
                }
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
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-primary btn-sm" OnClientClick="return jsValidateForm();" OnClick="lnkSave_Click">
                                <i class="icon-save mr5"></i> Save </asp:LinkButton>
                            <asp:LinkButton ID="lnkBack" runat="server" CssClass="btn btn-sm btn-default" Text="Login"  OnClientClick="openTab(1);" OnClick="lnkBack_Click" >
                                <i class="icon-backward mr5"></i> Back </asp:LinkButton>
                        </div>
                        <div class="pt15" runat="server" id="divView"> 
                            <%--<div class="col-md-9">
                                <div class="pull-right">
                                    <div class="input-group mb15">
                                        <span class="input-group-addon"> <i class="icon-search"></i></span>
                                        <asp:TextBox ID="txtSearch" runat="server" class="form-control" placeholder="Enter a keyword to search"></asp:TextBox>               
                                    </div> 
                                </div>
                            </div>--%>
                            <div class="col-md-12">                                      
                                <div class="pull-right">
                                    <%--<div class="btn-group">
                                        <a class="btn btn-success  btn-sm dropdown-toggle"  href="#" data-toggle="dropdown">
                                            <i class="icon-cogs"></i> Tools
                                            <i class="icon-angle-down"></i>
                                        </a>
                                        <ul class="dropdown-menu" role="menu">
                                            <li><a href="#"><i class="icon-pencil"></i>Save as PDF</a></li>
                                            <li><a href="#"><i class="icon-trash"></i>Save as Excel</a></li>
                                            <li><a href="#"><i class="icon-print"></i>Print</a></li>
                                            <li class="divider"></li>
                                            <li><a href="#"><i class="icon-refresh"></i>Refresh</a></li>
                                        </ul>
                                    </div>--%>
                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="openTab(2);" OnClick="lnkAddNew_Click" ><i class="icon-plus"></i> Add</asp:LinkButton>
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
                            <asp:UpdatePanel ID="UplView" runat="server" UpdateMode="Conditional">
                                <ContentTemplate> 
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="GridViewTable" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                AllowPaging="true" FooterStyle="table table-striped table-bordered table-hover" PageSize="25" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                                EmptyDataText="No Data is Available" OnPageIndexChanging="GridViewTable_PageIndexChanging"
                                                OnRowDataBound="GridViewTable_RowDataBound">
                                                <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowIndex" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Id" HeaderText="Stage ID" ItemStyle-Width="10%">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Stage" HeaderText="Stage Name"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Id") %>' CommandName='<%#Constants.UpdateAction %>' CssClass="btn btn-xs btn-success"
                                                            ToolTip="Click here to Edit" OnClientClick=" openTab(2); " runat="server" OnClick="lnkEdit_Click" ><i class="icon-edit"></i> Edit</asp:LinkButton>
                                                        <%--<asp:LinkButton ID="lnkDelete" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Id") %>' CommandName='<%#Constants.DeleteAction %>' CssClass="btn btn-xs btn-danger"
                                                            ToolTip="Click here to Delete" OnClientClick=" openTab(2); " runat="server" ><i class="icon-trash"></i> Delete</asp:LinkButton>                                         --%>           
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView> 
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>                         
                            <!-- END EXAMPLE TABLE PORTLET-->   
                        </div>
                    </div>
                    <div class="tab-pane" id="tab_2"> 
                        <asp:UpdatePanel ID="uplForm" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>                           
                                <div class="row">
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <label>Stage Id</label>
                                            <div>
                                                <asp:HiddenField ID="hidType" runat="server" /> 
                                                <asp:HiddenField ID="hidAction" runat="server" />   
                                                <asp:TextBox ID="txtStageId" runat="server" CssClass="form-control" placeholder="Team Id" MaxLength="2" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Stage Name</label>
                                            <div>
                                                <asp:TextBox ID="txtStageName" runat="server" CssClass="form-control" placeholder="Team Name" MaxLength="120"></asp:TextBox>
                                            </div>
                                        </div>  
                                    </div>
                                    <div class="col-md-8" id="divLink" runat="server">
                                        <div class="col-md-9">
                                            <div class="form-group">
                                                <label>Link</label>
                                                <div>                                                
                                                    <asp:TextBox ID="txtLink" runat="server" CssClass="form-control" placeholder="Link" MaxLength="120"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>   
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>SubLink</label>
                                                <div>                                                
                                                    <asp:DropDownList ID="ddlSunLink" runat="server" CssClass="form-control chosen">
                                                        <asp:ListItem Value="FR">Forecasting</asp:ListItem>
                                                        <asp:ListItem Value="BT">BillingTracking</asp:ListItem>
                                                        <asp:ListItem Value="FV">Forecasting Views</asp:ListItem>
                                                         <asp:ListItem Value="BV">BillTracking Views</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>                                     
                                    </div>
                                </div>
                                 
                                <div class="row">
                                    <div class="col-md-3">
                                        <ezyuc:CategoriesControl runat="server" id="TeamId" />
                                    </div>
                                    <div class="col-md-9">
                                        <div class="panel panel-default ">
                                            <header class="panel-heading"><b>Referance</b></header>
                                            <div class="panel-body ">
                                                <div class="row" runat="server" id="divRefAdd">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label>Label Name</label>
                                                            <div>
                                                                <asp:TextBox ID="txtLabelName" runat="server" CssClass="form-control" placeholder="Label Name" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>Text/Date</label>
                                                            <div>
                                                                <asp:DropDownList ID="ddlTextorDate" runat="server" CssClass="form-control">
                                                                    <asp:ListItem>TEXT</asp:ListItem>
                                                                    <asp:ListItem>DATE</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1 pt25">
                                                        <asp:LinkButton ID="lnkAddRef" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="return Validate();" OnClick="lnkAddRef_Click"><i class="icon-plus"></i> Add</asp:LinkButton>
                                                    </div>
                                                </div>   
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:GridView ID="GVReferance" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                            AllowPaging="false" FooterStyle="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                                            EmptyDataText="No Data is Available" OnRowDataBound="GVReferance_RowDataBound">
                                                            <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowIndex" runat="server" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="WFCDESP" HeaderText="Label Name" ItemStyle-Width="50%">
                                                                    </asp:BoundField>
                                                                <asp:BoundField DataField="Relation1" HeaderText="Text/Date"></asp:BoundField>
                                                                <asp:TemplateField HeaderText="Actions">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDelete" CssClass="btn btn-xs btn-danger"
                                                                            ToolTip="Click here to Delete" runat="server" OnClick="lnkDelete_Click" OnClientClick="return validateAction();"><i class="icon-trash"></i> Delete</asp:LinkButton>                                                    
                                                                    </ItemTemplate>
                                                                        <ItemStyle Width="20%"></ItemStyle>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView> 
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
        </div><!-- END PAGE CONTENT-->
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
        function validateAction()
        {
            <%--if (TrimAll($('#<%=hidAction.ClientID %>').val()) == "U")
            {
                showCustomMessage("Validation Failed", "Cant be delete in edit", "Error");
                return false;
            }--%>
            
            return true;
        }
        function Validate()
        {            
            if (!validateMandatoryField($('#<%=txtLabelName.ClientID %>'), "Please Enter Label Name")) return false;
            var labelName =TrimAll($('#<%=txtLabelName.ClientID %>').val());
            var Type =TrimAll($('#<%=ddlTextorDate.ClientID %>').val());
            var tabID = "<%=GVReferance.ClientID%>";
            var TabLength;
            var jvRowData;
            var jvRowData1;
            var jvString;
            var jvString1;
            if (document.getElementById(tabID) != null) {
                TabLength = document.getElementById(tabID).rows.length;
            }
            
            for (var Row = 1; Row < parseInt(TabLength) ; Row++) {
                jvRowData = document.getElementById(tabID).rows[Row].cells[1];
                jvString = TrimAll(jvRowData.innerText);
                jvRowData1 = document.getElementById(tabID).rows[Row].cells[2];
                jvString1 = TrimAll(jvRowData1.innerText);
                if(labelName==jvString&&Type==jvString1)
                {
                    showCustomMessage("Validation Failed", "Label Name already at Row -> " + Row, "Error");
                    return false
                }
            }

            return true;
        }
    </script>
</asp:Content>
