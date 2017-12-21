<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomMessageControl.ascx.cs" Inherits="FBTS.View.UserControls.Common.CustomMessageControl" %>

<script type="text/javascript" >
    function showCustomMessage(messageHeader, messageText, messageType) {
        $.jGrowl(messageText, { header: messageHeader, position: "bottom-right", messagetype: messageType });
    }
</script>
 