using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Filter;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    [ClientAuthorize]
    public class DonHangController : Controller
    {
        private MyDBconText db = new MyDBconText();
        // GET: DonHang
        public ActionResult Index()
        {
            string userId = Request.Cookies["UserSession"]["UserId"];
            ViewBag.userID = userId;
            return View();
        }
        public ActionResult capNhatTrangThai(string id , string trangThai)
        {
            HoaDon hd = db.HoaDons.Where(row => row.maHD == id).FirstOrDefault();
            hd.trangThai = trangThai;
            db.SaveChanges();
            return RedirectToAction("Index","DonHang");
        }
    }
}