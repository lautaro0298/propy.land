namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v75 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActividadUsuario",
                c => new
                    {
                        IDActividadUsuario = c.Guid(nullable: false),
                        descripcionActividad = c.String(),
                        fechaActividad = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IDActividadUsuario)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActividadUsuario", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ActividadUsuario", new[] { "UserId" });
            DropTable("dbo.ActividadUsuario");
        }
    }
}
