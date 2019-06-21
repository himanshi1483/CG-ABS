namespace ABS_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
           // AddColumn("dbo.FormDatas", "Image", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
           // DropColumn("dbo.FormDatas", "Image");
        }
    }
}
