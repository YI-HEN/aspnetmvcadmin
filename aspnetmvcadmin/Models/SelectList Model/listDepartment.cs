using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 部門列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> DepartmentList()
    {
        using (z_repoDepartments model = new z_repoDepartments())  //引用repo新增修改刪除
        {
            var data = model.GetDapperDataList() //引用repo內方法GetDapperDataList()   [此行接續下一行.Select]
                .Select(u => new SelectListItem  //內建方法
                {
                    Text = u.DeptNo + ' ' + u.DeptName,  //畫面顯示
                    Value = u.DeptNo    //實際存入資料
                }).ToList();
            return data;
        }
    }
}