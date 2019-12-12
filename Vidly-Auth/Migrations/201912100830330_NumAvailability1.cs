namespace Vidly_Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumAvailability1 : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Movies SET NumAvailability = NumInStock");           
        }
        
        public override void Down()
        {
        }
    }
}
