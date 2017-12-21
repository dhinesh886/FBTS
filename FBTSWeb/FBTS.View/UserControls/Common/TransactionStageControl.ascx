<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransactionStageControl.ascx.cs" Inherits="FBTS.View.UserControls.Common.TransactionStageControl" %>
     
<asp:UpdatePanel ID="uplView" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:HiddenField ID="hidActiveStage"  runat ="server"/> 
        <asp:HiddenField ID="hidSubLink"  runat ="server"/> 
        
        <ul class="nav nav-tabs dv-tabheader" runat="server" id="ulTabs" >
		    <li runat="server" id="liTab1" class="tab1 active"><asp:LinkButton ID="lnkTab1" runat="server"  OnClick="OnTabChanged">Section 1</asp:LinkButton> </li>
			<li runat="server" id="liTab2" class="tab2"><asp:LinkButton ID="lnkTab2" runat="server" OnClick="OnTabChanged">Section 2</asp:LinkButton> </li>
			<li runat="server" id="liTab3" class="tab3"><asp:LinkButton ID="lnkTab3" runat="server" OnClick="OnTabChanged">Section 3</asp:LinkButton> </li>
            <li runat="server" id="liTab4" class="tab4"><asp:LinkButton ID="lnkTab4" runat="server" OnClick="OnTabChanged">Section 4</asp:LinkButton> </li>
            <li runat="server" id="liTab5" class="tab5"><asp:LinkButton ID="lnkTab5" runat="server" OnClick="OnTabChanged">Section 5</asp:LinkButton> </li>
            <li runat="server" id="liTab6" class="tab6"><asp:LinkButton ID="lnkTab6" runat="server" OnClick="OnTabChanged">Section 6</asp:LinkButton> </li>
            <li runat="server" id="liTab7" class="tab7"><asp:LinkButton ID="lnkTab7" runat="server" OnClick="OnTabChanged">Section 7</asp:LinkButton> </li>
            <li runat="server" id="liTab8" class="tab8"><asp:LinkButton ID="lnkTab8" runat="server" OnClick="OnTabChanged">Section 8</asp:LinkButton> </li>
        </ul>
    </ContentTemplate>
</asp:UpdatePanel>