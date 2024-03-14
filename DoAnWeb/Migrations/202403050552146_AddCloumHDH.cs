namespace DoAnWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCloumHDH : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SanPhams", "heDieuHanh", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SanPhams", "heDieuHanh");
        }
    }
}
