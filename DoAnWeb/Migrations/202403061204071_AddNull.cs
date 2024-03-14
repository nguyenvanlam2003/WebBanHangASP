namespace DoAnWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChiTietGioHangs", "gia", c => c.Double());
            AlterColumn("dbo.ChiTietGioHangs", "soLuong", c => c.Int());
            AlterColumn("dbo.ChiTietGioHangs", "thanhTien", c => c.Double());
            AlterColumn("dbo.GioHangs", "soLuong", c => c.Int());
            AlterColumn("dbo.GioHangs", "tongTien", c => c.Double());
            AlterColumn("dbo.ChiTietHDs", "gia", c => c.Double());
            AlterColumn("dbo.ChiTietHDs", "soLuong", c => c.Int());
            AlterColumn("dbo.ChiTietHDs", "thanhTien", c => c.Double());
            AlterColumn("dbo.HoaDons", "ngayMua", c => c.DateTime());
            AlterColumn("dbo.HoaDons", "soLuong", c => c.Int());
            AlterColumn("dbo.HoaDons", "tongTien", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HoaDons", "tongTien", c => c.Double(nullable: false));
            AlterColumn("dbo.HoaDons", "soLuong", c => c.Int(nullable: false));
            AlterColumn("dbo.HoaDons", "ngayMua", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ChiTietHDs", "thanhTien", c => c.Double(nullable: false));
            AlterColumn("dbo.ChiTietHDs", "soLuong", c => c.Int(nullable: false));
            AlterColumn("dbo.ChiTietHDs", "gia", c => c.Double(nullable: false));
            AlterColumn("dbo.GioHangs", "tongTien", c => c.Double(nullable: false));
            AlterColumn("dbo.GioHangs", "soLuong", c => c.Int(nullable: false));
            AlterColumn("dbo.ChiTietGioHangs", "thanhTien", c => c.Double(nullable: false));
            AlterColumn("dbo.ChiTietGioHangs", "soLuong", c => c.Int(nullable: false));
            AlterColumn("dbo.ChiTietGioHangs", "gia", c => c.Double(nullable: false));
        }
    }
}
