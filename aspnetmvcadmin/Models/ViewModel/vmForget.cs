using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class vmForget
{
    [Display(Name = "帳號或電子信箱")]
    [Required(ErrorMessage = "帳號或電子信箱不可空白!!")]
    public string UserNo { get; set; }
}