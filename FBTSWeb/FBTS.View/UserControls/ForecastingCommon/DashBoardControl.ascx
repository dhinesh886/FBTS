<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashBoardControl.ascx.cs" Inherits="FBTS.View.UserControls.ForecastingCommon.DashBoardControl" %>

<section class="panel pl5 pr5"> 
    <div class="panel panel-default ">
        <header class="panel-heading" runat="server" id="divheader"><b>GPRS</b></header>
        <div class="panel-body ">              
            <div class="row">
                <div class="col-md-12">
                    <div class="no-more-tables">                                       
                        <asp:GridView ID="GVListData" runat="server" CssClass="table table-striped table-bordered table-hover"
                            AllowPaging="true" PageSize="100" FooterStyle="table table-bordered table-striped table-condensed" Width="100%" AutoGenerateColumns="false" 
                            AllowSorting="true" OnRowDataBound="GVListData_RowDataBound"
                            EmptyDataText="No Data is Available">
                            <PagerStyle CssClass="dataTables_pager" HorizontalAlign="Right" VerticalAlign="Middle"></PagerStyle>
                            <Columns>                              
                                <asp:BoundField DataField="Description" HeaderText="Status" ItemStyle-Width="40%"></asp:BoundField>                             
                                <asp:BoundField DataField="LessThen7days" HeaderText="< 7 Days" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"></asp:BoundField> 
                                <asp:BoundField DataField="LessThen7To15days" HeaderText="7 to 15 Days" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="LessThen15To30days" HeaderText="15 to 30 Days" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"></asp:BoundField> 
                                <asp:BoundField DataField="LessThen30To60days" HeaderText="30 to 60 Days" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>    
                                <asp:BoundField DataField="MoreThen60days" HeaderText="> 60 Days" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"></asp:BoundField> 
                                <asp:BoundField DataField="GrandTotal" HeaderText="Grand Total" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"></asp:BoundField>                                                         
                            </Columns>
                        </asp:GridView>  
                    </div>
                </div>
            </div>
        </div>
    </div>
 </section> 