<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="BMS.index" %>
<!DOCTYPE html>
<html class="wide wow-animation" lang="ru">
  <head>
    <title>BMS</title>
    <meta charset="utf-8">
    <meta name="format-detection" content="telephone=no">
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
    <!--Stylesheets-->
    <link rel="icon" href="images/favicon.ico" type="image/x-icon">
    <link rel="stylesheet" href="css/style.css">

<!--[if lt IE 10]>
    <div style="background: #212121; padding: 10px 0; box-shadow: 3px 3px 5px 0 rgba(0,0,0,.3); clear: both; text-align:center; position: relative; z-index:1;"><a href="https://windows.microsoft.com/en-US/internet-explorer/.."><img src="images/ie8-panel/warning_bar_0000_us.jpg" border="0" height="42" width="820" alt="You are using an outdated browser. For a faster, safer browsing experience, upgrade for free today."></a></div>
    <script src="js/html5shiv.min.js"></script><[endif]-->
  </head>

  <body>
    <div class="page">
      <header class="page-header">
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
                  <li class="active"><a href="index.aspx">Главная</a></li>
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
        <section>
          <!-- Parallax-->
          <section class="parallax-container">
            <div class="material-parallax"><img src="images/header-6.jpg" alt="" width="2054" height="1116"></div>
            <div class="parallax-content well-parallax jumbotron">
              <div class="text-center">
                <h1><small>Система управления бизнесом</small>Business Management System
                </h1>
                <p class="big">Мы создаем качественную продукцию<br /> с учетом ваших потребностей и требований.</p>
				<div class='btn-group-variant'>
				<a class='btn btn-default round-xl btn-sm' href='price.aspx'>Купить</a>
				<a class='btn btn-default round-xl btn-sm' href='index.aspx#freetry'>Попробовать</a>
				</div>
              </div>
            </div>
          </section>
        </section>
      </header>
 
      <main class="page-content">
        <!--Start section-->
        <section class="text-center well well-sm">
          <div class="container">
            <div class="row">
              <div class="col-lg-10 col-lg-offset-1">
                <h1 class="text-bold">Зачем мне BMS?</h1>
                <p class="lead big"><b>BMS</b> позволяет организовать работу компании просто и удобно.<br />Вы получите готовые отчеты и показатели работы персонала.<br />
				Для сотрудников, простая и понятная программа для управления своими задачами и временем, площадка для совместной работы и достижения лучших результатов.
				</p>
				<img class="box-shadow offset-2 margin-negative" src="images/index_img1.jpg" alt="">
              </div>
            </div>
          </div>
        </section>
        <!--End section-->

        <!--Start section-->
        <section class="well well-sm bg-lighter relative text-center">
          <div class="container">
            <div class="row">
              <div class="col-lg-6 col-lg-offset-3">
                <h1 class="text-bold">Возможности BMS</h1>
                <p class="lead">
					Совместная работа над задачами и контролем - ускорит рост вашей компании.
                </p>
              </div>
            </div>
            <div class="row offset-1 text-md-center flow-offset-1">
              <div class="col-sm-6 col-md-3"><span class="icon icon-lg icon-primary fa-heartbeat"></span>
                <h5>Контроль задач</h5>
                <p>Каждый знает, в чем состоит его задача и к какому сроку необходимо ее выполнить.</p>
              </div>
              <div class="col-sm-6 col-md-3"><span class="icon icon-lg icon-primary fa-compass"></span>
                <h5>Управление продажами</h5>
                <p>Анализируйте продажи, планируйте активности и повышайте лояльность клиентов.</p>
              </div>
              <div class="col-sm-6 col-md-3"><span class="icon icon-lg icon-primary fa-edit"></span>
                <h5>Чистый Дизайн</h5>
                <p>Адаптивный и интуитивный дизайн, с которым не возникнет трудностей.</p>
              </div>
              <div class="col-sm-6 col-md-3"><span class="icon icon-lg icon-primary fa-comments-o"></span>
                <h5>Вовлечение сотруников</h5>
                <p>Обменивайтесь новыми идеями, обсуждайте задачи в чате, следите за показателями.</p>
              </div>
            </div>
          </div>
        </section>
        <!--End section-->

        <!--Start section-->
        <section class="well well-sm text-center text-md-left">
          <div class="container">
            <div class="row">
              <div class="col-md-6 col-lg-5">
                <h1 class="text-bold">BMS</h1>
                <p class="lead">
				Помогает повысить эффективность компании на 75% уже через три месяца.
Контролируйте продажи, проекты, документы и задачи онлайн
				</p>
              </div>
              <div class="col-md-6 col-lg-6 col-lg-offset-1 text-left">
                <p class="font-secondary big text-uppercase text-light-clr inset-2">Продажи</p>
                <div class="progress">
                  <div class="progress-bar" role="progressbar" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100"></div><span class="small text-light-clr"></span>
                </div>
                <p class="font-secondary big text-uppercase text-light-clr">Маркетинг</p>
                <div class="progress">
                  <div class="progress-bar" role="progressbar" aria-valuenow="85" aria-valuemin="0" aria-valuemax="100"></div><span class="small text-light-clr"></span>
                </div>
                <p class="font-secondary big text-uppercase text-light-clr">Автоматизация</p>
                <div class="progress">
                  <div class="progress-bar" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100"></div><span class="small text-light-clr"></span>
                </div>
              </div>
            </div>
          </div>
        </section>
        <!--End section-->

        <!--Start section-->
        <section class="bg-dark-var1 text-center">
          <div class="container counter-panel">
            <div class="row">
              <div class="col-xs-6 col-sm-6 col-md-3">
                <div class="counter" data-from="0" data-to="157"></div>
                <p class="text-opacity font-secondary text-uppercase">Чашек кофе</p>
              </div>
              <div class="col-xs-6 col-sm-6 col-md-3">
                <div class="counter" data-from="0" data-to="27"></div>
                <p class="text-opacity font-secondary text-uppercase">Новых клиентов</p>
              </div>
              <div class="col-xs-6 col-sm-6 col-md-3">
                <div class="counter" data-from="0" data-to="98"></div>
                <p class="text-opacity font-secondary text-uppercase">Постоянных клиентов</p>
              </div>
              <div class="col-xs-6 col-sm-6 col-md-3">
                <div class="counter" data-from="0" data-to="4230"></div>
                <p class="text-opacity font-secondary text-uppercase">Доход гривен</p>
              </div>
            </div>
          </div>
        </section>
        <!--End section-->

        <!--Start section-->
        <section class="well well-sm well-inset-3 bg-image bg-image-1 context-dark text-center" id="freetry">
         <form id="form1" runat="server">
          <div class="container">
            <div class="row">
              <div class="col-md-8 col-md-offset-2">
                <h1 class="text-bold">Попробуйте бесплатно!</h1>
                <p class="lead">Система управления бизнесом (BMS), которая позволяет компаниям расти и развиваться с учетом постоянно растущих требований рынка.</p>
                <div class="rd-mailform subscribe-form offset-1" data-form-output="form-output-global" data-form-type="forms">
                  <div class="form-wrap form-wrap-validation">
                    <asp:TextBox runat="server" CssClass="form-input" TextMode="Email" placeholder="email@domainname.com" ID="EmailText" required></asp:TextBox>
                  </div>
                  <div class="button-wrap text-center">
                    <asp:Button runat="server" CssClass="btn btn-primary btn-xs round-xl" ID="GetButton" Text="Получить" OnClick="GetButton_Click"/>
                  </div>
                </div>
              </div>
            </div>
          </div>
         </form>
        </section>
        <!--End section-->

        <!--Start section-->
        <section class="well well-sm well-inset-2 text-center">
          <div class="container">
            <div class="row">
              <div class="col-lg-6 col-lg-offset-3">
                <h1 class="text-bold text-center">Отзывы</h1>
                <p class="lead">Incidunt deleniti blanditiis quas aperiam recusandae consequatur ullam quibusdam cum libero illo rerum!</p>
              </div>
            </div>
            <div class="row offset-1">
              <!--Owl Carousel-->
              <div class="owl-carousel" data-items="1" data-sm-items="2" data-xs-items="1" data-md-items="3" data-nav="true" data-margin="30">
                <div class="owl-item">
                  <blockquote class="quote-2"><img class="img-circle" src="images/index_img5.jpg" alt="">
                    <h6>
                      <cite>Jennifer Rogers</cite>
                    </h6>
                    <p class="small text-light-clr text-uppercase">web-designer</p>
                    <p class="h6 text-italic font-base text-base">
                      <q>Lectus tincidunt pellentesque augue urna sit sed, arcu sed ante ac montes pellentesque consectetuer, neque magnis penatibus laoreet vehicula nulla orci, a malesuada justo laoreet ipsum, in ac fusce.</q>
                    </p>
                  </blockquote>
                </div>
                <div class="owl-item">
                  <blockquote class="quote-2"><img class="img-circle" src="images/index_img6.jpg" alt="">
                    <h6>
                      <cite>Walter Williams</cite>
                    </h6>
                    <p class="small text-light-clr text-uppercase">web-designer</p>
                    <p class="h6 text-italic font-base text-base">
                      <q>Lectus tincidunt pellentesque augue urna sit sed, arcu sed ante ac montes pellentesque consectetuer, neque magnis penatibus laoreet vehicula nulla orci, a malesuada justo laoreet ipsum, in ac fusce.</q>
                    </p>
                  </blockquote>
                </div>
                <div class="owl-item">
                  <blockquote class="quote-2"><img class="img-circle" src="images/index_img7.jpg" alt="">
                    <h6>
                      <cite>Derrick Whitehead</cite>
                    </h6>
                    <p class="small text-light-clr text-uppercase">web-designer</p>
                    <p class="h6 text-italic font-base text-base">
                      <q>Lectus tincidunt pellentesque augue urna sit sed, arcu sed ante ac montes pellentesque consectetuer, neque magnis penatibus laoreet vehicula nulla orci, a malesuada justo laoreet ipsum, in ac fusce.</q>
                    </p>
                  </blockquote>
                </div>
              </div>
            </div>
          </div>
        </section>
        <!--End section-->
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