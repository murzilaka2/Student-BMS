using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS.panel.assets
{
    public partial class edit_user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cookie_Check();
            if (Checker() == true)
            {
                SetImage();
                NewMessagesCount();
                LoadNewMessages();
                if (!IsPostBack)
                {
                    ShowUser();
                }
            }else
            {
                Response.Redirect("users.aspx");
            }
        }
        private bool Checker()
        {
            string Login = Request.Cookies["le"].Value.Substring(3);
            string EditLogin = Request.QueryString["edit_login"];
            DataBase.DataBase Database = new DataBase.DataBase();
            if (Login == EditLogin)
            {
                return true;
            }
            else if(Database.CheckIsAdmin(Login) == true)
            {
                AdminRadio.Enabled = true;
                UserRadio.Enabled = true;
                return true;
            }
            else
            {
                return false;
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
        protected void Edit_User_Click(object sender, EventArgs e)
        {
            SaveUser();
        }
        private void ShowUser()
        {
            string EditLogin = Request.QueryString["edit_login"];
            if (EditLogin != null)
            {
                DataBase.DataBase Database = new DataBase.DataBase();
                if (Database.UserGroup(Request.Cookies["le"].Value.Substring(3)) == Database.UserGroup(EditLogin))
                {
                    List<DataBase.User> User = new List<DataBase.User>();
                    foreach (var item in Database.ShowSelectedUser(EditLogin))
                    {
                        if (item.GetIsAdmin() == true)
                        {
                            AdminRadio.Checked = true;
                        }
                        else
                        {
                            UserRadio.Checked = true;
                        }
                        Login.Text = item.GetLogin();
                        Fio.Text = item.GetFio();
                        Email.Text = item.GetEmail();
                        Phone.Text = item.GetPhone();
                        StartDate.Text = item.GetStartDate();
                        Salary.Text = item.GetSalary().ToString();
                        Position.Text = item.GetPosition();
                        ImageUrl.Text = item.GetImageUrl();
                    }
                }
                else
                {
                    Response.Redirect("users.aspx");
                }
            }else
            {
                Response.Redirect("users.aspx");
            }
            }    
        private void SaveUser()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            List<DataBase.User> User = new List<DataBase.User>();
            DataBase.Helper Help = new DataBase.Helper();
            try
            {               
                bool isAdmin = false;
                if (AdminRadio.Checked == true)
                {
                    isAdmin = true;
                }
                User.Add(new DataBase.User(0, isAdmin, Login.Text, null, Fio.Text, Email.Text, Phone.Text, StartDate.Text, Convert.ToInt32(Salary.Text), Position.Text, ImageUrl.Text));
                if (Database.EditUser(User) == true)
                {
                    output.InnerText = "Пользователь успешно изменен.";
                }
                else
                {
                    output.InnerText = "Ошибка. Попробуйте еще раз.";
                }
            }
            catch (Exception ex) { }                 
            }
        protected void PasswordReset_Click(object sender, EventArgs e)
        {
            if (OldPassword.Text.Length > 1 && NewPassword.Text.Length >1)
            {
                ChangePassword();
            }else
            {
                password_output.InnerText = "Введите пароли.";
            }
        }
        private void ChangePassword()
        {
            DataBase.Helper Help = new DataBase.Helper();
            DataBase.DataBase Database = new DataBase.DataBase();
            if(Database.ChangePassword(Request.Cookies["le"].Value.Substring(3), Help.MD5HASH(OldPassword.Text), Help.MD5HASH(NewPassword.Text)) == true)
            {
                password_output.InnerText = "Пароль успешно изменен.";
            }else
            {
                password_output.InnerText = "Введите верный пароль.";
            }
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