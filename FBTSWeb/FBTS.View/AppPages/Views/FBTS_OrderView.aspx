<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_OrderView.aspx.cs" Inherits="FBTS.View.AppPages.Views.FBTS_OrderView" %>
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
             $('.datepicker').datepicker({
                 format: "dd/mm/yyyy"
             });
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
                                        <h4 runat="server" id="pageTitle"><b>Order Detail View</b></h4> 
                                    </header>
                                </div>                                                            
                                <div class="col-md-12 pt15" runat="server" id="divDate">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label><b>SR#</b></label>
                                            <div>
                                                <asp:TextBox runat="server" ID="txtSRSearch" CssClass="form-control" placeholder="SR#"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>    
                                    <%--<div class="col-md-2">
                                        <div class="form-group">
                                            <label><b>Region</b></label>
                                            <div>
                                                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" AutoPostBack="true">
                                                </asp:DropDownList>                   
                                            </div>
                                        </div>
                                    </div> --%>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label><b>Modality</b></label>
                                            <div>
                                                <asp:DropDownList ID="ddlModality" runat="server" CssClass="form-control" AutoPostBack="true">
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
                                     <div class="col-md-2 pt25">
                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="btn btn-sm btn-info" OnClick="lnkView_Click"><i class="ti-files"></i> View</asp:LinkButton>
                                    </div>                                                                                                                               
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                        <asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row p10">
                                <div class="col-md-12" style="width:100%; height:500px; overflow:auto;">                                
                                   <%-- <uc1:ForecastingGridViewListControl runat="server" id="ForecastingGridViewListControlId" />--%> 
                        <asp:GridView ID="GVListData" runat="server" CssClass="table table-striped table-bordered table-hover" HeaderStyle-CssClass="FixedHeader"
                        AllowPaging="true" PageSize="10" FooterStyle="table table-bordered table-striped table-condensed" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                        EmptyDataText="No Data is Available" OnRowDataBound="GVListData_RowDataBound" OnPageIndexChanging="GVListData_PageIndexChanging">
                        <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Sl#">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowIndex" runat="server" />                                                                  
                                </ItemTemplate>
                                <ItemStyle Width="3%"></ItemStyle>
                            </asp:TemplateField>                                  
                            <asp:BoundField DataField="orderHead.OrderNumber" HeaderText="SR#" ItemStyle-Width="10%"></asp:BoundField>                             
                            <asp:BoundField DataField="orderHead.RelatedSR" HeaderText="Related SR#" ItemStyle-Width="10%"></asp:BoundField> 
                            <asp:BoundField DataField="orderHead.OrderDate" HeaderText="Date" ItemStyle-Width="10%" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                            <asp:BoundField DataField="orderHead.Aging" HeaderText="Ageing" ItemStyle-Width="10%" DataFormatString="{0:n0}"></asp:BoundField>
                            <asp:BoundField DataField="orderHead.Customer.Name" HeaderText="Customer" ItemStyle-Width="30%"></asp:BoundField> 
                            <asp:BoundField DataField="orderHead.WarehouseFrom" HeaderText="Reqested Location" ItemStyle-Width="20%"></asp:BoundField> 
                            <asp:BoundField DataField="OrderDetails[0].PartDetail.PartNumber" HeaderText="Part#" ItemStyle-Width="35%"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].PartDetail.Description" HeaderText="Part Description" ItemStyle-Width="30%"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].MaterialGroup.Description" HeaderText="Part Type" ItemStyle-Width="30%"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].MaterialType.Description" HeaderText="Category" ItemStyle-Width="20%"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].Quantity" HeaderText="Qty" ItemStyle-Width="30%" DataFormatString="{0:n0}"></asp:BoundField>

                            <asp:BoundField DataField="OrderDetails[0].Rate" HeaderText="Unit Value" ItemStyle-Width="30%" DataFormatString="{0:n0}"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].Value" HeaderText="Total Value" ItemStyle-Width="30%" DataFormatString="{0:n0}"></asp:BoundField>
                            <%--<asp:BoundField DataField="orderHead.WarehouseTo" HeaderText="Billing Location" ItemStyle-Width="30%" DataFormatString="{0:n0}"></asp:BoundField>--%>
                            <asp:BoundField DataField="OrderDetails[0].ModalityDesp" HeaderText="Modality" ItemStyle-Width="30%" DataFormatString="{0:n0}"></asp:BoundField>
                            <%--<asp:BoundField DataField="OrderDetails[0].LogisticOrderNumber" HeaderText="Order#" ItemStyle-Width="30%"></asp:BoundField>--%>
                            <asp:BoundField DataField="OrderDetails[0].CurrentStatus" HeaderText="In Order Team"  Visible="false"></asp:BoundField>  
                            <asp:BoundField DataField="OrderDetails[0].SQuantity" HeaderText="Remaining Qty"  DataFormatString="{0:0.0}" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].GPRSStatus" HeaderText="In GPRS/C09 Team"  Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].ShQuantity" HeaderText="Remaining Qty"  DataFormatString="{0:0.0}" Visible="false"></asp:BoundField>

                            <asp:TemplateField HeaderText="Order Team">
                            <ItemTemplate>
                             <%# Eval("OrderDetails[0].CurrentStatus") %> <%# "Qty" %> <%# Eval("OrderDetails[0].SQuantity") %>
                            </ItemTemplate>
                            <ItemStyle Width="30%"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="GPRS/C09 Team">
                            <ItemTemplate>
                             <%# Eval("OrderDetails[0].GPRSStatus") %> <%# "Qty" %> <%# Eval("OrderDetails[0].ShQuantity") %>
                            </ItemTemplate>
                            <ItemStyle Width="30%"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>                             
                                </div> 
                                    </div>       
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
<%--    <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }
    </style>--%>
</asp:Content>
