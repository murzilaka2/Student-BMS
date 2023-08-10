using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS.panel
{
    public partial class reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cookie_Check();
            SetImage();
            NewMessagesCount();
            LoadNewMessages();
            LoadProductReports();
            LoadServiceReports();
            if (!IsPostBack)
            {
                LoadOwnProductReport();
                GetOwnServiceReport();
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
        private void LoadProductReports()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            int Group = Database.UserGroup(Request.Cookies["le"].Value.Substring(3));
            foreach (var item in Database.GetProductsReport(Request.Cookies["le"].Value.Substring(3),Group))
            {
                foreach (var item2 in Database.GetProductByID(item.GetProductID()))
                {
                    ReportsPlaceholder.Controls.Add(new LiteralControl("<tr><td>" + item.GetDate() + "</td><td>" + item2.GetName() + "</td><td>1</td><td>" + item2.GetCostOfSales() +"</td><td>" + item.GetUserName() + "</td><td>" + item.GetTime() + "</td></tr>"));
                }               
            }
        }
        private void LoadServiceReports()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            int Group = Database.UserGroup(Request.Cookies["le"].Value.Substring(3));
            foreach (var item in Database.GetServicesReport(Request.Cookies["le"].Value.Substring(3), Group))
            {
                foreach (var item2 in Database.GetServiceByID(item.GetServiceID()))
                {
                    ReportsPlaceholder.Controls.Add(new LiteralControl("<tr><td>" + item.GetDate() + "</td><td>" + item2.GetName() + "</td><td>1</td><td>" + item2.GetCostOfSales() + "</td><td>" + item.GetUserName() + "</td><td>" + item.GetTime() + "</td></tr>"));
                }
            }
        }
        private void LoadOwnProductReport()
        {
            Date.Text = DateTime.Now.ToShortDateString();
            DateService.Text = DateTime.Now.ToShortDateString();
        }
        protected void GetReport_Click(object sender, EventArgs e)
        {
            if (Date.Text.Length > 1)
            {
                GetOwnReport();
            }
        }
        private void GetOwnReport()
        {
            int Sum = 0;
            int Count = 0;
            DataBase.DataBase Database = new DataBase.DataBase();
            int Group = Database.UserGroup(Request.Cookies["le"].Value.Substring(3));
            foreach (var item in Database.LoadOwnReport(Request.Cookies["le"].Value.Substring(3), Group,Date.Text))
            {
                foreach (var item2 in Database.GetProductByID(item.GetProductID()))
                {
                    Count++;
                    Sum += item2.GetCostOfSales();
                    OwnReportsPlaceholder.Controls.Add(new LiteralControl("<tr><td>" + item.GetDate() + "</td><td>" + item2.GetName() + "</td><td>1</td><td>" + item2.GetCostOfSales() + "</td><td>" + item.GetUserName() + "</td><td>" + item.GetTime() + "</td></tr>"));
                }
            }
            if (Count >= 1)
            {
                OwnReportsPlaceholder.Controls.Add(new LiteralControl("<tr><td><b>Всего</b></td><td></td><td>" + Count + "</td><td>" + Sum + "</td><td></td><td></td></tr>"));
            }          
        }
        private void GetOwnServiceReport()
        {
            int Sum = 0;
            int Count = 0;
            DataBase.DataBase Database = new DataBase.DataBase();
            int Group = Database.UserGroup(Request.Cookies["le"].Value.Substring(3));
            foreach (var item in Database.LoadOwnServiceReport(Request.Cookies["le"].Value.Substring(3), Group, Date.Text))
            {
                foreach (var item2 in Database.GetServiceByID(item.GetServiceID()))
                {
                    OwnSericeReportsPlaceholder.Controls.Add(new LiteralControl("<tr><td>" + item.GetDate() + "</td><td>" + item2.GetName() + "</td><td>1</td><td>" + item2.GetCostOfSales() + "</td><td>" + item.GetUserName() + "</td><td>" + item.GetTime() + "</td></tr>"));
                }
            }
            if (Count >= 1)
            {
                OwnSericeReportsPlaceholder.Controls.Add(new LiteralControl("<tr><td><b>Всего</b></td><td></td><td>" + Count + "</td><td>" + Sum + "</td><td></td><td></td></tr>"));
            }
        }
        protected void GerReportService_Click(object sender, EventArgs e)
        {
            if (DateService.Text.Length > 1)
            {
                GetOwnServiceReport();
            }
        }
    }
}