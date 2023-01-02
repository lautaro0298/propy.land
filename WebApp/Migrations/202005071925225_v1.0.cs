namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "tipoDocumento");
            DropColumn("dbo.AspNetUsers", "sexo");
            DropColumn("dbo.AspNetUsers", "nroDocumento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "nroDocumento", c => c.Long(nullable: false));
            AddColumn("dbo.AspNetUsers", "sexo", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "tipoDocumento", c => c.String(maxLength: 50));
        }
    }
}
