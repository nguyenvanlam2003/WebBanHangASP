using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [StringLength(200)]
        public string maTK { get; set; }
        [Required]
        [StringLength(500)]
        public string tenUser { get; set; }
        [StringLength(20)]
        public string SDT { get; set; }
        [StringLength(200)]
        public string email { get; set; }
        [Required]
        [StringLength(100)]
        public string taiKhoan { get; set; }
        [Required]
        [StringLength(100)]
        public string matKhau { get; set; }
    }
}