<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="TxnStageFieldRequest.aspx.cs" Inherits="FBTS.View.TxnStageFieldRequest" %>

<%@ Register Src="~/UserControls/Common/TransactionStageControl.ascx" TagPrefix="uc1" TagName="TransactionStageControl" %> 
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
<div class="row">
    <div class="col-md-12">
        <div class="tabbable tabbable-custom">
            <uc1:TransactionStageControl runat="server" id="TransactionStageControl" />
            <div class="tab-content">
                Field Request Stage Content here 
            </div>
        </div>
    </div>
</div> 
</asp:Content>
