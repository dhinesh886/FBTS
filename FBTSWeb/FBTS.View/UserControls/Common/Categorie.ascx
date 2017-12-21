<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Categorie.ascx.cs" Inherits="FBTS.View.UserControls.Common.Categorie" %>

<script>
   
    function IsSelected<%=this.ClientID%>(Id)
    {       
        var cbl = $('#<%= Chk.ClientID %>')
        if (cbl.length) {

            var cbElements = cbl.find('TR').filter(function (index, element) {
                return $(this).find('input:checked').length;
            });
            if(!cbElements.length)
                return false
        }
        return true;
    }
   $(function () {
      
        var cbl = $('#<%= this.Chk.ClientID %>')      
        if (cbl.length) {
            var cbElements = cbl.find('TR').filter(function (index, element) {
                return $(this).find('input:checked').length;
            });
            cbElements.each(function () {
                $(this).prependTo(cbl);
            });
        }
    });
    function sortCheckBoxList<%=this.ClientID %>(txtSearch)
    {
       
        var cbl = $('#<%= Chk.ClientID %>')
       
        if (cbl.length) {
            
            var cbElements = cbl.find('TR').filter(function (index, element) {
                return $(this).find('input:checked').length;
            });
            cbElements.each(function () {
                $(this).prependTo(cbl);
            });
        }
    }
    function Search<%=this.ClientID %>(txtSearch) {
       
        var cblChk = '#<%=Chk.ClientID %>';
        if ($(txtSearch).val() != "") {
            var count = 0;           
            $(cblChk).children('tbody').children('tr').each(function () {
                var match = false;
                $(this).children('td').children('label').each(function () {
                    if ($(this).text().toUpperCase().indexOf($(txtSearch).val().toUpperCase()) > -1)
                        match = true;
                });
                if (match) {
                    $(this).show();
                    count++;
                }
                else { $(this).hide(); }
            });
           // $('#spnCount').html((count) + ' match');
        }
        else {
            $(cblChk).children('tbody').children('tr').each(function () {
                $(this).show();
            });
            $('#spnCount').html('');
        }
    }    
</script>
       <div class="panel panel-default ">
            <header class="panel-heading"><b runat="server" id="header"></b></header>           
    <div class="panel-body">
        <asp:UpdatePanel ID="uplComp" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row" runat="server" id="divSearch">                     
                    <div class="col-md-12">
                        <asp:TextBox ID="txtSearch" runat="server" 
                            placeholder="Search"></asp:TextBox>
                        <span id="spnCount"></span>
                    </div>
                </div>
                <div class="row pl10">
                    <%--<div style="height:400px;overflow:auto">--%>
                    <div>
                        <asp:CheckBoxList ID="Chk" runat="server" OnSelectedIndexChanged="Chk_SelectedIndexChanged"></asp:CheckBoxList>                       
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>   
    </div>          </div>
 