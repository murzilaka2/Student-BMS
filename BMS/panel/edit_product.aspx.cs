using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS.panel
{
    public partial class edit_product : System.Web.UI.Page
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
                    ShowProduct();
                }
            }
            else
            {
                Response.Redirect("products.aspx");
            }
        }
        private bool Checker()
        {
            int res;
            bool isInt = Int32.TryParse(Request.QueryString["edit_id"], out res);
            if (isInt == true)
            {
                res = Convert.ToInt32(Request.QueryString["edit_id"]);
                DataBase.DataBase Database = new DataBase.DataBase();
                if (Database.UserGroup(Request.Cookies["le"].Value.Substring(3)) == Database.GetProductGroup(res))
                {
                    return true;
                }else
                {
                    return false;
                }               
            }
            else
            {
                Response.Redirect("products.aspx");
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
        private void ShowProduct()
        {
            try
            {
                int ID = 0;
                DataBase.DataBase Database = new DataBase.DataBase();
                int res = Convert.ToInt32(Request.QueryString["edit_id"]);
                foreach (var item in Database.GetProductCategory(Request.Cookies["le"].Value.Substring(3)))
                {
                    Category.Items.Add(item.GetName());
                }
                foreach (var item in Database.ShowProduct(res))
                {
                    Name.Text = item.GetName();
                    Count.Text = item.GetCount().ToString();
                    PurchasePrice.Text = item.GetPurchasePrice().ToString();
                    CostOfSales.Text = item.GetCostOfSales().ToString();
                    Description.Text = item.GetDescription();
                    Image.Text = item.GetImage();
                    ID = item.GetCategoryID();                    
                }
                Category.Items.FindByValue(Database.GetProductCategoryName(ID)).Selected = true;
            }catch(Exception ex) { }
        }
        private bool SaveProduct()
        {
            int res = Convert.ToInt32(Request.QueryString["edit_id"]);
            DataBase.DataBase Database = new DataBase.DataBase();
            int CategoryID = Database.GetProductCategoryID(Category.SelectedValue);
            List<DataBase.ProductCategory.Product> Product = new List<DataBase.ProductCategory.Product>();
            Product.Add(new DataBase.ProductCategory.Product(0,0, CategoryID, Name.Text, Convert.ToInt32(Count.Text), Convert.ToInt32(PurchasePrice.Text), Convert.ToInt32(CostOfSales.Text),Description.Text,Image.Text));
            if (Database.EditProduct(Product,res))
            {
                return true;
            }
            else
            {
                return false;
            }           
        }
        protected void Edit_Product_Click(object sender, EventArgs e)
        {
            if (SaveProduct())
            {
                output.InnerText = "Продукт успешно изменен.";
            }
            else
            {
                output.InnerText = "Ошибка";
            }
        }
    }
}