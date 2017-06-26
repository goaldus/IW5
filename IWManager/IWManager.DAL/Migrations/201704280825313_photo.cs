using System.Data.Entity.Migrations;

namespace IWManager.DAL.Migrations
{
    public partial class photo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Photo", c => c.Binary());
        }
    }
}
