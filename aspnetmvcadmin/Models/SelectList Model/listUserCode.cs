using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 使用者類別列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> UserCodeList()
    {
        using (z_repoCodeDatas model = new z_repoCodeDatas())
        {
            var data = model.GetDapperDataList("User")
                .Select(u => new SelectListItem
                {
                    Text = u.CodeName,
                    Value = u.CodeNo
                }).ToList();
            return data;
        }
    }
}