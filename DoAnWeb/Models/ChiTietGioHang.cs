using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models

{
    [Table("ChiTietGioHangs")]
    public class ChiTietGioHang
    {
        [Key]
        [StringLength(200)]
        public string ID { get; set; }
        [StringLength(200)]
        public string maGH { get; set; }
        [StringLength(200)]
        public string maSP { get; set; }
        public double? gia { get; set; }
        public int? soLuong { get; set; }
        public double? thanhTien { get; set; }
        [ForeignKey("maGH")]
        public virtual GioHang GioHang { get; set; }
        [ForeignKey("maSP")]
        public virtual SanPham SanPham { get; set; }
    }
}