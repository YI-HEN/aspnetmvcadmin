using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspnetmvcadmin.Models
{
    [MetadataType(typeof(z_metaUsers))]
    public partial class Users
    {
        [NotMapped]              //使用[NotMapped]不核對原TABLE
        [Display(Name = "類別")]  //建立原TABLE沒有的欄位
        public string CodeName { get; set; }
        [NotMapped]
        [Display(Name = "角色")]
        public string RoleName { get; set; }
        [NotMapped]
        [Display(Name = "性別")]
        public string GenderName { get; set; }
        [NotMapped]
        [Display(Name = "部門名稱")]
        public string DeptName { get; set; }
        [NotMapped]
        [Display(Name = "職稱")]
        public string TitleName { get; set; }
    }
}

public partial class z_metaUsers
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    public bool IsValid { get; set; }
    [Display(Name = "員工編號")]
    [Required(ErrorMessage = "員工編號不可空白!!")]
    public string UserNo { get; set; }
    [Display(Name = "員工姓名")]
    [Required(ErrorMessage = "員工姓名不可空白!!")]
    public string UserName { get; set; }
    [Display(Name = "登入密碼")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Display(Name = "類別代號")]
    public string CodeNo { get; set; }
    [Display(Name = "角色代號")]
    public string RoleNo { get; set; }
    [Display(Name = "性別")]
    public string GenderCode { get; set; }
    [Display(Name = "部門代號")]
    public string DeptNo { get; set; }
    [Display(Name = "職稱代號")]
    public string TitleNo { get; set; }
    [Display(Name = "出生日期")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    public Nullable<System.DateTime> Birthday { get; set; }
    [Display(Name = "到職日期")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    public Nullable<System.DateTime> OnboardDate { get; set; }
    [Display(Name = "離職日期")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    public Nullable<System.DateTime> LeaveDate { get; set; }
    [Display(Name = "電子信箱")]
    [EmailAddress(ErrorMessage = "電子信箱格式不正確!!")]
    public string ContactEmail { get; set; }
    [Display(Name = "連絡電話")]
    public string ContactTel { get; set; }
    [Display(Name = "連絡地址")]
    public string ContactAddress { get; set; }
    [Display(Name = "驗證碼")]
    public string ValidateCode { get; set; }
    [Display(Name = "備註")]
    public string Remark { get; set; }
}