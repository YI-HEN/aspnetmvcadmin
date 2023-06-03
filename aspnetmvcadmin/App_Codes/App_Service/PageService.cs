using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 分頁相關服務
/// </summary>
public static class PageService
{
    /// <summary>
    /// 使用分頁功能
    /// </summary>
    public static bool UsePageList { get { return (PageCount > 0); } }
    /// <summary>
    /// 目前頁數
    /// </summary>
    public static int Page { get; set; }
    /// <summary>
    /// 每頁筆數
    /// </summary>
    public static int PageCount { get; set; }
    /// <summary>
    /// 設定程式資訊
    /// </summary>
    /// <param name="page">目前頁數</param>
    /// <param name="pageCount">每頁筆數</param>
    public static void SetPage(int page, int pageCount)
    {
        Page = page;
        PageCount = pageCount;
    }
}