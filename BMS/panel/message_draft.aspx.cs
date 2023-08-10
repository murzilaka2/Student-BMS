using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BMS.panel.assets
{
    public partial class message_draft : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cookie_Check();
            SetImage();
            NewMessagesCount();
            LoadNewMessages();
            LoadMessages();
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
            MessagesCount.InnerText = count.ToString();
            NewMessageHeader.InnerText = count.ToString();
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
        private void LoadMessages()
        {
            string Status = "unread";
            DataBase.DataBase Database = new DataBase.DataBase();
            foreach (var item in Database.LoadDraftMessages(Request.Cookies["le"].Value.Substring(3)))
            {
                if (item.GetIsRead() == true)
                {
                    Status = "read";
                }
                //Сделать удаление черновика!               
                Messages.Controls.Add(new LiteralControl("<tr class=\"" + Status + "\"><td class=\"hidden-xs\"><span><input runat=\"server\" type=\"checkbox\" id=\"messagecheck\" class=\"checkbox-mail\"></span></td><td onclick=\"document.location = 'new_message.aspx?message_to=" + item.GetTo() + "&message_subject=" + item.GetSubject() + "&message_text=" + item.GetMessageText() + "';\"  class=\"hidden-xs\"><i class=\"fa fa-star icon-state-warning\"></i></td> <td onclick=\"document.location = 'new_message.aspx?message_to=" + item.GetTo() + "&message_subject=" + item.GetSubject() + "&message_text=" + item.GetMessageText() + "';\" class=\"hidden-xs\">" + item.GetFrom() + "</td><td onclick=\"document.location = 'new_message.aspx?message_to=" + item.GetTo() + "&message_subject=" + item.GetSubject() + "&message_text=" + item.GetMessageText() + "';\" >" + item.GetMessageText() + "</td><td></td><td onclick=\"document.location = 'new_message.aspx?message_to=" + item.GetTo() + "&message_subject=" + item.GetSubject() + "&message_text=" + item.GetMessageText() + "';\" >" + item.GetDate() + "<td></tr>"));
            }
        }

        protected void DeleteMessage_ServerClick(object sender, EventArgs e)
        {
            //Удаление сообщений

        }
    }
}