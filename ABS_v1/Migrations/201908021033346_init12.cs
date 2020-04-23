namespace ABS_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventModels",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventDate = c.DateTime(precision: 0),
                        EventName = c.String(unicode: false),
                        EventDescription = c.String(unicode: false),
                        EventLocation = c.String(unicode: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.NewsModels",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        NewsDate = c.DateTime(precision: 0),
                        NewsTitle = c.String(unicode: false),
                        NewsDescription = c.String(unicode: false),
                        Location = c.String(unicode: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.NewsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NewsModels");
            DropTable("dbo.EventModels");
        }
    }
}
