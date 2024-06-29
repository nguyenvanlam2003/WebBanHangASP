using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using DoAnWeb.Filter;
namespace DoAnWeb.Areas.Admin.Controllers
{
    [CustomAuthorize]
    public class SanPhamController : Controller
    {
        // GET: Admin/SanPham
        private MyDBconText db = new MyDBconText();
        public ActionResult Index(string search = "", int page=1 )
        {
            var ketqua = db.SanPhams.Where(row => row.tenSP.Contains(search)).ToList();
            int NoOfrecorPerPage =4;
            int NoOfPages = (int)Math.Ceiling((double)ketqua.Count / NoOfrecorPerPage);
            int NoOfRecorToSkip = (page - 1) * NoOfrecorPerPage;

            // Lấy danh sách sản phẩm cho trang hiện tại
            var sanPhamTrangHienTai = ketqua.Skip(NoOfRecorToSkip).Take(NoOfrecorPerPage).ToList();

            // Gửi dữ liệu tới view
            ViewBag.timkiem = search;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;

            return View(sanPhamTrangHienTai);
        }
        public ActionResult ThemSP()
        {
            ViewBag.hangSX = new SelectList(db.DanhMucHangs.ToList(), "maHangSX", "tenHangSX");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // ngăn chặn tấn công Cross-Site Request Forgery (CSRF)
        public ActionResult ThemSP(SanPham sp, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)// kiểm tra dữ liệu gủi lên có hợp lệ không
            {
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    var filePath = Path.Combine(Server.MapPath("~/Assets/Img"), Path.GetFileName(HinhAnh.FileName));
                    HinhAnh.SaveAs(filePath);
                    sp.hinhAnh = "~/Assets/Img/" + HinhAnh.FileName;
                }
                else
                {
                    sp.hinhAnh = null;
                }
                TaoMaTuDong masp = new TaoMaTuDong();
                sp.maSP = masp.sinhMa("SP_");
                db.SanPhams.Add(sp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sp);
        }
        public ActionResult Edit(string id)
        {
            ViewBag.hangSX = new SelectList(db.DanhMucHangs.ToList(), "maHangSX", "tenHangSX");
            SanPham sp = db.SanPhams.Where(row => row.maSP == id).FirstOrDefault();
            return View(sp);
        }
        [HttpPost]
        public ActionResult Edit(SanPham model, HttpPostedFileBase HinhAnh)
        {
            SanPham sp = db.SanPhams.Where(row => row.maSP  == model.maSP).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (HinhAnh != null)
                {
                    // Kiểm tra và xóa ảnh cũ
                    if (!string.IsNullOrEmpty(sp.hinhAnh ))
                    {
                        // Lấy tên tệp tin từ đường dẫn
                        string tenanh = Path.GetFileName(sp.hinhAnh );

                        // Đường dẫn đầy đủ của tệp tin ảnh cũ
                        string oldFilePath = Path.Combine(Server.MapPath("~/Assets/Img/"), tenanh);

                        // Kiểm tra xem tệp tin tồn tại trước khi xóa
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            // Xóa tệp tin từ thư mục
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                }
                // Lưu ảnh mới
                if (HinhAnh != null)
                {
                    string fileName = Path.GetFileName(HinhAnh.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Assets/Img"), fileName);
                    HinhAnh.SaveAs(filePath);

                    // Cập nhật đường dẫn ảnh mới trong model

                    model.hinhAnh = "~/Assets/Img/" + fileName;
                    sp.hinhAnh = model.hinhAnh;
                }
                sp.tenSP = model.tenSP;
                sp.giaBan = model.giaBan;
                sp.maHangSX = model.maHangSX;
                sp.manHinhRong = model.manHinhRong;
                sp.conNgheMH = model.conNgheMH;
                sp.Ram = model.Ram;
                sp.CPU = model.CPU;
                sp.heDieuHanh = model.heDieuHanh;
                sp.doPhanGiai = model.doPhanGiai;
                sp.Pin = model.Pin;
                sp.Rom = model.Rom;
                sp.doPhanGiaiCam = model.doPhanGiaiCam;
                sp.Mang = model.Mang;
                sp.soKheSim = model.soKheSim;
                sp.giaMoi = model.giaMoi;
                sp.giaNhap = model.giaNhap;
                SanPham sp1 = sp;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try {
                var item = db.SanPhams.Find(id);
                if (item != null)
                {
                    if (!string.IsNullOrEmpty(item.hinhAnh))
                    {
                        // Lấy tên tệp tin từ đường dẫn
                        string tenanh = Path.GetFileName(item.hinhAnh);

                        // Đường dẫn đầy đủ của tệp tin ảnh cũ
                        string oldFilePath = Path.Combine(Server.MapPath("~/Assets/Img/"), tenanh);

                        // Kiểm tra xem tệp tin tồn tại trước khi xóa
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            // Xóa tệp tin từ thư mục
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    db.SanPhams.Remove(item);
                    db.SaveChanges();
                    return Json(new { success = true });
                }

                return Json(new { success = false, message = "Sản phẩm này vẫn đang sử dụng không được phép xóa." });
            }
            catch 
            {
                return Json(new { success = false, message = "Sản phẩm này vẫn đang sử dụng không được phép xóa." });
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
                        var obj = db.SanPhams.Find(Convert.ToString(item));
                        if (!string.IsNullOrEmpty(obj.hinhAnh))
                        {
                            // Lấy tên tệp tin từ đường dẫn
                            string tenanh = Path.GetFileName(obj.hinhAnh);

                            // Đường dẫn đầy đủ của tệp tin ảnh cũ
                            string oldFilePath = Path.Combine(Server.MapPath("~/Assets/Img/"), tenanh);

                            // Kiểm tra xem tệp tin tồn tại trước khi xóa
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                // Xóa tệp tin từ thư mục
                                System.IO.File.Delete(oldFilePath);
                            }
                        }
                        db.SanPhams.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}