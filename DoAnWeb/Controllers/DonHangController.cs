using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult Index(int page = 1)
        {
            string userId = Request.Cookies["UserSession"]["UserId"];
            //ViewBag.userID = userId;
            //return View();

            List<HoaDon> ketqua = db.HoaDons.Where(r => r.maTK == userId).OrderBy(row => DbFunctions.DiffSeconds(row.ngayMua, DateTime.Now)).ToList();
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
        public ActionResult capNhatTrangThai(string id , string trangThai)
        {
            HoaDon hd = db.HoaDons.Where(row => row.maHD == id).FirstOrDefault();
            hd.trangThai = trangThai;
            db.SaveChanges();
            return RedirectToAction("Index","DonHang");
        }
    }
}