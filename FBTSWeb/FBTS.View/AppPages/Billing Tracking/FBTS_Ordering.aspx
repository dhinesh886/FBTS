<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_Ordering.aspx.cs" Inherits="FBTS.View.AppPages.Billing_Tracking.FBTS_Ordering"%>


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
    <link href="../../Resources/UI/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../Resources/UI/js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        function pageLoad() {
            mulitipleDdlCss();
        }
        // Retains the calender after Postback        
        $(document).ready(function () {
                //tagTabLinkAttributes();
                
                Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                mulitipleDdlCss();
            });
            function BeginRequestHandler(sender, args) {
                var elem = args.get_postBackElement();
                ShowPreLoader();
            }

            function EndRequestHandler(sender, args) {
                HidePreLoader();
                mulitipleDdlCss();
            }

            function jsValidateForm() {
                
               
                if (!jsValidateHeader<%=ForecastingControlId.ClientID%>()) return false;
                if (!validateGrid<%=BillingPartControlId.ClientID%>()) return false;                
                
                var FormType = $('#<%=hidType.ClientID%>')   
                
                if (FormType[0].value == 'DB') {
                    if (!jsValidateStatus<%=ForecastingControlId.ClientID%>()) return false;
                }

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

        function getConfirmation() {
            var retVal = confirm("Do you want to continue ?");
            if (retVal == true) {
                //document.write("User wants to continue!");
                return true;
            }
            else {
                //document.write("User does not want to continue!");
                return false;
            }
        }
        function validateAlternativePart() {
            
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
                                <div class="col-md-3 pt15" runat="server" id="divFilterOrder" style="display:none">
                                    <asp:DropDownList ID="ddlfilterOrder" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlfilterOrder_SelectedIndexChanged">
                                        <asp:ListItem Value="FO">Fresh Order</asp:ListItem>
                                        <asp:ListItem Value="FD">Fresh Deviation Order</asp:ListItem>
                                        <asp:ListItem Value="PO">Pending Order</asp:ListItem>                                      
                                        <asp:ListItem Value="DO">Deviation Order</asp:ListItem>
                                        <asp:ListItem Value="GP">Sent Back from GSPO</asp:ListItem>
                                        <asp:ListItem Value="C0">Sent Back from C09</asp:ListItem>
                                        <asp:ListItem Value="PDO">Pending Deviation Order</asp:ListItem> 
                                        <asp:ListItem Value="AN">Alternative Part</asp:ListItem> 
                                    </asp:DropDownList>
                                </div>                                    
                                <div class="col-md-8 pt20">
                                    <div class="pull-right" id="divSave" runat="server" visible="false">
                                        <asp:LinkButton ID="lnkSave" runat="server" CssClass="btn btn-primary btn-sm " Text="Login" OnClientClick="return jsValidateForm();" OnClick="lnkSave_Click">
                                            <i class="icon-save mr5"></i> Save </asp:LinkButton>
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
                                    <uc1:MessageBox runat="server" id="ConfirmationPopup" />
                                    <uc1:ForecastingGridViewListControl runat="server" id="ForecastingGridViewListControlId" />
                                </div>                                                 
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
                                        <asp:HiddenField ID="hidOrd_No1" runat="server" />
                                        <asp:HiddenField ID="hidPOValue" runat="server" />
                                        <asp:HiddenField ID="hidType" runat="server" />
                                        <uc1:ForecastingControl runat="server" id="ForecastingControlId" />
                                    </div>                                
                                </div>
                                <div class="row" id="divValidation" runat="server">
                                    <div class="col-md-12">  
                                        <uc1:ValidationDdl runat="server" ID="ValidationDdlId" />    
                                    </div>                                
                                </div> 
                                <div class="row" runat="server" id="divstatuschange" style="display:none">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label><b>Status</b></label>
                                                <div>
                                                    <asp:DropDownList ID="ddlChangestatus" CssClass="form-control chosen" runat="server">
                                                        <asp:ListItem Value="">Select Status</asp:ListItem>
                                                        <asp:ListItem Value="BO">Back Order</asp:ListItem>                                 
                                                        <asp:ListItem Value="ST">Stock Transfer</asp:ListItem>
                                                        <asp:ListItem Value="WP">Wating for more part</asp:ListItem>
                                                        <asp:ListItem Value="EP">EOL Part</asp:ListItem>
                                                        <asp:ListItem Value="OH">On Hold</asp:ListItem>
                                                        <asp:ListItem Value="CN">Cancelled</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>                                        
                                    </div>
                                </div>
                                <div class="row" id="divPartforPending" runat="server" style="display:none">
                                       <asp:HiddenField ID="hidTMOrd_no" runat="server"></asp:HiddenField>
                                       <asp:HiddenField ID="hidTMStage" runat="server"></asp:HiddenField>
                                    <div class="col-md-12">   
                                        <uc1:BillingPartControl runat="server" id="BillingPartControlIdPending" />  
                                    </div>                                                    
                                </div> 
                                <div class="row" visible="false" runat="server" id="divTmReferences">
                                    
                                        <div class="col-md-2 pl25" runat="server" id="divPart"></div>
                                        <div class="col-md-offset-10 pull-right pr25">
                                            <asp:LinkButton ID="lnkSaveTmData" runat="server" CssClass="btn btn-primary btn-sm " Text="Login" OnClick="lnkSaveTmData_Click">
                                            <i class="icon-save mr5"></i> Save </asp:LinkButton>
                                            <asp:LinkButton ID="lnkCancelTmData" runat="server" CssClass="btn btn-default btn-sm " Text="Cancel" OnClick="lnkCancelTmData_Click">
                                            <i class="ui-icon-cancel mr5"></i> Cancel </asp:LinkButton>
                                        </div>                                       
                                   
                                    <div class="col-md-4" runat="server" id="divTmref1" visible="false">
                                        <uc1:RefrenceControl runat="server" id="RefrenceControlTM1"/>
                                    </div>    
                                    <div class="col-md-4"  runat="server" id="divTmref2" visible="false">
                                        <uc1:RefrenceControl runat="server" id="RefrenceControlTM2"/>
                                    </div>
                                    <div class="col-md-4" runat="server" id="divTmref3" visible="false">
                                        <uc1:RefrenceControl runat="server" id="RefrenceControlTM3"/>
                                    </div>  
                                     <div class="col-md-4" runat="server" id="divTmref4" visible="false">
                                        <uc1:RefrenceControl runat="server" id="RefrenceControlTM4" />
                                    </div>    
                                    <div class="col-md-4" runat="server" id="divTmref5" visible="false">
                                        <uc1:RefrenceControl runat="server" id="RefrenceControlTM5" />
                                    </div>
                                    <div class="col-md-4" runat="server" id="divTmref6" visible="false">
                                        <uc1:RefrenceControl runat="server" id="RefrenceControlTM6" />
                                    </div>  
                                </div>
                                <div class="row" id="divPartforFresh" runat="server">
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
                    <div class="tab-pane" id="tab3">
                        <asp:UpdatePanel ID="uplViewDetails" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row p10">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label><b>SR#</b></label>
                                            <div>
                                                <asp:TextBox runat="server" ID="txtSRSearch" CssClass="form-control" placeholder="SR#" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-1 pt25 pull-right">
                                        <asp:LinkButton ID="lnkViewBack" runat="server" CssClass="btn btn-sm btn-default" OnClientClick="openTab(1);" OnClick="lnkViewBack_Click" >
                                            <i class="icon-backward mr5"></i> Back </asp:LinkButton>  
                                    </div> 
                                    <div class="col-md-1 pt25 pull-right" visible="false">
                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="btn btn-sm btn-info" OnClick="lnkView_Click" Visible="false"><i class="ti-files"></i> View</asp:LinkButton>
                                    </div>                                      
                                    <div class="row p10">
                                        <div class="col-md-12" style="width: 100%; height: 500px; overflow: auto;">
                                            <%--<uc1:ForecastingGridViewListControl runat="server" id="ForecastingGridViewListControlId" />--%>
                                            <%--Using Column index to hide the column--%>

                                            <asp:GridView ID="GVListData2" runat="server" CssClass="table table-striped table-bordered table-hover" HeaderStyle-CssClass="FixedHeader"
                                                FooterStyle="table table-bordered table-striped table-condensed" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
                                                EmptyDataText="No Data is Available" OnRowDataBound="GVListData2_RowDataBound">
                                                <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowIndex" runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="3%"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="orderHead.OrderNumber" HeaderText="SR#" ItemStyle-Width="10%"></asp:BoundField>
                                                    <%--<asp:BoundField DataField="orderHead.RelatedSR" HeaderText="Related SR#" ItemStyle-Width="10%"></asp:BoundField>--%>
                                                    <asp:BoundField DataField="orderHead.OrderDate" HeaderText="Date" ItemStyle-Width="10%" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                                                    <asp:BoundField DataField="orderHead.Aging" HeaderText="Ageing" ItemStyle-Width="10%" DataFormatString="{0:n0}"></asp:BoundField>
                                                    <%--<asp:BoundField DataField="orderHead.Customer.Name" HeaderText="Customer" ItemStyle-Width="30%"></asp:BoundField>
                                                    <asp:BoundField DataField="orderHead.WarehouseFrom" HeaderText="Reqested Location" ItemStyle-Width="25%"></asp:BoundField>--%>
                                                    <asp:BoundField DataField="OrderDetails[0].PartDetail.PartNumber" HeaderText="Part#" ItemStyle-Width="30%"></asp:BoundField>
                                                    <asp:BoundField DataField="OrderDetails[0].PartDetail.Description" HeaderText="Part Description" ItemStyle-Width="30%"></asp:BoundField>
                                                    <asp:BoundField DataField="OrderDetails[0].MaterialGroup.Description" HeaderText="Part Type" ItemStyle-Width="30%"></asp:BoundField>
                                                    <asp:BoundField DataField="OrderDetails[0].MaterialType.Description" HeaderText="Category" ItemStyle-Width="20%"></asp:BoundField>
                                                    <asp:BoundField DataField="OrderDetails[0].Quantity" HeaderText="Qty" ItemStyle-Width="30%" DataFormatString="{0:n0}"></asp:BoundField>
                                                    <asp:BoundField DataField="OrderDetails[0].Rate" HeaderText="Unit Value" ItemStyle-Width="30%" DataFormatString="{0:n0}"></asp:BoundField>
                                                    <asp:BoundField DataField="OrderDetails[0].Value" HeaderText="Total Value" ItemStyle-Width="30%" DataFormatString="{0:n0}"></asp:BoundField>
                                                    <%--<asp:BoundField DataField="orderHead.WarehouseTo" HeaderText="Billing Location" ItemStyle-Width="30%" DataFormatString="{0:n0}"></asp:BoundField>--%>
                                                    <asp:BoundField DataField="OrderDetails[0].ModalityDesp" HeaderText="Modality" ItemStyle-Width="30%" DataFormatString="{0:n0}"></asp:BoundField>
                                                    <%--<asp:BoundField DataField="OrderDetails[0].LogisticOrderNumber" HeaderText="Order#" ItemStyle-Width="30%"></asp:BoundField>--%>
                                                    <asp:BoundField DataField="OrderDetails[0].CurrentStatus" HeaderText="In Order Team" Visible="false"></asp:BoundField>
                                                    <asp:BoundField DataField="OrderDetails[0].SQuantity" HeaderText="Remaining Qty" DataFormatString="{0:0.0}" Visible="false"></asp:BoundField>
                                                    <asp:BoundField DataField="OrderDetails[0].GPRSStatus" HeaderText="In GPRS/C09 Team" Visible="false"></asp:BoundField>
                                                    <asp:BoundField DataField="OrderDetails[0].ShQuantity" HeaderText="Remaining Qty" DataFormatString="{0:0.0}" Visible="false"></asp:BoundField>
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
                                                    <asp:BoundField DataField="orderHead.OrdRemark" HeaderText="Remark" Visible="false"></asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
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
        function openTab(tabId) {
            if (tabId == 1) {
                $("#tabhd1").addClass("active");
                $("#tab1").addClass("active");

                $("#tabhd2").removeClass("active");
                $("#tab2").removeClass("active");
                $("#tabhd_3").removeClass("active");
                $("#tab3").removeClass("active");
            }
            else if (tabId == 2) {
                $("#tabhd2").addClass("active");
                $("#tab2").addClass("active");

                $("#tabhd_1").removeClass("active");
                $("#tab1").removeClass("active");
                $("#tabhd_3").removeClass("active");
                $("#tab3").removeClass("active");
            }
            else if (tabId == 3) {
                $("#tabhd3").addClass("active");
                $("#tab3").addClass("active");

                $("#tabhd_1").removeClass("active");
                $("#tab1").removeClass("active");
                $("#tabhd_2").removeClass("active");
                $("#tab2").removeClass("active");
            }
        }
    </script>

</asp:Content>
