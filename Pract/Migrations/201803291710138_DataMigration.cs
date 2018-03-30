namespace Pract.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "AdminName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AdminName", c => c.String());
        }
    }
}
