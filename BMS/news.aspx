﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="news.aspx.cs" Inherits="BMS.news" %>
<form id="form1" runat="server"></form>
<!DOCTYPE html>
<html class="wide wow-animation" lang="ru">
  <head>
    <title>Новости</title>
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
                  <li class="active"><a href="news.aspx">Новости</a></li>
                  <li><a href="contact.aspx">Обратная связь</a></li>
                </ul>
                <!--Навигация конец-->
              </div>
            </div>
          </nav>
        </div>
        <!--Меню конец-->
        <section>
          <!--Swiper-->
          <div class="swiper-container swiper-slider" data-autoplay="5000" data-slide-effect="fade" data-loop="false">
            <div class="jumbotron text-center">
              <h1><small>Blog</small>Наши последние новости
              </h1>
              <p class="big"></p>
            </div>
            <%--<div class="swiper-wrapper">
              <div class="swiper-slide" data-slide-bg="images/header-1.jpg">
                <div class="swiper-slide-caption"></div>
              </div>
              <div class="swiper-slide" data-slide-bg="images/header-3.jpg">
                <div class="swiper-slide-caption"></div>
              </div>
              <div class="swiper-slide" data-slide-bg="images/header-4.jpg">
                <div class="swiper-slide-caption"></div>
              </div>
            </div>--%>
          </div>
        </section>
      </header>

      <main class="page-content">
        <section class="section-border text-center text-md-left">
          <div class="container">
           
          </div>
        </section>

        <!--Start section-->
        <section class="text-center text-md-left offset-1">
          <div class="container">
            <div class="row">
              <div class="col-xs-12 section-border">
                <article class="thumbnail well">
				<h4><a href="blog_post.html">Ранний доступ BMS будет открыт 01.01.2018</a></h4>
				<img src="images/news/news1.jpg" alt="Ранний доступ BMS">
                  <div class="caption">
                    <p>Оставляйте заявку и гарантированно получите ранний доступ к Business Management System совершенно бесплатно на первый месяц.<br />Оставьте свой отзыв о сервисе и получите 3 месяца бесплатного доступа в подарок!</p>
                    <div class="blog-info">
                      <div class="pull-md-left">
                        <time class="meta fa-calendar" datetime="2015">7 Ноября, 2017</time><a class="badge fa-user text-uppercase font-secondary">Admin</a><span class="tags"><a class="badge fa-tags"></a><a class="post-tag round-xl small">General</a></span>
                      </div><a class="btn-link" href="blog_post.html">Прочитать подробнее</a>
                    </div>
                  </div>
                </article>
              </div>
                          
            </div>
          </div>
        </section>
		
        <!--Start section-->
        <!-- <section class="well text-center text-md-left"> -->
          <!-- <div class="container"> -->
            <!-- <div class="col-inset-2 text-center btn-group" aria-label="First group" role="group"><a class="btn btn-default round-small active" href="blog_default.html#">1</a><a class="btn btn-default round-small" href="blog_default.html#">2</a><a class="btn btn-default round-small" href="blog_default.html#">3</a><span class="text-light-clr font-secondary">...</span><a class="btn btn-default round-small" href="blog_default.html#">8</a></div> -->
          <!-- </div> -->
        <!-- </section> -->
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