namespace DoAnWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGiaBan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SanPhams", "giaBan", c => c.Double());
            AddColumn("dbo.SanPhams", "giaNhap", c => c.Double());
            DropColumn("dbo.SanPhams", "gia");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SanPhams", "gia", c => c.Double());
            DropColumn("dbo.SanPhams", "giaNhap");
            DropColumn("dbo.SanPhams", "giaBan");
        }
    }
}
