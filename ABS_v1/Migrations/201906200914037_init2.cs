namespace ABS_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegistrationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(unicode: false),
                        BirthDay = c.DateTime(nullable: false, precision: 0),
                        BirthPlace = c.String(unicode: false),
                        FathersName = c.String(unicode: false),
                        MothersName = c.String(unicode: false),
                        Gotra = c.String(unicode: false),
                        Education = c.String(unicode: false),
                        Profession = c.String(unicode: false),
                        AnnualIncome = c.String(unicode: false),
                        MaritalStatus = c.String(unicode: false),
                        BrothersDetails = c.String(unicode: false),
                        SistersDetails = c.String(unicode: false),
                        BramhinType = c.String(unicode: false),
                        AgeDifference = c.Int(nullable: false),
                        Agreement = c.String(unicode: false),
                        GuardiansContact = c.String(unicode: false),
                        FullAddress = c.String(unicode: false),
                        InformantDetails = c.String(unicode: false),
                        RegDate = c.DateTime(nullable: false, precision: 0),
                        RegPlace = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RegistrationModels");
        }
    }
}
