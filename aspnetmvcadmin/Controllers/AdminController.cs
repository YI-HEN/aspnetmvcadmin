using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnetmvcadmin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            PrgService.SetProgram("Admin", "後台管理");
            ViewService.SetView(enAction.Home, enCardSize.max, "");
            return View();
        }
    }
}