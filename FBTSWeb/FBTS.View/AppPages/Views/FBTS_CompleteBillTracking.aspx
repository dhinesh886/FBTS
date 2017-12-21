<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_CompleteBillTracking.aspx.cs" Inherits="FBTS.View.AppPages.Views.FBTS_CompleteBillTracking" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/Common/TransactionStageControl.ascx" TagPrefix="uc1" TagName="TransactionStageControl" %>
<%@ Register Src="~/UserControls/ForecastingCommon/ForecastingGridViewListControl.ascx" TagPrefix="uc1" TagName="ForecastingGridViewListControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <script>
          function pageLoad() {
              $('.datepicker').datepicker({
                  format: "dd/mm/yyyy"
              });
              $('.chosen').chosen();
              $('.chosen-container').css('width', '100%');

          }


          // Retains the calender after Postback        
          $(document).ready(function () {
              //tagTabLinkAttributes();
              Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
              Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

              $('.datepicker').datepicker({
                  format: "dd/mm/yyyy"
              });
          });

          <%--function validate() {
              if (!validateMandatoryField($('#<%=txtSR.ClientID%>'), "Please Enter SR#!!")) return false;
              return true;
          }--%>

          function BeginRequestHandler(sender, args) {
              var elem = args.get_postBackElement();
              ShowPreLoader();
          }

          function EndRequestHandler(sender, args) {
              HidePreLoader();
          }

          function dateCheck() {

          }
    </script>
    <div class="row">
        <div class="col-md-12">
            <div class="tabbable tabbable-custom">
                <uc1:TransactionStageControl runat="server" id="TransactionStageControlId" />
                <div class="tab-content">
                    <div class="row">
                        <asp:UpdatePanel ID="uplActions" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-md-12">
                                    <header class="panel-heading no-b">
                                        <h4 runat="server" id="pageTitle"><b>Bill Tracking</b></h4> 
                                    </header>
                                </div> 
                                <div class="col-md-12 pt15" runat="server" id="divDate">   
                                    <div class="row">     
                                    <%--<div class="col-md-2">
                                        <div class="form-group">
                                            <label><b>Location:</b></label>
                                            <div>
                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>--%>                      
                                    <%--<div class="col-md-2">
                                        <div class="form-group">
                                            <label><b>Teams:</b></label>
                                            <div>
                                                <asp:DropDownList ID="ddlStage" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStage_SelectedIndexChanged">                                                   
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-2" id="divTxt" runat="server">
                                        <div class="form-group">
                                            <label><b>SR#:</b></label>
                                            <div>
                                                <asp:TextBox ID="txtSR" runat="server" CssClass="form-control" placeholder="SR#"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>   
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label><b>From Date</b></label>
                                            <div>
                                                <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control datepicker" placeholder="From Date"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>    
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label><b>To Date</b></label>
                                            <div>
                                                <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control datepicker" placeholder="To Date"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>                                                                                
                                    <div class="col-md-1 pt25">
                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="btn btn-sm btn-info" OnClick="lnkView_Click"><i class="ti-files"></i> Generate</asp:LinkButton>
                                    </div>                                        
                                   </div>                                                                                                                                              
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane active" id="tab1">
                        <asp:UpdatePanel ID="uplForm" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>                                             
                                <div class="row p10">
                                   <div class="col-md-12" style="padding-top:5px;">
                                       <rsweb:ReportViewer ID="BillTrackingViewer" runat="server" Width="100%" Height="600px"></rsweb:ReportViewer>
                                   </div>
                                </div>                                                 
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
