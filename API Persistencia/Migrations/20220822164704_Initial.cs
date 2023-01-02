using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_Persistencia.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caracteristica",
                columns: table => new
                {
                    caracteristicaId = table.Column<string>(nullable: false),
                    nombreCaracteristica = table.Column<string>(nullable: true),
                    activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caracteristica", x => x.caracteristicaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoAmbiente",
                columns: table => new
                {
                    tipoAmbienteId = table.Column<string>(nullable: false),
                    nombreTipoAmbiente = table.Column<string>(nullable: true),
                    activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAmbiente", x => x.tipoAmbienteId);
                });

            migrationBuilder.CreateTable(
                name: "TipoConstruccion",
                columns: table => new
                {
                    tipoConstruccionId = table.Column<string>(nullable: false),
                    nombreTipoConstruccion = table.Column<string>(nullable: true),
                    activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoConstruccion", x => x.tipoConstruccionId);
                });

            migrationBuilder.CreateTable(
                name: "TipoMoneda",
                columns: table => new
                {
                    tipoMonedaId = table.Column<string>(nullable: false),
                    denominacionMoneda = table.Column<string>(nullable: true),
                    activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMoneda", x => x.tipoMonedaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoPropiedad",
                columns: table => new
                {
                    tipoPropiedadId = table.Column<string>(nullable: false),
                    nombreTipoPropiedad = table.Column<string>(nullable: true),
                    activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPropiedad", x => x.tipoPropiedadId);
                });

            migrationBuilder.CreateTable(
                name: "TipoPublicacion",
                columns: table => new
                {
                    tipoPublicacionId = table.Column<string>(nullable: false),
                    nombreTipoPublicacion = table.Column<string>(nullable: true),
                    activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPublicacion", x => x.tipoPublicacionId);
                });

            migrationBuilder.CreateTable(
                name: "TipoPublicante",
                columns: table => new
                {
                    tipoPublicanteId = table.Column<string>(nullable: false),
                    nombreTipoPublicante = table.Column<string>(nullable: true),
                    activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPublicante", x => x.tipoPublicanteId);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    usuarioId = table.Column<string>(nullable: false),
                    nombreUsuario = table.Column<string>(nullable: true),
                    apellidoUsuario = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    contraseña = table.Column<byte[]>(nullable: true),
                    emailConfirmado = table.Column<bool>(nullable: false),
                    permiterSerContactadoPorPublicante = table.Column<bool>(nullable: false),
                    permiteSerNotificado = table.Column<bool>(nullable: false),
                    telefono1 = table.Column<long>(nullable: false),
                    telefono2 = table.Column<long>(nullable: false),
                    admin = table.Column<bool>(nullable: false),
                    Key = table.Column<byte[]>(nullable: true),
                    Vector = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.usuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Credito",
                columns: table => new
                {
                    PaqueteID = table.Column<string>(nullable: false),
                    CantidadCreditos = table.Column<int>(nullable: false),
                    Precio = table.Column<decimal>(nullable: false),
                    Activo = table.Column<bool>(nullable: false),
                    NombrePack = table.Column<string>(nullable: true),
                    TipoMonedaID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credito", x => x.PaqueteID);
                    table.ForeignKey(
                        name: "FK_Credito_TipoMoneda_TipoMonedaID",
                        column: x => x.TipoMonedaID,
                        principalTable: "TipoMoneda",
                        principalColumn: "tipoMonedaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    planId = table.Column<string>(nullable: false),
                    nombrePlan = table.Column<string>(nullable: true),
                    permiteVideo = table.Column<bool>(nullable: false),
                    accesoEstadisticasAvanzadas = table.Column<bool>(nullable: false),
                    cantidadCreditosIniciales = table.Column<int>(nullable: false),
                    precioPlan = table.Column<decimal>(nullable: false),
                    cantidadMaxImagenesPermitidasPorPublicacion = table.Column<int>(nullable: false),
                    activo = table.Column<bool>(nullable: false),
                    tipoMonedaId = table.Column<string>(nullable: true),
                    Vencimiento = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.planId);
                    table.ForeignKey(
                        name: "FK_Plan_TipoMoneda_tipoMonedaId",
                        column: x => x.tipoMonedaId,
                        principalTable: "TipoMoneda",
                        principalColumn: "tipoMonedaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoPropiedadTipoAmbiente",
                columns: table => new
                {
                    tipoPropiedadTipoAmbienteId = table.Column<string>(nullable: false),
                    activo = table.Column<bool>(nullable: false),
                    tipoAmbienteId = table.Column<string>(nullable: true),
                    tipoPropiedadId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPropiedadTipoAmbiente", x => x.tipoPropiedadTipoAmbienteId);
                    table.ForeignKey(
                        name: "FK_TipoPropiedadTipoAmbiente_TipoAmbiente_tipoAmbienteId",
                        column: x => x.tipoAmbienteId,
                        principalTable: "TipoAmbiente",
                        principalColumn: "tipoAmbienteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TipoPropiedadTipoAmbiente_TipoPropiedad_tipoPropiedadId",
                        column: x => x.tipoPropiedadId,
                        principalTable: "TipoPropiedad",
                        principalColumn: "tipoPropiedadId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Actividad",
                columns: table => new
                {
                    actividadId = table.Column<string>(nullable: false),
                    fechaActividad = table.Column<DateTime>(nullable: false),
                    descripcionActividad = table.Column<string>(nullable: true),
                    usuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividad", x => x.actividadId);
                    table.ForeignKey(
                        name: "FK_Actividad_Usuario_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuario",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PagoMP",
                columns: table => new
                {
                    PagoMPId = table.Column<string>(nullable: false),
                    idPago = table.Column<string>(nullable: true),
                    currency_id = table.Column<string>(nullable: true),
                    date_approved = table.Column<DateTime>(nullable: false),
                    date_created = table.Column<DateTime>(nullable: false),
                    operation_type = table.Column<string>(nullable: true),
                    payment_type_id = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    transaction_amount = table.Column<float>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagoMP", x => x.PagoMPId);
                    table.ForeignKey(
                        name: "FK_PagoMP_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Propiedad",
                columns: table => new
                {
                    propiedadId = table.Column<string>(nullable: false),
                    descripcionPropiedad = table.Column<string>(nullable: true),
                    ubicacion = table.Column<string>(nullable: true),
                    pais = table.Column<string>(nullable: true),
                    AreaAdmNivel1 = table.Column<string>(nullable: true),
                    AreaAdmNivel2 = table.Column<string>(nullable: true),
                    latitud = table.Column<double>(nullable: false),
                    longitud = table.Column<double>(nullable: false),
                    precioPropiedad = table.Column<decimal>(nullable: false),
                    fechaRegistro = table.Column<DateTime>(nullable: false),
                    nroPisos = table.Column<int>(nullable: false),
                    superficieTerreno = table.Column<int>(nullable: false),
                    superficieCubierta = table.Column<int>(nullable: false),
                    amueblado = table.Column<bool>(nullable: false),
                    importeExpensasUltimoMes = table.Column<decimal>(nullable: false),
                    añosAntiguedad = table.Column<int>(nullable: false),
                    tipoPublicanteId = table.Column<string>(nullable: true),
                    tipoConstruccionId = table.Column<string>(nullable: true),
                    tipoPropiedadId = table.Column<string>(nullable: true),
                    usuarioId = table.Column<string>(nullable: true),
                    tipoMonedaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedad", x => x.propiedadId);
                    table.ForeignKey(
                        name: "FK_Propiedad_TipoConstruccion_tipoConstruccionId",
                        column: x => x.tipoConstruccionId,
                        principalTable: "TipoConstruccion",
                        principalColumn: "tipoConstruccionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Propiedad_TipoMoneda_tipoMonedaId",
                        column: x => x.tipoMonedaId,
                        principalTable: "TipoMoneda",
                        principalColumn: "tipoMonedaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Propiedad_TipoPublicante_tipoPublicanteId",
                        column: x => x.tipoPublicanteId,
                        principalTable: "TipoPublicante",
                        principalColumn: "tipoPublicanteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Propiedad_Usuario_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuario",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanUsuario",
                columns: table => new
                {
                    planUsuarioId = table.Column<string>(nullable: false),
                    activo = table.Column<bool>(nullable: false),
                    fechaContratacion = table.Column<DateTime>(nullable: false),
                    fechaVencimiento = table.Column<DateTime>(nullable: false),
                    cantidadCreditosActivos = table.Column<int>(nullable: false),
                    planId = table.Column<string>(nullable: true),
                    usuarioId = table.Column<string>(nullable: true),
                    NumFactura = table.Column<long>(nullable: false),
                    FechaCompra = table.Column<DateTime>(nullable: false),
                    CreditoPaqueteId = table.Column<string>(nullable: true),
                    PagoMPId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanUsuario", x => x.planUsuarioId);
                    table.ForeignKey(
                        name: "FK_PlanUsuario_Credito_CreditoPaqueteId",
                        column: x => x.CreditoPaqueteId,
                        principalTable: "Credito",
                        principalColumn: "PaqueteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanUsuario_PagoMP_PagoMPId",
                        column: x => x.PagoMPId,
                        principalTable: "PagoMP",
                        principalColumn: "PagoMPId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanUsuario_Plan_planId",
                        column: x => x.planId,
                        principalTable: "Plan",
                        principalColumn: "planId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanUsuario_Usuario_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuario",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImagenPropiedad",
                columns: table => new
                {
                    ImagenPropiedadId = table.Column<string>(nullable: false),
                    rutaImagenPropiedad = table.Column<string>(nullable: true),
                    activo = table.Column<bool>(nullable: false),
                    propiedadId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagenPropiedad", x => x.ImagenPropiedadId);
                    table.ForeignKey(
                        name: "FK_ImagenPropiedad_Propiedad_propiedadId",
                        column: x => x.propiedadId,
                        principalTable: "Propiedad",
                        principalColumn: "propiedadId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropiedadTipoAmbiente",
                columns: table => new
                {
                    propiedadTipoAmbienteId = table.Column<string>(nullable: false),
                    activo = table.Column<bool>(nullable: false),
                    cantidad = table.Column<int>(nullable: false),
                    propiedadId = table.Column<string>(nullable: true),
                    tipoAmbienteId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropiedadTipoAmbiente", x => x.propiedadTipoAmbienteId);
                    table.ForeignKey(
                        name: "FK_PropiedadTipoAmbiente_Propiedad_propiedadId",
                        column: x => x.propiedadId,
                        principalTable: "Propiedad",
                        principalColumn: "propiedadId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropiedadTipoAmbiente_TipoAmbiente_tipoAmbienteId",
                        column: x => x.tipoAmbienteId,
                        principalTable: "TipoAmbiente",
                        principalColumn: "tipoAmbienteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Publicacion",
                columns: table => new
                {
                    publicacionId = table.Column<string>(nullable: false),
                    fechaInicioPublicacion = table.Column<DateTime>(nullable: false),
                    fechaFinPublicacion = table.Column<DateTime>(nullable: false),
                    tipoPublicacionId = table.Column<string>(nullable: true),
                    propiedadId = table.Column<string>(nullable: true),
                    estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicacion", x => x.publicacionId);
                    table.ForeignKey(
                        name: "FK_Publicacion_Propiedad_propiedadId",
                        column: x => x.propiedadId,
                        principalTable: "Propiedad",
                        principalColumn: "propiedadId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Publicacion_TipoPublicacion_tipoPublicacionId",
                        column: x => x.tipoPublicacionId,
                        principalTable: "TipoPublicacion",
                        principalColumn: "tipoPublicacionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoPropiedadCaracteristica",
                columns: table => new
                {
                    caracteristicaId = table.Column<string>(nullable: false),
                    propiedadId = table.Column<string>(nullable: false),
                    tipoPropiedadCaracteristicaID = table.Column<string>(nullable: true),
                    propiedadId1 = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPropiedadCaracteristica", x => new { x.propiedadId, x.caracteristicaId });
                    table.ForeignKey(
                        name: "FK_TipoPropiedadCaracteristica_TipoPropiedad_caracteristicaId",
                        column: x => x.caracteristicaId,
                        principalTable: "TipoPropiedad",
                        principalColumn: "tipoPropiedadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TipoPropiedadCaracteristica_Caracteristica_propiedadId",
                        column: x => x.propiedadId,
                        principalTable: "Caracteristica",
                        principalColumn: "caracteristicaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TipoPropiedadCaracteristica_Propiedad_propiedadId1",
                        column: x => x.propiedadId1,
                        principalTable: "Propiedad",
                        principalColumn: "propiedadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorito",
                columns: table => new
                {
                    favoritoId = table.Column<string>(nullable: false),
                    activo = table.Column<bool>(nullable: false),
                    publicacionId = table.Column<string>(nullable: true),
                    usuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorito", x => x.favoritoId);
                    table.ForeignKey(
                        name: "FK_Favorito_Publicacion_publicacionId",
                        column: x => x.publicacionId,
                        principalTable: "Publicacion",
                        principalColumn: "publicacionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favorito_Usuario_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuario",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudContactoVisitante",
                columns: table => new
                {
                    solicitudContactoVisitanteId = table.Column<string>(nullable: false),
                    cantidadVecesRealizoSolicitud = table.Column<int>(nullable: false),
                    fechaSolicitud = table.Column<DateTime>(nullable: false),
                    publicacionId = table.Column<string>(nullable: true),
                    usuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudContactoVisitante", x => x.solicitudContactoVisitanteId);
                    table.ForeignKey(
                        name: "FK_SolicitudContactoVisitante_Publicacion_publicacionId",
                        column: x => x.publicacionId,
                        principalTable: "Publicacion",
                        principalColumn: "publicacionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitudContactoVisitante_Usuario_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuario",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VisitaInmueble",
                columns: table => new
                {
                    visitaInmuebleId = table.Column<string>(nullable: false),
                    fechaHoraVisitaInmueble = table.Column<DateTime>(nullable: false),
                    cantidadVecesQueRepitioVisita = table.Column<int>(nullable: false),
                    contactoPublicante = table.Column<bool>(nullable: false),
                    usuarioId = table.Column<string>(nullable: true),
                    publicacionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitaInmueble", x => x.visitaInmuebleId);
                    table.ForeignKey(
                        name: "FK_VisitaInmueble_Publicacion_publicacionId",
                        column: x => x.publicacionId,
                        principalTable: "Publicacion",
                        principalColumn: "publicacionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitaInmueble_Usuario_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuario",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VisitaPerfilPublicante",
                columns: table => new
                {
                    visitaPerfilPublicanteId = table.Column<string>(nullable: false),
                    fechaHoraVisitaPerfilPublicante = table.Column<DateTime>(nullable: false),
                    publicacionId = table.Column<string>(nullable: true),
                    usuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitaPerfilPublicante", x => x.visitaPerfilPublicanteId);
                    table.ForeignKey(
                        name: "FK_VisitaPerfilPublicante_Publicacion_publicacionId",
                        column: x => x.publicacionId,
                        principalTable: "Publicacion",
                        principalColumn: "publicacionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitaPerfilPublicante_Usuario_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuario",
                        principalColumn: "usuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividad_usuarioId",
                table: "Actividad",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Credito_TipoMonedaID",
                table: "Credito",
                column: "TipoMonedaID");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_publicacionId",
                table: "Favorito",
                column: "publicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_usuarioId",
                table: "Favorito",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagenPropiedad_propiedadId",
                table: "ImagenPropiedad",
                column: "propiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_PagoMP_UsuarioId",
                table: "PagoMP",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_tipoMonedaId",
                table: "Plan",
                column: "tipoMonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanUsuario_CreditoPaqueteId",
                table: "PlanUsuario",
                column: "CreditoPaqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanUsuario_PagoMPId",
                table: "PlanUsuario",
                column: "PagoMPId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanUsuario_planId",
                table: "PlanUsuario",
                column: "planId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanUsuario_usuarioId",
                table: "PlanUsuario",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_tipoConstruccionId",
                table: "Propiedad",
                column: "tipoConstruccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_tipoMonedaId",
                table: "Propiedad",
                column: "tipoMonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_tipoPublicanteId",
                table: "Propiedad",
                column: "tipoPublicanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_usuarioId",
                table: "Propiedad",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PropiedadTipoAmbiente_propiedadId",
                table: "PropiedadTipoAmbiente",
                column: "propiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_PropiedadTipoAmbiente_tipoAmbienteId",
                table: "PropiedadTipoAmbiente",
                column: "tipoAmbienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Publicacion_propiedadId",
                table: "Publicacion",
                column: "propiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_Publicacion_tipoPublicacionId",
                table: "Publicacion",
                column: "tipoPublicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudContactoVisitante_publicacionId",
                table: "SolicitudContactoVisitante",
                column: "publicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudContactoVisitante_usuarioId",
                table: "SolicitudContactoVisitante",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadCaracteristica_caracteristicaId",
                table: "TipoPropiedadCaracteristica",
                column: "caracteristicaId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadCaracteristica_propiedadId1",
                table: "TipoPropiedadCaracteristica",
                column: "propiedadId1");

            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadTipoAmbiente_tipoAmbienteId",
                table: "TipoPropiedadTipoAmbiente",
                column: "tipoAmbienteId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoPropiedadTipoAmbiente_tipoPropiedadId",
                table: "TipoPropiedadTipoAmbiente",
                column: "tipoPropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitaInmueble_publicacionId",
                table: "VisitaInmueble",
                column: "publicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitaInmueble_usuarioId",
                table: "VisitaInmueble",
                column: "usuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitaPerfilPublicante_publicacionId",
                table: "VisitaPerfilPublicante",
                column: "publicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitaPerfilPublicante_usuarioId",
                table: "VisitaPerfilPublicante",
                column: "usuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actividad");

            migrationBuilder.DropTable(
                name: "Favorito");

            migrationBuilder.DropTable(
                name: "ImagenPropiedad");

            migrationBuilder.DropTable(
                name: "PlanUsuario");

            migrationBuilder.DropTable(
                name: "PropiedadTipoAmbiente");

            migrationBuilder.DropTable(
                name: "SolicitudContactoVisitante");

            migrationBuilder.DropTable(
                name: "TipoPropiedadCaracteristica");

            migrationBuilder.DropTable(
                name: "TipoPropiedadTipoAmbiente");

            migrationBuilder.DropTable(
                name: "VisitaInmueble");

            migrationBuilder.DropTable(
                name: "VisitaPerfilPublicante");

            migrationBuilder.DropTable(
                name: "Credito");

            migrationBuilder.DropTable(
                name: "PagoMP");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "Caracteristica");

            migrationBuilder.DropTable(
                name: "TipoAmbiente");

            migrationBuilder.DropTable(
                name: "TipoPropiedad");

            migrationBuilder.DropTable(
                name: "Publicacion");

            migrationBuilder.DropTable(
                name: "Propiedad");

            migrationBuilder.DropTable(
                name: "TipoPublicacion");

            migrationBuilder.DropTable(
                name: "TipoConstruccion");

            migrationBuilder.DropTable(
                name: "TipoMoneda");

            migrationBuilder.DropTable(
                name: "TipoPublicante");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
