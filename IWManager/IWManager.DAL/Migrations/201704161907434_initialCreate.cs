using System.Data.Entity.Migrations;

namespace IWManager.DAL.Migrations
{
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CourseName = c.String(nullable: false),
                        Abbreviation = c.String(nullable: false, maxLength: 3),
                        AcademicYear = c.Int(nullable: false),
                        Credits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GeneralRatings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        MaxPoints = c.Int(nullable: false),
                        Notes = c.String(),
                        Course_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Points = c.Int(nullable: false),
                        GeneralRating_Id = c.Guid(nullable: false),
                        Student_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GeneralRatings", t => t.GeneralRating_Id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: true)
                .Index(t => t.GeneralRating_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Login = c.String(nullable: false, maxLength: 8),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Birthdate = c.Time(nullable: false, precision: 7),
                        Photo = c.Binary(),
                        Sex = c.Int(nullable: false),
                        Nationality = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(maxLength: 12),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        Student_Id = c.Guid(nullable: false),
                        Course_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_Id, t.Course_Id })
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Student_Id)
                .Index(t => t.Course_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.StudentCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.StudentCourses", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Ratings", "GeneralRating_Id", "dbo.GeneralRatings");
            DropForeignKey("dbo.GeneralRatings", "Course_Id", "dbo.Courses");
            DropIndex("dbo.StudentCourses", new[] { "Course_Id" });
            DropIndex("dbo.StudentCourses", new[] { "Student_Id" });
            DropIndex("dbo.Ratings", new[] { "Student_Id" });
            DropIndex("dbo.Ratings", new[] { "GeneralRating_Id" });
            DropIndex("dbo.GeneralRatings", new[] { "Course_Id" });
            DropTable("dbo.StudentCourses");
            DropTable("dbo.Students");
            DropTable("dbo.Ratings");
            DropTable("dbo.GeneralRatings");
            DropTable("dbo.Courses");
        }
    }
}
