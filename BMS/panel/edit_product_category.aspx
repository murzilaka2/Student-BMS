<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit_product_category.aspx.cs" Inherits="BMS.panel.edit_product_category" %>
<% @ Register TagName="Styles" TagPrefix="Styles" Src="~/panel/fragments/Styles.ascx" %>
<% @ Register TagName="Scripts" TagPrefix="Scripts" Src="~/panel/fragments/Scripts.ascx" %>

<!DOCTYPE html>
<html>
     <head>      
        <title>BMS | Редактирование Категории Товара</title>       
        <meta content="width=device-width, initial-scale=1" name="viewport"/>
        <meta charset="UTF-8">
        <meta name="description" content="BMS - Редактирование Категории Товара." />
        
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
                <div class="page-breadcrumb">
                    <ol class="breadcrumb container">
                        <li><a href="product_category.aspx">Категория Товара</a></li>
                        <li class="active">Редактирование Категории Товара</li>
                    </ol>
                </div>
                <div class="page-title">
                    <div class="container">
                        <h3>Редактирование Категории Товара</h3>
                    </div>
                </div>
                <div id="main-wrapper" class="container">
                    <form id="form1" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                     <ContentTemplate>
                    <div class="row">                        
                        <div class="col-md-12">
                            <div class="panel panel-white">
                                <div class="panel-heading clearfix">
                                </div>
                                <div class="panel-body">                                   
                                        <div class="form-group">
                                            <label for="input-Default" class="col-sm-2 control-label">Название</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="Name" required></asp:TextBox>
                                                <br />
                                            </div>
                                        </div>                                                                                                                                       
                                     <div class="form-group">
                                            <label for="input-help-block" class="col-sm-2 control-label">Описание</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="Description" TextMode="MultiLine" style="resize: vertical;"></asp:TextBox>
                                                <br />                                    
                                            </div>
                                        </div>
                                    <div class="form-group" style="text-align: -webkit-center;"><br />
                                    <asp:Button runat="server" CssClass="btn btn-default btn-lg" ID="Edit_Category_Product" Text="Редактировать" OnClick="Edit_Category_Product_Click"/>                                   
                                    </div>
                                    <div class="form-group" style="text-align: -webkit-center;"><br />
                                    <label runat="server" id="output"></label>                                  
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div><!-- Row -->
                     </ContentTemplate>
                    </asp:UpdatePanel>
                  </form>
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
        
    </body>
</html>

