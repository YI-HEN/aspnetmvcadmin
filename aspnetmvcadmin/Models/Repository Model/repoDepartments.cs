using aspnetmvcadmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class z_repoDepartments : BaseClass
{
    public IEFGenericRepository<Departments> repo;
    public z_repoDepartments()
    {
        repo = new EFGenericRepository<Departments>(new dbEntities());
    }

    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <returns></returns>
    public List<Departments> GetDapperDataList()
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect(); //SQL選擇
            str_query += GetSQLWhere();        //SQLwhere條件
            str_query += GetSQLOrderBy();      //SQL排序條件
            var model = dp.ReadAll<Departments>(str_query);//引用Dapper方法的ReadAll<CodeDatas>(str_query)
            return model;
        }
    }
    /// <summary>
    /// 取得 SQL 欄位及表格名稱
    /// <summary>
    /// <returns></returns>
    private string GetSQLSelect()
    {
        string str_query = @"
SELECT Id, DeptNo, DeptName, Remark FROM Departments
";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// <summary>
    /// <param name="searchText">查詢文字</param>
    /// <returns></returns>
    private string GetSQLWhere()
    {
        string str_query = "";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 排序
    /// <summary>
    /// <returns></returns>
    private string GetSQLOrderBy()
    {
        return " ORDER BY  DeptNo";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(Departments model)
    {
        repo.CreateEdit(model, model.Id);
    }
    /// <summary>
    /// 刪除
    /// <summary>
    /// <param name="id">Id</param>
    public void Delete(int id)
    {
        var model = repo.ReadSingle(m => m.Id == id);
        if (model != null) repo.Delete(model, true);
    }
    /// <summary>
    /// 檢查 Id 是否存在
    /// <summary>
    /// <param name="id">Id</param>
    /// <returns></returns>
    public bool IdExists(int id)
    {
        var model = repo.ReadSingle(m => m.Id == id);
        return (model != null);
    }
}