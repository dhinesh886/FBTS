<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfirmationPopup.ascx.cs" Inherits="Ezy.ERP.View.UserControls.Common.ConfirmationPopup" %>
<%--PopupControlID="divPopupControl" --%>
<ajaxToolkit:ModalPopupExtender ID="mpeMessageBox" runat="server"
    BehaviorID="mpeMessageBoxBehavior"
    PopupControlID="pnlPopup"
    CancelControlID="btnNo"
    TargetControlID="btnHiddenTarget"
    BackgroundCssClass="modalBackground"
    Drag="true"
    PopupDragHandleControlID="secHeader"
    DropShadow="true"
    RepositionMode="RepositionOnWindowResizeAndScroll"
    X="370"
    Y="250">
</ajaxToolkit:ModalPopupExtender>

<a href="javascript:;" id="btnHiddenTarget" style="display: none" runat="server"></a>

<asp:UpdatePanel ID="uplConfMsg" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none; width: 350px;">
            <asp:HiddenField ID="hdnKey1" runat="server" />
            <asp:HiddenField ID="hdnKey2" runat="server" />
            <div runat="server" id="divMessageBody">
                <section class="panel">
                    <header class="panel-heading" id="secHeader" runat="server">Horizontal Form</header>
                    <div class="panel-body">
                        <div class="form-horizontal" id="divPopupBody" runat="server">
                        </div>
                        <div class="form-group" id="divMessageBodyText" runat="server">
                        </div>
                        <div class="row" id="divRemark" runat="server" visible="false">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Remarks</label>
                                    <div>
                                        <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control" MaxLength="250" TextMode="MultiLine" onkeypress="return validentry('SQ',event);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divActionsYesNo" runat="server">
                            <div class="col-sm-10">
                                <div class="pull-right">
                                    <asp:Button ID="btnNo" runat="server" Text="No" OnClick="btnNo_Click" class="btn btn-default" />
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="pull-right">
                                    <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick="btnYes_Click" class="btn btn-success" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="divActionOk" runat="server" style="display: none">
                            <div class="col-sm-offset-5">
                                <asp:Button ID="btnOk" runat="server" Text="OK" OnClick="btnOk_Click" class="btn btn-default" />
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>


<%--<asp:UpdatePanel ID="uplConfMsg" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none; width: 350px;">
        <div id="divPopupControl" style="display: none; width: 350px;" Class="modalPopup">
            <asp:HiddenField ID="hdnKey1" runat="server" />
            <asp:HiddenField ID="hdnKey2" runat="server" />
            <div runat="server" id="divMessageBody">
                <section class="panel">
                    <header class="panel-heading" id="secHeader" runat="server">Horizontal Form</header>
                    <div class="panel-body">
                        <div class="form-horizontal" id="divPopupBody" runat="server">
                        </div>
                        <div class="form-group" id="divMessageBodyText" runat="server">
                        </div>
                        <div class="form-group" id="divActions">
                            <div class="col-sm-10">
                                <div class="pull-right">
                                    <asp:Button ID="btnNo" runat="server" Text="No" OnClick="btnNo_Click" class="btn btn-default" /></div>
                            </div>
                            <div class="col-sm-2">
                                <div class="pull-right">
                                    <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick="btnYes_Click" class="btn btn-success" />
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>--%>
