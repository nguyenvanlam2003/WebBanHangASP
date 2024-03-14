using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models
{
    [Table("SanPhams")]
    public class SanPham
    {
        [Key]
        [StringLength(200)]
        public string maSP { get; set; }
        [StringLength(200)]
        public string tenSP { get; set; }
        [StringLength(1000)]
        public string hinhAnh { get; set; }
        public double? giaBan { get; set; }
        public double? giaMoi { get; set; }
        public double? giaNhap { get; set; }
        [StringLength(200)]
        public string maHangSX { get; set; }
        [StringLength(200)]
        public string conNgheMH { get; set; }
        [StringLength(200)]
        public string doPhanGiai { get; set; }
        [StringLength(200)]
        public string manHinhRong { get; set; }
        [StringLength(200)]
        public string doPhanGiaiCam { get; set; }
        [StringLength(200)]
        public string CPU { get; set; }
        [StringLength(200)]
        public string heDieuHanh { get; set; }
        [StringLength(200)]
        public string Rom { get; set; }
        [StringLength(200)]
        public string Ram { get; set; }
        [StringLength(200)]
        public string Mang { get; set; }
        [StringLength(200)]
        public string soKheSim { get; set; }
        [StringLength(200)]
        public string Pin { get; set; }
        [ForeignKey("maHangSX")]
        public virtual DanhMucHang DanhMucHang { get; set; }

    }
}