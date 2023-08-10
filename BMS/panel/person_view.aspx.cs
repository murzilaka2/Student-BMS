using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS.panel
{
    public partial class person_view : System.Web.UI.Page
    {
        private bool isFriend { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Cookie_Check();
            SetImage();
            NewMessagesCount();
            LoadNewMessages();
            LoadUser();
            FriendCheck();
        }
        private string UserEmail { get; set; }
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
        private void LoadUser()
        {
            string UserLogin = Request.QueryString["login"];
            if (!string.IsNullOrEmpty(UserLogin))
            {
                DataBase.DataBase Database = new DataBase.DataBase();
                if (Database.ShowSelectedUser(UserLogin).Count >= 1)
                {
                    foreach (var item in Database.ShowSelectedUser(UserLogin))
                    {
                        Image.Src = item.GetImageUrl();
                        Login.InnerText = item.GetLogin();
                        FIO.InnerText = item.GetFio();
                        Position.InnerText = item.GetPosition();
                        Email.InnerText = item.GetEmail();
                        UserEmail = item.GetEmail();
                        Phone.InnerText = item.GetPhone();
                    }
                }
                else
                {
                    Response.Redirect("index.aspx");
                }
            }
            else
            {
                Response.Redirect("index.aspx");
            }
            
        }
        protected void Send_Message_Click(object sender, EventArgs e)
        {
            Response.Redirect("new_message.aspx?message_to="+ UserEmail + "");
        }
        private void FriendCheck()
        {
            string UserLogin = Request.QueryString["login"];
            DataBase.DataBase Database = new DataBase.DataBase();
            if (Database.LoadFriends(Request.Cookies["le"].Value.Substring(3)).Count >= 1)
            {
                foreach (var item in Database.LoadFriends(Request.Cookies["le"].Value.Substring(3)))
                {
                    foreach (var item2 in Database.ShowSelectedUser(item.GetFriendName()))
                    {
                        if (UserLogin == item2.GetLogin())
                        {
                            Add_To_Friends.Text = "Убрать из друзей";
                            isFriend = true;
                        }
                    }
                }
            }
            else
            {
                Add_To_Friends.Text = "Добавить в друзья";
                isFriend = false;
            }
            
        }
        protected void Add_To_Friends_Click(object sender, EventArgs e)
        {
            FriendOperation();
        }
        private void FriendOperation()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            List<DataBase.Friends> Friends = new List<DataBase.Friends>();
            Friends.Add(new DataBase.Friends(0, Request.Cookies["le"].Value.Substring(3), Request.QueryString["login"]));
            if (isFriend == false)
            {
                Database.AddToFriends(Friends);
            }
            else
            {
                Database.DeleteFromFriends(Friends);
            }
            Response.Redirect("person_view.aspx?login="+ Request.QueryString["login"]);
        }
    }
}