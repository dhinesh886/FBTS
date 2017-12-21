<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="FBTS_DeleteAllTransactionData.aspx.cs" Inherits="FBTS.View.AppPages.CPanel.FBTS_DeleteAllTransactionData" %>

<%@ Register Src="~/UserControls/Common/CustomMessageControl.ascx" TagPrefix="uc1" TagName="CustomMessageControl" %>
<%@ Register Src="~/UserControls/Common/DeleteAndBanPopup.ascx" TagPrefix="uc1" TagName="DeleteAndBanPopup" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
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
    <section class="panel">
        <uc1:DeleteAndBanPopup runat="server" ID="DeleteAndBanPopup" />
        <uc1:CustomMessageControl runat="server" id="CustomMessageControl" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
               
              
                <div class="row">
                    <div class="col-md-12">
                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-sm btn-danger" OnClick="lnkDelete_Click">
                            <i class="icon-trash"></i> Delete
                        </asp:LinkButton>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </section>
</asp:Content>
