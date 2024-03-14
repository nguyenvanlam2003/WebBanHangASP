using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using DoAnWeb.Filter;
namespace DoAnWeb.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        [CustomAuthorize]
        public ActionResult Index()
        {
            TaoMaTuDong hd = new TaoMaTuDong();
            string sp = hd.sinhMa("SP");
            ViewBag.taoma = sp;
            return View();
        }
       
    }
}