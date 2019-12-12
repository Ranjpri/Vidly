namespace Vidly_Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumAvailability : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumAvailability", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "NumAvailability");
        }
    }
}
