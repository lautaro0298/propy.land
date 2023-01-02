namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v74 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanUsuario", "IDDescuentoCredito", "dbo.ParametrizacionDescuentoCredito");
            DropIndex("dbo.PlanUsuario", new[] { "IDDescuentoCredito" });
            DropColumn("dbo.PlanUsuario", "IDDescuentoCredito");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlanUsuario", "IDDescuentoCredito", c => c.Guid(nullable: false));
            CreateIndex("dbo.PlanUsuario", "IDDescuentoCredito");
            AddForeignKey("dbo.PlanUsuario", "IDDescuentoCredito", "dbo.ParametrizacionDescuentoCredito", "IDDescuentoCredito", cascadeDelete: true);
        }
    }
}
