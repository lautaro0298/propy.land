namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v80 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistorialCompraCredito",
                c => new
                    {
                        HistorialCompraCreditoID = c.Guid(nullable: false),
                        FechaCompraCredito = c.DateTime(nullable: false),
                        CreditoPlanID = c.Guid(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.HistorialCompraCreditoID)
                .ForeignKey("dbo.CreditoPlan", t => t.CreditoPlanID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.CreditoPlanID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.HistorialCompraPlan",
                c => new
                    {
                        HistorialCompraPlanID = c.Guid(nullable: false),
                        FechaCompra = c.DateTime(nullable: false),
                        PlanID = c.Guid(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.HistorialCompraPlanID)
                .ForeignKey("dbo.Plan", t => t.PlanID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.PlanID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistorialCompraPlan", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.HistorialCompraPlan", "PlanID", "dbo.Plan");
            DropForeignKey("dbo.HistorialCompraCredito", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.HistorialCompraCredito", "CreditoPlanID", "dbo.CreditoPlan");
            DropIndex("dbo.HistorialCompraPlan", new[] { "UserID" });
            DropIndex("dbo.HistorialCompraPlan", new[] { "PlanID" });
            DropIndex("dbo.HistorialCompraCredito", new[] { "UserID" });
            DropIndex("dbo.HistorialCompraCredito", new[] { "CreditoPlanID" });
            DropTable("dbo.HistorialCompraPlan");
            DropTable("dbo.HistorialCompraCredito");
        }
    }
}
