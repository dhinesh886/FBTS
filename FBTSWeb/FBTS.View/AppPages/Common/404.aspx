<%@ Page Title="" Language="C#" MasterPageFile="~/AppPages/Common/Site.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="FBTS.View.AppPages.Common._404" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <!-- error wrapper -->
    <div class="center-wrapper">

        <div class="center-content text-center">

            <div class="error-number animated bounceIn">404</div>

            <div class="mb25">PAGE NOT FOUND</div>

            <p>Sorry, but the page you were trying to view does not exist.</p>

            <div class="search">
                <form class="form-inline" role="form">
                    <div class="search-form">
                        <button class="search-button" type="submit" title="Search">
                            <i class="ti-search"></i>
                        </button>
                        <input type="text" class="form-control no-b" placeholder="Search Admin Panel">
                    </div>
                </form>
            </div>

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
