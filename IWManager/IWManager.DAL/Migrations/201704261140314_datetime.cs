using System.Data.Entity.Migrations;

namespace IWManager.DAL.Migrations
{
    public partial class datetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Birthdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Birthdate", c => c.DateTime(nullable: false));
        }
    }
}
