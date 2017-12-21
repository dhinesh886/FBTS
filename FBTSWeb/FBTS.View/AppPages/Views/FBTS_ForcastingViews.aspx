<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_ForcastingViews.aspx.cs" Inherits="FBTS.View.AppPages.Views.FBTS_ForcastingViews" %>
<%@ Import Namespace="FBTS.Model.Common" %>
<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="uc1" TagName="CustomMessageControl" %>
<%@ Register Src="~/UserControls/Common/TransactionStageControl.ascx" TagPrefix="uc1" TagName="TransactionStageControl" %>
 <%@ Register Src="~/UserControls/ForecastingCommon/ForecastingGridViewListControl.ascx" TagPrefix="uc1" TagName="ForecastingGridViewListControl" %>
<%@ Register Src="~/UserControls/ForecastingCommon/DynamicGridControl.ascx" TagPrefix="uc1" TagName="DynamicGridControl" %>

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
         });
         function BeginRequestHandler(sender, args) {
             var elem = args.get_postBackElement();
             ShowPreLoader();
         }

         function EndRequestHandler(sender, args) {
             HidePreLoader(); 
         }
         function dateCheck()
         {
             
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
                                <div class="col-md-3">
                                    <header class="panel-heading no-b">
                                        <h4 runat="server" id="pageTitle"><b>Forecasting View</b></h4> 
                                    </header>
                                </div>                             
                                <div class="col-md-8 pt15" runat="server" id="divDate">
                                    <div class="col-md-2">
                                        <div class="pull-right">
                                            <label><b>From Date</b></label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control datepicker" MaxLength="10" placeholder="From Date"></asp:TextBox>
                                    </div>    
                                    <div class="col-md-2">
                                        <div class="pull-right">
                                            <label><b>To Date</b></label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control datepicker" MaxLength="10" onblur="" placeholder="To Date"></asp:TextBox>                                      
                                    </div>      
                                    <div class="col-md-2">
                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="btn btn-sm btn-success" OnClick="lnkView_Click" ><i class="fa-road"></i> View</asp:LinkButton>
                                    </div>                                                                                                                                
                                </div>
                                <div class="col-md-9" runat="server" id="divBack" visible="false">
                                    <div class="pull-right pt10">
                                      <%--  <div class="btn-group">
                                        <a class="btn btn-success  btn-sm dropdown-toggle"  href="#" data-toggle="dropdown">
                                            <i class="icon-cogs"></i> Tools
                                            <i class="icon-angle-down"></i>
                                        </a>
                                        <ul class="dropdown-menu" role="menu">
                                            <li><a href="#"><i class="icon-pencil"></i>Save as PDF</a></li>
                                            <li><a href="#"><i class="icon-trash" ></i>Save as Excel</a></li>
                                            <li><a href="#"><i class="icon-print"></i>Print</a></li>
                                            <li class="divider"></li>
                                            <li><a href="#"><i class="icon-refresh"></i>Refresh</a></li>
                                        </ul>
                                    </div>--%>
                                        <asp:LinkButton ID="lnkExpexcel" runat="server" CssClass="btn  btn-sm btn-success" Text="Login"  OnClick="lnkExpexcel_Click">
                                            <i class="icon-trash"></i> Export Excel </asp:LinkButton> 
                                        <asp:LinkButton ID="lnkBack" runat="server" CssClass="btn btn-sm btn-default" Text="Login"  OnClientClick="openTab(1);" OnClick="lnkBack_Click">
                                            <i class="icon-backward mr5"></i> Back </asp:LinkButton>  
                                    </div>                                    
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane active" id="tab1">
                        <asp:UpdatePanel ID="uplForm" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>                                             
                                <div class="row p10">
                                   <uc1:ForecastingGridViewListControl runat="server" id="ForecastingGridViewListControlId" />
                                </div>                                                 
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane" id="tab2">                         
                        <asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row p10">
                                    <%--<uc1:DynamicGridControl runat="server" ID="DynamicGridControlId" />  --%>
                                    <uc1:ForecastingGridViewListControl runat="server" id="ForecastingGridViewListControlViewId" />                            
                                </div>        
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
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
