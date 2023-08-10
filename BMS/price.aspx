<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="price.aspx.cs" Inherits="BMS.price" %>

<!DOCTYPE html>
<html class="wide wow-animation" lang="ru">
  <head>
    <!--Site Title-->
    <title>Цены</title>
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
      <header class="page-header subpage_header">
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
            <%--Цены--%>
         <section class="text-center well well-sm section-border">
          <div class="container">
            <div class="row">
              <div class="col-lg-6 col-lg-offset-3 bg-white">
                <h1 class="text-bold">Выберите тип подписки</h1>
                <p class="lead">
                  Для Вашего удобства, мы разработали 4 варианта подписки.
                </p>
              </div>
            </div>
            <div class="row offset-1 flow-offset-2 pricing-border-left">
              <div class="col-sm-6 col-lg-3 pricing-box pricing-box-hover bg-white">
                <div class="thumbnail thumbnail-3">
                  <h6 class="text-uppercase text-light-clr letter-spacing-1">3 дня</h6><span class="icon icon-xl icon-light fa-hand-pointer-o"></span>
                  <div class="caption">
                    <h2 class="text-uppercase text-bold letter-spacing-1">Free</h2>
                    <p>3 дня бесплатного пользования системой управления бизнесом. Организуйте работу программы, почувствуйте как это удобно.</p><a class="btn btn-variant-1 btn-default btn-sm round-xl" href="#">Попробовать</a>
                  </div>
                </div>
              </div>
              <div class="col-sm-6 col-lg-3 pricing-box pricing-box-hover bg-white">
                <div class="thumbnail thumbnail-3">
                  <h6 class="text-uppercase text-light-clr letter-spacing-1">1 месяц</h6><span class="icon icon-xl icon-light fa-hand-peace-o"></span>
                  <div class="caption">
                    <h2 class="text-uppercase text-bold letter-spacing-1">$20</h2>
                    <p>Duis ligula turpis, placerat eget lobortis vitae, euismod vulputate orci. Suspendisse potenti. In hac habitasse platea dictumst. Quisque non consectetur lorem.</p><a class="btn btn-variant-1 btn-default btn-sm round-xl" href="#">Купить</a>
                  </div>
                </div>
              </div>
              <div class="col-sm-6 col-lg-3 pricing-box pricing-box-hover bg-white">
                <div class="thumbnail thumbnail-3">
                  <h6 class="text-uppercase text-light-clr letter-spacing-1">6 месяцев</h6><span class="icon icon-xl icon-light fa-hand-spock-o"></span>
                  <div class="caption">
                    <h2 class="text-uppercase text-bold letter-spacing-1">$50</h2>
                    <p>Duis ligula turpis, placerat eget lobortis vitae, euismod vulputate orci. Suspendisse potenti. In hac habitasse platea dictumst. Quisque non consectetur lorem.</p><a class="btn btn-variant-1 btn-default btn-sm round-xl" href="#">Купить</a>
                  </div>
                </div>
              </div>
              <div class="col-sm-6 col-lg-3 pricing-box pricing-box-hover bg-white">
                <div class="thumbnail thumbnail-3">
                  <h6 class="text-uppercase text-light-clr letter-spacing-1">1 год</h6><span class="icon icon-xl icon-light fa-hand-stop-o"></span>
                  <div class="caption">
                    <h2 class="text-uppercase text-bold letter-spacing-1">$100</h2>
                    <p>Duis ligula turpis, placerat eget lobortis vitae, euismod vulputate orci. Suspendisse potenti. In hac habitasse platea dictumst. Quisque non consectetur lorem.</p><a class="btn btn-variant-1 btn-default btn-sm round-xl" href="#">Купить</a>
                  </div>
                </div>
              </div>
            </div>
          </div>
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
