using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SakaryaBel.Services.LibraryService
{
    public static class EmailService
    {
        public static bool emailSend(string username, string email, string subject, string body)
        {
            string bodyTemplate = @"<table border='0' cellpadding='0' cellspacing='0' width='100%' style='background-color:#1f1f1f; height:52px;'><tr><td align='center'><center>
                                    <table border='0' cellpadding='0' cellspacing='0' width='600px' style='height:100%;'><tr><td align='left' valign='middle' style='padding-left:20px;'>
                                    <img src='{OrganizationLogo}' width='86px' height='14px' /></td></tr></table></center></td></tr></table>
                                    <table border='0' cellpadding='0' cellspacing='0' width='100%' style='background-color:#f8f8f8;border-bottom:1px solid #e7e7e7;'><tr><td><center>
                                    <table border='0' cellpadding='0' cellspacing='0' width='600px' style='height:100%;'><tr><td valign='top' style='padding:20px;'>
                                    <h2>Merhaba {username} :</h2><br />
                                    <div class='textdark'><p>{content}</p><br/>

                                    <p align='center'>
                                    <a href='{link}' title='Go To Web Page'><input name='Continue' type='button' value='Web Sitemize Gitmek için Tıklayınız' /></a>
                                    </p>

                                    <p>If the button above does't work, you can copy and paste this URL in your browser: <strong>{link}</strong></p></div><br />
                                    </td></tr></table></center></td></tr></table><table border='0' cellpadding='0' cellspacing='0' width='100%' style='background-color:#1f1f1f;'><tr>
                                    <td align='center'><center><table border='0' cellpadding='0' cellspacing='0' width='600px' style='height:100%;'><tr>
                                    <td align='right' valign='middle' class='textwhite' style='font-size:12px;padding:20px;'>
                                    This e-mail is sent to you automatically. Please do not reply to this e-mail..<br />
                                    <br />&copy; 2015 WebSiteName.<strong></strong></td></tr></table></center></td></tr></table>";

            bool returnValue = false;
            try
            {
                SmtpClient client = new SmtpClient("mail.prodemarge.com", 587);
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("iletisim@prodemarge.com", "temppass246");
                //client.EnableSsl = true;
                // Message
                MailMessage newMessage = new MailMessage();
                newMessage.From = new MailAddress("iletisim@prodemarge.com", "Prodem", System.Text.Encoding.UTF8);
                newMessage.To.Add(email);               
                newMessage.IsBodyHtml = true;
                bodyTemplate = bodyTemplate.Replace("{username}", username);
                bodyTemplate = bodyTemplate.Replace("{content}", body);
                bodyTemplate = bodyTemplate.Replace("{OrganizationLogo}", "http://www.prodemarge.com/images/logo.png");
                bodyTemplate = bodyTemplate.Replace("{link}", "http://www.prodemarge.com/");
                newMessage.Body = bodyTemplate; //"Merhaba " + username + "; </br></br>" + body;
                newMessage.Subject = subject;

                client.Send(newMessage);
                returnValue = true;
            }
            catch (Exception)
            {
                returnValue = false;
                throw;
            }

            return returnValue;
        }
    }
}
