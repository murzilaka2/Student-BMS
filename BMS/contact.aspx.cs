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
    public partial class contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void Contact()
        {
            try
            {
                MailAddress from = new MailAddress("" + Email.Value + "", "" + Name.Value + "");
                MailAddress to = new MailAddress("enykoruna1@gmail.com");
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Сообщение с сервиса BMS";
                m.Body = "Email - " + Email.Value + "<br />Имя - " + Name.Value + "<br />Фамилия - " + LastName.Value + "<br />Сообщение :<br />" + Message.Value;
                m.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("enykoruna1@gmail.com", "jobs192837leny1996");
                smtp.EnableSsl = true;
                smtp.Send(m);

                Result.InnerText = "Ваше сообщение отправлено";
            }
            catch(Exception ex)
            {
                Result.InnerText = ex.Message;
            }
        }

        protected void Send_Message_Click(object sender, EventArgs e)
        {
            Contact();
            Email.Value = "";
            Name.Value = "";
            LastName.Value = "";
            Message.Value = "";
        }
    }
}