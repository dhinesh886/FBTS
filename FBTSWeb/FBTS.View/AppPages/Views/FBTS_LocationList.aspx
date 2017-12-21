<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_LocationList.aspx.cs" Inherits="FBTS.View.AppPages.Views.FBTS_LocationList" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Import Namespace="FBTS.Model.Common" %>
<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="uc1" TagName="CustomMessageControl" %>
<%@ Register Src="~/UserControls/Common/TransactionStageControl.ascx" TagPrefix="uc1" TagName="TransactionStageControl" %>
<%@ Register Src="~/UserControls/ForecastingCommon/ForecastingGridViewListControl.ascx" TagPrefix="uc1" TagName="ForecastingGridViewListControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <script type="text/javascript">
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
          });
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
                <uc1:CustomMessageControl runat="server" id="CustomMessageControl" />
                <div class="tab-content">
                    <div class="row">
                        <asp:UpdatePanel ID="uplActions" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                              <%--  <div class="col-md-12">
                                    <header class="panel-heading no-b">
                                        <h4 runat="server" id="pageTitle"><b>Billed Part View</b></h4> 
                                    </header>
                                </div> --%>                                                            
                                <%--<div class="col-md-12 pt15" runat="server" id="divDate">                                                                                                                                                       
                                    <div class="col-md-2 pt25">
                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="btn btn-sm btn-info"><i class="ti-files"></i> Generate</asp:LinkButton>
                                    </div>                                                                                                                                
                                </div>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane active" id="tab1">
                        <asp:UpdatePanel ID="uplForm" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>                                             
                                <div class="row p10">
                                   <div class="col-md-12" style="padding-top:5px;">
                                       <rsweb:ReportViewer ID="LocationViewer" runat="server" Width="100%"></rsweb:ReportViewer>
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
