namespace ABS_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormDatas", "ImageName", c => c.String(unicode: false));
            DropColumn("dbo.FormDatas", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FormDatas", "Image", c => c.Binary());
            DropColumn("dbo.FormDatas", "ImageName");
        }
    }
}
