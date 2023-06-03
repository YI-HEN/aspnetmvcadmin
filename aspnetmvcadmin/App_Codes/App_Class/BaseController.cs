using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

/// <summary>
/// 控制器底層
/// </summary>
public class BaseController : Controller
{
    protected override void ExecuteCore()
    {
        if (string.IsNullOrEmpty(AppService.LanguageNo)) AppService.LanguageNo = "zh-TW";
        CultureInfo culture = new CultureInfo(AppService.LanguageNo);
        Thread.CurrentThread.CurrentUICulture = culture;
        Thread.CurrentThread.CurrentCulture = culture;
        if (AppService.DebugMode && !UserService.IsLogin)
        {
            UserService.RoleNo = "User";
            if (!string.IsNullOrEmpty(ActionService.Area))
                UserService.RoleNo = ActionService.Area;
            UserService.DemoUser();
        }
        base.ExecuteCore();
    }

    protected override bool DisableAsyncSupport
    {
        get { return true; }
    }

    protected virtual void SetViewAction(enAction actionName) { ViewService.Action = actionName; }

    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="id">欄位名稱</param>
    /// <returns></returns>
    [HttpGet]
    public ActionResult Sort(string id)
    {
        PrgService.SetSort(id);
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });
    }

    /// <summary>
    /// 查詢
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [LoginAuthorize()]
    public ActionResult Search()
    {
        object obj_text = Request.Form[ActionService.SearchText];
        string str_text = (obj_text == null) ? string.Empty : obj_text.ToString();
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area, searchText = str_text });
    }

    /// <summary>
    /// 選取
    /// </summary>
    /// <param name="id">記錄 ID</param>
    /// <returns></returns>
    [HttpGet]
    public ActionResult Select(int id = 0)
    {
        PrgService.SelectedId = id;
        return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area, page = PageService.Page, searchText = PrgService.SearchText });
    }
}