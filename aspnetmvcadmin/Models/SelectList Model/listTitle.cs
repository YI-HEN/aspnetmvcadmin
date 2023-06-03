using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 職稱列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> TitleList()
    {
        using (z_repoTitles model = new z_repoTitles())
        {
            var data = model.GetDapperDataList()
                .Select(u => new SelectListItem
                {
                    Text = u.TitleNo + ' ' + u.TitleName,
                    Value = u.TitleNo
                }).ToList();
            return data;
        }
    }
}