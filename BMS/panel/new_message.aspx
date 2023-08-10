<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="new_message.aspx.cs" Inherits="BMS.panel.new_message" %>
<% @ Register TagName="Styles" TagPrefix="Styles" Src="~/panel/fragments/Styles.ascx" %>
<% @ Register TagName="Scripts" TagPrefix="Scripts" Src="~/panel/fragments/Scripts.ascx" %>
<!DOCTYPE html>
<html>
    <head>
        <title>BMS | Новое Сообщение</title>     
        <meta content="width=device-width, initial-scale=1" name="viewport"/>
        <meta charset="UTF-8">
        <meta name="description" content="BMS - Новое Сообщение." />        
        <!-- Styles -->
        <Styles:Styles runat="server"/>
        
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
                        <li><a href="index.aspx"><span class="menu-icon icon-speedometer"></span><p>Главная</p></a></li>
                        <li class="active"><a href="messages.aspx"><span class="menu-icon icon-envelope-open"></span><p>Сообщения</p><span class="arrow"></span></a></li>
                        <li><a href="reports.aspx"><span class="menu-icon icon-briefcase"></span><p>Отчеты</p><span class="arrow"></span></a></li>
                       <li class="droplink"><a href="orders.aspx"><span class="menu-icon icon-note"></span><p>Заказы</p><span class="arrow"></span></a>
                             <ul class="sub-menu">
                                <li><a href="products.aspx">Товары</a></li>
                                <li><a href="add_product.aspx">Добавить Товар</a></li>
                                <li><a href="add_product_category.aspx">Добавить Категорию Товара</a></li>
                                <li><a href="services.aspx">Услуги</a></li>
                                <li><a href="add_service.aspx">Добавить Услугу</a></li>
                                <li><a href="add_service_category.aspx">Добавить Категорию Услуги</a></li>
                            </ul>
                        </li>
                        <li><a href="objectives.aspx"><span class="menu-icon icon-bar-chart"></span><p>Цели</p><span class="arrow"></span></a></li>
                        <li><a href="users.aspx"><span class="menu-icon icon-user"></span><p>Персонал</p><span class="arrow"></span></a></li>                      
                    </ul>
                </div><!-- Page Sidebar Inner -->
            </div><!-- Page Sidebar -->
            <div class="page-inner">
                  <form id="form1" runat="server">
                <div id="main-wrapper" class="container">
                    <div class="row m-t-md">
                        <div class="col-md-12">
                            <div class="row mailbox-header">
                                <div class="col-md-2">
                                    <a href="messages.aspx" class="btn btn-success btn-block">Назад</a>
                                </div>
                                <div class="col-md-6">
                                    <h2>Новое письмо</h2>
                                </div>
                                <div class="col-md-4">
                                    <div class="compose-options">
                                        <div class="pull-right">
                                            <a href="#" class="btn btn-default" runat="server" ID="Draft" onserverclick="Draft_ServerClick"><i class="fa fa-file-text-o m-r-xs"></i>Черновик</a>
                                            <a href="messages.aspx" class="btn btn-danger"><i class="fa fa-trash m-r-xs"></i>Удалить</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                           <ul class="list-unstyled mailbox-nav">
                                <li><a href="messages.aspx"><i class="fa fa-inbox"></i>Входящие <span class="badge badge-success pull-right" runat="server" ID="MessagesCount"></span></a></li>
                                <li><a href="send_messages.aspx"><i class="fa fa-sign-out"></i>Отправленные</a></li>
                                <li><a href="message_draft.aspx"><i class="fa fa-file-text-o"></i>Черновики</a></li>
                            </ul>
                        </div>
                        <div class="col-md-10">
                            <div class="panel panel-white">
                                <div class="panel-body mailbox-content">
                                    <div class="compose-body">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="to" class="col-sm-2 control-label">Email</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox runat="server" ID="To" CssClass="form-control" required TextMode="Email"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="subject" class="col-sm-2 control-label">Заголовок</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox runat="server" ID="Subject" CssClass="form-control" required></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="compose-message">
                                         <div class="note-editor">
                                             <asp:TextBox runat="server" TextMode="MultiLine" ID="Message_Text" style="height:350px; width:100%; border: none; resize: vertical;" required></asp:TextBox>
                                             </div>
                                    </div>
                                    <div class="compose-options">
                                        <div class="pull-right">
                                            <asp:Button runat="server" ID="Send_Message" OnClick="Send_Message_ServerClick" CssClass="btn btn-success" Text="Отправить"/>                                          
                                        </div>
                                        <label runat="server" id="output"></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div><!-- Row -->
                </div><!-- Main Wrapper -->
                <div class="page-footer">
                    <div class="container">
                          <p class="no-s">© 2017 All Rights Reserved BMS</p>
                    </div>
                </div>
                 </form>
            </div><!-- Page Inner -->
        </main><!-- Page Content -->
        
        <div class="cd-overlay"></div>
	

        <!-- Javascripts -->
        <Scripts:Scripts runat="server"/>
        
    </body>
</html>

