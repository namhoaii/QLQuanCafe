using System;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;

namespace QLQuanCafe.Helpers
{
    public static class Globals
    {

        #region Attribute

        public static string PathDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QLQuanCafe.db3");

        public static string FromEmail = "itnautomations@gmail.com";
        public static string Subject = "Thư xác nhận thay đổi mật khẩu";
        public static string PasswordEmail = "pbjkyvnsnaiutydt";


        #endregion

        #region Method

        public static string BodyEmail(string email, string username, string password) =>
            $@"<html><body><h1>Chào {email}</h1><div><p>- Tài khoản của bạn là: <span style=""color: red; font-weight: bold;"">{username}</span></p><p>- Mật khẩu mới của bạn là: <span style=""color: red; font-weight: bold;"">{password}</span></p><b>Không cung cấp mật khẩu cho bất kỳ ai. Hãy đổi mật khẩu ngay sau khi nhận được thư này.</b><br><br><b>Vui lòng không trả lời email này. Xin cảm ơn</b><b>Trân Trọng!</b></div></body></html>";

        public static bool SendEmail(string subject, string bodyMail, string toEmail)
        {

            MailMessage mail = new MailMessage(FromEmail, toEmail);
            mail.Subject = subject;
            mail.Body = bodyMail;
            mail.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new NetworkCredential(FromEmail, PasswordEmail);
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string RandomPass(int length = 8)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        #endregion


    }
}
