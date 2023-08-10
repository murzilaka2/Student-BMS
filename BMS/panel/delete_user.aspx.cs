using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS.panel
{
    public partial class delete_user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Cookie_Check();
            string Login = Request.QueryString["delete_login"];
            DataBase.DataBase Database = new DataBase.DataBase();
            if (Login.Length != 0)
            {
                if (Database.UserGroup(Request.Cookies["le"].Value.Substring(3)) == Database.UserGroup(Login))
                {
                    Database.DeleteUser(Request.Cookies["le"].Value.Substring(3), Login);
                    if (Request.Cookies["le"].Value.Substring(3) == Login)
                    {
                        Exit();
                    }
                }
            }
            Response.Redirect("users.aspx");
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
        private void Exit()
        {
            HttpCookie Cookie = new HttpCookie("le");
            Cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(Cookie);

            HttpCookie BbadCcookie = new HttpCookie("lo");
            BbadCcookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(BbadCcookie);

            Response.Redirect("../login.aspx");
        }
    }
}