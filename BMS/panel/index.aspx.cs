using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS.panel
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cookie_Check();
            SetImage();
            NewMessagesCount();
            LoadNewMessages();
            LoadStats();
            LoadObjectives();
            LoadFriends();
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
            MessagesHeaderText.InnerText = count.ToString();
        }
        private void LoadNewMessages()
        {
            int count = 0;
            DataBase.DataBase Database = new DataBase.DataBase();
            foreach (var item in Database.LoadNewMessages(Request.Cookies["le"].Value.Substring(3)))
            {
                if (count < 3)
                {
                    NewMessages.Controls.Add(new LiteralControl("<li><a href=\"message_view.aspx?message_id=" + item.GetID() + "\"><div class=\"msg-img\"><div class=\"online on\"></div><img class=\"img-circle\" src=\""+Database.GetNewMessageImage(item.GetFrom())+ "\"></div><p class=\"msg-name\">" + item.GetFrom() + "</p><p class=\"msg-text\">" + item.GetSubject() + "</p><p class=\"msg-time\">" + item.GetTime() + " <span style=\"float:right;\"> " + item.GetDate()+"</span></p></a></li>"));
                    count++;
                }                   
            }
            if (count >= 1){ MessagesHeaderText.InnerText = "У Вас новое сообщение!"; }           
        }
        private void LoadStats()
        {
            int Orders = 0;
            int Money = 0;
            int UsersCount = 0;
            DataBase.DataBase Database = new DataBase.DataBase();
            int Group = Database.UserGroup(Request.Cookies["le"].Value.Substring(3));
            foreach (var item in Database.LoadStatsReport(Request.Cookies["le"].Value.Substring(3), Group))
            {
                foreach (var item2 in Database.GetProductByID(item.GetProductID()))
                {
                    Orders++;
                    Money += item2.GetCostOfSales();
                }
            }
            MoneyForDay.InnerText = Money.ToString();
            OrdersForDay.InnerText = Orders.ToString();
            foreach (var item in Database.ShowUsers(Request.Cookies["le"].Value.Substring(3)))
            {
                UsersCount++;
            }
            Users.InnerText = UsersCount.ToString();
        }
        private void LoadObjectives()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            foreach (var item in Database.LoadObjectives(Request.Cookies["le"].Value.Substring(3)))
            {
                string Done = "label label-danger";
                string Text = "Не выполнен";
                if (item.GetIsDone() == true)
                {
                    Done = "label label-success";
                    Text = "Выполнен";
                }
                ObjectivesPlaceholder.Controls.Add(new LiteralControl("<tr><td>" + item.GetText() + "</td><td><span class=\"" + Done + "\">" + Text + "</span></td><td>" + item.GetAuthor() + "</td><td>" + item.GetDate() +"</td></tr>"));             
            }
        }
        private void LoadFriends()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            foreach (var item in Database.LoadFriends(Request.Cookies["le"].Value.Substring(3)))
            {
                foreach (var item2 in Database.ShowSelectedUser(item.GetFriendName()))
                {
                    FriendsPlaceholder.Controls.Add(new LiteralControl("<a href=\"person_view.aspx?login="+item2.GetLogin()+"\"><div class=\"inbox-item\"><div class=\"inbox-item-img\"><img src=\"" + item2.GetImageUrl()+"\" class=\"img-circle\"></div><p class=\"inbox-item-author\">"+item2.GetLogin()+"</p><p class=\"inbox-item-text\">"+item2.GetFio()+"</p><p class=\"inbox-item-date\">"+item2.GetPosition()+"</p></div></a>"));
                }                
            }
        }
    }
}