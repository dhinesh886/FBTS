<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_OrderDetails.aspx.cs" Inherits="FBTS.View.AppPages.Views.FBTS_OrderDetails" %>
<%@ Import Namespace="FBTS.Model.Common" %>
<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="uc1" TagName="CustomMessageControl" %>
<%@ Register Src="~/UserControls/Common/TransactionStageControl.ascx" TagPrefix="uc1" TagName="TransactionStageControl" %>
<%@ Register Src="~/UserControls/ForecastingCommon/ForecastingGridViewListControl.ascx" TagPrefix="uc1" TagName="ForecastingGridViewListControl" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
                                <div class="col-md-12 pt15" runat="server" id="divDate">
                                    <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label><b>Location:</b></label>
                                            <div>
                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <label><b>Category:</b></label>
                                            <div>
                                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control chosen" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label><b>Order Type:</b></label>
                                            <div>
                                                <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="form-control chosen" AutoPostBack="true">
                                                    <asp:ListItem Value="">Select Order Type</asp:ListItem>
                                                    <asp:ListItem Value="BO">Back Order</asp:ListItem>
                                                    <asp:ListItem Value="ST">Stock Transfer</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label><b>Type:</b></label>
                                            <div>
                                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control chosen" AutoPostBack="true">
                                                    <asp:ListItem Value="">Select Type</asp:ListItem>
                                                    <asp:ListItem Value="FI">Processed</asp:ListItem>
                                                    <asp:ListItem Value="IP">Pending</asp:ListItem>
                                                </asp:DropDownList>
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
                                    <div class="col-md-1 pt20 checkbox-inline" runat="server">                                       
                                        <asp:CheckBox ID="chkSearch" runat="server"  AutoPostBack="true" Text="Based On SR#" OnCheckedChanged="chkSearch_CheckedChanged"></asp:CheckBox>
                                    </div>           
                                    <div class="col-md-2" id="divTxt" runat="server" visible="false">
                                        <div class="form-group">
                                            <label><b>SR#:</b></label>
                                            <div>
                                                <asp:TextBox ID="txtSR" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div> 
                                    <div class="row">
                                   <div>
                                    
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
                                       <rsweb:ReportViewer ID="OrderViewer" runat="server" Width="100%"></rsweb:ReportViewer>
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
