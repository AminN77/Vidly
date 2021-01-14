namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V21_populateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres ( Name) VALUES ('comedy')");
            Sql("INSERT INTO Genres ( Name) VALUES ('drama')");
            Sql("INSERT INTO Genres ( Name) VALUES ('tragic')");
            Sql("INSERT INTO Genres ( Name) VALUES ('sci-fi')");
            Sql("INSERT INTO Genres ( Name) VALUES ('animation')");
        }

        public override void Down()
        {
        }
    }
}
