namespace DoAnWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhuongthucThanhtoan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HoaDons", "hinhThucThanhToan", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HoaDons", "hinhThucThanhToan");
        }
    }
}
