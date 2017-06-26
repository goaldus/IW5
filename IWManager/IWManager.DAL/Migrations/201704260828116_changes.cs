using System.Data.Entity.Migrations;

namespace IWManager.DAL.Migrations
{
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "Notes", c => c.String());
            AlterColumn("dbo.Students", "Birthdate", c => c.DateTime(nullable: false));
            DropColumn("dbo.GeneralRatings", "Notes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GeneralRatings", "Notes", c => c.String());
            AlterColumn("dbo.Students", "Birthdate", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Ratings", "Notes");
        }
    }
}
