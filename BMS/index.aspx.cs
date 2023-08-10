using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BMS
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void GetButton_Click(object sender, EventArgs e)
        {
            string Email = EmailText.Text;
            try
            {
                MailAddress from = new MailAddress(Email, "Гость BMS");
                MailAddress to = new MailAddress("enykoruna1@gmail.com");
                MailMessage m = new MailMessage(from, to);
                m.Subject = "BMS - Запрос доступа.";
                m.Body = "Запрос доступа к системе BMS.<br />Email - "+ Email;
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("enykoruna1@gmail.com", "jobs192837leny1996");
                smtp.EnableSsl = true;
                smtp.Send(m);
                Response.Write("<script>alert('Запрос доступа успешно отправлен.')</script>");
            }
            catch (Exception ex) { Response.Write("<script>alert('Ошибка. Попробуйте еще раз.')</script>"); }
        }
    }
}