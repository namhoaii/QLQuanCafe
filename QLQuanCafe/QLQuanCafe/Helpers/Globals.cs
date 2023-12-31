﻿using System;
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

        public static string FromEmail = "Nhập email của bạn";
        public static string Subject = "Thư xác nhận thay đổi mật khẩu";
        public static string PasswordEmail = "app password";

        public static string KeyUsername = "Username";

        #endregion

        #region Method

        public static string BodyEmail(string email, string username, string password) =>
            $@"<html><body><h1>Chào {email}</h1><div><p>- Tài khoản của bạn là: <span style=""color: red; font-weight: bold;"">{username}</span></p><p>- Mật khẩu mới của bạn là: <span style=""color: red; font-weight: bold;"">{password}</span></p><p><b>Không cung cấp mật khẩu cho bất kỳ ai. Hãy đổi mật khẩu ngay sau khi nhận được thư này.</b></p><p><b>Vui lòng không trả lời email này. Xin cảm ơn</b></p><b>Trân Trọng!</b></div></body></html>";

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
            catch (Exception)
            {
                return false;
            }
        }

        public static string RandomPass(int length = 8)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
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
