namespace ABS_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormDatas", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            AddColumn("dbo.FormDatas", "Image", c => c.Byte(nullable: false));
        }
    }
}
