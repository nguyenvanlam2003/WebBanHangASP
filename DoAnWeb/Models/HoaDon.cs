using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models

{
    [Table("HoaDons")]
    public class HoaDon
    {
        [Key]
        [StringLength(200)]
        public string maHD { get; set; }
        [StringLength(200)]
        public string maTK { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(200)]
        public string tenKH { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(500)]
        public string diaChi { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(200)]
        public string SDT { get; set; }
        public DateTime? ngayMua { get; set; }
        public int? soLuong { get; set; }
        public double? tongTien { get; set; }
        [StringLength(200)]
        public string trangThai { get; set; }
        [StringLength(200)]
        public string hinhThucThanhToan { get; set; }
        [StringLength(1000)]
        public string ghichu { get; set; }
        [ForeignKey("maTK")]
        public virtual User User { get; set; }
    }
}