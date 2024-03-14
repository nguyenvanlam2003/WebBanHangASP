using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models
{
    [Table("DanhMucHangs")]
    public class DanhMucHang
    {
        [Key]
        [StringLength(200)]
        public string maHangSX { get; set; }
        [Required]
        [StringLength(100)]
        public string tenHangSX { get; set; }
    }
}