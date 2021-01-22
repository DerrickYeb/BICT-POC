namespace BICT_POC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentModelValidateModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Time");
        }
    }
}
