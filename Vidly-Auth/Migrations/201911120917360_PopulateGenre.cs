namespace Vidly_Auth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres([GenreName]) VALUES('Horror')");
            Sql("INSERT INTO Genres([GenreName]) VALUES('Comedy')");
            Sql("INSERT INTO Genres([GenreName]) VALUES('Family')");
            Sql("INSERT INTO Genres([GenreName]) VALUES('Action')");
            Sql("INSERT INTO Genres([GenreName]) VALUES('Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
