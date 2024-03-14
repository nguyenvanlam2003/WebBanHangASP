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
    public class HangSXController : Controller
    {
        // GET: Admin/HangSX
        private MyDBconText db = new MyDBconText();
        public ActionResult Index(string search = "", int page = 1)
        {
            
            List<DanhMucHang> ketqua = db.DanhMucHangs.Where(row => row.tenHangSX.Contains(search)).ToList();
            int NoOfrecorPerPage = 4;
            int NoOfPages = (int)Math.Ceiling((double)ketqua.Count / NoOfrecorPerPage);
            int NoOfRecorToSkip = (page - 1) * NoOfrecorPerPage;
            var hangSXTrangHienTai = ketqua.Skip(NoOfRecorToSkip).Take(NoOfrecorPerPage).ToList();
            ViewBag.timkiem = search;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            return View(hangSXTrangHienTai);
        }
        public ActionResult themHangSX()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult themHangSX(DanhMucHang model)
        {
            if (ModelState.IsValid)
            {
                TaoMaTuDong maHSX = new TaoMaTuDong();
                model.maHangSX = maHSX.sinhMa("HDT_");
                db.DanhMucHangs.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(string id)
        {
            DanhMucHang ds = db.DanhMucHangs.Where(row => row.maHangSX ==id).FirstOrDefault();
            return View(ds);
        }
        [HttpPost]
        public ActionResult Edit(DanhMucHang model)
        {
           DanhMucHang ds = db.DanhMucHangs.Where(row => row.maHangSX == model.maHangSX).FirstOrDefault();
            ds.tenHangSX = model.tenHangSX;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var item = db.DanhMucHangs.Find(id);
            if (item != null)
            {
                db.DanhMucHangs.Remove(item);
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
                        var obj = db.DanhMucHangs.Find(Convert.ToString(item));
                        db.DanhMucHangs.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}