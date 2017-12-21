<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_CategoryMaster.aspx.cs" Inherits="FBTS.View.AppPages.CPanel.FBTS_CategoryMaster" %>

<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="uc1" TagName="CustomMessageControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">

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
                        if (jvLen <= 1) {
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
    <section class="panel">
    <!-- BEGIN PAGE HEADER-->
        
    <div class="row">
        <div class="col-md-6">
            <header class="panel-heading no-b">
                <h4 runat="server" id="pageTitle">Category Master</h4>
            </header>
        </div>
        <div class="col-md-6 ">
            <asp:UpdatePanel ID="uplActions" runat="server" UpdateMode="Conditional">
                <ContentTemplate> 
                    <div class="pull-right panel-body" runat="server" id="divActions"> 
                      <%--  <div class="btn-group">
                                        <a class="btn btn-success  btn-sm dropdown-toggle mr5"  href="#" data-toggle="dropdown"><i class="icon-cogs"></i> Tools
                                            <i class="icon-angle-down"></i></a>
                                        <ul class="dropdown-menu" role="menu">
                                            <li><asp:LinkButton ID="lnkPDF" runat="server" OnClick="lnkPDF_Click" Text="Save as PDf"></asp:LinkButton></li>
                                            <li><a href="#"><i class="icon-pencil"></i>Save as PDF</a></li>
                                            <li><a href="#"><i class="icon-trash"></i>Save as Excel</a></li>
                                            <li><a href="#"><i class="icon-print"></i>Print</a></li>
                                            <li class="divider"></li>
                                            <li><a href="#"><i class="icon-refresh"></i>Refresh</a></li>
                                        </ul>
                                    </div>--%>
                            <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-success btn-sm" OnClientClick="return validate();" OnClick="lnkSave_Click"><i class="icon-save mr5"></i>Save</asp:LinkButton>
                            <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-sm btn-info mr5" OnClientClick="return checkComponent();" OnClick="lnkAddNew_Click"><i class="icon-plus"></i> Add</asp:LinkButton>
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
                <div class="tab-pane active" >
                    <uc1:CustomMessageControl runat="server" id="CustomMessageControl" /> 
                    <asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
                    <ContentTemplate> 
                    <div class="row">
                                                            
                        <!-- BEGIN EXAMPLE TABLE PORTLET-->                                  
                        <div class="col-md-12">   
                            <asp:HiddenField ID="hidType" runat="server"></asp:HiddenField>                     
                            <asp:GridView ID="GridViewTable" runat="server" CssClass="table table-striped table-bordered table-hover"
                                FooterStyle="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                EmptyDataText="No Data is Available" OnRowDataBound="GridViewTable_OnRowDataBoound">
                                <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowIndex" runat="server" />
                                            <asp:HiddenField ID="hdnAction" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hidCreatedDate" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hidSuspent" runat="server"></asp:HiddenField>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtID" runat="server" class="form-control uperCase" onkeypress="return validentry('H',event);" placeholder="ID" MaxLength="8" ></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%"></ItemStyle>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDesp" runat="server" class="form-control"  placeholder="Description" MaxLength="25"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="35%"></ItemStyle>
                                    </asp:TemplateField>                                                                                         
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>                               
                                            <%-- <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-xs btn-danger mr5"><i class="icon-trash"></i>Delete</asp:LinkButton>--%>
                                            <asp:LinkButton ID="lnkBan" runat="server" CssClass="btn btn-xs btn-default" OnClick="lnkBan_Click"><i class="icon-ban-circle"></i>Ban</asp:LinkButton>   
                                        </ItemTemplate>
                                        <ItemStyle Width="10%"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>                                 
                        </div>
                        <!-- END EXAMPLE TABLE PORTLET-->
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
