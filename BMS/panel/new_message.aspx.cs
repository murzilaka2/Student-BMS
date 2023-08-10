using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS.panel
{
    public partial class new_message : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cookie_Check();
            SetImage();
            NewMessagesCount();
            LoadNewMessages();
            if (!IsPostBack)
            {
                if (Request.QueryString["message_to"] != String.Empty)
                {
                    To.Text = Request.QueryString["message_to"];
                    Subject.Text = Request.QueryString["message_subject"];
                    Message_Text.Text = Request.QueryString["message_text"];
                }
                else
                {
                    Get();
                }
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
        protected void Send_Message_ServerClick(object sender, EventArgs e)
        {
            Send();
        }
        private void Send()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            if (Database.CheckEmail(To.Text) == true)
            {
                if (Database.SendMessage(Request.Cookies["le"].Value.Substring(3), To.Text, Subject.Text, Message_Text.Text) == true)
                {
                    Response.Redirect("messages.aspx");
                }
                else
                {
                    output.InnerText = "Ошибка при отправке сообщения.";
                }
            }else
            {
                output.InnerText = "Email не найден в базе.";
            }
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
        private void Get()
        {
            string Email = Request.QueryString["email"];
            if (Email != String.Empty)
            {
                To.Text = Email;
            }
        }
        protected void Draft_ServerClick(object sender, EventArgs e)
        {
            Draft();
        }
        private void Draft()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            if (Database.CheckEmail(To.Text) == true)
            {
                if (Subject.Text != String.Empty || Message_Text.Text != String.Empty)
                {
                    if (Database.SaveDraftMessage(Request.Cookies["le"].Value.Substring(3), To.Text, Subject.Text, Message_Text.Text) == true)
                    {
                        Response.Redirect("messages.aspx");
                    }
                    else
                    {
                        output.InnerText = "Ошибка при сохранении сообщения.";
                    }
                }else
                {
                    output.InnerText = "Заполните все поля";
                }
            }else
            {
                output.InnerText = "Email не найден в базе.";
            }
        }
    }
}