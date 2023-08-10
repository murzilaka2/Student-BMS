using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS.panel
{
    public partial class objectives : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cookie_Check();
            SetImage();
            NewMessagesCount();
            LoadNewMessages();
            LoadObjectives();
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
        private void LoadObjectives()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            foreach (var item in Database.LoadObjectives(Request.Cookies["le"].Value.Substring(3)))
            {
                string Done = "";
                CheckBox checkbox = new CheckBox();
                checkbox.ID = item.GetID().ToString();
                checkbox.CheckedChanged += checkbox_CheckedChanged;
                checkbox.AutoPostBack = true;
                if (item.GetIsDone() == true)
                {
                    Done = "complete";
                    checkbox.Checked = true;
                }
                Todo.Controls.Add(new LiteralControl("<div class=\"todo-item " + Done + "\"><a href=\"delete_objective.aspx?objective_id=" + item.GetID() + "\" class=\"pull-right remove-todo-item\"><i class=\"fa fa-times\"></i></a>"));
                Todo.Controls.Add(checkbox);
                Todo.Controls.Add(new LiteralControl("<span>" + item.GetDate() + "</span><span style=\"float:right\">"+item.GetAuthor()+"</span>"));
                Todo.Controls.Add(new LiteralControl("<div class=\"todo-item\"><span>" + item.GetText() + "</span></div></div>"));
            }
        }
        private void checkbox_CheckedChanged(object sender, EventArgs e)
        {
            bool isDone = false;
            int ID = Convert.ToInt32((sender as CheckBox).ClientID);
            DataBase.DataBase Database = new DataBase.DataBase();         
            if ((sender as CheckBox).Checked == true)
            {
                isDone = true;
            }
            Database.ChangeObjectiveStatus(ID, isDone);
            Todo.Controls.Clear();
            LoadObjectives();
        }
        protected void GetValue_Click(object sender, EventArgs e)
        {
            if (NewObjective.Text.Length > 1)
            {
                AddObjectives(NewObjective.Text);
            }          
        }
        private void AddObjectives(string Text)
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            Database.AddObjectives(Request.Cookies["le"].Value.Substring(3), Text);
            Response.Redirect("objectives.aspx");
        }
    }
}