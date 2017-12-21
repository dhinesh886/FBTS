<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_Location.aspx.cs" Inherits="FBTS.View.AppPages.CPanel.FBTS_Location" %>

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
                            showCustomMessage("Validation Failed", "ID at Row -> " + Row + "  : Cannot be Blank.", "Error");
                        else if (i == 2)
                            showCustomMessage("Validation Failed", "Description at Row -> " + Row + "  : Cannot be Blank.", "Error");
                        jvRowData[0].focus();
                        return false;
                    }
                    else {
                        jvLen = jvString.length;
                        if (jvLen <= 1 && i != 3) {
                            showCustomMessage("Input is Short", "Provided Value to this field is too short....\n\n at least 2 characters are expected.", "Error");
                            jvRowData[0].focus();
                            return false;
                        }
                        else {
                            jvEditedvalue = jvString.toLowerCase();
                            for (var Row1 = 1; Row1 < parseInt(TabLength) ; Row1++) {
                                if (i == 1) { jvMsgString = "ID"; }
                                else if (i == 2) { jvMsgString = "Description"; }
                                jvRowData1 = document.getElementById(tabID).rows[Row1].cells[i].children;
                                jvString1 = TrimAll(jvRowData1[0].value);
                                if ((jvString1.toLowerCase() == jvEditedvalue) && (Row != Row1) && i != 3) {
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
                    <h4 runat="server" id="pageTitle">Location</h4>
                </header>
            </div>           
                <asp:UpdatePanel ID="uplActions" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>  
                       <div class="col-md-6 " runat="server" id="divButtons">
                        <div class="pull-right panel-body"> 
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-success btn-sm  " Text="Login"  OnClientClick="return validate();" OnClick="lnkSave_Click">
                                <i class="icon-save mr5"></i> Save </asp:LinkButton>
                            <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-sm btn-info" OnClick="lnkAddNew_Click"><i class="icon-plus" ></i> Add</asp:LinkButton>                            
                        </div> 
                       </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
       
        <div class="panel-body"> 
            <!-- BEGIN PAGE CONTENT-->
            <div class="tabbable tabbable-custom boxless">
                <div class="tab-content">
                    <div class="tab-pane active" id="tab_1">                        
                        <div class="responsive" data-tablet="span12 fix-offset" data-desktop="span6">
                            <!-- BEGIN EXAMPLE TABLE PORTLET--> 
                         <asp:UpdatePanel ID="uplDdl" runat="server" UpdateMode="Conditional">
                           <ContentTemplate> 
                            <div class="col-md-2">
                                        <div class="form-group">
                                            <label><b>Region:</b></label>
                                            <div>
                                                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                          </ContentTemplate>
                        </asp:UpdatePanel>
                            <div class="col-md-12">
                                <asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate> 
                                        <asp:HiddenField ID="hidType" runat="server"></asp:HiddenField>
                                        <asp:GridView ID="GridViewTable" runat="server" CssClass="table table-striped table-bordered table-hover"
                                            FooterStyle="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false" 
                                            AllowSorting="true" EmptyDataText="No Data is Available" OnRowDataBound="GridViewTable_RowDataBound" >
                                            <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowIndex" runat="server" />
                                                        <asp:HiddenField ID="hdnAction" runat="server"></asp:HiddenField>
                                                        <asp:HiddenField ID="hidCreatedDate" runat="server"></asp:HiddenField>
                                                        <asp:HiddenField ID="hidSuspent" runat="server"></asp:HiddenField>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control text-uppercase" MaxLength="2"/>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" MaxLength="120"/>
                                                    </ItemTemplate>                                                    
                                                </asp:TemplateField>                                               
                                                <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>                                                        
                                                       <%-- <asp:LinkButton ID="lnkDelete" CssClass="btn btn-xs btn-danger"
                                                            ToolTip="Click here to Delete" runat="server" OnClick="lnkDelete_Click"><i class="icon-trash"></i> Delete</asp:LinkButton>--%>
                                                        <asp:LinkButton ID="lnkBan" ToolTip="Click here to Ban"  runat="server"  CssClass="btn btn-xs btn-default" OnClick="lnkBan_Click"><i class="icon-ban-circle"></i> Ban</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView> 
                                    </ContentTemplate>
                                </asp:UpdatePanel> 
                            </div>
                            <!-- END EXAMPLE TABLE PORTLET-->
                        </div>
                    </div>                
                </div>
            </div> 
        </div>         
        <!-- END PAGE CONTENT-->
    </section>
</asp:Content>
