<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_VarificationTeam.aspx.cs" Inherits="FBTS.View.AppPages.Billing_Tracking.FBTS_VarificationTeam" %>

<%@ Import Namespace="FBTS.Model.Common" %>
<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="uc1" TagName="CustomMessageControl" %>
<%@ Register Src="~/UserControls/Common/TransactionStageControl.ascx" TagPrefix="uc1" TagName="TransactionStageControl" %> 
<%@ Register Src="~/UserControls/ForecastingCommon/ForecastingGridViewListControl.ascx" TagPrefix="uc1" TagName="ForecastingGridViewListControl" %>
<%@ Register Src="~/UserControls/ForecastingCommon/ForecastingControl.ascx" TagPrefix="uc1" TagName="ForecastingControl" %> 
<%@ Register Src="~/UserControls/ForecastingCommon/RefrenceControl.ascx" TagPrefix="uc1" TagName="RefrenceControl" %>
<%@ Register Src="~/UserControls/ForecastingCommon/ValidationDdl.ascx" TagPrefix="uc1" TagName="ValidationDdl" %>
<%@ Register Src="~/UserControls/ForecastingCommon/BillingPartControl.ascx" TagPrefix="uc1" TagName="BillingPartControl" %>
<%@ Register Src="~/UserControls/Common/ConfirmationPopup.ascx" TagPrefix="uc1" TagName="MessageBox" %> 


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        // Retains the calender after Postback        
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
                
                
                if (!jsValidateHeader<%=ForecastingControlId.ClientID%>()) return false;                                
                
                var FormType=$('#<%=hidType.ClientID%>')
                var ddlcurrentStatus = $('#<%=ForecastingControlId.FindControl("ddlCurrentStatus").ClientID %>'); 
                //FormType[0].value == 'DV' ||
               if (TrimAll(ddlcurrentStatus.val())=='OR') {
                    if (!jsValidate<%=ValidationDdlId.ClientID%>()) return false;
                    if (FormType[0].value == 'DV') {
                        if (!jsValidateStatus<%=ForecastingControlId.ClientID%>()) return false;
                    }
                }

                if (!validateGrid<%=BillingPartControlId.ClientID%>()) return false;

                var ref = "<%=divref1.ClientID%>";
                if ((document.getElementById(ref)) != null)
                    if (!jsValidateReferance<%=RefrenceControlId1.ClientID%>()) return false;

                ref = "<%=divref2.ClientID%>";
                if (document.getElementById(ref) != null)
                    if (!jsValidateReferance<%=RefrenceControlId2.ClientID%>()) return false;

                ref = "<%=divref3.ClientID%>";
                if (document.getElementById(ref) != null)
                    if (!jsValidateReferance<%=RefrenceControlId3.ClientID%>()) return false;

                ref = "<%=divref4.ClientID%>";
                if (document.getElementById(ref) != null)
                    if (!jsValidateReferance<%=RefrenceControlId4.ClientID%>()) return false;

                ref = "<%=divref5.ClientID%>";
                if (document.getElementById(ref) != null)
                    if (!jsValidateReferance<%=RefrenceControlId5.ClientID%>()) return false;

                ref = "<%=divref6.ClientID%>";
                if (document.getElementById(ref) != null)
                    if (!jsValidateReferance<%=RefrenceControlId6.ClientID%>()) return false;

                return true;
            }
    </script> 
    <div class="row">
        <div class="col-md-12">
            <div class="tabbable tabbable-custom">
                <uc1:TransactionStageControl runat="server" id="TransactionStageControlId" />
                <uc1:CustomMessageControl runat="server" id="CustomMessageControl" />
                <div class="tab-content">
                    <div class="row">
                        
                        <asp:UpdatePanel ID="uplActions" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-md-4">
                                    <header class="panel-heading no-b">
                                        <h4 runat="server" id="pageTitle"><b>Verification</b></h4> 
                                    </header>
                                </div>
                                <div class="col-md-3 pt10" runat="server" id="divStatus" visible="false">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">                                        
                                        <asp:ListItem Value="IP">Fresh</asp:ListItem>
                                        <asp:ListItem Value="PN">Pending</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-8 pt20">
                                    <div class="pull-right" id="divSave" runat="server" visible="false">
                                        <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-primary btn-sm " Text="Login" OnClientClick="return jsValidateForm();" OnClick="lnkSave_Click">
                                            <i class="icon-save mr5"></i> Save </asp:LinkButton>
                                        <asp:LinkButton ID="lnkBack" runat="server" CssClass="btn btn-sm btn-default" Text="Login"  OnClientClick="openTab(1);" OnClick="lnkBack_Click">
                                            <i class="icon-backward mr5"></i> Back </asp:LinkButton>
                                        <asp:LinkButton ID="lnkSendTo" runat="server" CssClass="btn btn-success btn-sm" Text="Login" OnClientClick="return jsValidateForm();" OnClick="lnkSendTo_Click" Visible="false">
                                             Send To <i class="icon-forward mr5"></i></asp:LinkButton>
                                    </div>
                                   <%-- <div class="col-md-12" id="divAdd" runat="server">--%>
                                        <div class="col-md-9" id="divAdd" runat="server">
                                            <%--<div class="pull-right">
                                                <div class="input-group mb15">
                                                    <span class="input-group-addon"> <i class="icon-search"></i></span>
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Enter a keyword to search"></asp:TextBox>               
                                                </div> 
                                            </div>--%>
                                        </div>
                                        <div class="col-md-3" id="divSearch" runat="server">                                      
                                            <div class="pull-right">                                                
                                                <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="openTab(2);" OnClick="lnkAddNew_Click"><i class="icon-plus"></i> Add</asp:LinkButton>
                                            </div>
                                        </div>  
                                    <%--</div>--%>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane active" id="tab1">                       
                        <asp:UpdatePanel ID="uplForm" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>                                
                                 <uc1:MessageBox runat="server" id="ConfirmationPopup" />
                                <uc1:ForecastingGridViewListControl runat="server" id="ForecastingGridViewListControlId" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane" id="tab2">                                          
                        <asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:HiddenField ID="hidAction" runat="server" />
                                        <asp:HiddenField ID="hidOrd_No" runat="server" />
                                        <asp:HiddenField ID="hidType" runat="server" />
                                        <uc1:ForecastingControl runat="server" id="ForecastingControlId" />
                                    </div>                                
                                </div>
                                <div class="row">
                                    <div class="col-md-12">  
                                        <uc1:ValidationDdl runat="server" ID="ValidationDdlId" />    
                                    </div>                                
                                </div> 
                                <div class="row" id="divUploadExcel" runat="server" style="display:none">                                    
                                    <div class="col-md-8 pl25">                 
                                        <div class="form-group">                         
                                            <label class="control-label pl10">Please Select Excel File</label>
                                            <div>
                                                <div class="col-md-7">
                                                     <ajaxToolkit:AsyncFileUpload runat="server" ID="fileExcelUpload" Width="400px" UploaderStyle="Modern"
                                                                            UploadingBackColor="#CCFFFF"   OnUploadedComplete="fileExcelUpload_UploadedComplete" CssClass="uploadinput"
                                                                        OnClientUploadComplete="uploadcomplete"/>     
                                                    <asp:HiddenField ID="hidFileName" runat="server" />                                                                                                                                                     
                                                </div>
                                              <div class="col-md-5 pb15">
                                                 <asp:LinkButton ID="lnkUpload" runat="server" CssClass="btn btn-default btn-sm mr5" OnClick="btnImport_Click">
                                                     Upload<i class="ti-upload ml5"></i> </asp:LinkButton>                                         
                                                 </div>                                                                                            
                                            </div>
                                        </div>                
                                    </div>
                                </div>                                 
                                <div class="row">
                                    <div class="col-md-12">                                                                 
                                        <uc1:BillingPartControl runat="server" id="BillingPartControlId" />  
                                    </div>                                
                                </div>   
                                <div class="row">
                                    <div class="col-md-4" runat="server" id="divref1" visible="false">
                                        <uc1:RefrenceControl runat="server" id="RefrenceControlId1"/>
                                    </div>    
                                    <div class="col-md-4"  runat="server" id="divref2" visible="false">
                                        <uc1:RefrenceControl runat="server" id="RefrenceControlId2"/>
                                    </div>
                                    <div class="col-md-4" runat="server" id="divref3" visible="false">
                                        <uc1:RefrenceControl runat="server" id="RefrenceControlId3"/>
                                    </div>                            
                                </div>   
                                <div class="row">
                                    <div class="col-md-4" runat="server" id="divref4" visible="false">
                                        <uc1:RefrenceControl runat="server" id="RefrenceControlId4" />
                                    </div>    
                                    <div class="col-md-4" runat="server" id="divref5" visible="false">
                                        <uc1:RefrenceControl runat="server" id="RefrenceControlId5" />
                                    </div>
                                    <div class="col-md-4" runat="server" id="divref6" visible="false">
                                        <uc1:RefrenceControl runat="server" id="RefrenceControlId6" />
                                    </div>                            
                                </div>                
                            </ContentTemplate>                           
                                                                        
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">    
       // AjaxControlToolkit.AsyncFileUpload.prototype.newFileName = null;
        function uploadcomplete(sender, e) {            
            $('#<%=hidFileName.ClientID%>')[0].value = e._fileName;          
        }
       
      <%-- function fetchFileame()
        {
            
            var file = $('#<%=ExcelfileUpload.ClientID%>');
            var hidfile = $('#<%=hidFileName.ClientID%>')
           var fileupload = $('#<%=ExcelfileUpload.ClientID%>')[0].files[0].name;
           var filePath = $('#<%=ExcelfileUpload.ClientID%>')[0].value;
           $('#<%=hidFileName.ClientID%>')[0].value = fileupload;
           $('#<%=hidFilePath.ClientID%>')[0].value = filePath;
        }--%>
        function openTab(tabId) {
            if (tabId == 1) {
                $("#tabhd1").addClass("active");
                $("#tab1").addClass("active");

                $("#tabhd2").removeClass("active");
                $("#tab2").removeClass("active");
            }
            else {
                $("#tabhd2").addClass("active");
                $("#tab2").addClass("active");

                $("#tabhd_1").removeClass("active");
                $("#tab1").removeClass("active");
            }
        }
    </script>
</asp:Content>
