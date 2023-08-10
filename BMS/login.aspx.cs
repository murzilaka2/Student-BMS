using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBase.Helper Help = new DataBase.Helper();
            if (Request.Cookies["le"] != null && Request.Cookies["lo"] != null)
            {
                if (Help.MD5HASH(Request.Cookies["le"].Value.Substring(3)) == Request.Cookies["lo"].Value.Substring(3)) { Response.Redirect("panel/index.aspx"); }
            }                  
        }
        protected void Authorization_Button_Click(object sender, EventArgs e)
        {
            Authorization();
        }
        private void Authorization()
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            if (Database.Authorization(Login.Value, Password.Value) == true)
            {
                Cookie(Login.Value);
                Response.Redirect("panel/index.aspx");
            }
            else
            {
                Error.Visible = true;
                Error.InnerText = "Логин или Пароль введен не верно.";
            }          
        }
        private void Cookie(string Login)
        {
            DataBase.Helper Help = new DataBase.Helper();

            HttpCookie Cookie = new HttpCookie("le");
            Cookie["le"] = Login;
            Cookie.Expires = DateTime.Now.AddMinutes(60);
            Response.Cookies.Add(Cookie);          

            HttpCookie BbadCcookie = new HttpCookie("lo");
            BbadCcookie["lo"] = Help.MD5HASH(Login);
            BbadCcookie.Expires = DateTime.Now.AddMinutes(60);
            Response.Cookies.Add(BbadCcookie);
        }
    }
}