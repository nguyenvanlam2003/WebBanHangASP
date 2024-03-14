namespace DoAnWeb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DoAnWeb.Models.MyDBconText>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DoAnWeb.Models.MyDBconText";
        }

        protected override void Seed(DoAnWeb.Models.MyDBconText context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
