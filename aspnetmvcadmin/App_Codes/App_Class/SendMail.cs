using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SendMail : BaseClass
{
    /// <summary>
    /// 會員註冊寄發驗證的電子郵件
    /// </summary>
    /// <param name="userNo">會員編號</param>
    /// <param name="userName">會員姓名</param>
    /// <param name="emailAddress">電子信箱</param>
    /// <param name="validateCode">驗證碼</param>
    /// <returns></returns>
    public string UserRegister(string userNo, string userName, string emailAddress, string validateCode)
    {
        using (GmailService gmail = new GmailService())
        {
            if (string.IsNullOrEmpty(AppService.WebSiteUrl)) { return "Web.config 未設定 WebSiteUrl 參數!!"; }
            //變數
            string str_member_no = userNo;
            string str_member_name = userName;
            string str_member_email = emailAddress;
            string str_reg_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            string str_controller = "Web";
            string str_action = "ValidateEmail";
            string str_url = AppService.WebSiteUrl;
            string str_validate_url = $"{str_url}/{str_controller}/{str_action}/{validateCode}";

            //信件內容
            gmail.ReceiveEmail = str_member_email;
            gmail.Subject = string.Format("{0} 會員註冊驗證通知信", AppService.AppName);
            gmail.Body = string.Format("敬愛的會員 {0} 您好!! <br /><br />", str_member_name);
            gmail.Body += string.Format("您於 {0} 在我們網站註冊了會員帳號<br />", str_reg_date);
            gmail.Body += string.Format("您的會員帳號為：{0}<br />", str_member_no);
            gmail.Body += "請您點擊以下連結進行帳號電子郵件驗證<br /><br />";
            gmail.Body += string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a><br /><br />", str_validate_url, str_validate_url);
            gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
            gmail.Body += "-------------------------------------------<br />";
            gmail.Body += string.Format("{0}<br />", AppService.AppName);
            gmail.Body += string.Format("{0}<br />", str_url);
            gmail.Body += "-------------------------------------------<br />";
            //寄信
            gmail.Send();
            return gmail.MessageText;
        }
    }

    /// <summary>
    /// 帳號忘記密碼寄發密碼重設的電子郵件
    /// </summary>
    /// <param name="areaName">Area Name</param>
    /// <param name="controllerName">Controller Name</param>
    /// <param name="actionName">Action Name</param>
    /// <param name="emailAddress">電子郵件</param>
    /// <param name="validateCode">驗證碼</param>
    /// <param name="UserName">會員名稱</param>
    /// <param name="userPassword">新的密碼</param>
    /// <returns></returns>
    public string UserForget(string areaName, string controllerName, string actionName, string emailAddress, string validateCode, string UserName, string userPassword)
    {
        using (GmailService gmail = new GmailService())
        {
            if (string.IsNullOrEmpty(AppService.WebSiteUrl)) { return "Web.config 未設定 WebSiteUrl 參數!!"; }
            //變數
            string str_reg_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            string str_url = AppService.WebSiteUrl;
            string str_validate_url = $"{str_url}/{controllerName}/{actionName}/{validateCode}";

            //信件內容
            gmail.ReceiveEmail = emailAddress;
            gmail.Subject = string.Format("{0} 帳號忘記密碼重新設定通知信", AppService.AppName);
            gmail.Body = string.Format("敬愛的會員 {0} 您好!! <br /><br />", UserName);
            gmail.Body += string.Format("您於 {0} 在我們網站執行了忘記密碼的功能，<br /><br />", str_reg_date);
            gmail.Body += string.Format("您新的密碼為： {0} <br /><br />", userPassword);
            gmail.Body += "請您點擊以下連結進行忘記密碼驗證，並再自行變更您熟悉的密碼！！<br /><br />";
            gmail.Body += string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a><br /><br />", str_validate_url, str_validate_url);
            gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
            gmail.Body += "-------------------------------------------<br />";
            gmail.Body += string.Format("{0}<br />", AppService.AppName);
            gmail.Body += string.Format("{0}/Shop<br />", str_url);
            gmail.Body += "-------------------------------------------<br />";
            //寄信
            gmail.Send();
            return gmail.MessageText;
        }
    }
}
