using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.Models;
using WebApp.Fachada;
using WebApp.Servicios;
using WebApp.DTO;
using WebApp.DTOJSon;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using WebApp.Factoria;
using WebApp.Enumerables;

namespace WebApp.Experto
{
    public class ExpertoGestionarPublicacion
    {
        public DTOListadoPublicacionesPorUsuario ObtenerListadoPublicacionesPorUsuario(string usuario) {
            DTOListadoPublicacionesPorUsuario datos = new DTOListadoPublicacionesPorUsuario();
            using (var db = new ApplicationDbContext()) {
                List<Publicacion> publicaciones = (from p in db.Publicacion
                                                   where p.UserId == usuario
                                                   select p).ToList();
                
                foreach (var publicacion in publicaciones) {
                    DTOPublicacionUsuario dto = new DTOPublicacionUsuario();
                    dto.publicacionId = publicacion.publicacionId;
                    switch (publicacion.TipoMoneda.nombreTipoMoneda) {
                        case "ARS":
                            dto.precio = "(ARS) " + String.Format("{0:C}", publicacion.precioPropiedad);
                            break;
                        case "USD":
                            dto.precio = "(USD) " + String.Format("{0:C}", publicacion.precioPropiedad);
                            break;
                        default:
                            dto.precio = String.Format("{0:C}", publicacion.precioPropiedad);
                            break;
                    }
                    if (publicacion.TipoMoneda.nombreTipoMoneda=="ARS") {
                        
                    }
                    dto.ubicacion = publicacion.Propiedad.direccionFormateada;
                    dto.fechaInicioPublicacion = publicacion.fechaInicioPublicacion;
                    foreach (var estado in publicacion.PublicacionEstado) {
                        dto.estado = estado.EstadoPublicacion.nombreEstadoPublicacion;
                        
                    }
                    foreach (var imagen in publicacion.ImagenPublicacion) {
                        string ruta = @imagen.rutaBD;
                        if (imagen.imagenRepresentativa==true) {
                            ruta = ImagenServicios.ObtenerRutaRelativa(imagen);
                            dto.rutaImagenBD = ruta;
                        }
                        
                        
                    }
                    datos.AgregarDtoPublicacionUsuario(dto);

                }
                
                
            }
            return datos;
        }

        public async Task<DTOError> CrearPublicacion(DTOCrearPublicacion datos,string ip) {
            DTOError error = new DTOError();
            CorreoServicios serviciosCorreo = new CorreoServicios();
            GeolocalizacionServicios serviciosGeoLocalizacion = new GeolocalizacionServicios();
            //Creo instancia de Propiedad
            using (ApplicationDbContext db = new ApplicationDbContext())
                using(var database = db.Database.BeginTransaction())
            {
                try
                {

                    #region Crear Propiedad
                    Guid propiedadId = System.Guid.NewGuid();
                    Propiedad propiedad = new Propiedad();
                    propiedad.propiedadId = propiedadId;
                    propiedad.antiguedad = datos.antiguedad;
                    propiedad.calle = datos.calle;
                    propiedad.nroCalle = datos.nroCalle;
                    propiedad.pais = datos.pais;
                    propiedad.areaAdministrativaNivel1 = datos.areaAdministrativaNivel1;
                    propiedad.areaAdministrativaNivel2 = datos.areaAdministrativaNivel2;
                    propiedad.direccionFormateada = datos.direccionFormateada;
                    propiedad.identificadorUbicacionGoogle = datos.identificadorUbicacionGoogle;
                    propiedad.latitud = datos.latitud;
                    propiedad.longitud = datos.longitud;
                    propiedad.tipoConstruccionId = datos.tipoConstruccion;
                    propiedad.tipoPropiedadId = datos.tipoPropiedad;
                    propiedad.fechaRegistro = DateTime.Now;
                    propiedad.superficieCubierta = datos.superficieCubierta;
                    propiedad.superficieTerreno = datos.superficieTerreno;
                    propiedad.nroPlantas = datos.nroPlantas;
                    propiedad.UserId = datos.usuarioId;
                    //Mando Orden a la bd de crear la instancia
                    Repository<Propiedad> propRepo = new Repository<Propiedad>();
                    propRepo.Crear(propiedad,db);
                    #endregion
                    #region Crear PropiedadTipoAmbiente
                    Repository<PropiedadTipoAmbiente> ptaRepo = new Repository<PropiedadTipoAmbiente>();
                    for (int i = 0; i < datos.tipoAmbiente.Count; i++)
                    {
                        var propiedadTipoAmbienteId = System.Guid.NewGuid();
                        PropiedadTipoAmbiente pta = new PropiedadTipoAmbiente();
                        pta.propiedadTipoAmbienteId = propiedadTipoAmbienteId;
                        pta.propiedadId = propiedad.propiedadId;
                        pta.tipoAmbienteId = datos.tipoAmbiente[i];
                        pta.cantidadAmbientes = datos.cantidadAmbientes[i];
                        
                        ptaRepo.Crear(pta, db);
                       
                    }
                    #endregion
                    #region Crear PropiedadExtras
                    Repository<PropiedadExtras> pexRepo = new Repository<PropiedadExtras>();
                    if (datos.extras.Count > 0)
                    {
                        for (int i = 0; i < datos.extras.Count; i++)
                        {
                            var propiedadExtraId = System.Guid.NewGuid();
                            PropiedadExtras propiedadExtras = new PropiedadExtras();
                            propiedadExtras.extrasId = datos.extras[i];
                            propiedadExtras.propiedadId = propiedad.propiedadId;
                            propiedadExtras.activo = true;
                            propiedadExtras.propiedadExtrasId = propiedadExtraId;
                            
                            pexRepo.Crear(propiedadExtras, db);
                            

                        }
                    }
                    #endregion
                    #region Crear Publicacion
                    Publicacion publicacion = new Publicacion();
                    Guid publicacionId = System.Guid.NewGuid();
                    DateTime fechaInicioPublicacion = DateTime.Now;
                    DateTime fechaFinPublicacion = fechaInicioPublicacion.AddDays(15);
                    Publicacion p = new Publicacion();
                    p.publicacionId = publicacionId;
                    p.fechaInicioPublicacion = fechaInicioPublicacion;
                    p.fechaFinPublicacion = fechaFinPublicacion;
                    p.tipoPublicacionId = datos.tipoOperacion;
                    p.tipoUserId = datos.tipoUsuario;
                    p.propiedadId = propiedad.propiedadId;
                    p.UserId = datos.usuarioId;
                    p.tipoMonedaId = datos.tipoMoneda;
                    p.precioPropiedad = datos.precioPropiedad;
                    p.observaciones = datos.observaciones;
                    Repository<Publicacion> pubRepo = new Repository<Publicacion>();
                    pubRepo.Crear(p,db);
                    #endregion
                    #region Crear PublicacionEstado
                    var dato = db.EstadoPublicacion.Find(Guid.Parse("DE7E6BD0-48CD-4D34-92CE-7AE2FB92BDE7"));
                    PublicacionEstado pe = new PublicacionEstado();
                    DateTime fechaDesde = DateTime.Now;
                    DateTime fechaHasta = fechaDesde.AddDays(15);
                    pe.publicacionEstadoId = System.Guid.NewGuid();
                    pe.fechaDesde = fechaDesde;
                    pe.fechaHasta = fechaHasta;
                    pe.publicacionId = publicacionId;
                    pe.estadoPublicacionId = dato.estadoPublicacionId;
                    Repository<PublicacionEstado> peRepo = new Repository<PublicacionEstado>();
                    peRepo.Crear(pe,db);
                    #endregion
                    #region Crear ImagenPublicacion
                    Repository<ImagenPublicacion> ipRepo = new Repository<ImagenPublicacion>();
                    for (int i = 0; i < datos.rutasImagenes.Count; i++)
                    {
                        ImagenPublicacion imagenPublicacion = new ImagenPublicacion();
                        if (i == 0)
                        {
                            imagenPublicacion.imagenRepresentativa = true;
                        }
                        else
                        {
                            imagenPublicacion.imagenRepresentativa = false;
                        }


                        imagenPublicacion.imagenPublicacionId = System.Guid.NewGuid();
                        imagenPublicacion.publicacionId = publicacionId;

                        //imagenPublicacion.imagen = datos.imagenes[i];
                        imagenPublicacion.rutaBD = datos.rutasImagenes[i];
                        
                        ipRepo.Crear(imagenPublicacion, db);
                    }
                    #endregion

                    string actividad = "Creó un nuevo inmueble, con la dirección " + datos.direccionFormateada;
                    ServiciosUsuario.RegistrarActividad(actividad,datos.usuarioId,db);

                    #region Guardar Cambios
                    pubRepo.Guardar(db);
                    database.Commit();
                    #endregion

                    error.codigoError = (int)Enums.CodigosError.codNoError;
                    //Envio correo de aviso nueva publicacion
                    await serviciosCorreo.EnviarCorreoAvisoNuevaPublicacion(publicacionId);

                }
                catch (Exception)
                {
                    
                    error.codigoError = (int)Enums.CodigosError.codErrorNuevaPublicacion;
                    error.descripcionError.Add(NotificacionesServicios.errorNuevaPublicacion);
                    database.Rollback();
                    
                }
            }

            return error;
        }
        public static string GetHTTP(string url) {
            WebRequest solicitudWeb = WebRequest.Create(url);
            WebResponse respuestaWeb = solicitudWeb.GetResponse();
            StreamReader sr = new StreamReader(respuestaWeb.GetResponseStream());
            return sr.ReadToEnd();

        }

        public DTOVistaNuevaPublicacion PrepararDatosNuevaPublicacion() {
            DTOVistaNuevaPublicacion dtoNuevaPublicacion = new DTOVistaNuevaPublicacion();
            using (var db = new ApplicationDbContext())
            {
                //Obtengo los paises
                
                List<TipoMoneda> listadoTipoMoneda = (from tipoMoneda in db.TipoMoneda orderby tipoMoneda.nombreTipoMoneda ascending
                                                      select tipoMoneda).ToList();
                List<TipoPublicacion> listadoTipoOperacion = (from tipoOperacion in db.TipoPublicacion
                                                              orderby tipoOperacion.nombreTipoPublicacion descending
                                                              select tipoOperacion).ToList();
                List<TipoConstruccion> listadoTipoConstruccion = (from tipoConstruccion in db.TipoConstruccion
                                                                  orderby tipoConstruccion.nombreTipoConstruccion descending
                                                                  select tipoConstruccion).ToList();
                List<TipoUser> listadoTipoUsuario = (from tipoUsuario in db.TipoUser
                                                     orderby tipoUsuario.nombreTipoUser descending
                                                     select tipoUsuario).ToList();
                List<TipoPropiedad> listadoTipoPropiedad = (from tipoPropiedad in db.TipoPropiedad
                                                            orderby tipoPropiedad.nombreTipoPropiedad ascending
                                                            select tipoPropiedad).ToList();
                List<TipoAmbiente> listadoTipoAmbiente = (from tipoAmbiente in db.TipoAmbiente
                                                          orderby tipoAmbiente.nombreTipoAmbiente ascending
                                                          select tipoAmbiente).ToList();
                List<Extras> listadoExtras = (from extra in db.Extras
                                              orderby extra.nombreExtra ascending
                                              select extra).ToList();

                //Empaqueto datos para mostrarlos a la interfaz
                dtoNuevaPublicacion.Inicializar();
                
                foreach (var tipoMoneda in listadoTipoMoneda)
                {
                    DTOTipoMoneda dtoTipoMoneda = new DTOTipoMoneda();
                    dtoTipoMoneda.tipoMonedaId = tipoMoneda.tipoMonedaId;
                    dtoTipoMoneda.nombreTipoMoneda = tipoMoneda.nombreTipoMoneda;
                    dtoNuevaPublicacion.AddDTOTipoMoneda(dtoTipoMoneda);
                }
                foreach (var tipoOperacion in listadoTipoOperacion)
                {
                    DTOTipoOperacion dtoTipoOperacion = new DTOTipoOperacion();
                    dtoTipoOperacion.tipoOperacionId = tipoOperacion.tipoPublicacionId;
                    dtoTipoOperacion.nombreTipoOperacion = tipoOperacion.nombreTipoPublicacion;
                    dtoNuevaPublicacion.AddDTOTipoOperacion(dtoTipoOperacion);
                }
                foreach (var tipoConstruccion in listadoTipoConstruccion)
                {
                    DTOTipoConstruccion dtoTipoconstruccion = new DTOTipoConstruccion();
                    dtoTipoconstruccion.tipoConstruccionId = tipoConstruccion.tipoConstuccionId;
                    dtoTipoconstruccion.nombreTipoConstruccion = tipoConstruccion.nombreTipoConstruccion;
                    dtoNuevaPublicacion.AddDTOTipoConstruccion(dtoTipoconstruccion);
                }
                foreach (var tipoUsuario in listadoTipoUsuario)
                {
                    DTOTipoUsuario dtoTipousuario = new DTOTipoUsuario();
                    dtoTipousuario.tipoUsuarioId = tipoUsuario.tipoUserId;
                    dtoTipousuario.nombreTipoUsuario = tipoUsuario.nombreTipoUser;
                    dtoNuevaPublicacion.AddDTOTipoUsuario(dtoTipousuario);

                }
                foreach (var tipoPropiedad in listadoTipoPropiedad)
                {
                    DTOTipoPropiedad dtoTipoPropiedad = new DTOTipoPropiedad();
                    dtoTipoPropiedad.tipoPropiedadId = tipoPropiedad.tipoPropiedadId;
                    dtoTipoPropiedad.nombreTipoPropiedad = tipoPropiedad.nombreTipoPropiedad;
                    dtoNuevaPublicacion.AddDTOTipoPropiedad(dtoTipoPropiedad);
                }
                foreach (var tipoAmbiente in listadoTipoAmbiente)
                {
                    DTOTipoAmbiente dtoTipoAmbiente = new DTOTipoAmbiente();
                    dtoTipoAmbiente.tipoAmbienteId = tipoAmbiente.tipoAmbienteId;
                    dtoTipoAmbiente.nombreTipoAmbiente = tipoAmbiente.nombreTipoAmbiente;
                    dtoNuevaPublicacion.AddDTOTipoAmbiente(dtoTipoAmbiente);
                }
                foreach (var extra in listadoExtras)
                {
                    DTOExtras dtoExtras = new DTOExtras();
                    dtoExtras.extraId = extra.extraId;
                    dtoExtras.nombreExtra = extra.nombreExtra;
                    dtoNuevaPublicacion.AddDTOExtras(dtoExtras);

                }
                return dtoNuevaPublicacion;
            }
        }
        public DTOVistaEditarPublicacion PrepararDatosEditarPublicacion(Guid publicacionId) {
            using (var db = new ApplicationDbContext())
            {
                
                try
                {
                    Publicacion publicacion = (from pub in db.Publicacion
                                               where pub.publicacionId == publicacionId
                                               select pub).FirstOrDefault();


                    DTOVistaEditarPublicacion dto = new DTOVistaEditarPublicacion();
                    dto.publicacionId = publicacion.publicacionId;
                    
                    dto.tipoUsuarioNuevo = publicacion.TipoUser.tipoUserId;
                    dto.tipoUsuarioViejo = publicacion.TipoUser.nombreTipoUser;
                    dto.tipoOperacionNueva = publicacion.TipoPublicacion.tipoPublicacionId;
                    dto.tipoOperacionVieja = publicacion.TipoPublicacion.nombreTipoPublicacion;
                    dto.direccion = publicacion.Propiedad.direccionFormateada;
                    dto.precioPropiedad = publicacion.precioPropiedad;
                    dto.tipoMonedaNueva = publicacion.TipoMoneda.tipoMonedaId;
                    dto.tipoMonedaVieja = publicacion.TipoMoneda.nombreTipoMoneda;
                    dto.tipoConstruccionNueva = publicacion.Propiedad.TipoConstruccion.tipoConstuccionId;
                    dto.tipoConstruccionVieja = publicacion.Propiedad.TipoConstruccion.nombreTipoConstruccion;
                    dto.tipoPropiedadNueva = publicacion.Propiedad.TipoPropiedad.tipoPropiedadId;
                    dto.tipoPropiedadVieja = publicacion.Propiedad.TipoPropiedad.nombreTipoPropiedad;
                    dto.comentarios = publicacion.observaciones;
                    dto.latitud = publicacion.Propiedad.latitud;
                    dto.longitud = publicacion.Propiedad.longitud;
                    //Obtengo los tipos de ambientes que eligio con sus cantidades
                    List<PropiedadTipoAmbiente> propTipoAmbiente = (from x in db.PropiedadTipoAmbiente
                                                                    where x.propiedadId == publicacion.propiedadId
                                                                    select x).ToList();
                    foreach (var pta in propTipoAmbiente) {
                        string nombreTipoAmbiente = pta.TipoAmbiente.nombreTipoAmbiente;
                        switch (nombreTipoAmbiente) {
                            case "Baños":
                                dto.cantidadBañosElegidos = pta.cantidadAmbientes;
                                break;
                            case "Dormitorios":
                                dto.cantidadDormitoriosElegidos = pta.cantidadAmbientes;
                                break;
                            case "Cocheras":
                                dto.cantidadCocherasElegidas = pta.cantidadAmbientes;
                                break;
                            case "Ambientes":
                                dto.cantidadAmbientesElegidos = pta.cantidadAmbientes;
                                break;
                        }
                    }

                    List<TipoAmbiente> tiposAmbientes = (from ta in db.TipoAmbiente
                                                         select ta).ToList();
                    foreach (var tipoAmbiente in tiposAmbientes) {
                        DTOTipoAmbiente dtoTipoAmbiente = new DTOTipoAmbiente();
                        dtoTipoAmbiente.tipoAmbienteId = tipoAmbiente.tipoAmbienteId;
                        dtoTipoAmbiente.nombreTipoAmbiente = tipoAmbiente.nombreTipoAmbiente;
                        dto.AgregarDTOTipoAmbiente(dtoTipoAmbiente);
                    }
                    //Obtengo los extras que eligio
                    
                    List<PropiedadExtras> propExtras = (from e in db.PropiedadExtras
                                  where e.propiedadId == publicacion.propiedadId && e.activo==true
                                  select e).ToList();
                    foreach (var pe in propExtras) {
                        DTOExtrasPrevios extrasPrevios = new DTOExtrasPrevios();
                        extrasPrevios.extraId = pe.extrasId;
                        extrasPrevios.nombreExtra = pe.Extras.nombreExtra;
                        dto.AgregarDTOExtraPrevio(extrasPrevios);
                    }

                    List<Extras> extrasFiltrados = new List<Extras>();

                    var extrasDisponibles = (from e in db.Extras
                              orderby e.nombreExtra descending
                              select e).ToList();

                    for (int i=0;i<extrasDisponibles.Count;i++) {
                        extrasFiltrados.Add(extrasDisponibles[i]);
                        for (int j=0;j<propExtras.Count;j++) {
                            if (extrasDisponibles[i].nombreExtra == propExtras[j].Extras.nombreExtra) {
                                extrasFiltrados.Remove(extrasDisponibles[i]);
                            }
                        }
                    }

                    
                    foreach (var extra in extrasFiltrados) {
                        DTOExtras dtoExtras = new DTOExtras();
                        dtoExtras.extraId = extra.extraId;
                        dtoExtras.nombreExtra = extra.nombreExtra;
                        dto.AgregarDTOExtra(dtoExtras);
                    }

                    List<TipoMoneda> tiposMonedas = (from moneda in db.TipoMoneda
                                                     where moneda.nombreTipoMoneda != dto.tipoMonedaVieja
                                                     select moneda).ToList();
                    foreach (var moneda in tiposMonedas) {
                        DTOTipoMoneda dtoTipoMoneda = new DTOTipoMoneda();
                        dtoTipoMoneda.tipoMonedaId = moneda.tipoMonedaId;
                        dtoTipoMoneda.nombreTipoMoneda = moneda.nombreTipoMoneda;
                        dto.AgregarDTOTipoMoneda(dtoTipoMoneda);
                    }

                    List<TipoPropiedad> tiposPropiedades = (from tipoProp in db.TipoPropiedad
                                                            where tipoProp.nombreTipoPropiedad != dto.tipoPropiedadVieja && tipoProp.activo == true
                                                            select tipoProp).ToList();
                    foreach (var tipoPropiedad in tiposPropiedades) {
                        DTOTipoPropiedad dtoTipoPropiedad = new DTOTipoPropiedad();
                        dtoTipoPropiedad.nombreTipoPropiedad = tipoPropiedad.nombreTipoPropiedad;
                        dtoTipoPropiedad.tipoPropiedadId = tipoPropiedad.tipoPropiedadId;
                        dto.AgregarDTOTipoPropiedad(dtoTipoPropiedad);
                    }

                    List<TipoConstruccion> tiposConstrucciones = (from tipoCon in db.TipoConstruccion
                                                                  where tipoCon.nombreTipoConstruccion != dto.tipoConstruccionVieja
                                                                  select tipoCon).ToList();
                    foreach (var tipoConstruccion in tiposConstrucciones) {
                        DTOTipoConstruccion dtoTipoConstruccion = new DTOTipoConstruccion();
                        dtoTipoConstruccion.nombreTipoConstruccion = tipoConstruccion.nombreTipoConstruccion;
                        dtoTipoConstruccion.tipoConstruccionId = tipoConstruccion.tipoConstuccionId;
                        dto.AgregarDTOTipoConstruccion(dtoTipoConstruccion);
                    }

                    

                    

                    

                    List<TipoUser> tiposUsuarios = (from tipoU in db.TipoUser
                                                    where tipoU.nombreTipoUser != dto.tipoUsuarioViejo
                                                    select tipoU).ToList();
                    foreach (var tipoUsuario in tiposUsuarios) {
                        DTOTipoUsuario dtoTipoUsuario = new DTOTipoUsuario();
                        dtoTipoUsuario.tipoUsuarioId = tipoUsuario.tipoUserId;
                        dtoTipoUsuario.nombreTipoUsuario = tipoUsuario.nombreTipoUser;
                        dto.AgregarDTOTipoUsuario(dtoTipoUsuario);
                    }

                    List<TipoPublicacion> tiposPublicaciones = (from tipoPub in db.TipoPublicacion
                                                                where tipoPub.nombreTipoPublicacion != dto.tipoOperacionVieja
                                                                select tipoPub).ToList();
                    foreach (var tipoPublicacion in tiposPublicaciones) {
                        DTOTipoOperacion dtoTipoOperacion = new DTOTipoOperacion();
                        dtoTipoOperacion.tipoOperacionId = tipoPublicacion.tipoPublicacionId;
                        dtoTipoOperacion.nombreTipoOperacion = tipoPublicacion.nombreTipoPublicacion;
                        dto.AgregarDTOTipoOperacion(dtoTipoOperacion);
                    }
                    return dto;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }

        }
        public DTOError EditarPublicacion(DTOVistaEditarPublicacion datos) {
            DTOError error = new DTOError();
            Repository<Publicacion> pubRepo = new Repository<Publicacion>();
            Repository<PropiedadExtras> propexRepo = new Repository<PropiedadExtras>();

            using (var db = new ApplicationDbContext()) {
                using (var database = db.Database.BeginTransaction()) {
                    try
                    {

                        Publicacion pub = pubRepo.Buscar(x => x.publicacionId == datos.publicacionId, db).FirstOrDefault();

                        //var pub = db.Publicacion.Find(datos.publicacionId);
                        if (pub != null)
                        {

                            pub.Propiedad.tipoConstruccionId = datos.tipoConstruccionNueva;
                            pub.tipoPublicacionId = datos.tipoOperacionNueva;
                            pub.tipoMonedaId = datos.tipoMonedaNueva;
                            pub.precioPropiedad = datos.precioPropiedad;
                            pub.Propiedad.direccionFormateada = datos.direccion;
                            pub.observaciones = datos.comentarios;
                            foreach (var propTipoAmbiente in pub.Propiedad.PropiedadTipoAmbiente)
                            {
                                switch (propTipoAmbiente.TipoAmbiente.nombreTipoAmbiente)
                                {
                                    case "Dormitorio":
                                        propTipoAmbiente.cantidadAmbientes = datos.cantidadDormitoriosElegidos;
                                        break;
                                    case "Baño":
                                        propTipoAmbiente.cantidadAmbientes = datos.cantidadBañosElegidos;
                                        break;
                                    case "Cochera":
                                        propTipoAmbiente.cantidadAmbientes = datos.cantidadCocherasElegidas;
                                        break;
                                    case "Ambientes":
                                        propTipoAmbiente.cantidadAmbientes = datos.cantidadAmbientesElegidos;
                                        break;
                                }
                            }

                            //Si no tenia extras los creo
                            if (pub.Propiedad.PropiedadExtras.Count == 0)
                            {
                                foreach (var extrasElegidos in datos.extrasIdElegidos)
                                {
                                    PropiedadExtras pe = new PropiedadExtras();
                                    pe.propiedadExtrasId = System.Guid.NewGuid();
                                    pe.extrasId = extrasElegidos;
                                    pe.propiedadId = pub.propiedadId;
                                    pe.activo = true;
                                    propexRepo.Crear(pe, db);
                                    //db.PropiedadExtras.Add(pe);

                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                if (datos.extrasIdElegidos != null)
                                {
                                    //Desactivo todos los extras previos
                                    foreach (var extras in pub.Propiedad.PropiedadExtras)
                                    {

                                        extras.activo = false;
                                    }
                                    //Creo los extras nuevos y los marco como activos
                                    foreach (var extrasElegidos in datos.extrasIdElegidos)
                                    {
                                        PropiedadExtras pe = new PropiedadExtras();
                                        pe.propiedadExtrasId = System.Guid.NewGuid();
                                        pe.extrasId = extrasElegidos;
                                        pe.propiedadId = pub.propiedadId;
                                        pe.activo = true;
                                        propexRepo.Crear(pe, db);
                                        //db.PropiedadExtras.Add(pe);
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    foreach (var extras in pub.Propiedad.PropiedadExtras)
                                    {

                                        extras.activo = false;
                                    }
                                }


                            }


                            pubRepo.Editar(pub, db);
                            string actividad = "Editó un nuevo inmueble, con la dirección " + datos.direccion;
                            ServiciosUsuario.RegistrarActividad(actividad,pub.Propiedad.ApplicationUser.Id, db);
                            //db.Entry(pub).State = System.Data.Entity.EntityState.Modified;
                            pubRepo.Guardar(db);
                            database.Commit();
                            //db.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        error.codigoError = (int)Enums.CodigosError.codErrorEditarPublicacion;
                        error.descripcionError.Add(NotificacionesServicios.errorEditarPublicacion);
                        database.Rollback();
                    }

                    
                }
                    
                    
                        
                    
                }
            
            return error;
        }
        
    }
}