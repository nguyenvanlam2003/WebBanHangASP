using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Areas.Admin.Controllers
{
    
    public class RoleController : Controller
    {
        // GET: Admin/Role
       
        public ActionResult Index()
        {
             MyDBconText db = new MyDBconText();
            List<object> list = db.Roles.ToList();
            List<Role> ketqua = list;
            return View(ketqua);
        }
    }
}