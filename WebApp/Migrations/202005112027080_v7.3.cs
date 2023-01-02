namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v73 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditoPlan",
                c => new
                    {
                        CreditoPlanID = c.Guid(nullable: false),
                        moneda = c.String(),
                        activo = c.Boolean(nullable: false),
                        precioPlanCredito = c.Decimal(nullable: false, precision: 18, scale: 2),
                        cantidadTotalCreditoPorPaquete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CreditoPlanID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CreditoPlan");
        }
    }
}
