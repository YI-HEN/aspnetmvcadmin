using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// 程式相關服務
/// </summary>
public static class PrgService
{
    /// <summary>
    /// 程式編號
    /// </summary>
    public static string ModuleNo { get; set; }
    /// <summary>
    /// 程式編號
    /// </summary>
    public static string PrgNo { get; set; }
    /// <summary>
    /// 程式名稱
    /// </summary>
    public static string PrgName { get; set; }
    /// <summary>
    /// 程式資訊
    /// </summary>
    public static string PrgInfo
    {
        get
        {
            if (string.IsNullOrEmpty(PrgNo)) return PrgName;
            return string.Format("{0} {1}", PrgNo, PrgName);
        }
    }
    /// <summary>
    /// 是否為排序模式
    /// </summary>
    public static bool SortMode { get; set; }
    /// <summary>
    /// 排序欄位
    /// </summary>
    public static string SortColumn { get; set; }
    /// <summary>
    /// 排序方式
    /// </summary>
    public static enSortDirection SortDirection { get; set; }
    public static int RowId { get; set; } = 0;
    /// <summary>
    /// 已選取的 Row Id 參數值
    /// </summary>
    public static int SelectedId { get; set; } = 0;
    /// <summary>
    /// SearchText
    /// </summary>
    public static string SearchText { get; set; } = "";
    /// <summary>
    /// 設定程式資訊
    /// </summary>
    /// <param name="prgNo">程式代號</param>
    /// <param name="prgName">程式名稱</param>
    public static void SetProgram(string prgNo, string prgName)
    {
        PrgNo = prgNo;
        PrgName = prgName;
    }

    /// <summary>
    /// 設定排序
    /// </summary>
    /// <param name="sortColumn">排序欄位</param>
    /// <param name="sortDirection">排序方向</param>
    public static void SetSort(string sortColumn, enSortDirection sortDirection)
    {
        SortMode = true;
        SortColumn = sortColumn;
        SortDirection = sortDirection;
    }
    /// <summary>
    /// 設定排序欄位及方向
    /// </summary>
    /// <param name="columnName">欄位</param>
    public static void SetSort(string columnName)
    {
        if (SortColumn == columnName)
        {
            if (SortDirection == enSortDirection.ASC)
                SortDirection = enSortDirection.DESC;
            else
                SortDirection = enSortDirection.ASC;
        }
        else
        {
            SortColumn = columnName;
            SortDirection = enSortDirection.ASC;
        }
    }
    /// <summary>
    /// 取得欄位排序圖示
    /// </summary>
    /// <returns></returns>
    public static string GetSortIcon(string columnName)
    {
        string str_icon = "";
        if (SortMode)
        {
            if (SortColumn == columnName)
            {
                if (SortDirection == enSortDirection.ASC) str_icon = "▲";
                if (SortDirection == enSortDirection.DESC) str_icon = "▼";
            }
        }
        return str_icon;
    }
}
