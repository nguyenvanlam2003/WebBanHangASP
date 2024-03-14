namespace DoAnWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        maTK = c.String(nullable: false, maxLength: 200),
                        tenAdmin = c.String(nullable: false, maxLength: 500),
                        taiKhoan = c.String(nullable: false, maxLength: 100),
                        matKhau = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.maTK);
            
            CreateTable(
                "dbo.ChiTietGioHangs",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 200),
                        maGH = c.String(maxLength: 200),
                        maSP = c.String(maxLength: 200),
                        gia = c.Double(nullable: false),
                        soLuong = c.Int(nullable: false),
                        thanhTien = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GioHangs", t => t.maGH)
                .ForeignKey("dbo.SanPhams", t => t.maSP)
                .Index(t => t.maGH)
                .Index(t => t.maSP);
            
            CreateTable(
                "dbo.GioHangs",
                c => new
                    {
                        maGH = c.String(nullable: false, maxLength: 200),
                        maTK = c.String(maxLength: 200),
                        soLuong = c.Int(nullable: false),
                        tongTien = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.maGH)
                .ForeignKey("dbo.Users", t => t.maTK)
                .Index(t => t.maTK);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        maTK = c.String(nullable: false, maxLength: 200),
                        tenUser = c.String(nullable: false, maxLength: 500),
                        taiKhoan = c.String(nullable: false, maxLength: 100),
                        matKhau = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.maTK);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        maSP = c.String(nullable: false, maxLength: 200),
                        tenSP = c.String(maxLength: 200),
                        hinhAnh = c.String(maxLength: 1000),
                        gia = c.Double(),
                        maHangSX = c.String(maxLength: 200),
                        conNgheMH = c.String(maxLength: 200),
                        doPhanGiai = c.String(maxLength: 200),
                        manHinhRong = c.String(maxLength: 200),
                        doPhanGiaiCam = c.String(maxLength: 200),
                        CPU = c.String(maxLength: 200),
                        Rom = c.String(maxLength: 200),
                        Ram = c.String(maxLength: 200),
                        Mang = c.String(maxLength: 200),
                        soKheSim = c.String(maxLength: 200),
                        Pin = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.maSP)
                .ForeignKey("dbo.DanhMucHangs", t => t.maHangSX)
                .Index(t => t.maHangSX);
            
            CreateTable(
                "dbo.DanhMucHangs",
                c => new
                    {
                        maHangSX = c.String(nullable: false, maxLength: 200),
                        tenHangSX = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.maHangSX);
            
            CreateTable(
                "dbo.ChiTietHDs",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 200),
                        maHD = c.String(maxLength: 200),
                        maSP = c.String(maxLength: 200),
                        gia = c.Double(nullable: false),
                        soLuong = c.Int(nullable: false),
                        thanhTien = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HoaDons", t => t.maHD)
                .ForeignKey("dbo.SanPhams", t => t.maSP)
                .Index(t => t.maHD)
                .Index(t => t.maSP);
            
            CreateTable(
                "dbo.HoaDons",
                c => new
                    {
                        maHD = c.String(nullable: false, maxLength: 200),
                        maTK = c.String(maxLength: 200),
                        tenKH = c.String(nullable: false, maxLength: 200),
                        diaChi = c.String(nullable: false, maxLength: 500),
                        SDT = c.String(nullable: false, maxLength: 200),
                        ngayMua = c.DateTime(nullable: false),
                        soLuong = c.Int(nullable: false),
                        tongTien = c.Double(nullable: false),
                        trangThai = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.maHD)
                .ForeignKey("dbo.Users", t => t.maTK)
                .Index(t => t.maTK);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChiTietHDs", "maSP", "dbo.SanPhams");
            DropForeignKey("dbo.ChiTietHDs", "maHD", "dbo.HoaDons");
            DropForeignKey("dbo.HoaDons", "maTK", "dbo.Users");
            DropForeignKey("dbo.ChiTietGioHangs", "maSP", "dbo.SanPhams");
            DropForeignKey("dbo.SanPhams", "maHangSX", "dbo.DanhMucHangs");
            DropForeignKey("dbo.ChiTietGioHangs", "maGH", "dbo.GioHangs");
            DropForeignKey("dbo.GioHangs", "maTK", "dbo.Users");
            DropIndex("dbo.HoaDons", new[] { "maTK" });
            DropIndex("dbo.ChiTietHDs", new[] { "maSP" });
            DropIndex("dbo.ChiTietHDs", new[] { "maHD" });
            DropIndex("dbo.SanPhams", new[] { "maHangSX" });
            DropIndex("dbo.GioHangs", new[] { "maTK" });
            DropIndex("dbo.ChiTietGioHangs", new[] { "maSP" });
            DropIndex("dbo.ChiTietGioHangs", new[] { "maGH" });
            DropTable("dbo.HoaDons");
            DropTable("dbo.ChiTietHDs");
            DropTable("dbo.DanhMucHangs");
            DropTable("dbo.SanPhams");
            DropTable("dbo.Users");
            DropTable("dbo.GioHangs");
            DropTable("dbo.ChiTietGioHangs");
            DropTable("dbo.Admins");
        }
    }
}
