using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using WebApp.DTO;
using WebApp.Fachada;
using WebApp.Models;
using WebApp.Servicios;

namespace WebApp.Experto
{
    public class ExpertoSolicitarDatosInmueble
    {
        public DTOEspecificacionInmueble SolicitarDatosInmueble(Guid publicacionId,string usuarioId) {
            DTOEspecificacionInmueble datos = new DTOEspecificacionInmueble();
            using (var db=new ApplicationDbContext()) {
                using (var database = db.Database.BeginTransaction()) {
                    try
                    {

                        Repository<Publicacion> pubRepo = new Repository<Publicacion>();


                        Publicacion pub = pubRepo.Buscar(x => x.publicacionId == publicacionId, db).FirstOrDefault();
                        datos.ubicacion = pub.Propiedad.direccionFormateada;
                        datos.tipoPropiedad = pub.Propiedad.TipoPropiedad.nombreTipoPropiedad;
                        datos.tipoConstrucción = pub.Propiedad.TipoConstruccion.nombreTipoConstruccion;
                        datos.tipoOperacion = pub.TipoPublicacion.nombreTipoPublicacion;
                        datos.publicacionId = pub.publicacionId;
                        datos.precioPropiedad = pub.precioPropiedad.ToString("N", CultureInfo.CreateSpecificCulture("es-AR"));
                        datos.comentarios = pub.observaciones;
                        datos.tipoMoneda = pub.TipoMoneda.nombreTipoMoneda;

                        Repository<PropiedadTipoAmbiente> ptaRepo = new Repository<PropiedadTipoAmbiente>();
                        List<PropiedadTipoAmbiente> instanciasPta = ptaRepo.Buscar(x => x.propiedadId == pub.propiedadId, db);
                        /*(from pta in db.PropiedadTipoAmbiente
                        where pta.propiedadId == pub.propiedadId
                        select pta).ToList();*/
                        foreach (var propiedadTipoAmbiente in instanciasPta)
                        {
                            switch (propiedadTipoAmbiente.TipoAmbiente.nombreTipoAmbiente)
                            {
                                case "Baños":
                                    datos.cantidadBaños = propiedadTipoAmbiente.cantidadAmbientes;
                                    break;
                                case "Dormitorios":
                                    datos.cantidadDormitorios = propiedadTipoAmbiente.cantidadAmbientes;
                                    break;
                                case "Cocheras":
                                    datos.cantidadCocheras = propiedadTipoAmbiente.cantidadAmbientes;
                                    break;
                                case "Ambientes":
                                    datos.cantidadAmbientes = propiedadTipoAmbiente.cantidadAmbientes;
                                    break;
                            }


                        }
                        Repository<PropiedadExtras> peRepo = new Repository<PropiedadExtras>();
                        List<PropiedadExtras> instanciasExtras = peRepo.Buscar(x => x.propiedadId == pub.propiedadId && x.activo == true, db);
                        /*List<PropiedadExtras> instanciasExtras = (from pe in db.PropiedadExtras
                                                                  where pe.propiedadId == pub.propiedadId
                                                                  select pe).ToList();*/
                        foreach (var extra in instanciasExtras)
                        {
                            datos.extras.Add(extra.Extras.nombreExtra);
                        }

                        var query = (from x in db.PropiedadExtras
                                     select x).ToList();

                        //List<ImagenPublicacion> imagenes = RepositorioImagenes.ObtenerListaPorPublicacion(pub.publicacionId,db);
                        List<ImagenPublicacion> imagenes = (from img in db.ImagenPublicacion
                                                            where img.publicacionId == publicacionId
                                                            select img).ToList();

                        foreach (var imagen in pub.ImagenPublicacion)
                        {
                            string ruta = ImagenServicios.ObtenerRutaRelativa(imagen);
                            datos.rutasImagenesBD.Add(ruta);
                        }
                        RegistrarClick(usuarioId, publicacionId);
                        string actividad = "Visitó la " + pub.Propiedad.TipoPropiedad.nombreTipoPropiedad + " ubicada en: " + pub.Propiedad.direccionFormateada;
                        ServiciosUsuario.RegistrarActividad(actividad, usuarioId, db);
                        database.Commit();
                    }
                    catch (Exception ex)
                    {

                        
                        database.Rollback();
                        
                    }
                }
                    
            }
                return datos;
        }
        
        public void RegistrarClick(string user, Guid publicacionId) {
            
            using (var db = new ApplicationDbContext()) {

                ClickPublicacion cp = new ClickPublicacion();
                cp.clickPublicacionId = System.Guid.NewGuid();
                cp.UserId = user;
                cp.publicacionId = publicacionId;
                cp.fechaHoraClickPublicacion = DateTime.UtcNow;

                db.ClickPublicacion.Add(cp);
                db.SaveChanges();
            }
        }
    }
}