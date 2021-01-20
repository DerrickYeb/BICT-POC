namespace BICT_POC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timeTableValidate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeTables", "StudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.TimeTables", "StudentId");
            AddForeignKey("dbo.TimeTables", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeTables", "StudentId", "dbo.Students");
            DropIndex("dbo.TimeTables", new[] { "StudentId" });
            DropColumn("dbo.TimeTables", "StudentId");
        }
    }
}
