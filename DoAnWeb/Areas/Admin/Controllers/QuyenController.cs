using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Areas.Admin.Controllers
{
    public class QuyenController : Controller
    {
        // GET: Admin/Quyen
        private MyDBconText db = new MyDBconText();
        public ActionResult Index(string search = "")
        {
            List<Quyen> ketqua = db.Roles.Where(row => row..Contains(search)).ToList();
            ViewBag.timkiem = search;
            return View(ketqua);
        }
    }
}