<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_UOMMaster.aspx.cs" Inherits="Ezy.ERP.View.AppPages.Ezy_UOMMaster" %>
 
<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="ezyuc" TagName="CustomMessageControl" %>
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
    	                        showCustomMessage("Validation Failed", "Id at Row -> " + Row + "  : Cannot be Blank.", "Error");
	                          else if (i == 2)
    	                        showCustomMessage("Validation Failed", "Description at Row -> " + Row + "  : Cannot be Blank.", "Error");	                        
	                        jvRowData[0].focus();
	                        return false;
	                    }
	                    else {
	                        jvLen = jvString.length;
    	                    if (jvLen < 2) {
    	                        showCustomMessage("Input is Short", "Provided Value to this field is too short....\n\n Should 2 characters are expected.", "Error");
	                            jvRowData[0].focus();
	                            return false;
	                        }
	                        else {
	                            jvEditedvalue = jvString.toLowerCase();
    	                        for (var Row1 = 1; Row1 < parseInt(TabLength) ; Row1++) {
	                                if (i == 1) { jvMsgString = "Unit Id"; }
	                                else if (i == 2) { jvMsgString = "Description"; }
	                                jvRowData1 = document.getElementById(tabID).rows[Row1].cells[i].children;
    	                            jvString1 = TrimAll(jvRowData1[0].value);
    	                            if ((jvString1.toLowerCase() == jvEditedvalue) && (Row != Row1)) {
    	                                showCustomMessage("Duplicate Value",jvMsgString + " Already Exist","Error");
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
    <header class="panel-heading no-b">
        <h4 runat="server">Unit Master</h4>
    </header>
    <!-- END PAGE HEADER-->
    <div class="panel-body pt0" > 
    <!-- BEGIN PAGE CONTENT-->
        <div class="tabbable tabbable-custom boxless">             
            <div class="tab-content">
                <div class="tab-pane active" > 
    <ezyuc:CustomMessageControl runat="server" id="CustomMessageControl" />
                    <asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
                    <ContentTemplate> 
                         <div class="row pb15" >                             
                            <div class="col-md-12">
                                <div class="pull-right">
                               
                                <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-sm btn-success mr5" OnClientClick="return validate();" OnClick="lnkSave_Click"><i class="icon-save"></i> Save</asp:LinkButton>                                       
                             
                                <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-sm btn-info mr5" OnClientClick="showWaitModalPopup();" OnClick="lnkAddNew_Click"><i class="icon-plus"></i> Add</asp:LinkButton>                                                                   
                            
                                
                             </div>
                             </div>       
                             </div>
                        <!-- BEGIN EXAMPLE TABLE PORTLET-->                                  
                        <div class="row">                               
                         <div class="col-md-12">                           
                                        <asp:GridView ID="GridViewTable" runat="server" CssClass="table table-striped table-bordered table-hover"
                                                        FooterStyle="table table-striped table-bordered table-hover" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                                        EmptyDataText="No Data is Available" OnRowDataBound="GridViewTable_OnRowDataBoound">
                                            <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl #">
                                                    <ItemTemplate>
                                                      <asp:Label ID="lblRowIndex" runat="server" />
                                                      <asp:HiddenField ID="hdnAction" runat="server"></asp:HiddenField>
                                                      <asp:HiddenField ID="hidCreatedDate" runat="server"></asp:HiddenField>
                                                      <asp:HiddenField ID="hidSuspend" runat="server"></asp:HiddenField>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%"></ItemStyle>
                                                </asp:TemplateField>  
                                                <asp:TemplateField HeaderText="Id">
                                                    <ItemTemplate>
                                                      <asp:TextBox ID="txtPrimary" runat="server" class="form-control uperCase" onkeypress="return validentry('A',event);" placeholder="Id" MaxLength="2" ></asp:TextBox>
                                                    </ItemTemplate>
                                                   <ItemStyle Width="15%"></ItemStyle>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                      <asp:TextBox ID="txtSecondary" runat="server" class="form-control uperCase" onkeypress="return validentry('A',event);" placeholder="Description" MaxLength="10" ></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="65%"></ItemStyle>
                                                </asp:TemplateField>                                                                                                                 
                                                <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>                               
                                                           <%-- <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-xs btn-danger mr5"><i class="icon-trash"></i>Delete</asp:LinkButton>--%>
                                                            <asp:LinkButton ID="lnkBan" runat="server" CssClass="btn btn-xs btn-default" OnClick="lnkBan_Click"><i class="icon-ban-circle"></i>Ban</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%"></ItemStyle>
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
    </section>
</asp:Content>