using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Filter;
using DoAnWeb.Models;
namespace DoAnWeb.Areas.Admin.Controllers
{
    [CustomAuthorize]
    public class ThongKeController : Controller
    {
        // GET: Admin/ThongKe
        private MyDBconText db = new MyDBconText();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetStatistical(string fromDate, string toDate)
        {
            var query = from hd in db.HoaDons
                        join cthd in db.ChiTietHDs
                        on hd.maHD equals cthd.maHD
                        join sp in db.SanPhams
                        on cthd.maSP equals sp.maSP
                        select new
                        {
                            CreatedDate = hd.ngayMua,
                            Quantity = hd.soLuong,
                            Price = cthd.gia,
                            OriginalPrice = sp.giaNhap
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreatedDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreatedDate < endDate);
            }

            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate)).Select(x => new
            {
                Date = x.Key.Value,
                TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
                TotalSell = x.Sum(y => y.Quantity * y.Price),
            }).Select(x => new
            {
                Date = x.Date,
                DoanhThu = x.TotalSell,
                LoiNhuan = x.TotalSell - x.TotalBuy
            });
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }

    }
}