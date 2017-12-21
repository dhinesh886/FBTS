<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DynamicGridControl.ascx.cs" Inherits="FBTS.View.UserControls.ForecastingCommon.DynamicGridControl" %>
  
    <div class="row">
        <div class="col-md-12">
            <asp:HiddenField ID="hidActionName" runat="server" />
                <div class="no-more-tables">
                    <asp:HiddenField ID="hidOrderNumber" runat="server"></asp:HiddenField>  
                    <asp:HiddenField ID="hidAmdno" runat="server" />                     
                    <asp:GridView ID="GVListData" runat="server" CssClass="table table-striped table-bordered table-hover"
                        AllowPaging="true" PageSize="10" FooterStyle="table table-bordered table-striped table-condensed" Width="100%" AutoGenerateColumns="true" AllowSorting="true"
                        EmptyDataText="No Data is Available" OnRowDataBound="GVListData_RowDataBound" OnPageIndexChanging="GVListData_PageIndexChanging">
                        <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>                     
                </asp:GridView>  
            </div>
        </div>
    </div>