namespace DoAnWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addsdt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "SDT", c => c.String(maxLength: 20));
            AddColumn("dbo.Admins", "email", c => c.String(maxLength: 200));
            AddColumn("dbo.Users", "SDT", c => c.String(maxLength: 20));
            AddColumn("dbo.Users", "email", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "email");
            DropColumn("dbo.Users", "SDT");
            DropColumn("dbo.Admins", "email");
            DropColumn("dbo.Admins", "SDT");
        }
    }
}
