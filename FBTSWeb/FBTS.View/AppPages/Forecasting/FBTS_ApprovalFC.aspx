<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_ApprovalFC.aspx.cs" Inherits="FBTS.View.AppPages.Forecasting.FBTS_ApprovalFC" %>

<%@ Import Namespace="FBTS.Model.Common" %>
<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="uc1" TagName="CustomMessageControl" %>
<%@ Register Src="~/UserControls/Common/TransactionStageControl.ascx" TagPrefix="uc1" TagName="TransactionStageControl" %>
<%@ Register Src="~/UserControls/ForecastingCommon/ForecastingGridViewListControl.ascx" TagPrefix="uc1" TagName="ForecastingGridViewListControl" %>
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
    </script>
    <uc1:TransactionStageControl runat="server" id="TransactionStageControlId" />
    <uc1:CustomMessageControl runat="server" id="CustomMessageControl" />
    <div class="row">
        <header class="panel-heading no-b">                                        
            <h4 runat="server" id="pageTitle"><b>Approval FC</b></h4>
        </header>   
    </div>    
    <div class="row">   
             
            <asp:UpdatePanel ID="uplForm" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc1:ForecastingGridViewListControl runat="server" id="ForecastingGridViewListControlId" />
                </ContentTemplate>
            </asp:UpdatePanel>
        
    </div>
</asp:Content>
