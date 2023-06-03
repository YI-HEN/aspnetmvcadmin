using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnetmvcadmin.Controllers
{
    public class UserController : Controller
    {
        public string sha256(string id)
        {
            using (CryptographyService cryp = new CryptographyService())
            {
                return cryp.SHA256Encode(id);
            }
        }


        [HttpGet]
        public ActionResult Login()
        {
            ActionService.ViewActionName = "登入";
            ActionService.ViewPrgNo = "";
            ActionService.ViewPrgName = "使用者";
            vmLogin model = new vmLogin();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(vmLogin model)
        {
            if (!ModelState.IsValid) return View(model);
            using (z_repoUsers user = new z_repoUsers())
            {
                if (!user.CheckLogin(model)) //比較帳密
                {
                    ModelState.AddModelError("UserNo", "登入帳號或密碼輸入不正確!!");
                    model.UserNo = "";
                    model.Password = "";
                    return View(model);
                }
                var data = user.repo.ReadSingle(m => m.UserNo == model.UserNo);//讀取使用者檔案
                UserService.Login(data.UserNo, data.UserName, data.RoleNo);//使用者資料用UserService.Login寫入UserService

                if (data.RoleNo == "Admin") return RedirectToAction("Index", "Admin", new { area = "" });
                if (data.RoleNo == "User") return RedirectToAction("Index", "Home", new { area = "" });

                ModelState.AddModelError("UserNo", "登入帳號角色設定不正確!!");
                model.UserNo = "";
                model.Password = "";
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            ActionService.ViewActionName = "註冊";
            ActionService.ViewPrgNo = "";
            ActionService.ViewPrgName = "使用者";
            vmRegister model = new vmRegister();
            model.GenderCode = "M";
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(vmRegister model)
        {
            //1.檢查輸入資料是否合格
            if (!ModelState.IsValid) return View(model);
            using (z_repoUsers user = new z_repoUsers())
            {
                //驗證重覆註冊
                if (!user.CheckRegisterUserNo(model.UserNo))
                {
                    ModelState.AddModelError("UserNo", "登入帳號重覆註冊!!");
                    return View(model);
                }
                if (!user.CheckRegisterEmail(model.Email))
                {
                    ModelState.AddModelError("Email", "電子信箱重覆註冊!!");
                    return View(model);
                }
                //2.新增一個未審核的使用者
                string str_code = user.RegisterUser(model);
                //3.寄出一封驗證信
                using (SendEmailService sendEmail = new SendEmailService())
                {
                    //寄出驗證信
                    string str_message = sendEmail.RegisterMail(model.UserName, model.Email, str_code);
                    if (string.IsNullOrEmpty(str_message))
                    {
                        str_message = "您的註冊資訊已建立，請記得收信完成驗證流程!!";
                    }
                    //4.顯示註冊後訊息
                    TempData["MessageText"] = str_message;
                    return RedirectToAction("MessageResult", "User", new { area = "" });
                }
            }
        }

        [HttpGet]
        public ActionResult RegisterValidate(string id)
        {
            using (z_repoUsers user = new z_repoUsers())
            {
                //1.將使用者狀態改為已審核
                user.RegisterConfirm(id);

                //2.顯示註冊後訊息
                TempData["MessageText"] = "恭喜您，您的帳號已通過驗證，您可以用註冊的帳號登入本系統!!";
                return RedirectToAction("MessageResult", "User", new { area = "" });
            }
        }

        [HttpGet]
        public ActionResult MessageResult()
        {
            ViewBag.MessageText = TempData["MessageText"].ToString();
            return View();
        }


        [HttpGet]
        public ActionResult Forget()
        {
            ActionService.ViewActionName = "忘記密碼";
            ActionService.ViewPrgNo = "";
            ActionService.ViewPrgName = "使用者";
            vmForget model = new vmForget();
            return View(model);
        }


        [HttpPost]
        public ActionResult Forget(vmForget model)
        {
            //1.檢查輸入是否合格
            if (!ModelState.IsValid) return View(model);

            //2.檢查帳號是否存在，存在則執行設定新密碼也設定狀態為未審核，回傳新密碼，空白表示有問題        
            using (z_repoUsers user = new z_repoUsers())
            {
                string str_new_password = user.Forget(model.UserNo);//新密碼
                if (string.IsNullOrEmpty(str_new_password))//檢查有無帳號
                {
                    ModelState.AddModelError("UserNo", "查無帳號或電子信箱資訊!!");
                    return View(model);
                }
                using (SendEmailService sendEmail = new SendEmailService())//寄出忘記密碼驗證信
                {
                    //寄出驗證信
                    var userData = user.repo.ReadSingle(m => m.UserNo == model.UserNo);
                    string str_message = sendEmail.ForgetMail(userData.UserName, userData.ContactEmail, userData.ValidateCode, str_new_password);//取得驗證碼
                    if (string.IsNullOrEmpty(str_message))
                    {
                        str_message = "您重設密碼的要求已受理，請記得收信完成重設密碼的流程!!";
                    }
                    //顯示註冊訊息
                    TempData["MessageText"] = str_message;
                    return RedirectToAction("MessageResult", "User", new { area = "" });
                }
            }
        }


        [HttpGet]
        public ActionResult ForgetValidate(string id) //接收忘記密碼使用者的ID
        {
            using (z_repoUsers user = new z_repoUsers())
            {
                //更新使用者為審核狀態
                user.ForgetConfirm(id);
                //顯示重設密碼訊息
                TempData["MessageText"] = "您的密碼已重設，新的密碼已於信件中告知，下次登入請使用新密碼!!";
                return RedirectToAction("MessageResult", "User", new { area = "" });
            }
        }



        [HttpGet]
        public ActionResult ResetPassword() 
        {
            ActionService.ViewActionName = "變更密碼";
            ActionService.ViewPrgNo = "";
            ActionService.ViewPrgName = "使用者";
            vmResetPassword model = new vmResetPassword();
            return View(model);
        }

        [HttpPost]
        public ActionResult ResetPassword(vmResetPassword model)
        {
            //1.檢查輸入合格性
            if(!ModelState.IsValid)return View(model);
            using(z_repoUsers user = new z_repoUsers()) 
            { //2.檢查原密碼是否正確
                if (!user.ResetPassword(model))
                {
                    ModelState.AddModelError("CurrentPassword", "原密碼輸入不正確!!");
                    return View(model);
                }
            }
            //3.回首頁
            TempData["MessageText"] = "您的密碼已變更，下次登入請用心密碼!!";
            return RedirectToAction("Index", "Home", new { area = "" });

        }



    }
}