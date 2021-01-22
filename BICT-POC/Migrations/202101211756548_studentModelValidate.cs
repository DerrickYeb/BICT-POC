namespace BICT_POC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentModelValidate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Day", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Day");
        }
    }
}
