namespace IWManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adfsdf : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "AcademicYear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "AcademicYear", c => c.String(nullable: false));
        }
    }
}
