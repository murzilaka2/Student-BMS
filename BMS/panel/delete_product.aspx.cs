using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS.panel
{
    public partial class delete_product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cookie_Check();
            Checker();
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
        private void Checker()
        {
            int res;
            bool isInt = Int32.TryParse(Request.QueryString["delete_id"], out res);
            if (isInt == true)
            {
                DeleteProduct(Convert.ToInt32(Request.QueryString["delete_id"]));
            }
            else
            {
                Response.Redirect("products.aspx");
            }
        }    
        private void DeleteProduct(int ID)
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            if (Database.GetProductGroup(ID) == Database.UserGroup(Request.Cookies["le"].Value.Substring(3)))
            {
                Database.DeleteProducts(ID);
            }
            Response.Redirect("products.aspx");
        }
    }
}