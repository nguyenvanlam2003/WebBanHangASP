using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models

{
    [Table("GioHangs")]
    public class GioHang
    {

        [Key]
        [StringLength(200)]
        public string maGH { get; set; }
        [StringLength(200)]
        public string maTK { get; set; }
        public int? soLuong { get; set; }
        public double? tongTien { get; set; }
        [ForeignKey("maTK")]
        public virtual User User { get; set; }
    }
}