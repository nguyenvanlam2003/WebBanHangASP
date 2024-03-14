using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private MyDBconText db = new MyDBconText();
        // GET: Admin/Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(string username, string password)
        {
            DoAnWeb.Models.Admin ad = db.Admins.Where(row => row.taiKhoan == username && row.matKhau == password).FirstOrDefault();

            if (ad != null)
            {
                // Lưu thông tin đăng nhập vào Session
                Session["AdId"] = ad.maTK; // Thay UserId bằng thuộc tính tương ứng trong đối tượng TaiKhoanADmin
                Session["TaiKhoan"] = ad.taiKhoan;
                Session["Username"] = ad.tenAdmin;
                // Chuyển hướng đến trang Index sau khi đăng nhập thành công
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            }
            else
            {
                return View();
            }
        }
        public ActionResult logout()
        {
            // Xóa thông tin đăng nhập từ Session
            Session["AdId"] = null;
            Session["Username"] = null;
            Session["TaiKhoan"] = null;

            // Chuyển hướng về trang đăng nhập
            return RedirectToAction("Login", "Account", new { area = "Admin" });
        }
    }
}