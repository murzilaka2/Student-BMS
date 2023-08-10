using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS.panel
{
    public partial class new_user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cookie_Check();
            SetImage();
            NewMessagesCount();
            LoadNewMessages();
            var localDateTime = DateTime.Now.ToString("yyyy-MM-dd").Replace(' ', 'T');
            StartDate.Text = localDateTime;
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
        protected void Add_User_Click(object sender, EventArgs e)
        {
            Add_User_Db();
        }
        private void Add_User_Db()
        {
                bool IsAdmin = false;
                if (Admin.Checked == true) { IsAdmin = true; }
                else { IsAdmin = false; }
                List<DataBase.User> User = new List<DataBase.User>();
                User.Add(new DataBase.User(0, IsAdmin, Login.Text, Password.Text, Fio.Text, Email.Text, Phone.Text, StartDate.Text, Convert.ToInt32(Salary.Text), Position.Text, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSMMqFV3bcUVCG4oVB1fPu7EkLnPOAu2R33F4kHBa9QVkh5mrNc"));
                DataBase.DataBase Database = new DataBase.DataBase();
                if (Database.AddUser(User, Request.Cookies["le"].Value.Substring(3)) == true) { output.InnerText = "Пользователь успешно добавлен!"; }
                else { output.InnerText = "Такой пользователь уже есть!"; }            
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
    }
}