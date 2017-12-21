<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_AccessLevelSetup.aspx.cs" Inherits="FBTS.View.AppPages.CPanel.FBTS_AccessLevelSetup" %>

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
               <%-- if (!validateMandatoryField($('#<%=txtStageId.ClientID %>'), "Please Enter Team Id")) return false;
                if (!validateMandatoryField($('#<%=txtStageName.ClientID %>'), "Please Enter Team Name")) return false;--%>

                return true;
            }
        function validate(tabID) {
            var tabID = "<%=GridViewTable.ClientID%>";
            var TabLength;
            var jvEditedvalue;
            var jvRowData;
            var jvRowData1;
            var jvString;
            var jvString1;
            var jvLen;
            var jvMsgString;
            if (document.getElementById(tabID) != null) {
                TabLength = document.getElementById(tabID).rows.length;
            }
            for (var Row = 1; Row < parseInt(TabLength) ; Row++) {
                for (var i = 1; i <= 2; i++) {
                    jvRowData = document.getElementById(tabID).rows[Row].cells[i].children;
                    jvString = TrimAll(jvRowData[0].value);
                    if (isEmpty(jvString)) {
                        if (i == 1)
                            showCustomMessage("Validation Failed", "Code at Row -> " + Row + "  : Cannot be Blank.", "Error");
                        else if (i == 2)
                            showCustomMessage("Validation Failed", "Description UOM at Row -> " + Row + "  : Cannot be Blank.", "Error");
                        jvRowData[0].focus();
                        return false;
                    }
                    else {
                        jvLen = jvString.length;
                        if (jvLen <= 1) {
                            showCustomMessage("Input is Short", "Provided Value to this field is too short....\n\n at least 2 characters are expected.", "Error");
                            jvRowData[0].focus();
                            return false;
                        }
                        else {
                            jvEditedvalue = jvString.toLowerCase();
                            for (var Row1 = 1; Row1 < parseInt(TabLength) ; Row1++) {
                                if (i == 1) { jvMsgString = "Code"; }
                                else if (i == 2) { jvMsgString = "Description"; }

                                jvRowData1 = document.getElementById(tabID).rows[Row1].cells[i].children;
                                jvString1 = TrimAll(jvRowData1[0].value);
                                if ((jvString1.toLowerCase() == jvEditedvalue) && (Row != Row1)) {
                                    showCustomMessage("Duplicate Value", jvMsgString + " Already Exist", "Error");
                                    jvRowData1[0].focus();
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
        }
    </script> 
    <ezyuc:CustomMessageControl runat="server" id="CustomMessageControl" />
    <section class="panel">
    <!-- BEGIN PAGE HEADER-->
        <div class="row">
            <div class="col-md-6">
                <header class="panel-heading no-b">
                    <h4 runat="server" id="pageTitle">Manage Access Levels</h4>
                </header>
            </div>
            <div class="col-md-6 ">
                <asp:UpdatePanel ID="uplActions" runat="server" UpdateMode="Conditional">
                    <ContentTemplate> 
                        <div class="pull-right panel-body" runat="server" id="divForm" visible="false"> 
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-primary btn-sm  " Text="Login"  OnClientClick="return jsValidateForm();" OnClick="lnkSave_Click">
                                <i class="icon-save mr5"></i> Save </asp:LinkButton>
                            <asp:LinkButton ID="lnkBack" runat="server" CssClass="btn btn-sm btn-default" Text="Login"  OnClientClick="openTab(1);" OnClick="lnkBack_Click" >
                                <i class="icon-backward mr5"></i> Back </asp:LinkButton>
                        </div>
                        <div class="pull-right panel-body" runat="server" id="divView"> 
                            <asp:LinkButton ID="lnkSaveDesignation" runat="server" CssClass="btn btn-primary btn-sm" OnClientClick="return validate();" OnClick="lnkSaveDesignation_Click" ><i class="icon-plus"></i> Save</asp:LinkButton>
                            <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-sm btn-success" OnClick="lnkAddNew_Click" ><i class="icon-plus"></i> Add</asp:LinkButton>                            
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
                                                FooterStyle="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                                EmptyDataText="No Data is Available" OnRowDataBound="GridViewTable_RowDataBound">
                                                    <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowIndex" runat="server" />
                                                            <asp:HiddenField ID="hdnAction" runat="server"></asp:HiddenField>
                                                            <asp:HiddenField ID="hdnSno" runat="server"></asp:HiddenField>
                                                            <asp:HiddenField ID="hidCreatedDate" runat="server"></asp:HiddenField>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="3%"></ItemStyle>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" onkeypress="return validentry('H',event);" placeholder="Code" MaxLength="8"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%"></ItemStyle>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Description" MaxLength="25" ></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30%"></ItemStyle>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Access Level">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAccess" runat="server" CssClass="form-control" onkeypress="return validentry('N',event);" placeholder="Access Level" MaxLength="1"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%"></ItemStyle>
                                                    </asp:TemplateField>         
                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-xs btn-danger mr5"><i class="icon-trash"></i>Delete</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkBan" runat="server" CssClass="btn btn-xs btn-default mr5"><i class="icon-ban-circle"></i>Ban</asp:LinkButton>  
                                                            <asp:LinkButton ID="lnkEdit" CommandArgument='<%#DataBinder.Eval(Container, "DataItem.Id") %>' CommandName='<%#DataBinder.Eval(Container, "DataItem.Description") %>' CssClass="btn btn-xs btn-success"
                                                                        ToolTip="Click here to Edit" OnClientClick=" openTab(2); " runat="server" OnClick="lnkEdit_Click"><i class="icon-edit"></i> Set Access Rights</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30%"></ItemStyle>
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
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row" style="padding-bottom: 15px;">
                                            <div class="col-md-8  ">
                                                Access Level : <asp:Label ID="lblAccessLevel" runat="server" Text="Label"></asp:Label>
                                            </div>     
                                            <div class="col-md-4  ">                             
                                                <div class="pull-right">                                                
                                                    <asp:HiddenField ID="hidKey" runat="server"></asp:HiddenField> 
                                                    <asp:HiddenField ID="hidAction" runat="server" />
                                                </div>
                                            </div>
                                        </div>      
                                        <div class="row">
                                            <header class="panel-heading">Menus</header>
                                            <!-- BEGIN EXAMPLE TABLE PORTLET-->                                  
                                            <div class="col-md-12">                                                     
                                                <asp:GridView ID="gvMenuRights" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                    AllowPaging="false" FooterStyle="table table-striped table-bordered table-hover" PageSize="15" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                                    EmptyDataText="No Data is Available" OnRowDataBound="gvMenuRights_RowDataBound">
                                                    <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowIndex" runat="server" /> 
                                                                <asp:HiddenField ID="hidMenuCode" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="3%"></ItemStyle>
                                                        </asp:TemplateField>  
                                                        <asp:BoundField DataField="MenuName" HeaderText="Menu Name" ItemStyle-Width="20%"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="Level">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtLevel" runat="server" class="form-control" placeholder="level"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="15%"></ItemStyle>
                                                        </asp:TemplateField>         
                                                        <asp:TemplateField HeaderText="Actions">
                                                            <ItemTemplate>                                                      
                                                                <asp:CheckBox ID="chkSelect" runat="server" />                                                                       
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%"></ItemStyle>
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
    </script>
</asp:Content>
