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
    public class TaiKhoanADController : Controller
    {
        // GET: Admin/TaiKhoanAD
        private MyDBconText db = new MyDBconText();
        public ActionResult Index(string search = "", int page = 1)
        {
            List< DoAnWeb.Models.Admin> ketqua = db.Admins.Where(row => row.tenAdmin.Contains(search)).ToList();
            int NoOfrecorPerPage = 4;
            int NoOfPages = (int)Math.Ceiling((double)ketqua.Count / NoOfrecorPerPage);
            int NoOfRecorToSkip = (page - 1) * NoOfrecorPerPage;
            var ADTrangHienTai = ketqua.Skip(NoOfRecorToSkip).Take(NoOfrecorPerPage).ToList();
            ViewBag.timkiem = search;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;

            return View(ADTrangHienTai);
        }
        public ActionResult themTaiKhoan()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult themTaiKhoan(DoAnWeb.Models.Admin model)
        {
            if (ModelState.IsValid)
            {
                TaoMaTuDong maHSX = new TaoMaTuDong();
                model.maTK = maHSX.sinhMa("AD_");
                db.Admins.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(string id)
        {
            DoAnWeb.Models.Admin sp = db.Admins.Where(row => row.maTK == id).FirstOrDefault();
            return View(sp);
        }
        [HttpPost]
        public ActionResult Edit(DoAnWeb.Models.Admin model)
        {
            DoAnWeb.Models.Admin ad = db.Admins.Where(row => row.maTK == model.maTK).FirstOrDefault();
            ad.tenAdmin = model.tenAdmin;
            ad.SDT = model.SDT;
            ad.email = model.email;
            ad.taiKhoan = model.taiKhoan;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var item = db.Admins.Find(id);
            if (item != null)
            {
                db.Admins.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
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
                        var obj = db.Admins.Find(Convert.ToString(item));
                        db.Admins.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}