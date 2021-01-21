namespace BICT_POC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newModelsAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "CourseId", "dbo.Courses");
            DropIndex("dbo.Students", new[] { "CourseId" });
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Day = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.StudentId);
            
            AddColumn("dbo.Courses", "Student_Id", c => c.Int());
            AddColumn("dbo.Students", "DateEnrolled", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Courses", "Title", c => c.String(nullable: false));
            CreateIndex("dbo.Courses", "Student_Id");
            AddForeignKey("dbo.Courses", "Student_Id", "dbo.Students", "StudentId");
            DropColumn("dbo.Students", "CourseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "CourseId", c => c.Int());
            DropForeignKey("dbo.Courses", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Enrollments", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses");
            DropIndex("dbo.Enrollments", new[] { "StudentId" });
            DropIndex("dbo.Enrollments", new[] { "CourseId" });
            DropIndex("dbo.Courses", new[] { "Student_Id" });
            AlterColumn("dbo.Courses", "Title", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Students", "DateEnrolled");
            DropColumn("dbo.Courses", "Student_Id");
            DropTable("dbo.Enrollments");
            CreateIndex("dbo.Students", "CourseId");
            AddForeignKey("dbo.Students", "CourseId", "dbo.Courses", "CourseId");
        }
    }
}
