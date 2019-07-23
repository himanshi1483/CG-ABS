namespace ABS_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PayeeName = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        Amount = c.Double(nullable: false),
                        PaymentReqId = c.String(unicode: false),
                        PaymentStatus = c.String(unicode: false),
                        RegId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaymentModels");
        }
    }
}
