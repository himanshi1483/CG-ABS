namespace ABS_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init23 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EventModels", "Image", c => c.String(unicode: false));
            AlterColumn("dbo.NewsModels", "Image", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NewsModels", "Image", c => c.Binary());
            AlterColumn("dbo.EventModels", "Image", c => c.Binary());
        }
    }
}
