using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS.panel.services
{
    public partial class add_product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cookie_Check();
            SetImage();
            NewMessagesCount();
            GetCategory();
            LoadNewMessages();
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
        private void GetCategory()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            foreach (var item in Database.GetProductCategory(Request.Cookies["le"].Value.Substring(3)))
            {
                Category.Items.Add(item.GetName());
            }
        }
        private void AddProduct()
        {
            if (Category.SelectedValue == String.Empty)
            {
                output.InnerText = "Добавьте хотя бы одну категорию.";
            }
            else
            {
                try
                {
                    DataBase.DataBase Database = new DataBase.DataBase();
                    int Group = Database.UserGroup(Request.Cookies["le"].Value.Substring(3));
                    int ProductCategory = Database.GetProductCategoryID(Category.SelectedValue);
                    List<DataBase.ProductCategory.Product> Product = new List<DataBase.ProductCategory.Product>();
                    Product.Add(new DataBase.ProductCategory.Product(0, Group, ProductCategory, Name.Text, Convert.ToInt32(Count.Text), Convert.ToInt32(PurchasePrice.Text), Convert.ToInt32(CostOfSales.Text), Description.Text,Image.Text));
                    if (Database.AddProduct(Product))
                    {
                        output.InnerText = "Продукт успешно добавлен.";
                    }
                    else
                    {
                        output.InnerText = "Ошибка при добавлении.";
                    }
                }
                catch (Exception ex)
                {
                    output.InnerText = "Ошибка при добавлении.";
                }
            }
            }
        protected void Add_Product_Click(object sender, EventArgs e)
        {
            if (Name.Text != String.Empty && Count.Text != String.Empty && PurchasePrice.Text != String.Empty && CostOfSales.Text != String.Empty)
            {
                AddProduct();
            }else
            {
                output.InnerText = "Заполните все поля.";
            }
        }
    }
}