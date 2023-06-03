using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SendEmailService : BaseClass
{
    /// <summary>
    /// 使用者註冊驗證信
    /// </summary>
    /// <param name="receiverName">收件者姓名</param>
    /// <param name="receiverMail">收件者電子信箱</param>
    /// <param name="validateCode">驗證碼</param>
    /// <returns>錯誤訊息</returns>
    public string RegisterMail(string receiverName, string receiverMail, string validateCode)
    {
        using (GmailService gmail = new GmailService())
        {
            string str_web_url = "http://localhost:13380";
            string str_url = str_web_url + "/User/RegisterValidate/" + validateCode;
            gmail.ReceiveEmail = receiverMail;
            gmail.Subject = "使用者註冊驗證";
            gmail.Body = $"親愛的使用者 {receiverName} 您好： <br /><br />";
            gmail.Body += "感謝您註冊了我們的會員，以下的連結為驗證連結，請點擊後即完成註冊程序!! <br /><br />";

            gmail.Body += $"<a href='{str_url}'>驗證連結網址</a><br /><br />";
            gmail.Body += "本信件為系統自動發出，請勿回信，謝謝!! <br /><br />";
            gmail.Send();
            return gmail.MessageText;
        }
    }

    /// <summary>
    /// 使用者忘記密碼驗證信
    /// </summary>
    /// <param name="receiverName">收件者姓名</param>
    /// <param name="receiverMail">收件者電子信箱</param>
    /// <param name="validateCode">驗證碼</param>
    /// <returns>錯誤訊息</returns>
    public string ForgetMail(string receiverName, string receiverMail, string validateCode, string newPassword)
    {
        using (GmailService gmail = new GmailService())
        {
            string str_web_url = "http://localhost:13380";
            string str_url = str_web_url + "/User/ForgetValidate/" + validateCode;
            gmail.ReceiveEmail = receiverMail;
            gmail.Subject = "使用者密碼重設";
            gmail.Body = $"親愛的使用者 {receiverName} 您好： <br /><br />";
            gmail.Body += "您執行了忘記密碼的功能，以下是您的新密碼: <br /><br />";
            gmail.Body += $"<h4>{newPassword}</h4><br /><br />";

            gmail.Body += "以下的連結為驗證連結，請點擊後即完成重設密碼程序!! <br /><br />";
            gmail.Body += $"<a href='{str_url}'>重設密碼連結網址</a><br /><br />";
            gmail.Body += "本信件為系統自動發出，請勿回信，謝謝!! <br /><br />";
            gmail.Send();
            return gmail.MessageText;
        }
    }
}