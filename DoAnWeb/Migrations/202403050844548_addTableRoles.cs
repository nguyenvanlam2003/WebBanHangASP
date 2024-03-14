namespace DoAnWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        maQuyen = c.String(nullable: false, maxLength: 200),
                        tenQuyen = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.maQuyen);
            
            AddColumn("dbo.Admins", "maQuyen", c => c.String(maxLength: 200));
            CreateIndex("dbo.Admins", "maQuyen");
            AddForeignKey("dbo.Admins", "maQuyen", "dbo.Roles", "maQuyen");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Admins", "maQuyen", "dbo.Roles");
            DropIndex("dbo.Admins", new[] { "maQuyen" });
            DropColumn("dbo.Admins", "maQuyen");
            DropTable("dbo.Roles");
        }
    }
}
