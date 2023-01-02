namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClickContactoPublicante",
                c => new
                    {
                        clickContactoPublicanteId = c.Guid(nullable: false),
                        fechaHoraClickContactoPublicante = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        publicacionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.clickContactoPublicanteId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Publicacion", t => t.publicacionId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.publicacionId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        nombre = c.String(nullable: false, maxLength: 256),
                        apellido = c.String(nullable: false, maxLength: 256),
                        tipoDocumento = c.String(maxLength: 50),
                        sexo = c.String(nullable: false, maxLength: 50),
                        nroDocumento = c.Long(nullable: false),
                        nroTelefono = c.Long(nullable: false),
                        permitirSerContactadoPublicante = c.Boolean(nullable: false),
                        permitirSerNotificado = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Plan_planId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Plan", t => t.Plan_planId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Plan_planId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Plan",
                c => new
                    {
                        planId = c.Guid(nullable: false),
                        itemId = c.String(),
                        nombrePlan = c.String(),
                        descripcionPlan = c.String(),
                        moneda = c.String(),
                        creditos = c.Int(nullable: false),
                        vencimientoCreditos = c.String(),
                        subirVideos = c.Boolean(nullable: false),
                        precioPlan = c.Decimal(nullable: false, precision: 18, scale: 2),
                        activo = c.Boolean(nullable: false),
                        cantidadMaxImagenesPermitidasPorPub = c.Int(nullable: false),
                        accesoEstadisticasPremium = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.planId);
            
            CreateTable(
                "dbo.Propiedad",
                c => new
                    {
                        propiedadId = c.Guid(nullable: false),
                        direccionFormateada = c.String(),
                        pais = c.String(nullable: false),
                        areaAdministrativaNivel1 = c.String(nullable: false),
                        areaAdministrativaNivel2 = c.String(nullable: false),
                        nroCalle = c.Int(nullable: false),
                        latitud = c.Double(nullable: false),
                        longitud = c.Double(nullable: false),
                        identificadorUbicacionGoogle = c.String(nullable: false),
                        antiguedad = c.Int(nullable: false),
                        nroPlantas = c.Int(nullable: false),
                        superficieTerreno = c.Single(nullable: false),
                        superficieCubierta = c.Single(nullable: false),
                        calle = c.String(nullable: false),
                        fechaRegistro = c.DateTime(nullable: false),
                        tipoPropiedadId = c.Guid(nullable: false),
                        tipoConstruccionId = c.Guid(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.propiedadId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.TipoConstruccion", t => t.tipoConstruccionId)
                .ForeignKey("dbo.TipoPropiedad", t => t.tipoPropiedadId, cascadeDelete: true)
                .Index(t => t.tipoPropiedadId)
                .Index(t => t.tipoConstruccionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PropiedadExtras",
                c => new
                    {
                        propiedadExtrasId = c.Guid(nullable: false),
                        propiedadId = c.Guid(nullable: false),
                        extrasId = c.Guid(nullable: false),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.propiedadExtrasId)
                .ForeignKey("dbo.Extras", t => t.extrasId, cascadeDelete: true)
                .ForeignKey("dbo.Propiedad", t => t.propiedadId, cascadeDelete: true)
                .Index(t => t.propiedadId)
                .Index(t => t.extrasId);
            
            CreateTable(
                "dbo.Extras",
                c => new
                    {
                        extraId = c.Guid(nullable: false),
                        nombreExtra = c.String(),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.extraId);
            
            CreateTable(
                "dbo.PropiedadTipoAmbientes",
                c => new
                    {
                        propiedadTipoAmbienteId = c.Guid(nullable: false),
                        cantidadAmbientes = c.Int(nullable: false),
                        tipoAmbienteId = c.Guid(nullable: false),
                        propiedadId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.propiedadTipoAmbienteId)
                .ForeignKey("dbo.Propiedad", t => t.propiedadId, cascadeDelete: true)
                .ForeignKey("dbo.TipoAmbiente", t => t.tipoAmbienteId, cascadeDelete: true)
                .Index(t => t.tipoAmbienteId)
                .Index(t => t.propiedadId);
            
            CreateTable(
                "dbo.TipoAmbiente",
                c => new
                    {
                        tipoAmbienteId = c.Guid(nullable: false),
                        nombreTipoAmbiente = c.String(),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.tipoAmbienteId);
            
            CreateTable(
                "dbo.TipoConstruccion",
                c => new
                    {
                        tipoConstuccionId = c.Guid(nullable: false),
                        nombreTipoConstruccion = c.String(),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.tipoConstuccionId);
            
            CreateTable(
                "dbo.TipoPropiedad",
                c => new
                    {
                        tipoPropiedadId = c.Guid(nullable: false),
                        nombreTipoPropiedad = c.String(),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.tipoPropiedadId);
            
            CreateTable(
                "dbo.Publicacion",
                c => new
                    {
                        publicacionId = c.Guid(nullable: false),
                        fechaInicioPublicacion = c.DateTime(nullable: false),
                        fechaFinPublicacion = c.DateTime(nullable: false),
                        observaciones = c.String(maxLength: 256),
                        precioPropiedad = c.Decimal(nullable: false, precision: 18, scale: 2),
                        tipoMonedaId = c.Guid(nullable: false),
                        propiedadId = c.Guid(nullable: false),
                        tipoUserId = c.Guid(nullable: false),
                        tipoPublicacionId = c.Guid(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.publicacionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Propiedad", t => t.propiedadId, cascadeDelete: true)
                .ForeignKey("dbo.TipoMoneda", t => t.tipoMonedaId, cascadeDelete: true)
                .ForeignKey("dbo.TipoPublicacion", t => t.tipoPublicacionId, cascadeDelete: true)
                .ForeignKey("dbo.TipoUser", t => t.tipoUserId, cascadeDelete: true)
                .Index(t => t.tipoMonedaId)
                .Index(t => t.propiedadId)
                .Index(t => t.tipoUserId)
                .Index(t => t.tipoPublicacionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ImagenPublicacion",
                c => new
                    {
                        imagenPublicacionId = c.Guid(nullable: false),
                        imagenRepresentativa = c.Boolean(nullable: false),
                        publicacionId = c.Guid(nullable: false),
                        rutaBD = c.String(),
                    })
                .PrimaryKey(t => t.imagenPublicacionId)
                .ForeignKey("dbo.Publicacion", t => t.publicacionId, cascadeDelete: true)
                .Index(t => t.publicacionId);
            
            CreateTable(
                "dbo.PublicacionEstado",
                c => new
                    {
                        publicacionEstadoId = c.Guid(nullable: false),
                        fechaDesde = c.DateTime(nullable: false),
                        fechaHasta = c.DateTime(nullable: false),
                        publicacionId = c.Guid(nullable: false),
                        estadoPublicacionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.publicacionEstadoId)
                .ForeignKey("dbo.EstadoPublicacion", t => t.estadoPublicacionId, cascadeDelete: true)
                .ForeignKey("dbo.Publicacion", t => t.publicacionId, cascadeDelete: true)
                .Index(t => t.publicacionId)
                .Index(t => t.estadoPublicacionId);
            
            CreateTable(
                "dbo.EstadoPublicacion",
                c => new
                    {
                        estadoPublicacionId = c.Guid(nullable: false),
                        nombreEstadoPublicacion = c.String(nullable: false, maxLength: 250),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.estadoPublicacionId);
            
            CreateTable(
                "dbo.TipoMoneda",
                c => new
                    {
                        tipoMonedaId = c.Guid(nullable: false),
                        nombreTipoMoneda = c.String(),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.tipoMonedaId);
            
            CreateTable(
                "dbo.TipoPublicacion",
                c => new
                    {
                        tipoPublicacionId = c.Guid(nullable: false),
                        nombreTipoPublicacion = c.String(),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.tipoPublicacionId);
            
            CreateTable(
                "dbo.TipoUser",
                c => new
                    {
                        tipoUserId = c.Guid(nullable: false),
                        nombreTipoUser = c.String(),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.tipoUserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ClickPublicacion",
                c => new
                    {
                        clickPublicacionId = c.Guid(nullable: false),
                        fechaHoraClickPublicacion = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        publicacionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.clickPublicacionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Publicacion", t => t.publicacionId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.publicacionId);
            
            CreateTable(
                "dbo.Cotizacion",
                c => new
                    {
                        cotizacionId = c.Guid(nullable: false),
                        source = c.String(),
                        target = c.String(),
                        value = c.Single(nullable: false),
                        quantity = c.Single(nullable: false),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.cotizacionId);
            
            CreateTable(
                "dbo.Favorito",
                c => new
                    {
                        favoritoId = c.String(nullable: false, maxLength: 128),
                        activo = c.Boolean(nullable: false),
                        fechaSeleccion = c.DateTime(nullable: false),
                        userId = c.String(maxLength: 128),
                        publicacionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.favoritoId)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .ForeignKey("dbo.Publicacion", t => t.publicacionId, cascadeDelete: true)
                .Index(t => t.userId)
                .Index(t => t.publicacionId);
            
            CreateTable(
                "dbo.PlanUsuario",
                c => new
                    {
                        planUsuarioID = c.Guid(nullable: false),
                        fechaContratacion = c.DateTime(nullable: false),
                        activo = c.Boolean(nullable: false),
                        TotalCreditosActivos = c.Single(nullable: false),
                        UserId = c.String(maxLength: 128),
                        planID = c.Guid(nullable: false),
                        IDDescuentoCredito = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.planUsuarioID)
                .ForeignKey("dbo.ParametrizacionDescuentoCredito", t => t.IDDescuentoCredito, cascadeDelete: true)
                .ForeignKey("dbo.Plan", t => t.planID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.planID)
                .Index(t => t.IDDescuentoCredito);
            
            CreateTable(
                "dbo.ParametrizacionDescuentoCredito",
                c => new
                    {
                        IDDescuentoCredito = c.Guid(nullable: false),
                        CoeficienteDescuentoCredito = c.Single(nullable: false),
                        DescuentoCompraSinPaquete = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.IDDescuentoCredito);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PlanUsuario", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PlanUsuario", "planID", "dbo.Plan");
            DropForeignKey("dbo.PlanUsuario", "IDDescuentoCredito", "dbo.ParametrizacionDescuentoCredito");
            DropForeignKey("dbo.Favorito", "publicacionId", "dbo.Publicacion");
            DropForeignKey("dbo.Favorito", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClickPublicacion", "publicacionId", "dbo.Publicacion");
            DropForeignKey("dbo.ClickPublicacion", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClickContactoPublicante", "publicacionId", "dbo.Publicacion");
            DropForeignKey("dbo.ClickContactoPublicante", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Publicacion", "tipoUserId", "dbo.TipoUser");
            DropForeignKey("dbo.Publicacion", "tipoPublicacionId", "dbo.TipoPublicacion");
            DropForeignKey("dbo.Publicacion", "tipoMonedaId", "dbo.TipoMoneda");
            DropForeignKey("dbo.PublicacionEstado", "publicacionId", "dbo.Publicacion");
            DropForeignKey("dbo.PublicacionEstado", "estadoPublicacionId", "dbo.EstadoPublicacion");
            DropForeignKey("dbo.Publicacion", "propiedadId", "dbo.Propiedad");
            DropForeignKey("dbo.ImagenPublicacion", "publicacionId", "dbo.Publicacion");
            DropForeignKey("dbo.Publicacion", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Propiedad", "tipoPropiedadId", "dbo.TipoPropiedad");
            DropForeignKey("dbo.Propiedad", "tipoConstruccionId", "dbo.TipoConstruccion");
            DropForeignKey("dbo.PropiedadTipoAmbientes", "tipoAmbienteId", "dbo.TipoAmbiente");
            DropForeignKey("dbo.PropiedadTipoAmbientes", "propiedadId", "dbo.Propiedad");
            DropForeignKey("dbo.PropiedadExtras", "propiedadId", "dbo.Propiedad");
            DropForeignKey("dbo.PropiedadExtras", "extrasId", "dbo.Extras");
            DropForeignKey("dbo.Propiedad", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Plan_planId", "dbo.Plan");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PlanUsuario", new[] { "IDDescuentoCredito" });
            DropIndex("dbo.PlanUsuario", new[] { "planID" });
            DropIndex("dbo.PlanUsuario", new[] { "UserId" });
            DropIndex("dbo.Favorito", new[] { "publicacionId" });
            DropIndex("dbo.Favorito", new[] { "userId" });
            DropIndex("dbo.ClickPublicacion", new[] { "publicacionId" });
            DropIndex("dbo.ClickPublicacion", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.PublicacionEstado", new[] { "estadoPublicacionId" });
            DropIndex("dbo.PublicacionEstado", new[] { "publicacionId" });
            DropIndex("dbo.ImagenPublicacion", new[] { "publicacionId" });
            DropIndex("dbo.Publicacion", new[] { "UserId" });
            DropIndex("dbo.Publicacion", new[] { "tipoPublicacionId" });
            DropIndex("dbo.Publicacion", new[] { "tipoUserId" });
            DropIndex("dbo.Publicacion", new[] { "propiedadId" });
            DropIndex("dbo.Publicacion", new[] { "tipoMonedaId" });
            DropIndex("dbo.PropiedadTipoAmbientes", new[] { "propiedadId" });
            DropIndex("dbo.PropiedadTipoAmbientes", new[] { "tipoAmbienteId" });
            DropIndex("dbo.PropiedadExtras", new[] { "extrasId" });
            DropIndex("dbo.PropiedadExtras", new[] { "propiedadId" });
            DropIndex("dbo.Propiedad", new[] { "UserId" });
            DropIndex("dbo.Propiedad", new[] { "tipoConstruccionId" });
            DropIndex("dbo.Propiedad", new[] { "tipoPropiedadId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Plan_planId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ClickContactoPublicante", new[] { "publicacionId" });
            DropIndex("dbo.ClickContactoPublicante", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ParametrizacionDescuentoCredito");
            DropTable("dbo.PlanUsuario");
            DropTable("dbo.Favorito");
            DropTable("dbo.Cotizacion");
            DropTable("dbo.ClickPublicacion");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.TipoUser");
            DropTable("dbo.TipoPublicacion");
            DropTable("dbo.TipoMoneda");
            DropTable("dbo.EstadoPublicacion");
            DropTable("dbo.PublicacionEstado");
            DropTable("dbo.ImagenPublicacion");
            DropTable("dbo.Publicacion");
            DropTable("dbo.TipoPropiedad");
            DropTable("dbo.TipoConstruccion");
            DropTable("dbo.TipoAmbiente");
            DropTable("dbo.PropiedadTipoAmbientes");
            DropTable("dbo.Extras");
            DropTable("dbo.PropiedadExtras");
            DropTable("dbo.Propiedad");
            DropTable("dbo.Plan");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ClickContactoPublicante");
        }
    }
}
