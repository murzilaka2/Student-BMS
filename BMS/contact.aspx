<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="BMS.contact" %>
<!DOCTYPE html>
<html class="wide wow-animation" lang="ru">
  <head>
    <!--Site Title-->
    <title>Обратная связь</title>
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
                  <li class="active"><a href="contact.aspx">Обратная связь</a></li>
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

        <section class="well well-sm">
          <div class="container">
            <div class="row flow-offset-1">
              <div class="col-md-8 btn-shadow inset-sm-min img-rounded bg-white">
                <h5 class="text-center">Обратная связь</h5>
                <form id="form1" runat="server">               
                  <div class="row row-20">
                   <label runat="server" ID="Result"></label>
                    <div class="col-sm-6">
                      <div class="form-wrap form-wrap-validation validation-with-outside-label">
                        <label class="form-label-outside" for="forms-name">Имя</label>
                        <input class="form-input" type="text" placeholder="Ваше Имя" required runat="server" ID="Name">
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="form-wrap form-wrap-validation validation-with-outside-label">
                        <label class="form-label-outside" for="forms-last-name">Фамилия</label>
                        <input class="form-input" type="text" placeholder="Ваша Фамилия" required runat="server" ID="LastName">
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="form-wrap form-wrap-validation validation-with-outside-label">
                        <label class="form-label-outside" for="forms-message">Сообщение</label>
                        <textarea class="form-input" placeholder="Ваше сообщение" required runat="server" ID="Message"></textarea>
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <div class="form-wrap form-wrap-validation validation-with-outside-label">
                        <label class="form-label-outside" for="forms-email">E-mail</label>
                        <input class="form-input" type="email" placeholder="example@domain.com" required runat="server" ID="Email">
                      </div>
                    </div>
                    <div class="col-sm-6">
                      <asp:Button runat="server" CssClass="btn btn-primary btn-xs round-xl btn-block form-el-offset-1" Text="Отправить" ID="Send_Message" OnClick="Send_Message_Click"/>
                    </div>
              <%--<div class="g-recaptcha" data-sitekey="6Le3xTcUAAAAAPsDd2Km7QdfLgG1fNbNH0_Z35Ry"></div>--%>
                  </div>
                </form>
              </div>
              <div class="col-md-4 text-center text-md-left">
                <address class="contact-block inset-sm-min-2 img-rounded bg-white btn-shadow">
                  <dl>
                    <dt class="h6">Мы открыты</dt>
                    <dd>С 9.00 - 18.00</dd>
                    <dt class="h6">Телефон</dt>
                    <dd><a href="callto:#">+ 380 970601478</a></dd>
                    <dt class="h6">E-MAIL</dt>
                    <dd><a href="emailto:#"> enykoruna1@gmail.com</a></dd>
                  </dl>
                  <ul class="list-inline list-inline-3">
                    <li><a class="fa-facebook-square" href="#"></a></li>
                    <li><a class="fa-twitter-square" href="#"></a></li>
                    <li><a class="fa-google-plus-square" href="#"></a></li>
                  </ul>
                </address>
              </div>
            </div>
          </div>
        </section>
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
    <script src='https://www.google.com/recaptcha/api.js'></script>
  </body>
 </html>