using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS
{
    public partial class forgot_password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBase.Helper Help = new DataBase.Helper();
            if (Request.Cookies["le"] != null && Request.Cookies["lo"] != null)
            {
                if (Help.MD5HASH(Request.Cookies["le"].Value.Substring(3)) == Request.Cookies["lo"].Value.Substring(3)) { Response.Redirect("panel/index.aspx"); }
            }
        }
        protected void Send_Click(object sender, EventArgs e)
        {
            if (Email.Value != String.Empty)
            {
                if (CheckEmail(Email.Value) == true)
                {
                    NewPassword(Email.Value);                   
                }
                else
                {
                    Result.InnerText = "Email не найден";
                    Result.Visible = true;
                }               
            }            
        }
        private bool CheckEmail(string Email)
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            foreach (var item in Database.GetUser(Email))
            {
                if (item.GetEmail() == Email)
                {
                    return true;
                }
            }
            return false;
        }
        private void NewPassword(string Email)
        {
            DataBase.DataBase Database = new DataBase.DataBase();
            Result.Visible = true;
            if (Database.GeneratePassword(Email) == true)
            {
                Result.InnerText = "Новый пароль успешно отправлен на указанный Email.";
            }else
            {
                Result.InnerText = "Ошибка.";
            }
        } 
    }
}