using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private MyDBconText db = new MyDBconText();
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(string username, string password)
        {
             User nd = db.Users.SingleOrDefault(row => row.taiKhoan == username && row.matKhau == password);

            if (nd != null)
            {
                // Tạo một cookie để đánh dấu đăng nhập thành công
                HttpCookie userCookie = new HttpCookie("UserSession");
                userCookie["Username"] = username;
                userCookie["UserId"] = nd.maTK.ToString();
                userCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(userCookie);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult logout()
        {
            if (Request.Cookies["UserSession"] != null)
            {
                var userCookie = new HttpCookie("UserSession");
                userCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(userCookie);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register(User kh)
        {
            TaoMaTuDong maTK = new TaoMaTuDong();
            kh.maTK = maTK.sinhMa("US_");
            db.Users.Add(kh);
            db.SaveChanges();
            return RedirectToAction("login", "Account");
        }
        public ActionResult Edit(string id)
        {
             User KH = db.Users.Where(row => row.maTK == id).FirstOrDefault();
            return View(KH);
        }
        [HttpPost]
        public ActionResult Edit(User model)
        {
            User khachhang = db.Users.Where(row => row.maTK == model.maTK).FirstOrDefault();

            khachhang.tenUser = model.tenUser;
            khachhang.email = model.email;
            khachhang.SDT = model.SDT;
            khachhang.taiKhoan = model.taiKhoan;
            if (model.matKhau != null)
            {
                khachhang.matKhau = model.matKhau;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
    }
}