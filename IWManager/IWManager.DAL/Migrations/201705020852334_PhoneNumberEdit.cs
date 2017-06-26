using System.Data.Entity.Migrations;

namespace IWManager.DAL.Migrations
{
    public partial class PhoneNumberEdit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "PhoneNumber", c => c.String(maxLength: 9));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "PhoneNumber", c => c.String(maxLength: 12));
        }
    }
}
