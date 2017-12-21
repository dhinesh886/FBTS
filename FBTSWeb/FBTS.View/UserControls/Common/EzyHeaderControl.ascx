<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EzyHeaderControl.ascx.cs" Inherits="FBTS.View.UserControls.Common.EzyHeaderControl" %>
 <header class="header header-fixed navbar">
            <div class="brand">
                <!-- toggle offscreen menu -->
                <a href="javascript:;" class="ti-menu off-left visible-xs" data-toggle="offscreen" data-move="ltr"></a>
                <!-- /toggle offscreen menu -->

                <!-- logo --> 
                <%--<a href="~/Ezy_Live_Welcome.aspx?cptitle=Welcome&mcode=MM00SM00" class="navbar-brand">--%>
               <a href="Ezy_Welcome.aspx?cptitle=Welcome&mcode=MM00SM00PM00" class="navbar-brand">
                    <%--<asp:Image ID="imgLogo" runat="server" ImageUrl ="~/Resources/Logo/logo.png"  AlternateText ="logo"/>--%>
                    <span class="heading-font">
                       FBTS
                    </span>
                </a>
                <!-- /logo -->
            </div>

            <ul class="nav navbar-nav">
                <li class="hidden-xs">
                    <!-- toggle small menu -->
                    <a href="javascript:;" class="toggle-sidebar">
                        <i class="ti-menu"></i>
                    </a>
                    <!-- /toggle small menu -->
                </li>
                <li class="header-search">
                    <!-- toggle search -->
                  <%--  <a href="javascript:;" class="toggle-search">
                        <i class="ti-search"></i>
                    </a>--%>
                    <!-- /toggle search -->
              <%--      <div class="search-container">
                        <form role="search">
                            <input type="text" class="form-control search" placeholder="type and press enter">
                        </form>
                    </div>--%>
                </li>
            </ul>

            <ul class="nav navbar-nav navbar-right">

               <%-- <li class="dropdown hidden-xs">
                    <a href="javascript:;" data-toggle="dropdown">
                        <i class="ti-more-alt"></i>
                    </a>
                    <ul class="dropdown-menu animated zoomIn">
                        <li class="dropdown-header">Quick Links</li>
                        <li>
                            <a href="javascript:;">Start New Campaign</a>
                        </li>
                        <li>
                            <a href="javascript:;">Review Campaigns</a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="javascript:;">Settings</a>
                        </li>
                        <li>
                            <a href="javascript:;">Wish List</a>
                        </li>
                        <li>
                            <a href="javascript:;">Purchases History</a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="javascript:;">Activity Log</a>
                        </li>
                        <li>
                            <a href="javascript:;">Settings</a>
                        </li>
                        <li>
                            <a href="javascript:;">System Reports</a>
                        </li>
                        <li class="divider"></li>
                        <li>
                            <a href="javascript:;">Help</a>
                        </li>
                        <li>
                            <a href="javascript:;">Report a Problem</a>
                        </li>
                    </ul>
                </li>--%>

                <%--<li class="notifications dropdown">
                    <a href="javascript:;" data-toggle="dropdown">
                        <i class="ti-bell"></i>
                        <div class="badge badge-top bg-danger animated flash">
                            <span>3</span>
                        </div>
                    </a>
                    <div class="dropdown-menu animated fadeInLeft">
                        <div class="panel panel-default no-m">
                            <div class="panel-heading small"><b>Notifications</b>
                            </div>
                            <ul class="list-group">
                                <li class="list-group-item">
                                    <a href="javascript:;">
                                        <span class="pull-left mt5 mr15">
                                            <img src="../../Resources/UI/img/faceless.jpg" class="avatar avatar-sm img-circle" alt="">
                                        </span>
                                        <div class="m-body">
                                            <div class="">
                                                <small><b>CRYSTAL BROWN</b></small>
                                                <span class="label label-danger pull-right">ASSIGN AGENT</span>
                                            </div>
                                            <span>Opened a support query</span>
                                            <span class="time small">2 mins ago</span>
                                        </div>
                                    </a>
                                </li>
                                <li class="list-group-item">
                                    <a href="javascript:;">
                                        <div class="pull-left mt5 mr15">
                                            <div class="circle-icon bg-danger">
                                                <i class="ti-download"></i>
                                            </div>
                                        </div>
                                        <div class="m-body">
                                            <span>Upload Progress</span>
                                            <div class="progress progress-xs mt5 mb5">
                                                <div class="progress-bar" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 40%">
                                                </div>
                                            </div>
                                            <span class="time small">Submited 23 mins ago</span>
                                        </div>
                                    </a>
                                </li>
                                <li class="list-group-item">
                                    <a href="javascript:;">
                                        <span class="pull-left mt5 mr15">
                                            <img src="../../Resources/UI/img/faceless.jpg" class="avatar avatar-sm img-circle" alt="">
                                        </span>
                                        <div class="m-body">
                                            <em>Status Update:</em>
                                            <span>All servers now online</span>
                                            <span class="time small">5 days ago</span>
                                        </div>
                                    </a>
                                </li>
                            </ul>

                            <div class="panel-footer">
                                <a href="javascript:;">See all notifications</a>
                            </div>
                        </div>
                    </div>
                </li>--%>

                <li class="off-right">
                    <a href="javascript:;" data-toggle="dropdown">
                         <asp:Image ID="imgAvatar"  CssClass="header-avatar img-circle" runat="server" ImageUrl ="~/assets/img/avatar1_small.jpg"  AlternateText ="user"/>
                        
                        <span class="hidden-xs ml10"><asp:Label ID="lblUserName" runat="server" Text=""></asp:Label></span> 
                        <i class="ti-angle-down ti-caret hidden-xs"></i>
                    </a>
                    <ul class="dropdown-menu animated fadeInRight">
<%--                        <li><a href="Ezy_Live_MyProfile.aspx?cptitle=My Profile&mcode=MM00SM00"><i class="icon-user"></i>My
                                        Profile</a></li>
                                <li><a href="Ezy_Live_MyCalendar.aspx?cptitle=My Calendar&mcode=MM00SM00"><i class="icon-calendar">
                                                                                                          </i>My Calendar</a></li>
                                <li><a href="#"><i class="icon-tasks"></i>My Tasks</a></li>
                                <li class="divider"></li>--%>
                                <li><a href='#' runat="server" id="aLogOut"><i class="icon-key"></i>Log Out</a></li>
                        

                        
                    </ul>
                </li>
            </ul>
        </header>