namespace DoAnWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewPropertyToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HoaDons", "ghichu", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HoaDons", "ghichu");
        }
    }
}
