<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="500.aspx.cs" Inherits="FBTS.View.AppPages.Common._500" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="center-wrapper">

        <div class="center-content text-center">

            <div class="error-number animated flash">
                <i class="ti-alert mr15 show"></i>
                <span>500</span>
            </div>

            <div class="mb25">SERVER ERROR</div>

            <p>We're experiencing an internal server problem.
                <br>
                <br>Please try again later or contact
                <a href="mailto:mail@contact.tld">mail@contact.tld</a>
            </p>

            <ul class="mt25 error-nav">
                <li>
                    <a href="javascript:;">&copy;
                        <span id="year" class="mr5"></span>Sublime LLC</a>
                </li>
                <li>
                    <a href="javascript:;">About</a>
                </li>
                <li>
                    <a href="javascript:;">Help</a>
                </li>
                <li>
                    <a href="javascript:;">Status</a>
                </li>
            </ul>
        </div>
    </div>
    <!-- /error wrapper -->

    <script type="text/javascript">
        var el = document.getElementById("year"),
            year = (new Date().getFullYear());
        el.innerHTML = year;
    </script>
</asp:Content>
