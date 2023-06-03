using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 使用者程式權限
/// </summary>
public static class SecurityService
{
    /// <summary>
    /// 新增
    /// </summary>
    public static bool IsAdd { get { return SessionService.GetBoolValue("IsAdd", false); } set { SessionService.SetValue("IsAdd", value); } }
    /// <summary>
    /// 新增修改
    /// </summary>
    public static bool IsCreateEdit { get { return SessionService.GetBoolValue("IsCreateEdit", false); } set { SessionService.SetValue("IsCreateEdit", value); } }
    /// <summary>
    /// 審核
    /// </summary>
    public static bool IsConfirm { get { return SessionService.GetBoolValue("IsConfirm", false); } set { SessionService.SetValue("IsConfirm", value); } }
    /// <summary>
    /// 刪除
    /// </summary>
    public static bool IsDelete { get { return SessionService.GetBoolValue("IsDelete", false); } set { SessionService.SetValue("IsDelete", value); } }
    /// <summary>
    /// 下載
    /// </summary>
    public static bool IsDownload { get { return SessionService.GetBoolValue("IsDownload", false); } set { SessionService.SetValue("IsDownload", value); } }
    /// <summary>
    /// 修改
    /// </summary>
    public static bool IsEdit { get { return SessionService.GetBoolValue("IsEdit", false); } set { SessionService.SetValue("IsEdit", value); } }
    /// <summary>
    /// 列表
    /// </summary>
    public static bool IsIndex { get { return SessionService.GetBoolValue("IsIndex", false); } set { SessionService.SetValue("IsIndex", value); } }
    /// <summary>
    /// 作廢
    /// </summary>
    public static bool IsInvalid { get { return SessionService.GetBoolValue("IsInvalid", false); } set { SessionService.SetValue("IsInvalid", value); } }
    /// <summary>
    /// 列印
    /// </summary>
    public static bool IsPrint { get { return SessionService.GetBoolValue("IsPrint", false); } set { SessionService.SetValue("IsPrint", value); } }
    /// <summary>
    /// 回復
    /// </summary>
    public static bool IsUndo { get { return SessionService.GetBoolValue("IsUndo", false); } set { SessionService.SetValue("IsUndo", value); } }
    /// <summary>
    /// 上傳
    /// </summary>
    public static bool IsUpload { get { return SessionService.GetBoolValue("IsUpload", false); } set { SessionService.SetValue("IsUpload", value); } }
    /// <summary>
    /// 權限初始化
    /// </summary>
    public static void InitSecurity()
    {
        IsAdd = false;
        IsCreateEdit = false;
        IsConfirm = false;
        IsDelete = false;
        IsDownload = false;
        IsEdit = false;
        IsIndex = false;
        IsInvalid = false;
        IsPrint = false;
        IsUndo = false;
        IsUpload = false;
    }
    /// <summary>
    /// 除錯模式權限
    /// </summary>
    public static void DebugModeSecurity()
    {
        IsAdd = true;
        IsCreateEdit = true;
        IsConfirm = true;
        IsDelete = true;
        IsDownload = true;
        IsEdit = true;
        IsIndex = true;
        IsInvalid = true;
        IsPrint = true;
        IsUndo = true;
        IsUpload = true;
    }
    /// <summary>
    /// 設定使用者程式的權限
    /// </summary>
    public static void ProgramSecurity()
    {
        if (AppService.DebugMode) { DebugModeSecurity(); return; }
        InitSecurity();
        //using (z_repoSecuritys sec = new z_repoSecuritys())
        //{
        //    var model = sec.repo.ReadSingle(m =>
        //        m.TargetNo == UserService.UserNo &&
        //        m.RoleNo == UserService.RoleNo &&
        //        m.ModuleNo == PrgService.ModuleNo &&
        //        m.PrgNo == PrgService.PrgNo);
        //    if (model != null)
        //    {
        //        IsAdd = model.IsAdd;
        //        IsCreateEdit = (model.IsAdd || model.IsEdit);
        //        IsConfirm = model.IsConfirm;
        //        IsDelete = model.IsDelete;
        //        IsDownload = model.IsDownload;
        //        IsEdit = model.IsEdit;
        //        IsIndex = true;
        //        IsInvalid = model.IsInvalid;
        //        IsPrint = model.IsPrint;
        //        IsUndo = model.IsUndo;
        //        IsUpload = model.IsUpload;
        //    }
        //}
    }
}