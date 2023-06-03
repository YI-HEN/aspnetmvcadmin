using Dapper;
using aspnetmvcadmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class z_repoCodeDatas : BaseClass
{
    public IEFGenericRepository<CodeDatas> repo;
    public z_repoCodeDatas()
    {
        repo = new EFGenericRepository<CodeDatas>(new dbEntities());
    }

    /// <summary>
    /// Dapper 來讀取資料集合
    /// </summary>
    /// <param name="baseNo">類別代號</param>
    /// <returns></returns>
    public List<CodeDatas> GetDapperDataList(string baseNo)
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect(); //SQL選擇
            str_query += GetSQLWhere();        //SQLwhere條件
            str_query += GetSQLOrderBy();      //SQL排序條件

            DynamicParameters parm = new DynamicParameters();  //引用Parameters存入參數
            parm.Add("BaseNo", baseNo);

            var model = dp.ReadAll<CodeDatas>(str_query, parm); //引用Dapper方法的ReadAll<CodeDatas>(str_query, parm)
            //實際執行為ReadAll(煮菜)，因此參數先存數或語法組合(備菜)誰先誰後並無太大關係
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
            SELECT Id, IsEnabled, BaseNo, ParentNo, SortNo, CodeNo, 
            CodeName, CodeValue, Remark
            FROM CodeDatas
            ";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 條件式
    /// </summary>
    /// <returns></returns>
    private string GetSQLWhere()
    {
        string str_query = " WHERE (BaseNo = @BaseNo)";
        return str_query;
    }
    /// <summary>
    /// 取得 SQL 排序
    /// <summary>
    /// <returns></returns>
    private string GetSQLOrderBy()
    {
        return " ORDER BY  SortNo, CodeNo";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(CodeDatas model)
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