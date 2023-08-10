﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS.panel
{
    public partial class message_view : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cookie_Check();
            SetImage();
            NewMessagesCount();
            LoadNewMessages();
            Checker();
        }
        private void Checker()
        {
            int res = 0;
            bool isInt = Int32.TryParse(Request.QueryString["message_id"], out res);
            if (isInt == true)
            {
              LoadMessage(Convert.ToInt32(Request.QueryString["message_id"]));              
            }
            else
            {
                Response.Redirect("messages.aspx");
            }
        }
        private void Cookie_Check()
        {
            DataBase.Helper Help = new DataBase.Helper();

            if (Request.Cookies["le"] != null && Request.Cookies["lo"] != null)
            {
                if (Help.MD5HASH(Request.Cookies["le"].Value.Substring(3)) != Request.Cookies["lo"].Value.Substring(3)) { Response.Redirect("../index.aspx"); }
            }
            else { Response.Redirect("../index.aspx"); }
        }
        protected void Exit_ServerClick(object sender, EventArgs e)
        {
            HttpCookie Cookie = new HttpCookie("le");
            Cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(Cookie);

            HttpCookie BbadCcookie = new HttpCookie("lo");
            BbadCcookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(BbadCcookie);

            Response.Redirect("../login.aspx");
        }
        private void SetImage()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            UserImage.Src = Database.UserImage(Request.Cookies["le"].Value.Substring(3));
        }
        private void NewMessagesCount()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            int count = Database.NewMessagesCount(Request.Cookies["le"].Value.Substring(3));
            NewMessageHeader.InnerText = count.ToString();
            MessagesCount.InnerText = count.ToString();
        }
        private void LoadNewMessages()
        {
            int count = 0;
            DataBase.DataBase Database = new DataBase.DataBase();
            foreach (var item in Database.LoadNewMessages(Request.Cookies["le"].Value.Substring(3)))
            {
                if (count < 3)
                {
                    NewMessages.Controls.Add(new LiteralControl("<li><a href=\"message_view.aspx?message_id=" + item.GetID() + "\"><div class=\"msg-img\"><div class=\"online on\"></div><img class=\"img-circle\" src=\"" + Database.GetNewMessageImage(item.GetFrom()) + "\"></div><p class=\"msg-name\">" + item.GetFrom() + "</p><p class=\"msg-text\">" + item.GetSubject() + "</p><p class=\"msg-time\">" + item.GetTime() + " <span style=\"float:right;\"> " + item.GetDate() + "</span></p></a></li>"));
                    count++;
                }
            }
            if (count >= 1) { MessagesHeaderText.InnerText = "У Вас новое сообщение!"; }
        }
        private void LoadMessage(int ID)
        {
            string Email = "";
            string OwnEmail = "";
            DataBase.DataBase Database = new DataBase.DataBase();
            foreach (var item in Database.ShowMessage(ID))
            {
                Email = item.GetFrom();
                OwnEmail = item.GetTo();
                MessageHeader.InnerText = item.GetSubject();
                MessageEmail.InnerText = item.GetFrom();
                MessageText.InnerText = item.GetMessageText();
                MessageDate.InnerText = item.GetDate();
                MessageTime.InnerText = item.GetTime();
                DeleteMessage.HRef = "delete_message.aspx?message_id=" + item.GetID()+"";
                DeleteMessage2.HRef = "delete_message.aspx?message_id=" + item.GetID() + "";
                
            }
            foreach (var item in Database.GetUser(Email))
            {
                MessageAvatar.Src = item.GetImageUrl();
                MesssageFio.InnerText = item.GetFio();
                ReSent.HRef = "new_message.aspx?message_to=" + item.GetEmail()+"";
                UserProfile.HRef = "person_view.aspx?login="+item.GetLogin()+"";
            }
            if (OwnEmail == Database.GetEmail(Request.Cookies["le"].Value.Substring(3)))
            {
                Database.ReadMessage(ID);
            }
            }                   
        }    
}