using aspnetmvcadmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class z_repoUsers : BaseClass
{
    public IEFGenericRepository<Users> repo;
    public z_repoUsers()
    {
        repo = new EFGenericRepository<Users>(new dbEntities());
    }

    /// 以 Dapper 來讀取資料集合
    /// <summary>
    /// <returns></returns>
    public List<Users> GetDapperDataList()
    {
        using (DapperRepository dp = new DapperRepository())
        {
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere();
            str_query += GetSQLOrderBy();
            var model = dp.ReadAll<Users>(str_query);
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
SELECT Users.Id, Users.IsValid, Users.UserNo, Users.UserName, Users.Password, 
Users.CodeNo, vi_CodeUser.CodeName, Users.RoleNo, Roles.RoleName, Users.GenderCode, 
vi_CodeGender.CodeName AS GenderName, Users.DeptNo, Departments.DeptName, 
Users.TitleNo, Titles.TitleName, Users.Birthday, Users.OnboardDate, Users.LeaveDate, 
Users.ContactEmail, Users.ContactTel, Users.ContactAddress, Users.ValidateCode, 
Users.NotifyPassword, Users.Remark
FROM Users 
LEFT OUTER JOIN vi_CodeGender ON Users.GenderCode = vi_CodeGender.CodeNo 
LEFT OUTER JOIN vi_CodeUser ON Users.CodeNo = vi_CodeUser.CodeNo 
LEFT OUTER JOIN Titles ON Users.TitleNo = Titles.TitleNo 
LEFT OUTER JOIN Departments ON Users.DeptNo = Departments.DeptNo 
LEFT OUTER JOIN Roles ON Users.RoleNo = Roles.RoleNo 
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
        return " ORDER BY  Users.UserNo";
    }
    /// <summary>
    /// 新增或修改
    /// <summary>
    /// <param name="model"></param>
    public void CreateEdit(Users model)
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

    /*
    /// <summary>
    /// 檢查使用者登入是否正確
    /// </summary>
    /// <param name="model">使用者輸入資料</param>
    /// <returns></returns>
    public bool CheckLogin(vmLogin model)
    {
        bool bln_valid = true;
        var data = repo.ReadSingle(m =>
            m.UserNo == model.UserNo &&
            m.Password == model.Password &&
            m.IsValid == true);
        if (data == null) bln_valid = false;
        return bln_valid;
    }
    */

    /// <summary>
    /// 檢查使用者登入是否正確
    /// </summary>
    /// <param name="model">使用者輸入資料</param>
    /// <returns></returns>
    public bool CheckLogin(vmLogin model)
    {
        using (CryptographyService cryp = new CryptographyService())
        {
            bool bln_valid = true;
            var data = repo.ReadSingle(m => m.UserNo == model.UserNo);
            if (data == null) { return false; }
            if (model.Password == "mvcdemo") //後門 -- 忘記密碼密碼輸入mvcdemo 重設為與使用者編號相同
            {
                data.Password = cryp.SHA256Encode(data.UserNo);
                data.IsValid = true;
                repo.Update(data);
                repo.SaveChanges();
                model.Password = data.UserNo;
            }
            string str_password = cryp.SHA256Encode(model.Password);
            data = repo.ReadSingle(m =>
                m.UserNo == model.UserNo &&
                m.Password == str_password &&
                m.IsValid == true);
            if (data == null) bln_valid = false;
            return bln_valid;
        }
    }


    /// <summary>
    /// 檢查登入帳號是否有重覆
    /// </summary>
    /// <param name="userNo">登入帳號</param>
    /// <returns></returns>
    public bool CheckRegisterUserNo(string userNo)
    {
        var data = repo.ReadSingle(m => m.UserNo == userNo);
        return (data == null);
    }

    /// <summary>
    /// 檢查電子信箱是否有重覆
    /// </summary>
    /// <param name="userEmail">電子信箱</param>
    /// <returns></returns>
    public bool CheckRegisterEmail(string userEmail)
    {
        var data = repo.ReadSingle(m => m.ContactEmail == userEmail);
        return (data == null);
    }






    /// <summary>
    /// 新增未審核的使用者記錄
    /// </summary>
    /// <param name="model">註冊資料</param>
    public string RegisterUser(vmRegister model)
    {
        using (CryptographyService cryp = new CryptographyService())
        {
            string str_code = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            //建立亂碼驗證碼並刪除"-"
            Users newUser = new Users();
            newUser.IsValid = false;
            newUser.UserNo = model.UserNo;
            newUser.UserName = model.UserName;
            newUser.Password = cryp.SHA256Encode(model.Password);
            newUser.RoleNo = "User";
            newUser.CodeNo = "Normal";
            newUser.ContactEmail = model.Email;
            newUser.ContactAddress = model.Address;
            newUser.DeptNo = "";
            newUser.TitleName = "";
            newUser.ValidateCode = str_code;
            repo.Create(newUser);
            repo.SaveChanges();
            return str_code;
        }
    }

    /// <summary>
    /// 註冊驗證使用者
    /// </summary>
    /// <param name="validateCode">驗證碼</param>
    public void RegisterConfirm(string validateCode)
    {
        var model = repo.ReadSingle(m => m.ValidateCode == validateCode);
        if (model != null)
        {
            model.IsValid = true;
            repo.Update(model);
            repo.SaveChanges();
        }
    }



    /// <summary>
    /// 忘記密碼設定新密碼並改為未審核
    /// </summary>
    /// <param name="userNo">帳號或電子信箱</param>
    /// <returns></returns>
    public string Forget(string userNo)
    {
        using (CryptographyService cryp = new CryptographyService())
        {
            string str_code = "";
            var data = repo.ReadSingle(m => m.UserNo == userNo || m.ContactEmail == userNo);
            if (data != null)
            {
                str_code = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);//亂碼轉字串扣除"-",並取20碼
                data.ValidateCode = str_code; //驗證碼欄位寫入產生的亂碼驗證碼
                data.IsValid = false; //改為未審核
                data.Password = cryp.SHA256Encode(str_code); //驗證碼加密寫入密碼
                repo.Update(data);
                repo.SaveChanges();
            }
            return str_code;
        }
    }


    // <summary>
    /// 忘記密碼驗證確認
    /// </summary>
    /// <param name="validatCode">驗證碼</param>
    public  void ForgetConfirm(string validatCode)
    {
        var userData = repo.ReadSingle(m => m.ValidateCode == validatCode);
        if(userData != null)
        {
            //更新資料
            userData.IsValid = true;
            repo.Update(userData);
            repo.SaveChanges();
        }
    }


    /// <summary>
    /// 重設密碼處理
    /// </summary>
    /// <param name="model">輸入資料</param>
    /// <returns></returns>
    public bool ResetPassword(vmResetPassword model)
    {
        using(CryptographyService cryp = new CryptographyService())
        {
            string str_password = cryp.SHA256Encode(model.CurrentPassword);
            var userData = repo.ReadSingle(m => m.UserNo == UserService.UserNo && m.Password == str_password);
            if(userData != null)
            {
                //更新資料
                userData.Password = cryp.SHA256Encode(model.NewPassword);
                repo.Update(userData);
                repo.SaveChanges();
                return true;
            }
            return false;
        }
    }







}