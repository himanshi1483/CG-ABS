namespace ABS_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FormDatas", "PaymentStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FormDatas", "PaymentStatus");
        }
    }
}
