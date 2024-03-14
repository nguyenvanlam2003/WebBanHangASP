using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DoAnWeb.Models
{
    public class MyDBconText:DbContext
    {
        public MyDBconText() : base("name=strConnect") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHD> ChiTietHDs { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public DbSet<DanhMucHang> DanhMucHangs { get; set; }
        public IEnumerable<object> Roles { get; internal set; }
    }
}