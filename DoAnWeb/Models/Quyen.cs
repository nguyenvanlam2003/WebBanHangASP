using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models
{
    [Table("Quyens")]
    public class Quyen
    {
        [Key]
        [StringLength(200)]
        public string maQuyen { get; set; }
        [Required]
        [StringLength(500)]
        public string tenQuyen { get; set; }
    }
}