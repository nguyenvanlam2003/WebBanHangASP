using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Filter;
using DoAnWeb.Models;
namespace DoAnWeb.Areas.Admin.Controllers
{
    [CustomAuthorize]
    public class DonhangController : Controller
    {
        // GET: Admin/Donhang
        private MyDBconText db = new MyDBconText();
        public ActionResult Index(int page = 1)
        {
            List<HoaDon> ketqua = db.HoaDons.ToList();
            int NoOfrecorPerPage = 4;
            int NoOfPages = (int)Math.Ceiling((double)ketqua.Count / NoOfrecorPerPage);
            int NoOfRecorToSkip = (page - 1) * NoOfrecorPerPage;

            // Lấy danh sách sản phẩm cho trang hiện tại
            var HDTrangHienTai = ketqua.Skip(NoOfRecorToSkip).Take(NoOfrecorPerPage).ToList();

            // Gửi dữ liệu tới view
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            return View(HDTrangHienTai);
        }
        public ActionResult chiTietDonHang(string id)
        {
            ViewBag.maHD = id;
            return View();
        }
        [HttpPost]
        public ActionResult capNhatHD(string id ,string trangthai)
        {
            HoaDon hd = db.HoaDons.Where(row => row.maHD == id).FirstOrDefault();
            if (hd != null)
            {

                hd.trangThai = trangthai;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "Unsuccess", Success = false });

        }
    }
}