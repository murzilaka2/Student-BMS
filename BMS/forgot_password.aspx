<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forgot_password.aspx.cs" Inherits="BMS.forgot_password" %>

<!DOCTYPE html>
<html class="wide wow-animation" lang="ru">
  <head>
    <!--Site Title-->
    <title>Забыли пароль?</title>
    <meta charset="utf-8">
    <meta name="format-detection" content="telephone=no">
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
    <link rel="icon" href="images/favicon.ico" type="image/x-icon">
    <link rel="stylesheet" href="css/style.css">
<!--[if lt IE 10]>
    <div style="background: #212121; padding: 10px 0; box-shadow: 3px 3px 5px 0 rgba(0,0,0,.3); clear: both; text-align:center; position: relative; z-index:1;"><a href="https://windows.microsoft.com/en-US/internet-explorer/.."><img src="images/ie8-panel/warning_bar_0000_us.jpg" border="0" height="42" width="820" alt="You are using an outdated browser. For a faster, safer browsing experience, upgrade for free today."></a></div>
    <script src="js/html5shiv.min.js"></script><[endif]-->
  </head>

  <body>
    <div class="page">
      <header class="bg-image bg-image-2 well-inset-3">
    <!--Меню-->
        <div class="rd-navbar-wrap">
          <nav class="rd-navbar top-panel-none-items" data-layout="rd-navbar-fixed" data-stick-up="false" data-sm-layout="rd-navbar-fullwidth" data-sm-device-layout="rd-navbar-fullwidth" data-md-layout="rd-navbar-static" data-md-device-layout="rd-navbar-static" data-lg-device-layout="rd-navbar-static" data-lg-layout="rd-navbar-static" data-sm-stick-up="true" data-md-stick-up="true" data-lg-stick-up="true" data-stick-up-clone="true">
            <div class="rd-navbar-inner">
              <div class="rd-navbar-panel">
                <button class="rd-navbar-toggle" data-rd-navbar-toggle=".rd-navbar"><span></span></button>
                <div class="rd-navbar-brand"><a class="brand-name" href="index.aspx">BMS</a></div>
              </div>
              <div class="rd-navbar-nav-wrap">
                <!--Поиск-->
                <div class="rd-navbar-search">
                  <form class="rd-navbar-search-form" action="#" method="GET">
                    <label class="rd-navbar-search-form-input">
                      <input type="text" name="s" placeholder="Search.." autocomplete="off">
                    </label>
                    <button class="rd-navbar-search-form-submit" type="submit"></button>
                  </form><span class="rd-navbar-live-search-results"></span>
                  <button class="rd-navbar-search-toggle" data-rd-navbar-toggle=".rd-navbar-search, .rd-navbar-live-search-results"></button>
                </div>
                <!--Поиск конец-->
                <!--Навигация-->
                <ul class="rd-navbar-nav">
                  <li><a href="index.aspx">Главная</a></li>
                  <li><a href="login.aspx">Аккаунт</a></li>
                  <li><a href="news.aspx">Новости</a></li>
                  <li><a href="contact.aspx">Обратная связь</a></li>
                </ul>
                <!--Навигация конец-->
              </div>
            </div>
          </nav>
        </div>
        <!--Меню конец-->
        <section class="text-center">
          <div class="jumbotron text-center offset-large">
          </div>
        </section>
        <form id="form1" runat="server">

        <section class="well well-sm">
          <div class="container">
            <div class="row flow-offset-1">
              <div class="col-md-6 col-md-offset-3 btn-shadow inset-sm-min img-rounded bg-white">
                <h5 class="text-center">Забыли пароль?</h5>
                <div class="rd-mailform max-width">
                  <div class="row row-20">
                    <div class="col-sm-12">
                      <div class="form-wrap form-wrap-validation validation-with-outside-label">
                        <label class="form-label-outside" for="forms-sub-name">Email</label>
                        <input class="form-input" type="email" required runat="server" ID="Email">
                      </div>
                        <label>Введите зарегистрированный Email адрес и мы отправим Вам новый пароль.</label>
                    </div>
                      <div class="col-sm-12">
                      <asp:Button runat="server" Text="Отправить" CssClass="btn btn-primary btn-xs round-xl btn-block form-el-offset-1" ID="Send" OnClick="Send_Click"/>
                      </div>                  
                    <label runat="server" ID="Result" visible="false"></label>                                     
                  </div>
                </div>
              </div>
            </div>    
          </div><div class="div-clear"></div>      
        </section>
            </form>
      </header>
	  
      <main class="page-content">
      </main>

      <footer class="page-footer footer-centered text-center">
        <section class="footer-content">
          <div class="container">
            <div class="navbar-brand"><a href="index.aspx">BMS</a></div>
            <p class="big">Система управления бизнесом (Business Management System).</p>
            <ul class="list-inline">
              <li><a class="fa-facebook" href="#"></a></li>
              <li><a class="fa-pinterest-p" href="#"></a></li>
              <li><a class="fa-twitter" href="#"></a></li>
              <li><a class="fa-google-plus" href="#"></a></li>
              <li><a class="fa-instagram" href="#"></a></li>
            </ul>
          </div>
        </section>
        <section class="copyright">
          <div class="container">
            <p>&#169; <span class="copyright-year"></span> All Rights Reserved <a href="index.aspx">BMS</a></p>
          </div>
        </section>
      </footer>
    </div>
    <div class="snackbars" id="form-output-global"></div>
    <script src="js/core.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/script.js"></script>
  </body>
 </html>
