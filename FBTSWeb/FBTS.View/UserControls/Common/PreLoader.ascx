<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreLoader.ascx.cs" Inherits="FBTS.View.UserControls.PreLoader" %> 
   
<ajaxToolkit:ModalPopupExtender ID="mpePreLoader" runat="server"
                                BehaviorID="mpePreLoaderBehavior"
                                PopupControlID="divPopupControl"  
                                TargetControlID="btnHiddenTarget" 
                                BackgroundCssClass="preLoaderBackground"></ajaxToolkit:ModalPopupExtender>
          

    
<a href="javascript:;" id="btnHiddenTarget" style="display: none" runat="server"></a>
<div id="divPopupControl" ><img src="../../Resources/UI/img/loading.gif" align="absmiddle"></div>

<script type="text/javascript">
    function ShowPreLoader() {
        var modalPopupBehavior = $find('mpePreLoaderBehavior');
        modalPopupBehavior.show();
    }

    function HidePreLoader() {
        var modalPopupBehavior = $find('mpePreLoaderBehavior');
        modalPopupBehavior.hide();
    }

</script>