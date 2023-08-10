using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS.panel
{
    public partial class orders : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            Cookie_Check();
            SetImage();
            NewMessagesCount();
            LoadNewMessages();
            LoadProduct();
            ShowSelectedProducts();
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
        private void LoadProduct()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            foreach (var item in Database.LoadProduct(Request.Cookies["le"].Value.Substring(3)))
            {
                ImageButton imageButton = new ImageButton();
                imageButton.Width = 80;
                imageButton.Height = 80;
                imageButton.ID = item.GetID().ToString();
                imageButton.Click += new ImageClickEventHandler(AddItem_Click);

                imageButton.ImageUrl = item.GetImage();
                ProductsPlace.Controls.Add(new LiteralControl("<div class=\"col-md-2\"><div class=\"panel panel-white-products panel-products\"><div class=\"panel-heading\"><h3 class=\"panel-title-product\">" + item.GetName() + "</h3></div><div class=\"panel-body\"><div>"));
                ProductsPlace.Controls.Add(imageButton);
                ProductsPlace.Controls.Add(new LiteralControl("</div>" + item.GetCostOfSales() + "₴</div></div></div>"));
            }
        }
        private void AddItem_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton item_button = sender as ImageButton;
            Issue.Visible = true;
            if (item_button.ID != null)
            {
                SaveSelectedProduct(Convert.ToInt32(item_button.ID));
            }
            ShowSelectedProducts();
        }
        private List<DataBase.ProductCategory.Product> GetProduct(int ID)
        {
            List<DataBase.ProductCategory.Product> Product = new List<DataBase.ProductCategory.Product>();
            DataBase.DataBase Database = new DataBase.DataBase();
            Product = Database.ShowProduct(ID);
            return Product;
        }       
        private void SaveSelectedProduct(int ID)
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            Database.AddSelectedProduct(Request.Cookies["le"].Value.Substring(3),ID);

        }
        private void ShowSelectedProducts()
        {
            ProductsPlaceholder.Controls.Clear();
            Total.InnerText = "";
            int Count = 0;
            DataBase.DataBase Database = new DataBase.DataBase();
            int Group = Database.UserGroup(Request.Cookies["le"].Value.Substring(3));
            foreach (var item in Database.GetSelectedProducts(Request.Cookies["le"].Value.Substring(3), Group))
            {
                ProductsPlaceholder.Controls.Add(new LiteralControl("<tr><td>"+item.GetName()+"</td><td>1</td><td>"+item.GetCostOfSales()+ " ₴</td></tr>"));
                Count += item.GetCostOfSales();
            }
            Total.InnerText = ""+Count+ " ₴";
        }
        protected void Issue_Click(object sender, EventArgs e)
        {
            if (DoIssue())
            {
                CleanOrder();
                ProductsPlaceholder.Controls.Clear();
                ProductsPlaceholder.Controls.Add(new LiteralControl("<p style=\"text-align:-webkit-center; font-size:16px;\">Заказ успешно обработан!</p>"));
            }
            else
            {
                ProductsPlaceholder.Controls.Add(new LiteralControl("<p style=\"text-align:-webkit-center; font-size:16px;\">Ошибка!</p>"));
            }
        }
        private bool DoIssue()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            int Group = Database.UserGroup(Request.Cookies["le"].Value.Substring(3));
            if (Database.AddProductReport(Request.Cookies["le"].Value.Substring(3), Group))
            {
                return true;
            }
            else { return false; }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            CleanOrder();
        }
        private void CleanOrder()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            Database.CancelOrder(Request.Cookies["le"].Value.Substring(3));
            ProductsPlaceholder.Controls.Clear();
            ShowSelectedProducts();
        } 
    }
}