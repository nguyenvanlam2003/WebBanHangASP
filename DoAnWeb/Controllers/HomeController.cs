using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class HomeController : Controller
    {
        private MyDBconText db = new MyDBconText();
        public ActionResult Index(string search = "")
        {
            List<SanPham> ketqua = db.SanPhams.Where(row => row.tenSP.Contains(search)).ToList();
            ViewBag.hangSX = db.DanhMucHangs.ToList().Select(x => new SelectListItem
            {
                Value = x.maHangSX,
                Text = x.tenHangSX
            });
            ViewBag.timkiem = search;
            ViewBag.PriceRange = "Giá";
            ViewBag.RamRange = "Ram";
            ViewBag.sapXep = "Sắp xếp";
            //int NoOfrecorPerPage = 3;
            //ViewBag.Page = page;
            //ViewBag.SoluongConLai = db.SanPhams.Count() - (NoOfrecorPerPage * page);
            //// Lấy danh sách sản phẩm cho trang hiện tại
          
            //var sanPhamTrangHienTai = ketqua.Take(NoOfrecorPerPage*page).ToList();
            //if (search != "")
            //{
            //    sanPhamTrangHienTai = ketqua.ToList();
            //    ViewBag.SoluongConLai= -1;
            //}
            return View(ketqua);
        }
        public ActionResult chiTietSP(string id)
        {
            SanPham ketqua = db.SanPhams.Where(row => row.maSP == id).FirstOrDefault();
            return View(ketqua);
        }
        public ActionResult hienthitheohang(string maHangsx)
        {
            List<SanPham> ketqua = db.SanPhams.Where(row => row.maHangSX.Contains(maHangsx)).ToList();
            ViewBag.hangSX = db.DanhMucHangs.ToList().Select(x => new SelectListItem
            {
                Value = x.maHangSX,
                Text = x.tenHangSX
            });
            ViewBag.mahangsx = maHangsx;
            ViewBag.PriceRange = "Giá";
            ViewBag.RamRange = "Ram";
            ViewBag.sapXep = "Sắp xếp";
            //ViewBag.SoluongConLai = 0;
            return View("Index", ketqua);
        }
        public ActionResult locgia(string priceRange = "")
        {
            List<SanPham> ketqua = db.SanPhams.ToList();
            ViewBag.hangSX = db.DanhMucHangs.ToList().Select(x => new SelectListItem
            {
                Value = x.maHangSX,
                Text = x.tenHangSX
            });
            ViewBag.PriceRange = priceRange;
            ViewBag.RamRange = "RAM";
            ViewBag.sapXep = "Sắp xếp";
            switch (priceRange)
            {
                case "Giá":
                    break;
                case "Dưới 10 triệu":
                    ketqua = ketqua.Where(row => row.giaMoi == null ? row.giaBan < 10000000 : row.giaMoi < 10000000).ToList();
                    break;
                case "Từ 10 đến 20 triệu":
                    ketqua = ketqua.Where(row => row.giaMoi==null?row.giaBan >= 10000000 && row.giaBan <= 20000000: row.giaMoi >= 10000000 && row.giaMoi <= 20000000).ToList();
                    break;
                case "Trên 20 triệu":
                    ketqua = ketqua.Where(row => row.giaMoi == null ? row.giaBan > 20000000: row.giaMoi > 20000000).ToList();
                    break;
                default:
                    // Nếu không có lựa chọn hoặc lựa chọn không hợp lệ, giữ nguyên danh sách.
                    break;
            }
            //ViewBag.SoluongConLai = 0;
            return View("Index", ketqua);
        }
        public ActionResult locRam(string ramRange = "")
        {
            List<SanPham> ketqua = db.SanPhams.ToList();
            ViewBag.hangSX = db.DanhMucHangs.ToList().Select(x => new SelectListItem
            {
                Value = x.maHangSX,
                Text = x.tenHangSX
            });
            ViewBag.RamRange = ramRange;
            ViewBag.PriceRange = "Giá";
            ViewBag.sapXep = "Sắp xếp";
            switch (ramRange)
            {
                case "RAM":
                    break;
                case "4GB":
                    ketqua = ketqua.Where(row => row.Ram.Contains( "4")).ToList();
                    break;
                case "6GB":
                    ketqua = ketqua.Where(row => row.Ram.Contains("6")).ToList();
                    break;
                case "8GB":
                    ketqua = ketqua.Where(row => row.Ram.Contains("8")).ToList();
                    break;
                case "12GB":
                    ketqua = ketqua.Where(row => row.Ram.Contains("12")).ToList();
                    break;
                default:
                    // Nếu không có lựa chọn hoặc lựa chọn không hợp lệ, giữ nguyên danh sách.
                    break;
            }
            //ViewBag.SoluongConLai = -1;
            return View("Index", ketqua);
        }
        public ActionResult SapXepGia(string sortPrice = "")
        {
            List<SanPham> ketqua = db.SanPhams.ToList();
            ViewBag.hangSX = db.DanhMucHangs.ToList().Select(x => new SelectListItem
            {
                Value = x.maHangSX,
                Text = x.tenHangSX
            });
            ViewBag.RamRange = "Ram";
            ViewBag.PriceRange = "Giá";
            ViewBag.sapXep = sortPrice;
            switch (sortPrice)
            {
                case "Sắp xếp":
                    break;
                case "Giá thấp đến cao":
                    ketqua = ketqua.OrderBy(row => row.giaMoi==null?row.giaBan:row.giaMoi).ToList();
                    break;
                case "Giá cao đến thấp":
                    ketqua = ketqua.OrderByDescending(row => row.giaMoi == null ? row.giaBan : row.giaMoi).ToList();
                    break;
                default:
                    break;
            }
            //ViewBag.SoluongConLai = -1;
            return View("Index", ketqua);
        }
        public ActionResult trungTamBH()
        {
            return View();
        }
        public ActionResult thongtin()
        {
            return View();
        }
    }
}