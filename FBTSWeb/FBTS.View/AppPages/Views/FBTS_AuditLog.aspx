<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_AuditLog.aspx.cs" Inherits="FBTS.View.AppPages.Views.FBTS_AuditLog" %>
<%@ Register Src="~/UserControls/Common/TransactionStageControl.ascx" TagPrefix="uc1" TagName="TransactionStageControl" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="uc1" TagName="CustomMessageControl" %>

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

          function validate() {             
              if (!validateMandatoryField($('#<%=txtSR.ClientID%>'), "Please Enter SR#!!")) return false;
              return true;
          }

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
                <uc1:CustomMessageControl runat="server" ID="CustomMessageControl" />
                <div class="tab-content">
                    <div class="row">
                        <asp:UpdatePanel ID="uplActions" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-md-12">
                                    <header class="panel-heading no-b">
                                        <h4 runat="server" id="pageTitle"><b>Audit Log</b></h4> 
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
                                    <div class="col-md-2 pl25" id="divTxt" runat="server">
                                        <div class="form-group">
                                            <label><b>SR#:</b></label>
                                            <div>
                                                <asp:TextBox ID="txtSR" runat="server" CssClass="form-control" placeholder="SR#" MaxLength="12"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>   
                                    <%--<div class="col-md-2">
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
                                    </div>  --%>                                                                              
                                    <div class="col-md-1 pt25">
                                        <asp:LinkButton ID="lnkView" runat="server" CssClass="btn btn-sm btn-info" OnClick="lnkView_Click" OnClientClick="return validate();"><i class="ti-files"></i> Generate</asp:LinkButton>
                                    </div>                                        
                                   </div>                                                                                                                                              
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="tab-pane active" id="tab1">
                        <asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>                                             
                                <div class="row p10">
                                   <div class="col-md-12" style="padding-top:5px;">
                        <asp:GridView ID="GVListData" runat="server" CssClass="table table-striped table-bordered table-hover" HeaderStyle-CssClass="FixedHeader"
                        AllowPaging="true" PageSize="15" FooterStyle="table table-bordered table-striped table-condensed" Width="100%" AutoGenerateColumns="false" AllowSorting="true"
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
                            <asp:BoundField DataField="OrderDetails[0].GPRSStatus" HeaderText="Stage" ItemStyle-Width="10%"></asp:BoundField>     
                            <%--<asp:BoundField DataField="orderHead.RelatedSR" HeaderText="Related SR#" ItemStyle-Width="10%"></asp:BoundField>--%> 
                            <asp:BoundField DataField="orderHead.OrderDate" HeaderText="Process Date" ItemStyle-Width="10%" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                           <%-- <asp:BoundField DataField="orderHead.Aging" HeaderText="Ageing" ItemStyle-Width="10%" DataFormatString="{0:n0}"></asp:BoundField>--%>
                            <asp:BoundField DataField="LoggedUserName" HeaderText="Modified By" ItemStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="orderHead.ProcessingDate" HeaderText="Modified On" ItemStyle-Width="10%" DataFormatString="{0:dd/MM/yyyy hh:mm tt}"></asp:BoundField> 
                            <asp:BoundField DataField="OrderDetails[0].PartDetail.PartNumber" HeaderText="Part#" ItemStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="OrderDetails[0].PartDetail.Description" HeaderText="Part Description" ItemStyle-Width="30%"></asp:BoundField>                            
                            <asp:BoundField DataField="OrderDetails[0].Quantity" HeaderText="Qty" ItemStyle-Width="7%" DataFormatString="{0:n0}"></asp:BoundField>                           
                            <asp:BoundField DataField="OrderDetails[0].CurrentStatus" HeaderText="Current Status" ItemStyle-Width="30%"></asp:BoundField>                                                          
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
    </div>
</asp:Content>
