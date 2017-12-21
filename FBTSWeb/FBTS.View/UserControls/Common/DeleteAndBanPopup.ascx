<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeleteAndBanPopup.ascx.cs" Inherits="FBTS.View.UserControls.Common.DeleteAndBanPopup" %>
<script>
    function showConfirmation() {
        //$('#myModal').appendTo("form:first");
        $('#myModal').modal('show');
    }
</script>
<style>
    .modal-backdrop {
        z-index: -1;
    }
</style>
<asp:UpdatePanel ID="uplConfMsg" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="modal fade bs-modal-sm" tabindex="-1" role="dialog" aria-hidden="true" id="myModal">
            <asp:HiddenField ID="hdnKey1" runat="server" />
            <asp:HiddenField ID="hdnKey2" runat="server" />
            <asp:HiddenField ID="hdnKey3" runat="server" />
            <asp:HiddenField ID="hdnType" runat="server" />
            <asp:HiddenField ID="hdnAction" runat="server" />
            <asp:HiddenField ID="hdnQueryType" runat="server" />
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 id="Header" class="modal-title" runat="server">Horizontal Form</h4>
                    </div>
                    <div class="modal-body">
                        <p id="divMessageBodyText" runat="server"></p>
                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <div class="col-md-offset-8 col-md-4">
                                <asp:Button ID="btnNo" runat="server" Text="No" class="btn btn-default" UseSubmitBehavior="false" />
                                <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick="btnYes_Click" class="btn btn-success" UseSubmitBehavior="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
