using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;

/// <summary>
/// 使用者相關服務
/// </summary>
public static class UserService
{
    public static string UserNo { get { return SessionService.GetValue("UserNo"); } set { SessionService.SetValue("UserNo", value); } }
    public static string UserName { get { return SessionService.GetValue("UserName"); } set { SessionService.SetValue("UserName", value); } }
    public static string RoleNo { get { return SessionService.GetValue("RoleNo", "Guest"); } set { SessionService.SetValue("RoleNo", value); } }
    public static string RoleName { get { return SessionService.GetValue("RoleName", "訪客"); } set { SessionService.SetValue("RoleName", value); } }
    public static string TitleNo { get { return SessionService.GetValue("TitleNo", "未設定"); } set { SessionService.SetValue("TitleNo", value); } }
    public static string TitleName { get { return SessionService.GetValue("TitleName", "未設定"); } set { SessionService.SetValue("TitleName", value); } }
    public static string DeptNo { get { return SessionService.GetValue("DeptNo", "未設定"); } set { SessionService.SetValue("DeptNo", value); } }
    public static string DeptName { get { return SessionService.GetValue("DeptName", "未設定"); } set { SessionService.SetValue("DeptName", value); } }
    public static string UserImage { get { return ImageService.GetImageUrl("~/Images/User", UserNo, "none", "jpg"); } }
    public static bool IsLogin { get { return SessionService.GetBoolValue("IsLogin", false); } set { SessionService.SetValue("IsLogin", value); } }
    /// <summary>
    /// 登入
    /// </summary>
    /// <param name="userNo">使用者代號</param>
    /// <param name="userName">使用者姓名</param>
    /// <param name="roleNo">角色代號</param>
    public static void Login(string userNo, string userName, string roleNo)
    {
        UserNo = userNo;
        UserName = userName;
        RoleNo = roleNo;
        IsLogin = true;
    }
    /// <summary>
    /// 登出
    /// </summary>
    public static void Logout()
    {
        UserNo = "";
        UserName = "";
        RoleNo = "";
        IsLogin = false;
    }
    /// <summary>
    /// 除錯模式預設使用者
    /// </summary>
    public static void DemoUser()
    {
        UserNo = "demo";
        UserName = "測試帳號";
        IsLogin = true;
    }
}