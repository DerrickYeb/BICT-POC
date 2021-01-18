namespace BICT_POC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validationMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "CourseId", "dbo.Courses");
            DropIndex("dbo.Students", new[] { "CourseId" });
            RenameColumn(table: "dbo.TimeTables", name: "CourseId", newName: "Course_Id");
            RenameIndex(table: "dbo.TimeTables", name: "IX_CourseId", newName: "IX_Course_Id");
            AlterColumn("dbo.Courses", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Students", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Guidian", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "GuideanContact", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Class", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "AcademicYear", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Address", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Students", "CourseId", c => c.Int());
            AlterColumn("dbo.TimeTables", "Time", c => c.String(nullable: false));
            AlterColumn("dbo.TimeTables", "Day", c => c.String(nullable: false));
            CreateIndex("dbo.Students", "CourseId");
            AddForeignKey("dbo.Students", "CourseId", "dbo.Courses", "CourseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "CourseId", "dbo.Courses");
            DropIndex("dbo.Students", new[] { "CourseId" });
            AlterColumn("dbo.TimeTables", "Day", c => c.String());
            AlterColumn("dbo.TimeTables", "Time", c => c.String());
            AlterColumn("dbo.Students", "CourseId", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "Address", c => c.String());
            AlterColumn("dbo.Students", "AcademicYear", c => c.String());
            AlterColumn("dbo.Students", "Gender", c => c.String());
            AlterColumn("dbo.Students", "Class", c => c.String());
            AlterColumn("dbo.Students", "GuideanContact", c => c.String());
            AlterColumn("dbo.Students", "Guidian", c => c.String());
            AlterColumn("dbo.Students", "LastName", c => c.String());
            AlterColumn("dbo.Students", "FirstName", c => c.String());
            AlterColumn("dbo.Courses", "Title", c => c.String(maxLength: 50));
            RenameIndex(table: "dbo.TimeTables", name: "IX_Course_Id", newName: "IX_CourseId");
            RenameColumn(table: "dbo.TimeTables", name: "Course_Id", newName: "CourseId");
            CreateIndex("dbo.Students", "CourseId");
            AddForeignKey("dbo.Students", "CourseId", "dbo.Courses", "CourseId", cascadeDelete: true);
        }
    }
}
