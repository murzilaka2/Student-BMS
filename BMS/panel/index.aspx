<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="BMS.panel.index" %>
<% @ Register TagName="Styles" TagPrefix="Styles" Src="~/panel/fragments/Styles.ascx" %>
<% @ Register TagName="Scripts" TagPrefix="Scripts" Src="~/panel/fragments/Scripts.ascx" %>
<!DOCTYPE html>
<html>
    <head>      
        <title>BMS</title>       
        <meta charset="utf-8">
        <meta name="format-detection" content="telephone=no">
        <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
        <meta name="description" content="BMS - Панель администратора" />
        <link rel="icon" href="../images/favicon.ico" type="image/x-icon">
        <!-- Styles -->
        <Styles:Styles runat="server"/> 
        <link href="assets/plugins/weather-icons-master/css/weather-icons.min.css" rel="stylesheet" type="text/css"/> 
        <link href="assets/plugins/metrojs/MetroJs.min.css" rel="stylesheet" type="text/css"/>	
        <link href="assets/plugins/toastr/toastr.min.css" rel="stylesheet" type="text/css"/>	    
        
    </head>
    <body class="page-header-fixed compact-menu page-horizontal-bar">
        <div class="overlay"></div>
                 
        <main class="page-content content-wrap">
            <div class="navbar">
                <div class="navbar-inner container">
                    <div class="sidebar-pusher">
                        <a href="javascript:void(0);" class="waves-effect waves-button waves-classic push-sidebar">
                            <i class="fa fa-bars"></i>
                        </a>
                    </div>
                    <div class="logo-box">
                        <a href="index.aspx" class="logo-text"><span>BMS</span></a>
                    </div><!-- Logo Box -->
                    <div class="search-button">
                        <a href="javascript:void(0);" class="waves-effect waves-button waves-classic show-search"><i class="fa fa-search"></i></a>
                    </div>
                    <div class="topmenu-outer">
                        <div class="top-menu">
                            <ul class="nav navbar-nav navbar-left">
                                <li>		
                                    <a href="javascript:void(0);" class="waves-effect waves-button waves-classic toggle-fullscreen"><i class="fa fa-expand"></i></a>
                                </li>                              
                            </ul>
                            <ul class="nav navbar-nav navbar-right">
                                <li class="dropdown">
                                    <a href="#" class="dropdown-toggle waves-effect waves-button waves-classic" data-toggle="dropdown"><i class="fa fa-envelope"></i><span class="badge badge-success pull-right" runat="server" ID="NewMessageHeader">1</span></a>
                                    <ul class="dropdown-menu title-caret dropdown-lg" role="menu">
                                        <li><p class="drop-title" runat="server" ID="MessagesHeaderText">У Вас нет новых сообщений!</p></li>
                                        <li class="dropdown-menu-list slimscroll messages">
                                            <ul class="list-unstyled">
                                                <asp:PlaceHolder runat="server" ID="NewMessages"></asp:PlaceHolder>
                                            </ul>
                                        </li>
                                        <li class="drop-all"><a href="messages.aspx" class="text-center">Все сообщения</a></li>
                                    </ul>
                                </li>                               
                                <li class="dropdown">
                                    <a href="person_view.aspx?login=<% Response.Write(Request.Cookies["le"].Value.Substring(3)); %>" class="dropdown-toggle waves-effect waves-button waves-classic">
                                        <span class="user-name"><% Response.Write(Request.Cookies["le"].Value.Substring(3)); %><i class="fa fa-angle"></i></span>
                                        <img class="img-circle avatar" width="40" height="40" alt="Аватар Пользователя" src="#" runat="server" ID="UserImage">
                                    </a>                                    						
                                </li>
                                <li>
								<!-- Выход -->
                                    <a href="#" class="waves-effect waves-button waves-classic" id="Exit" runat="server" onserverclick="Exit_ServerClick">
                                        <i class="fa fa-sign-out m-r-xs"></i>
                                    </a>
                                </li>
                            </ul><!-- Nav -->
                        </div><!-- Top Menu -->
                    </div>
                </div>
            </div><!-- Navbar -->
            <div class="page-sidebar sidebar horizontal-bar">
                <div class="page-sidebar-inner">
                    <ul class="menu accordion-menu">
                        <li class="nav-heading"><span>Навигация</span></li>
                        <li class="active"><a href="index.aspx"><span class="menu-icon icon-speedometer"></span><p>Главная</p></a></li>
                        <li><a href="messages.aspx"><span class="menu-icon icon-envelope-open"></span><p>Сообщения</p><span class="arrow"></span></a></li>
                        <li><a href="reports.aspx"><span class="menu-icon icon-briefcase"></span><p>Отчеты</p><span class="arrow"></span></a></li>
                        <li class="droplink"><a href="orders.aspx"><span class="menu-icon icon-note"></span><p>Заказы</p><span class="arrow"></span></a>
                             <ul class="sub-menu">
                                <li><a href="products.aspx">Товары</a></li>
                                <li><a href="add_product.aspx">Добавить Товар</a></li>
                                <li><a href="product_category.aspx">Категории Товаров</a></li>
                                <li><a href="add_product_category.aspx">Добавить Категорию Товара</a></li>
                                <li><a href="services.aspx">Услуги</a></li>
                                <li><a href="add_service.aspx">Добавить Услугу</a></li>
                                <li><a href="service_category.aspx">Категории Услуг</a></li>
                                <li><a href="add_service_category.aspx">Добавить Категорию Услуги</a></li>
                            </ul>
                        </li>
                        <li><a href="objectives.aspx"><span class="menu-icon icon-bar-chart"></span><p>Цели</p><span class="arrow"></span></a></li>
                        <li><a href="users.aspx"><span class="menu-icon icon-user"></span><p>Персонал</p><span class="arrow"></span></a></li>                      
                    </ul>
                </div><!-- Page Sidebar Inner -->
            </div><!-- Page Sidebar -->
            <div class="page-inner">
                <div class="page-title">
                    <div class="container">
                        <h3>Главная</h3>
                    </div>
                </div>
                <div id="main-wrapper" class="container">
                    <div class="row">
                        <div class="col-lg-3 col-md-6">
                            <div class="panel info-box panel-white">
                                <div class="panel-body">
                                    <div class="info-box-stats">
                                        <p class="counter" runat="server" ID="Users"></p>
                                        <span class="info-box-title">Всего пользователей</span>
                                    </div>
                                    <div class="info-box-icon">
                                        <i class="icon-users"></i>
                                    </div>
                                    <div class="info-box-progress">
                                        <div class="progress progress-xs progress-squared bs-n">
                                            <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 40%">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="panel info-box panel-white">
                                <div class="panel-body">
                                    <div class="info-box-stats">
                                        <p class="counter" runat="server" ID="OrdersForDay"></p>
                                        <span class="info-box-title">Заказов</span>
                                    </div>
                                    <div class="info-box-icon">
                                        <i class="icon-eye"></i>
                                    </div>
                                    <div class="info-box-progress">
                                        <div class="progress progress-xs progress-squared bs-n">
                                            <div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 80%">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="panel info-box panel-white">
                                <div class="panel-body">
                                    <div class="info-box-stats">
                                        <p><span class="counter" runat="server" ID="MoneyForDay"></span> грн</p>
                                        <span class="info-box-title">Заработано</span>
                                    </div>
                                    <div class="info-box-icon">
                                        <i class="icon-basket"></i>
                                    </div>
                                    <div class="info-box-progress">
                                        <div class="progress progress-xs progress-squared bs-n">
                                            <div class="progress-bar progress-bar-warning" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="panel info-box panel-white">
                                <div class="panel-body">
                                    <div class="info-box-stats">
                                        <p class="counter" runat="server" ID="MessageCount"></p>
                                        <span class="info-box-title">Сообщений</span>
                                    </div>
                                    <div class="info-box-icon">
                                        <i class="icon-envelope"></i>
                                    </div>
                                    <div class="info-box-progress">
                                        <div class="progress progress-xs progress-squared bs-n">
                                            <div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100" style="width: 50%">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div><!-- Row -->
                    <div class="row">
                        <div class="col-lg-9 col-md-12">
                            <div class="panel panel-white">
                                <div class="row">
                                    <div class="col-sm-8">
                                        <div class="visitors-chart">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">Visitors</h4>
                                            </div>
                                            <div class="panel-body">
                                                <div id="flotchart1"></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="stats-info">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">Browser Stats</h4>
                                            </div>
                                            <div class="panel-body">
                                                <ul class="list-unstyled">
                                                    <li>Google Chrome<div class="text-success pull-right">32%<i class="fa fa-level-up"></i></div></li>
                                                    <li>Firefox<div class="text-success pull-right">25%<i class="fa fa-level-up"></i></div></li>
                                                    <li>Internet Explorer<div class="text-success pull-right">16%<i class="fa fa-level-up"></i></div></li>
                                                    <li>Safari<div class="text-danger pull-right">13%<i class="fa fa-level-down"></i></div></li>
                                                    <li>Opera<div class="text-danger pull-right">7%<i class="fa fa-level-down"></i></div></li>
                                                    <li>Mobile &amp; tablet<div class="text-success pull-right">4%<i class="fa fa-level-up"></i></div></li>
                                                    <li>Others<div class="text-success pull-right">3%<i class="fa fa-level-up"></i></div></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="panel panel-white" style="height: 100%;">
                                <div class="panel-heading">
                                    <h4 class="panel-title">Server Load</h4>
                                    <div class="panel-control">
                                        <a href="javascript:void(0);" data-toggle="tooltip" data-placement="top" title="Expand/Collapse" class="panel-collapse"><i class="icon-arrow-down"></i></a>
                                        <a href="javascript:void(0);" data-toggle="tooltip" data-placement="top" title="Reload" class="panel-reload"><i class="icon-reload"></i></a>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="server-load">
                                        <div class="server-stat">
                                            <span>Total Usage</span>
                                            <p>67GB</p>
                                        </div>
                                        <div class="server-stat">
                                            <span>Total Space</span>
                                            <p>320GB</p>
                                        </div>
                                        <div class="server-stat">
                                            <span>CPU</span>
                                            <p>57%</p>
                                        </div>
                                    </div>
                                    <div id="flotchart2"></div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5 col-md-6">
                            <div class="panel panel-white">
                                <div class="panel-heading">
                                    <h4 class="panel-title">Weather</h4>
                                    <div class="panel-control">
                                        <a href="javascript:void(0);" data-toggle="tooltip" data-placement="top" title="Reload" class="panel-reload"><i class="icon-reload"></i></a>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="weather-widget">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="weather-top">
                                                    <div class="weather-current pull-left">
                                                        <i class="wi wi-day-cloudy weather-icon"></i>
                                                        <p><span>83<sup>&deg;F</sup></span></p>
                                                    </div>
                                                    <h2 class="weather-day pull-right">Miami, FL<br><small><b>13th April, 2015</b></small></h2>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <ul class="list-unstyled weather-info">
                                                    <li>Wind <span class="pull-right"><b>ESE 16 mph</b></span></li>
                                                    <li>Humidity <span class="pull-right"><b>64%</b></span></li>
                                                    <li>Pressure <span class="pull-right"><b>30.15 in</b></span></li>
                                                    <li>UV Index <span class="pull-right"><b>6</b></span></li>
                                                </ul>
                                            </div>
                                            <div class="col-md-6">
                                                <ul class="list-unstyled weather-info">
                                                    <li>Cloud Cover <span class="pull-right"><b>60%</b></span></li>
                                                    <li>Ceiling <span class="pull-right"><b>17800 ft</b></span></li>
                                                    <li>Dew Point <span class="pull-right"><b>70° F</b></span></li>
                                                    <li>Visibility <span class="pull-right"><b>10 mi</b></span></li>
                                                </ul>
                                            </div>
                                            <div class="col-md-12">
                                                <ul class="list-unstyled weather-days row">
                                                    <li class="col-xs-4 col-sm-2"><span>12:00</span><i class="wi wi-day-cloudy"></i><span>82<sup>&deg;F</sup></span></li>
                                                    <li class="col-xs-4 col-sm-2"><span>13:00</span><i class="wi wi-day-cloudy"></i><span>82<sup>&deg;F</sup></span></li>
                                                    <li class="col-xs-4 col-sm-2"><span>14:00</span><i class="wi wi-day-cloudy"></i><span>82<sup>&deg;F</sup></span></li>
                                                    <li class="col-xs-4 col-sm-2"><span>15:00</span><i class="wi wi-day-cloudy"></i><span>83<sup>&deg;F</sup></span></li>
                                                    <li class="col-xs-4 col-sm-2"><span>16:00</span><i class="wi wi-day-cloudy"></i><span>82<sup>&deg;F</sup></span></li>
                                                    <li class="col-xs-4 col-sm-2"><span>17:00</span><i class="wi wi-day-sunny-overcast"></i><span>82<sup>&deg;F</sup></span></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-6">
                            <div class="panel panel-white">
                                <div class="panel-heading">
                                    <h4 class="panel-title">Друзья</h4>
                                    <div class="panel-control">
                                        <a href="index.aspx" data-toggle="tooltip" data-placement="top" title="Reload" class="panel-reload"><i class="icon-reload"></i></a>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="inbox-widget slimscroll">
                                    <asp:PlaceHolder runat="server" ID="FriendsPlaceholder"></asp:PlaceHolder>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="panel twitter-box">
                                <div class="panel-body">
                                    <div class="live-tile" data-mode="flip" data-speed="750" data-delay="5000">
                                        <span class="tile-title pull-right">BMS</span>
                                        <i class="fa icon-diamond"></i>
                                        <div><a href="add_product.aspx"><h2 class="no-m">Добавить товар</h2><span class="tile-date">Создайте свой товар для автоматического добавления и расчета доходности.</span></a></div>
                                        <div><a href="add_service.aspx"><h2 class="no-m">Добавить услугу</h2><span class="tile-date">Создайте свою услугу для автоматического добавления и расчета доходности.</span></a></div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel facebook-box">
                                <div class="panel-body">
                                    <div class="live-tile" data-mode="carousel" data-direction="horizontal" data-speed="750" data-delay="5000">
                                        <span class="tile-title pull-right">BMS</span>
                                        <i class="fa icon-docs"></i>
                                         <div><a href="products.aspx"><h2 class="no-m">Просмотр товаров</h2><span class="tile-date">Просматривайте и редактируйте текущий список товаров.</span></a></div>
                                         <div><a href="services.aspx"><h2 class="no-m">Просмотр услуг</h2><span class="tile-date">Просматривайте и редактируйте текущий список услуг.</span></a></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12">
                            <div class="panel panel-white">
                                <div class="panel-heading">
                                    <h4 class="panel-title">Текущие цели</h4>
                                </div>
                                <div class="panel-body">
                                    <div class="table-responsive project-stats">  
                                       <table class="table">
                                           <thead>
                                               <tr>
                                                   <th>Проект</th>
                                                   <th>Статус</th>
                                                   <th>Автор</th>
                                                   <th>Дата</th>
                                               </tr>
                                           </thead>
                                           <tbody>
                                               <asp:PlaceHolder runat="server" ID="ObjectivesPlaceholder"></asp:PlaceHolder>
                                           </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div><!-- Main Wrapper -->
                <div class="page-footer">
                    <div class="container">
                        <p class="no-s">© 2017 All Rights Reserved BMS</p>
                    </div>
                </div>
            </div><!-- Page Inner -->
        </main><!-- Page Content -->       
        <div class="cd-overlay"></div>
	
        <!-- Javascripts -->
        <Scripts:Scripts runat="server"/>
        <script src="assets/plugins/waypoints/jquery.waypoints.min.js"></script>
        <script src="assets/plugins/jquery-counterup/jquery.counterup.min.js"></script>
        <script src="assets/plugins/toastr/toastr.min.js"></script>
        <script src="assets/plugins/flot/jquery.flot.min.js"></script>
        <script src="assets/plugins/flot/jquery.flot.time.min.js"></script>
        <script src="assets/plugins/flot/jquery.flot.symbol.min.js"></script>
        <script src="assets/plugins/flot/jquery.flot.resize.min.js"></script>
        <script src="assets/plugins/flot/jquery.flot.tooltip.min.js"></script>
        <script src="assets/plugins/curvedlines/curvedLines.js"></script>
        <script src="assets/plugins/metrojs/MetroJs.min.js"></script>
        <script src="assets/js/modern.js"></script>
        <script src="assets/js/pages/dashboard.js"></script>   
             
    </body>
</html>
<form id="form1" runat="server"></form>