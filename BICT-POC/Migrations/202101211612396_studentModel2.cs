namespace BICT_POC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentModel2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Courses", "Day");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Day", c => c.Int(nullable: false));
        }
    }
}
