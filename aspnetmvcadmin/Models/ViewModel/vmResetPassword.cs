using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class vmResetPassword
{
    [Display(Name = "目前密碼")]
    [Required(ErrorMessage = "目前密碼不可空白!!")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [Display(Name = "新的密碼")]
    [Required(ErrorMessage = "登入密碼不可空白!!")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Display(Name = "確認密碼")]
    [Required(ErrorMessage = "確認密碼不可空白!!")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "與登入密碼輸入不相符!!")]
    public string ConfirmPassword { get; set; }
}