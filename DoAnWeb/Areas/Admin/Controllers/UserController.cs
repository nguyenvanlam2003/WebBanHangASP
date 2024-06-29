using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using DoAnWeb.Filter;
namespace DoAnWeb.Areas.Admin.Controllers
{
    [CustomAuthorize]
    public class UserController : Controller
    {
        // GET: Admin/User
        private MyDBconText db = new MyDBconText();
        public ActionResult Index(string search = "", int page = 1)
        {
            List<User> ketqua = db.Users.Where(row => row.tenUser.Contains(search)).ToList();
            int NoOfrecorPerPage = 4;
            int NoOfPages = (int)Math.Ceiling((double)ketqua.Count / NoOfrecorPerPage);
            int NoOfRecorToSkip = (page - 1) * NoOfrecorPerPage;
            var usTrangHienTai = ketqua.Skip(NoOfRecorToSkip).Take(NoOfrecorPerPage).ToList();

            // Gửi dữ liệu tới view
            ViewBag.timkiem = search;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            return View(usTrangHienTai);
        }
        public ActionResult themTaiKhoan()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult themTaiKhoan(User model)
        {
            if (ModelState.IsValid)
            {
                TaoMaTuDong maHSX = new TaoMaTuDong();
                model.maTK = maHSX.sinhMa("Us_");
                db.Users.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(string id)
        {
            User sp = db.Users.Where(row => row.maTK == id).FirstOrDefault();
            return View(sp);
        }
        [HttpPost]
        public ActionResult Edit(User model)
        {
            User ad = db.Users.Where(row => row.maTK == model.maTK).FirstOrDefault();
            ad.tenUser = model.tenUser;
            ad.SDT = model.SDT;
            ad.email = model.email;
            ad.taiKhoan = model.taiKhoan;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try {
                var item = db.Users.Find(id);
                if (item != null)
                {
                    db.Users.Remove(item);
                    db.SaveChanges();
                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "Tài khoản này vẫn đang sử dụng không được phép xóa." });
            }
            catch
            {
                return Json(new { success = false , message = "Tài khoản này vẫn đang sử dụng không được phép xóa." });
            }
            
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.Users.Find(Convert.ToString(item));
                        db.Users.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}