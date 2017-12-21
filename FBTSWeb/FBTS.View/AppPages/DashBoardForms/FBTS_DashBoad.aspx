<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_DashBoad.aspx.cs" Inherits="FBTS.View.AppPages.DashBoardForms.FBTS_DashBoad" %>

<%@ Register Src="~/UserControls/ForecastingCommon/DashBoardControl.ascx" TagPrefix="uc1" TagName="DashBoardControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-6">
            <uc1:DashBoardControl runat="server" ID="DashBoardControlGPRSId"/>
        </div>        
        <div class="col-md-6">
            <uc1:DashBoardControl runat="server" ID="DashBoardControlC09Id"/>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <uc1:DashBoardControl runat="server" ID="DashBoardControlXRayId"/>
        </div>
    </div>
</asp:Content>
