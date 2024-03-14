namespace DoAnWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGiaMoi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SanPhams", "giaMoi", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SanPhams", "giaMoi");
        }
    }
}
