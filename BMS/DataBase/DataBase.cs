using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BMS.DataBase
{
    public class DataBase
    {
        //"Data Source = LEO-PC\\SQLEXPRESS; Initial Catalog = BMS; Integrated Security = true;"
        //"Data Source = localhost; Initial Catalog = u0371616_bms; User id = u0371616_bms; Password = Ky6r3~t8"
        private string Connection = "Data Source = LEO-PC\\SQLEXPRESS; Initial Catalog = BMS; Integrated Security = true;";

        public bool GeneratePassword(string Email)
        {
            Helper Help = new Helper();
            string Password = Help.GeneratePassword();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("Update [User] set [Password] = '" + Help.MD5HASH(Password) + "' WHERE [Email] = '" + Email + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                    if (SendEmailPassword(Email, Password) == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex) { return false; }
        }
        private bool SendEmailPassword(string Email, string Password)
        {
            try
            {
                MailAddress from = new MailAddress("enykoruna1@gmail.com", "Администрация");
                MailAddress to = new MailAddress(Email);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "BMS - Восстановление пароля.";
                m.Body = "Ваш новый пароль - <b>" + Password + "</b><br />Не забудьте изменить полученный пароль на свой.";
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("enykoruna1@gmail.com", "jobs192837leny1996");
                smtp.EnableSsl = true;
                smtp.Send(m);
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public bool Authorization(string Login, string Password)
        {
            Helper Help = new Helper();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [User] Where [Login] = '" + Login + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        return (ReaderUrl.GetValue(2).ToString() == Login && ReaderUrl.GetValue(3).ToString() == Help.MD5HASH(Password)) ? true : false;
                    }
                    ReaderUrl.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int UserGroup(string Login)
        {
            try
            {
                int GroupNumber = 0;
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [User] WHERE [Login] = '" + Login + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        GroupNumber = Convert.ToInt32(ReaderUrl.GetValue(10));
                    }
                    ReaderUrl.Close();
                }
                return GroupNumber;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public List<User> ShowUsers(string Login)
        {
            List<User> User = new List<User>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [User] WHERE [Group] = '" + UserGroup(Login) + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        User.Add(new User(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToBoolean(ReaderUrl.GetValue(1)), ReaderUrl.GetValue(2).ToString(), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString(), ReaderUrl.GetValue(6).ToString(), ReaderUrl.GetValue(7).ToString(), Convert.ToInt32(ReaderUrl.GetValue(8)), ReaderUrl.GetValue(9).ToString(), ReaderUrl.GetValue(11).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return User;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private bool CheckUser(List<User> User)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    foreach (var item in User)
                    {
                        SqlCommand Url = new SqlCommand("select * from [User] Where [Email] = '" + item.GetEmail() + "'", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        while (ReaderUrl.Read())
                        {
                            if (ReaderUrl.GetValue(5) != null) { ReaderUrl.Close(); con.Close();  return false; }
                        }
                        ReaderUrl.Close();
                    }
                    foreach (var item in User)
                    {
                        SqlCommand Url = new SqlCommand("select * from [User] Where [Login] = '" + item.GetLogin() + "'", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        while (ReaderUrl.Read())
                        {
                            if (ReaderUrl.GetValue(2) != null) { ReaderUrl.Close(); con.Close();  return false; }
                        }
                        ReaderUrl.Close();
                    }
                }
            }
            catch (Exception ex) { }
            return true;
        }
        public bool AddUser(List<User> User, string Login)
        {
            Helper Help = new Helper();
            if (CheckUser(User) == false) { return false; }
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    foreach (var item in User)
                    {
                        SqlCommand Url = new SqlCommand("insert into [User] values ('" + item.GetIsAdmin() + "','" + item.GetLogin() + "','" + Help.MD5HASH(item.GetPassword()) + "','" + item.GetFio() + "','" + item.GetEmail() + "','" + item.GetPhone() + "','" + item.GetStartDate() + "','" + item.GetSalary() + "','" + item.GetPosition() + "', '" + UserGroup(Login) + "', '" + item.GetImageUrl() + "' )", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                    }
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public void DeleteUser(string Login, string DeleteLogin)
        {
            bool IsAdmin = false;
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [User] WHERE [Login] = '" + Login + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        IsAdmin = Convert.ToBoolean(ReaderUrl.GetValue(1));
                    }
                    ReaderUrl.Close();
                }
                if (IsAdmin == true)
                {
                    using (SqlConnection con = new SqlConnection(Connection))
                    {
                        con.Open();
                        SqlCommand Url = new SqlCommand("delete from [User] WHERE [Login] = '" + DeleteLogin + "'", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                    }
                }
                else if (Login == DeleteLogin)
                {
                    using (SqlConnection con = new SqlConnection(Connection))
                    {
                        con.Open();
                        SqlCommand Url = new SqlCommand("delete from [User] WHERE [Login] = '" + DeleteLogin + "'", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                    }
                }
            }
            catch (Exception ex) { }
        }
        public List<User> ShowSelectedUser(string Login)
        {
            List<User> User = new List<User>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [User] WHERE [Login] = '" + Login + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        User.Add(new User(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToBoolean(ReaderUrl.GetValue(1)), ReaderUrl.GetValue(2).ToString(), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString(), ReaderUrl.GetValue(6).ToString(), ReaderUrl.GetValue(7).ToString(), Convert.ToInt32(ReaderUrl.GetValue(8)), ReaderUrl.GetValue(9).ToString(), ReaderUrl.GetValue(11).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return User;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool EditUser(List<User> User)
        {
            int IsAdmin = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    foreach (var item in User)
                    {
                        if (item.GetIsAdmin() == true)
                        {
                            IsAdmin = 1;
                        }
                        else
                        {
                            IsAdmin = 0;
                        }
                        SqlCommand Url = new SqlCommand("Update [User] set [IsAdmin] = " + IsAdmin + ", [Login] = '" + item.GetLogin() + "', [Fio] = '" + item.GetFio() + "', [Email] = '" + item.GetEmail() + "', [Phone] = '" + item.GetPhone() + "', [StartDate] = '" + item.GetStartDate() + "', [Salary] = '" + item.GetSalary() + "', [Postion] = '" + item.GetPosition() + "', [Image] = '" + item.GetImageUrl() + "' WHERE [Login] = '" + item.GetLogin() + "'", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                    }
                    return true;
                }
            }
            catch (Exception ex) { return false; }
        }
        public string UserImage(string Login)
        {
            string ImageUrl = "";
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [User] WHERE [Login] = '" + Login + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        ImageUrl = ReaderUrl.GetValue(11).ToString();
                    }
                    ReaderUrl.Close();
                }
                return ImageUrl;
            }
            catch (Exception ex)
            {
                return "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSMMqFV3bcUVCG4oVB1fPu7EkLnPOAu2R33F4kHBa9QVkh5mrNc";
            }

        }
        public bool CheckIsAdmin(string Login)
        {
            bool IsAdmin = false;
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [User] Where [Login] = '" + Login + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        IsAdmin = Convert.ToBoolean(ReaderUrl.GetValue(1));
                    }
                    ReaderUrl.Close();
                }
                return IsAdmin;
            }
            catch (Exception ex)
            {
                return IsAdmin;
            }
        }
        public bool ChangePassword(string Login, string OldPassword, string NewPassword)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("Select * from [User] WHERE [Login] = '" + Login + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        if (OldPassword != ReaderUrl.GetValue(3).ToString())
                        {
                            ReaderUrl.Close();
                            con.Close();
                            return false;
                        }
                    }
                    ReaderUrl.Close();
                }

                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("Update [User] set [Password] = '" + NewPassword + "' WHERE [Login] = '" + Login + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public bool CheckEmail(string Email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [User] Where [Email] = '" + Email + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        if (ReaderUrl.GetValue(5).ToString() == Email)
                        {
                            ReaderUrl.Close();
                            con.Close();
                            return true;
                        }
                        else
                        {
                            ReaderUrl.Close();
                            con.Close();
                            return false;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string GetEmail(string Login)
        {
            string Email = "";
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [User] Where [Login] = '" + Login + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Email = ReaderUrl.GetValue(5).ToString();
                    }
                    ReaderUrl.Close();
                }
                return Email;
            }
            catch (Exception ex)
            {
                return Email;
            }
        }
        public bool SendMessage(string Login, string Email, string Subject, string Message)
        {
            string Date = DateTime.Today.ToShortDateString();
            string Time = DateTime.Now.ToShortTimeString();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("insert into [Message] values(0,'" + GetEmail(Login) + "','" + Email + "','" + Date + "','" + Time + "','" + Subject + "','" + Message + "')", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public List<Message> LoadMessages(string Login)
        {
            List<Message> Messages = new List<Message>();
            string Email = GetEmail(Login);
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Message] Where [To] = '" + Email + "' ORDER BY [ID] DESC", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Messages.Add(new Message(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToBoolean(ReaderUrl.GetValue(1)), ReaderUrl.GetValue(2).ToString(), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString(), ReaderUrl.GetValue(6).ToString(), ReaderUrl.GetValue(7).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Messages;
            }
            catch (Exception ex)
            {
                return Messages;
            }
        }
        public List<Message> LoadNewMessages(string Login)
        {
            List<Message> Messages = new List<Message>();
            string Email = GetEmail(Login);
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Message] Where [To] = '"+Email+"' AND [IsRead] = 0 ORDER BY [ID] DESC", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Messages.Add(new Message(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToBoolean(ReaderUrl.GetValue(1)), ReaderUrl.GetValue(2).ToString(), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString(), ReaderUrl.GetValue(6).ToString(), ReaderUrl.GetValue(7).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Messages;
            }
            catch (Exception ex)
            {
                return Messages;
            }
        }
        public string GetNewMessageImage(string Email)
        {
            string Image = "";
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select [Image] from [User] Where [Email] = '" + Email + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Image = ReaderUrl.GetValue(0).ToString();
                    }
                    ReaderUrl.Close();
                }
                return Image;
            }
            catch (Exception ex)
            {
                return Image;
            }
        }
        public List<Message> LoadSendMessages(string Login)
        {
            List<Message> Messages = new List<Message>();
            string Email = GetEmail(Login);
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Message] Where [From] = '" + Email + "' ORDER BY [ID] DESC", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Messages.Add(new Message(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToBoolean(ReaderUrl.GetValue(1)), ReaderUrl.GetValue(2).ToString(), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString(), ReaderUrl.GetValue(6).ToString(), ReaderUrl.GetValue(7).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Messages;
            }
            catch (Exception ex)
            {
                return Messages;
            }
        }
        public int NewMessagesCount(string Login)
        {
            string Email = GetEmail(Login);
            int count = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Message] Where [To] = '" + Email + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        if (Convert.ToBoolean(ReaderUrl.GetValue(1)) == false)
                        {
                            count++;
                        }
                    }
                    ReaderUrl.Close();
                }
                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }
        public List<Message> ShowMessage(int ID)
        {
            List<Message> Messages = new List<Message>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Message] Where [ID] = '" + ID + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Messages.Add(new Message(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToBoolean(ReaderUrl.GetValue(1)), ReaderUrl.GetValue(2).ToString(), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString(), ReaderUrl.GetValue(6).ToString(), ReaderUrl.GetValue(7).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Messages;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<User> GetUser(string Email)
        {
            List<User> User = new List<User>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [User] WHERE [Email] = '" + Email + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        User.Add(new User(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToBoolean(ReaderUrl.GetValue(1)), ReaderUrl.GetValue(2).ToString(), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString(), ReaderUrl.GetValue(6).ToString(), ReaderUrl.GetValue(7).ToString(), Convert.ToInt32(ReaderUrl.GetValue(8)), ReaderUrl.GetValue(9).ToString(), ReaderUrl.GetValue(11).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return User;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void ReadMessage(int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("Update [Message] Set [IsRead] = 1 WHERE [ID] = " + ID + "", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) { }
        }
        public void DeleteMessage(int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("DELETE [Message] WHERE [ID] = " + ID + "", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) { }
        }
        public string CheckToDeleteMessage(int ID)
        {
            string Email = "";
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select [To] from [Message] WHERE [ID] = '" + ID + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Email = ReaderUrl.GetValue(0).ToString();
                    }
                    ReaderUrl.Close();
                }
                return Email;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string CheckFromDeleteMessage(int ID)
        {
            string Email = "";
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select [From] from [Message] WHERE [ID] = '" + ID + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Email = ReaderUrl.GetValue(0).ToString();
                    }
                    ReaderUrl.Close();
                }
                return Email;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Message> LoadDraftMessages(string Login)
        {
            List<Message> Messages = new List<Message>();
            string Email = GetEmail(Login);
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [DraftMessage] Where [From] = '" + Email + "' ORDER BY [ID] DESC", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Messages.Add(new Message(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToBoolean(ReaderUrl.GetValue(1)), ReaderUrl.GetValue(2).ToString(), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString(), ReaderUrl.GetValue(6).ToString(), ReaderUrl.GetValue(7).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Messages;
            }
            catch (Exception ex)
            {
                return Messages;
            }
        }
        public bool SaveDraftMessage(string Login, string Email, string Subject, string Message)
        {
            string Date = DateTime.Today.ToShortDateString();
            string Time = DateTime.Now.ToShortTimeString();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("insert into [DraftMessage] values(0,'" + GetEmail(Login) + "','" + Email + "','" + Date + "','" + Time + "','" + Subject + "','" + Message + "')", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public List<Objectives> LoadObjectives(string Login)
        {
            int Group = UserGroup(Login);
            List<Objectives> Objectives = new List<Objectives>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Objectives] Where [Group] = '" + Group + "' ORDER BY [ID] DESC", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Objectives.Add(new Objectives(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToBoolean(ReaderUrl.GetValue(1)), Convert.ToInt32(ReaderUrl.GetValue(2)), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Objectives;
            }
            catch (Exception ex)
            {
                return Objectives;
            }
        }
        public void AddObjectives(string Login, string Text)
        {
            int Group = UserGroup(Login);
            string Date = DateTime.Today.ToShortDateString();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("insert into [Objectives] values(0," + Group + ",'" + Login + "','" + Text + "','" + Date + "')", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) { }
        }
        public int GetObjectivesGroup(int ID)
        {
            int Group = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Objectives] Where [ID] = '" + ID + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Group = Convert.ToInt32(ReaderUrl.GetValue(2));
                    }
                    ReaderUrl.Close();
                }
                return Group;
            }
            catch (Exception ex)
            {
                return Group;
            }
        }
        public void DeleteObjectives(string Login, int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("DELETE [Objectives] WHERE [ID] = " + ID + "", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) { }
        }
        public void ChangeObjectiveStatus(int ID, bool isDone)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("Update [Objectives] set [IsDone] = '" + isDone + "' WHERE [ID] = " + ID + "", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) { }
        }
        public List<ProductCategory> GetProductCategory(string Login)
        {
            List<ProductCategory> Productcategory = new List<ProductCategory>();
            int Group = UserGroup(Login);
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [ProductCategory] Where [Group] = '" + Group + "' ORDER BY [ID] DESC", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Productcategory.Add(new ProductCategory(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToInt32(ReaderUrl.GetValue(1)), ReaderUrl.GetValue(2).ToString(), ReaderUrl.GetValue(3).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Productcategory;
            }
            catch (Exception ex)
            {
                return Productcategory;
            }
        }
        public bool CheckProductCategory(string Name)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select [Name] from [ProductCategory] WHERE [Name] = '" + Name + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        if (ReaderUrl.GetValue(0).ToString() == Name)
                        {
                            ReaderUrl.Close();
                            con.Close();
                            return false;
                        }
                    }
                    ReaderUrl.Close();
                    return true;
                }
            }
            catch (Exception ex) { return false; }
        }
        public int GetProductCategoryID(string Name)
        {
            int ID = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select [ID] from [ProductCategory] WHERE [Name] = '" + Name + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        ID = Convert.ToInt32(ReaderUrl.GetValue(0));
                        ReaderUrl.Close();
                        con.Close();
                        return ID;
                    }
                    ReaderUrl.Close();
                }
                return ID;
            }
            catch (Exception ex) { return 0; }
        }
        public void AddProductCategory(string Login, string Name, string Description)
        {
            int Group = UserGroup(Login);
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("insert into [ProductCategory] values(" + Group + ",'" + Name + "','" + Description + "')", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) { }
        }
        public bool AddProduct(List<ProductCategory.Product> Product)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    foreach (var item in Product)
                    {
                        SqlCommand Url = new SqlCommand("insert into [Product] values (" + item.GetGroup() + "," + item.GetCategoryID() + ",'" + item.GetName() + "'," + item.GetCount() + "," + item.GetPurchasePrice() + "," + item.GetCostOfSales() + ",'" + item.GetDescription() + "','"+item.GetImage()+"')", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                    }
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public List<ProductCategory.Product> ShowProducts(string Login)
        {
            List<ProductCategory.Product> Products = new List<ProductCategory.Product>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Product] WHERE [Group] = '" + UserGroup(Login) + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Products.Add(new ProductCategory.Product(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToInt32(ReaderUrl.GetValue(1)), Convert.ToInt32(ReaderUrl.GetValue(2)), ReaderUrl.GetValue(3).ToString(), Convert.ToInt32(ReaderUrl.GetValue(4)), Convert.ToInt32(ReaderUrl.GetValue(5)), Convert.ToInt32(ReaderUrl.GetValue(6)), ReaderUrl.GetValue(7).ToString(), ReaderUrl.GetValue(8).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Products;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int GetProductGroup(int ID)
        {
            int Group = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Product] Where [ID] = '" + ID + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Group = Convert.ToInt32(ReaderUrl.GetValue(1));
                    }
                    ReaderUrl.Close();
                }
                return Group;
            }
            catch (Exception ex)
            {
                return Group;
            }
        }
        public void DeleteProducts(int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("DELETE [Product] WHERE [ID] = " + ID + "", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) { }
        }
        public string GetProductCategoryName(int ID)
        {
            string Name = "";
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select [Name] from [ProductCategory] WHERE [ID] = '" + ID + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Name = ReaderUrl.GetValue(0).ToString();
                        ReaderUrl.Close();
                        return Name;
                    }
                    ReaderUrl.Close();
                }
                return Name;
            }
            catch (Exception ex) { return null; }
        }
        public List<ProductCategory.Product> ShowProduct(int ID)
        {
            List<ProductCategory.Product> Products = new List<ProductCategory.Product>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Product] WHERE [ID] = '" + ID + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Products.Add(new ProductCategory.Product(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToInt32(ReaderUrl.GetValue(1)), Convert.ToInt32(ReaderUrl.GetValue(2)), ReaderUrl.GetValue(3).ToString(), Convert.ToInt32(ReaderUrl.GetValue(4)), Convert.ToInt32(ReaderUrl.GetValue(5)), Convert.ToInt32(ReaderUrl.GetValue(6)), ReaderUrl.GetValue(7).ToString(), ReaderUrl.GetValue(8).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Products;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool EditProduct(List<ProductCategory.Product> Products, int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    foreach (var item in Products)
                    {
                     SqlCommand Url = new SqlCommand("Update [Product] set [CategoryID] = "+item.GetCategoryID()+", [Name] = '" + item.GetName() + "', [Count] = " + item.GetCount() + ", [PurchasePrice] = " + item.GetPurchasePrice() + ", [CostOfSales] = " + item.GetCostOfSales() + ", [Description] = '" + item.GetDescription() + "', [ImageUrl] = '"+item.GetImage()+"'  WHERE [ID] = " + ID + "", con);
                     SqlDataReader ReaderUrl = Url.ExecuteReader();
                     ReaderUrl.Close();
                    }
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public bool CheckServiceCategory(string Name)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select [Name] from [ServicesCategory] WHERE [Name] = '" + Name + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        if (ReaderUrl.GetValue(0).ToString() == Name)
                        {
                            ReaderUrl.Close();
                            con.Close();
                            return false;
                        }
                    }
                    ReaderUrl.Close();
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public List<ServiceCategory> GetServiceCategory(string Login)
        {
            List<ServiceCategory> Servicecategory = new List<ServiceCategory>();
            int Group = UserGroup(Login);
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [ServicesCategory] Where [Group] = '" + Group + "' ORDER BY [ID] DESC", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Servicecategory.Add(new ServiceCategory(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToInt32(ReaderUrl.GetValue(1)), ReaderUrl.GetValue(2).ToString(), ReaderUrl.GetValue(3).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Servicecategory;
            }
            catch (Exception ex)
            {
                return Servicecategory;
            }
        }
        public string GetServiceCategoryName(int ID)
        {
            string Name = "";
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select [Name] from [ServicesCategory] WHERE [ID] = '" + ID + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Name = ReaderUrl.GetValue(0).ToString();
                    }
                    ReaderUrl.Close();
                }
                return Name;
            }
            catch (Exception ex) { return null; }
        }
        public int GetServiceID(string Name)
        {
            int ID = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select [ID] from [ServicesCategory] WHERE [Name] = '" + Name + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        ID = Convert.ToInt32(ReaderUrl.GetValue(0));
                        return ID;
                    }
                    ReaderUrl.Close();
                }
                return ID;
            }
            catch (Exception ex) { return 0; }
        }
        public void AddServiceCategory(string Login, string Name, string Description)
        {
            int Group = UserGroup(Login);
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("insert into [ServicesCategory] values(" + Group + ",'" + Name + "','" + Description + "')", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) { }
        }
        public bool AddService(List<ServiceCategory.Service> Service)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    foreach (var item in Service)
                    {
                        SqlCommand Url = new SqlCommand("insert into [Service] values ("+item.GetGroup()+","+item.GetCategoryID()+",'"+item.GetName()+"', "+item.GetCostOfSales()+", '"+item.GetDescription()+"', '"+item.GetImage()+"')", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                    }
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public List<ServiceCategory.Service> ShowServices(string Login)
        {
            List<ServiceCategory.Service> Services = new List<ServiceCategory.Service>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Service] WHERE [Group] = '" + UserGroup(Login) + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Services.Add(new ServiceCategory.Service(Convert.ToInt32(ReaderUrl.GetValue(0)),Convert.ToInt32(ReaderUrl.GetValue(1)),Convert.ToInt32(ReaderUrl.GetValue(2)), ReaderUrl.GetValue(3).ToString(),Convert.ToInt32(ReaderUrl.GetValue(4)), ReaderUrl.GetValue(5).ToString(), ReaderUrl.GetValue(6).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Services;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ServiceCategory.Service> ShowService(int ID)
        {
            List<ServiceCategory.Service> Services = new List<ServiceCategory.Service>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Service] WHERE [ID] = '" + ID + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Services.Add(new ServiceCategory.Service(Convert.ToInt32(ReaderUrl.GetValue(0)),Convert.ToInt32(ReaderUrl.GetValue(1)),Convert.ToInt32(ReaderUrl.GetValue(2)), ReaderUrl.GetValue(3).ToString(),Convert.ToInt32(ReaderUrl.GetValue(4)), ReaderUrl.GetValue(5).ToString(), ReaderUrl.GetValue(6).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Services;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int GetServiceGroup(int ID)
        {
            int Group = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Service] Where [ID] = '" + ID + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Group = Convert.ToInt32(ReaderUrl.GetValue(1));
                    }
                    ReaderUrl.Close();
                }
                return Group;
            }
            catch (Exception ex)
            {
                return Group;
            }
        }
        public void DeleteServices(int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("DELETE [Service] WHERE [ID] = " + ID + "", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) { }
        }
        public bool EditService(List<ServiceCategory.Service> Services, int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    foreach (var item in Services)
                    {
                        SqlCommand Url = new SqlCommand("Update [Service] set [CategoryID] = " + item.GetCategoryID() + ", [Name] = '" + item.GetName() + "', [CostOfSales] = " + item.GetCostOfSales() + ", [Description] = '" + item.GetDescription() + "', [ImageUrl] = '"+item.GetImage()+"' WHERE [ID] = " + ID + "", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                    }
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public void DeleteProductsCategory(int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("DELETE [ProductCategory] WHERE [ID] = " + ID + "", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) { }
        }
        public int GetProductCategoryGroup(int ID)
        {
            int Group = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [ProductCategory] Where [ID] = '" + ID + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Group = Convert.ToInt32(ReaderUrl.GetValue(1));
                    }
                    ReaderUrl.Close();
                }
                return Group;
            }
            catch (Exception ex)
            {
                return Group;
            }
        }
        public bool DeleteProductsFromCategory(int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("DELETE [Product] WHERE [CategoryID] = " + ID + "", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public List<ProductCategory> GetProductCategory(int ID)
        {
            List<ProductCategory> ProductCategory = new List<ProductCategory>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [ProductCategory] WHERE [ID] = '" + ID + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        ProductCategory.Add(new ProductCategory(Convert.ToInt32(ReaderUrl.GetValue(0)),Convert.ToInt32(ReaderUrl.GetValue(1)), ReaderUrl.GetValue(2).ToString(), ReaderUrl.GetValue(3).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return ProductCategory;
            }
            catch (Exception ex) { return null; }

        }
        public bool EditProductCategory(List<ProductCategory> ProductCategory, int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    foreach (var item in ProductCategory)
                    {
                        SqlCommand Url = new SqlCommand("Update [ProductCategory] set [Name] = '" + item.GetName() + "', [Description] = '" + item.GetDescription() + "'  WHERE [ID] = " + ID + "", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                    }
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public int GetServiceCategoryGroup(int ID)
        {
            int Group = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [ServicesCategory] Where [ID] = '" + ID + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Group = Convert.ToInt32(ReaderUrl.GetValue(1));
                    }
                    ReaderUrl.Close();
                }
                return Group;
            }
            catch (Exception ex)
            {
                return Group;
            }
        }
        public bool DeleteServiceFromCategory(int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("DELETE [Service] WHERE [CategoryID] = " + ID + "", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public void DeleteServiceCategory(int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("DELETE [ServicesCategory] WHERE [ID] = " + ID + "", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) { }
        }
        public List<ServiceCategory> GetServiceCategory(int ID)
        {
            List<ServiceCategory> ServiceCategory = new List<ServiceCategory>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [ServicesCategory] WHERE [ID] = '" + ID + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        ServiceCategory.Add(new ServiceCategory(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToInt32(ReaderUrl.GetValue(1)), ReaderUrl.GetValue(2).ToString(), ReaderUrl.GetValue(3).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return ServiceCategory;
            }
            catch (Exception ex) { return null; }

        }
        public bool EditServiceCategory(List<ServiceCategory> ServiceCategory, int ID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    foreach (var item in ServiceCategory)
                    {
                        SqlCommand Url = new SqlCommand("Update [ServicesCategory] set [Name] = '" + item.GetName() + "', [Description] = '" + item.GetDescription() + "'  WHERE [ID] = " + ID + "", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                    }
                    return true;
                }
            }
            catch (Exception ex) { return false; }
        }
        public List<ProductCategory.Product> LoadProduct(string Login)
        {
            int Group = UserGroup(Login);
            List<ProductCategory.Product> Product = new List<ProductCategory.Product>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Product] Where [Group] = '" + Group + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Product.Add(new ProductCategory.Product(Convert.ToInt32(ReaderUrl.GetValue(0)),Convert.ToInt32(ReaderUrl.GetValue(1)),Convert.ToInt32(ReaderUrl.GetValue(2)), ReaderUrl.GetValue(3).ToString(),Convert.ToInt32(ReaderUrl.GetValue(4)),Convert.ToInt32(ReaderUrl.GetValue(5)),Convert.ToInt32(ReaderUrl.GetValue(6)), ReaderUrl.GetValue(7).ToString(), ReaderUrl.GetValue(8).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Product;
            }
            catch (Exception ex)
            {
                return Product;
            }
        }
        public List<ServiceCategory.Service> LoadService(string Login)
        {
            int Group = UserGroup(Login);
            List<ServiceCategory.Service> Service = new List<ServiceCategory.Service>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Service] Where [Group] = '" + Group + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Service.Add(new ServiceCategory.Service(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToInt32(ReaderUrl.GetValue(1)),Convert.ToInt32(ReaderUrl.GetValue(2)), ReaderUrl.GetValue(3).ToString(),Convert.ToInt32(ReaderUrl.GetValue(4)), ReaderUrl.GetValue(5).ToString(), ReaderUrl.GetValue(6).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Service;
            }
            catch (Exception ex)
            {
                return Service;
            }
        }
        public List<ProductCategory.Product> GetProductByID(int ProductID)
        {
            List<ProductCategory.Product> Product = new List<ProductCategory.Product>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Product] WHERE ID = "+ ProductID + "", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Product.Add(new ProductCategory.Product(Convert.ToInt32(ReaderUrl.GetValue(0)),Convert.ToInt32(ReaderUrl.GetValue(1)),Convert.ToInt32(ReaderUrl.GetValue(2)), ReaderUrl.GetValue(3).ToString(),Convert.ToInt32(ReaderUrl.GetValue(4)),Convert.ToInt32(ReaderUrl.GetValue(5)),Convert.ToInt32(ReaderUrl.GetValue(6)), ReaderUrl.GetValue(7).ToString(), ReaderUrl.GetValue(8).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Product;
            }
            catch (Exception ex) { return null; }
        }
        public List<ServiceCategory.Service> GetServiceByID(int ServiceID)
        {
            List<ServiceCategory.Service> Service = new List<ServiceCategory.Service>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Service] WHERE ID = " + ServiceID + "", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Service.Add(new ServiceCategory.Service(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToInt32(ReaderUrl.GetValue(1)), Convert.ToInt32(ReaderUrl.GetValue(2)), ReaderUrl.GetValue(3).ToString(), Convert.ToInt32(ReaderUrl.GetValue(4)), ReaderUrl.GetValue(5).ToString(), ReaderUrl.GetValue(6).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Service;
            }
            catch (Exception ex) { return null; }
        }
        public void AddSelectedProduct(string Login, int ID)
        {
            int Group = UserGroup(Login);
            string Time = DateTime.Now.ToShortTimeString();
            string Date = DateTime.Today.ToShortDateString();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("insert into [SelectedProducts] values("+ Group + ","+ID+",'"+Login+"','"+Time+"','"+Date+"')", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) {  }
        }
        public List<ProductCategory.Product> GetSelectedProducts(string Login, int Group)
        {           
            List<ProductCategory.Product> Products = new List<ProductCategory.Product>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select [ProductID] from [SelectedProducts] WHERE [Group] = " + Group+ " AND [UserName] = '"+Login+"'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        foreach (var item in GetProductByID(Convert.ToInt32(ReaderUrl.GetValue(0))))
                        {
                            Products.Add(new ProductCategory.Product(item.GetID(),item.GetGroup(),item.GetCategoryID(),item.GetName(),item.GetCount(),item.GetPurchasePrice(),item.GetCostOfSales(),item.GetDescription(),item.GetImage()));
                        }                       
                    }
                    ReaderUrl.Close();
                }
                return Products;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void CancelOrder(string Login)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("delete from [SelectedProducts] WHERE [UserName] = '"+Login+"'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) {  }
        }
        public void CancelService(string Login)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("delete from [SelectedServices] WHERE [UserName] = '" + Login + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) { }
        }
        public bool AddProductReport(string Login, int Group)
        {
            string Time = DateTime.Now.ToShortTimeString();
            string Date = DateTime.Today.ToShortDateString();
            try
                {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    foreach (var item in GetSelectedProducts(Login, Group))
                    {
                        SqlCommand Url = new SqlCommand("insert into [ProductReports] values("+Group+","+item.GetID()+",'"+Login+"','"+Time+"','"+Date+"')", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                    }
                }
                    return true;
                }
                catch (Exception ex) { return false; }
            }
        public bool AddServiceReport(string Login, int Group)
        {
            string Time = DateTime.Now.ToShortTimeString();
            string Date = DateTime.Today.ToShortDateString();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    foreach (var item in GetSelectedServices(Login, Group))
                    {
                        SqlCommand Url = new SqlCommand("insert into [ServiceReports] values(" + Group + "," + item.GetID() + ",'" + Login + "','" + Time + "','" + Date + "')", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                    }
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public List<ProductCategory.ProductReports> GetProductsReport(string Login, int Group)
        {
            List<ProductCategory.ProductReports> ProductReports = new List<ProductCategory.ProductReports>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [ProductReports] WHERE [Group] = "+Group+ "", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        ProductReports.Add(new ProductCategory.ProductReports(Convert.ToInt32(ReaderUrl.GetValue(0)),Convert.ToInt32(ReaderUrl.GetValue(1)),Convert.ToInt32(ReaderUrl.GetValue(2)), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return ProductReports;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ServiceCategory.ServiceReports> GetServicesReport(string Login, int Group)
        {
            List<ServiceCategory.ServiceReports> ServiceReports = new List<ServiceCategory.ServiceReports>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [ServiceReports] WHERE [Group] = " + Group + "", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        ServiceReports.Add(new ServiceCategory.ServiceReports(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToInt32(ReaderUrl.GetValue(1)), Convert.ToInt32(ReaderUrl.GetValue(2)), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return ServiceReports;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void AddSelectedService(string Login, int ID)
        {
            int Group = UserGroup(Login);
            string Time = DateTime.Now.ToShortTimeString();
            string Date = DateTime.Today.ToShortDateString();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("insert into [SelectedServices] values(" + Group + "," + ID + ",'" + Login + "','" + Time + "','" + Date + "')", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    ReaderUrl.Close();
                }
            }
            catch (Exception ex) { }
        }
        public List<ServiceCategory.Service> GetSelectedServices(string Login, int Group)
        {
            List<ServiceCategory.Service> Services = new List<ServiceCategory.Service>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select [ServiceID] from [SelectedServices] WHERE [Group] = " + Group + " AND [UserName] = '" + Login + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        foreach (var item in GetServiceByID(Convert.ToInt32(ReaderUrl.GetValue(0))))
                        {
                            Services.Add(new ServiceCategory.Service(item.GetID(), item.GetGroup(), item.GetCategoryID(), item.GetName(), item.GetCostOfSales(), item.GetDescription(), item.GetImage()));
                        }
                    }
                    ReaderUrl.Close();
                }
                return Services;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ProductCategory.ProductReports> LoadOwnReport(string Login, int Group, string Date)
        {
            List<ProductCategory.ProductReports> ProductReports = new List<ProductCategory.ProductReports>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [ProductReports] WHERE [Group] = " + Group + " AND [Date] = '"+Date+"'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        ProductReports.Add(new ProductCategory.ProductReports(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToInt32(ReaderUrl.GetValue(1)), Convert.ToInt32(ReaderUrl.GetValue(2)), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return ProductReports;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ServiceCategory.ServiceReports> LoadOwnServiceReport(string Login, int Group, string Date)
        {
            List<ServiceCategory.ServiceReports> ServiceReports = new List<ServiceCategory.ServiceReports>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [ServiceReports] WHERE [Group] = " + Group + " AND [Date] = '" + Date + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        ServiceReports.Add(new ServiceCategory.ServiceReports(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToInt32(ReaderUrl.GetValue(1)), Convert.ToInt32(ReaderUrl.GetValue(2)), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return ServiceReports;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ProductCategory.ProductReports> LoadStatsReport(string Login, int Group)
        {
            List<ProductCategory.ProductReports> ProductReports = new List<ProductCategory.ProductReports>();
            string Date = DateTime.Today.ToShortDateString();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [ProductReports] WHERE [Group] = " + Group + " AND [Date] = '" + Date + "'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        ProductReports.Add(new ProductCategory.ProductReports(Convert.ToInt32(ReaderUrl.GetValue(0)), Convert.ToInt32(ReaderUrl.GetValue(1)), Convert.ToInt32(ReaderUrl.GetValue(2)), ReaderUrl.GetValue(3).ToString(), ReaderUrl.GetValue(4).ToString(), ReaderUrl.GetValue(5).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return ProductReports;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Friends> LoadFriends (string Login)
        {
            List<Friends> Friends = new List<Friends>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    SqlCommand Url = new SqlCommand("select * from [Friends] WHERE [Name] = '"+Login+"'", con);
                    SqlDataReader ReaderUrl = Url.ExecuteReader();
                    while (ReaderUrl.Read())
                    {
                        Friends.Add(new BMS.DataBase.Friends(Convert.ToInt32(ReaderUrl.GetValue(0)), ReaderUrl.GetValue(1).ToString(), ReaderUrl.GetValue(2).ToString()));
                    }
                    ReaderUrl.Close();
                }
                return Friends;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void AddToFriends(List<Friends> Friends)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    foreach (var item in Friends)
                    {
                        SqlCommand Url = new SqlCommand("insert into [Friends] values ('"+item.GetName()+"','"+item.GetFriendName()+"')", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                    }
                }
            }
            catch (Exception ex) { }
        }
        public void DeleteFromFriends(List<Friends> Friends)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    con.Open();
                    foreach (var item in Friends)
                    {
                        SqlCommand Url = new SqlCommand("delete from [Friends] WHERE [FriendName] = '"+item.GetFriendName()+"'", con);
                        SqlDataReader ReaderUrl = Url.ExecuteReader();
                        ReaderUrl.Close();
                    }
                }
            }
            catch (Exception ex) { }
        }
    }
}