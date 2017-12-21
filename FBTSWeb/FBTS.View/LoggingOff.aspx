<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoggingOff.aspx.cs" Inherits="FBTS.View.LoggingOff" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- meta -->
    <meta charset="utf-8" />
    <meta name="description" content="Flat, Clean, Responsive, application admin template built with bootstrap 3" />
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1" />
    <!-- /meta -->

    <!-- page level plugin styles -->
    <!-- /page level plugin styles -->

    <!-- core styles -->

    <script src="Resources/UI/plugins/jquery-1.11.1.min.js"></script>
    <link rel="stylesheet" href="Resources/UI/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="Resources/UI/css/font-awesome.css" />
    <link rel="stylesheet" href="Resources/UI/css/themify-icons.css" />
    <link rel="stylesheet" href="Resources/UI/css/animate.min.css" />
    <!-- /core styles -->

    <!-- template styles -->
    <link rel="stylesheet" href="Resources/UI/css/skins/palette.css" />
    <link rel="stylesheet" href="Resources/UI/css/fonts/font.css" />
    <link rel="stylesheet" href="Resources/UI/css/main.css" />
    <link href="Resources/UI/css/CustomStyles.css" rel="stylesheet" />
    <!-- template styles -->

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
            <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
            <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
        <![endif]-->

    <!-- load modernizer -->
    <script src="Resources/UI/plugins/modernizr.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
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
</head>
<body class="bg-primary animated zoomIn">
    <div class="center-wrapper">
        <div class="center-content">
            <div class="row">
                <div class="col-xs-10 col-xs-offset-1 col-sm-6 col-sm-offset-3 col-md-4 col-md-offset-4">
                    <section class="panel bg-white no-b">
                        <div class="panel-heading ">
                            <div class="row">
                                <div class="col-md-8">
                                    <h4><strong>logging out...</strong></h4>                                    
                                </div>
                                <div class="col-md-4">
                                    <div class="pull-right">
                                        <img src="Resources/UI/img/FBTSLogo.png" /></div>
                                </div>
                            </div>
                        </div>
                    </section>
                    <p class="text-center">
                        <asp:Label ID="lblCopyRightYear" runat="server" Text=""></asp:Label>
                    </p>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
