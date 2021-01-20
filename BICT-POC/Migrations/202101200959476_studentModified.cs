namespace BICT_POC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentModified : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeTables", "StudentId", "dbo.Students");
            DropIndex("dbo.TimeTables", new[] { "StudentId" });
            AddColumn("dbo.Students", "TimeTableId", c => c.Int(nullable: false));
            DropColumn("dbo.TimeTables", "StudentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeTables", "StudentId", c => c.Int(nullable: false));
            DropColumn("dbo.Students", "TimeTableId");
            CreateIndex("dbo.TimeTables", "StudentId");
            AddForeignKey("dbo.TimeTables", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
        }
    }
}
