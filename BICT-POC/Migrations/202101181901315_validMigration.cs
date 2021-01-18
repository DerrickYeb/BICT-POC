namespace BICT_POC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TimeTables", name: "Course_Id", newName: "CourseId");
            RenameIndex(table: "dbo.TimeTables", name: "IX_Course_Id", newName: "IX_CourseId");
            AlterColumn("dbo.TimeTables", "Day", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeTables", "Day", c => c.String(nullable: false));
            RenameIndex(table: "dbo.TimeTables", name: "IX_CourseId", newName: "IX_Course_Id");
            RenameColumn(table: "dbo.TimeTables", name: "CourseId", newName: "Course_Id");
        }
    }
}
