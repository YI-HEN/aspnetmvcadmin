using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public partial class ListItemData : BaseClass
{
    /// <summary>
    /// 角色列表
    /// </summary>
    /// <returns></returns>
    public List<SelectListItem> RoleList()
    {
        using (z_repoRoles model = new z_repoRoles())
        {
            var data = model.GetDapperDataList()
                .Select(u => new SelectListItem
                {
                    Text = u.RoleName,
                    Value = u.RoleNo
                }).ToList();
            return data;
        }
    }
}