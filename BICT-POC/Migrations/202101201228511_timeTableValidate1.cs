namespace BICT_POC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timeTableValidate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "TimeTableId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "TimeTableId", c => c.Int(nullable: false));
        }
    }
}
