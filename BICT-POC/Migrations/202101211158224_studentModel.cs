namespace BICT_POC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "CourseId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "CourseId");
        }
    }
}
