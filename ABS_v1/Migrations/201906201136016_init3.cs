namespace ABS_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RegistrationModels", newName: "FormData");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.FormData", newName: "RegistrationModels");
        }
    }
}
