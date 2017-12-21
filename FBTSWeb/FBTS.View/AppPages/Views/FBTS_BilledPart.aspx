<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_BilledPart.aspx.cs" Inherits="FBTS.View.AppPages.Views.FBTS_BilledPart" %>
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
                                        <h4 runat="server" id="pageTitle"><b>Billed Part</b></h4> 
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
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label><b>Part Type</b></label>
                                            <div>
                                                <asp:DropDownList ID="ddlPartType" runat="server" CssClass="form-control chosen">
                                                    </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
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
                                     <div class="col-md-1 pt25">
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
                        <asp:GridView ID="GVListData" runat="server" CssClass="table table-striped table-bordered table-hover" HeaderStyle-CssClass="FixedHeader"
                        AllowPaging="true" PageSize="10" FooterStyle="table table-bordered table-striped table-condensed" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                        EmptyDataText="No Data is Available" OnRowDataBound="GVListData_RowDataBound" OnPageIndexChanging="GVListData_PageIndexChanging">
                        <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Sl#">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowIndex" runat="server" />
                                    <asp:HiddenField ID="hidCustCode" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hidAddressCode" runat="server"></asp:HiddenField>    
                                    <asp:HiddenField ID="hidAmdno" runat="server" />                                   
                                </ItemTemplate>
                                <ItemStyle Width="3%"></ItemStyle>
                            </asp:TemplateField>                                  
                            <asp:BoundField DataField="orderHead.OrderNumber" HeaderText="SR#" ItemStyle-Width="10%"></asp:BoundField>                             
                            <asp:BoundField DataField="orderHead.RelatedSR" HeaderText="Related SR#" ItemStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="orderHead.OrderDate" HeaderText="Inv Date" ItemStyle-Width="10%" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                            <asp:BoundField DataField="orderHead.Customer.Name" HeaderText="Customer/Dealer" ItemStyle-Width="30%"></asp:BoundField> 
                            <asp:BoundField DataField="orderHead.WarehouseFrom" HeaderText="Requested Location" ItemStyle-Width="20%"></asp:BoundField> 
                            <asp:BoundField DataField="OrderDetails[0].PartDetail.PartNumber" HeaderText="Part#" ItemStyle-Width="30%"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].PartDetail.Description" HeaderText="Part Description" ItemStyle-Width="40%"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].MaterialGroup.Description" HeaderText="Part Type" ItemStyle-Width="30%"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].MaterialType.Description" HeaderText="Category" ItemStyle-Width="20%"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].Quantity" HeaderText="Qty" ItemStyle-Width="30%" DataFormatString="{0:n0}"></asp:BoundField>

                            <asp:BoundField DataField="OrderDetails[0].Rate" HeaderText="Unit Value" ItemStyle-Width="30%" DataFormatString="{0:n0}"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].Value" HeaderText="Total Value" ItemStyle-Width="30%" DataFormatString="{0:n0}"></asp:BoundField>
                            <asp:BoundField DataField="orderHead.WarehouseTo" HeaderText="Billing Location" ItemStyle-Width="30%"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].ModalityDesp" HeaderText="Modality" ItemStyle-Width="30%"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].GPRSStatus" HeaderText="Docket#" ItemStyle-Width="30%"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].Off" HeaderText="Invoice#" ItemStyle-Width="30%"></asp:BoundField>

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
</asp:Content>
