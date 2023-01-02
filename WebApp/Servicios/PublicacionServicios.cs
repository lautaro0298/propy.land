using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.ViewModels;
using WebApp.DTO;
using System.IO;

namespace WebApp.Servicios
{
    public class PublicacionServicios
    {
        public DTOVistaNuevaPublicacion PrepararDatosPublicacion()
        {
            DTOVistaNuevaPublicacion dtoNuevaPublicacion = new DTOVistaNuevaPublicacion();
            using (var db = new ApplicationDbContext())
            {
                //Obtengo los paises
                
                List<TipoMoneda> listadoTipoMoneda = (from tipoMoneda in db.TipoMoneda
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
                                                            orderby tipoPropiedad.nombreTipoPropiedad descending
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
                foreach (var tipoAmbiente in listadoTipoAmbiente) {
                    DTOTipoAmbiente dtoTipoAmbiente = new DTOTipoAmbiente();
                    dtoTipoAmbiente.tipoAmbienteId = tipoAmbiente.tipoAmbienteId;
                    dtoTipoAmbiente.nombreTipoAmbiente = tipoAmbiente.nombreTipoAmbiente;
                    dtoNuevaPublicacion.AddDTOTipoAmbiente(dtoTipoAmbiente);
                }
                foreach (var extra in listadoExtras) {
                    DTOExtras dtoExtras = new DTOExtras();
                    dtoExtras.extraId = extra.extraId;
                    dtoExtras.nombreExtra = extra.nombreExtra;
                    dtoNuevaPublicacion.AddDTOExtras(dtoExtras);

                }
                return dtoNuevaPublicacion;
            }
        }

        public List<DTOListarPublicacion> ListarPublicaciones(string usuario)
        {
            using (var db = new ApplicationDbContext())
            {
                var data = from publicacionEstado in db.PublicacionEstado
                           join publicacion in db.Publicacion on publicacionEstado.publicacionId equals publicacion.publicacionId
                           join estadoPublicacion in db.EstadoPublicacion on publicacionEstado.estadoPublicacionId equals estadoPublicacion.estadoPublicacionId
                           join tipoOperacion in db.TipoPublicacion on publicacion.tipoPublicacionId equals tipoOperacion.tipoPublicacionId
                           join propiedad in db.Propiedad on publicacion.propiedadId equals propiedad.propiedadId
                           join tipoPropiedad in db.TipoPropiedad on propiedad.tipoPropiedadId equals tipoPropiedad.tipoPropiedadId
                           where publicacion.UserId == usuario
                           orderby publicacion.fechaInicioPublicacion descending
                           select new DTOListarPublicacion
                           {
                               publicacionId = publicacion.publicacionId,
                               fechaInicioPublicacion = publicacion.fechaInicioPublicacion,
                               fechaFinPublicacion = publicacion.fechaFinPublicacion,
                               estado = estadoPublicacion.nombreEstadoPublicacion,
                               tipoOperacion = tipoOperacion.nombreTipoPublicacion,
                               propiedad = tipoPropiedad.nombreTipoPropiedad,
                               direccion = propiedad.direccionFormateada,
                               antiguedad = propiedad.antiguedad


                           };

                return data.ToList();
            }
        }

        public Guid CrearPublicacion(Guid tipoMoneda,Guid tipoOperacion,Guid tipoUsuario, Guid propiedadId,string userId, List<byte[]> imagen,decimal precioPropiedad)
        {
            using (var db = new ApplicationDbContext())
            {
                try
                {
                    //Creo identificador unico de Publicacion
                    Guid publicacionId = System.Guid.NewGuid();
                    //Configuro fechas
                    DateTime fechaInicioPublicacion = DateTime.Now;
                    DateTime fechaFinPublicacion = fechaInicioPublicacion.AddDays(15);

                    //Creo instancia de tipo Publicacion
                    Publicacion p = new Publicacion();

                    //Configuro instancia con valores
                    p.publicacionId = publicacionId;
                    p.fechaInicioPublicacion = fechaInicioPublicacion;
                    p.fechaFinPublicacion = fechaFinPublicacion;
                    p.tipoPublicacionId = tipoOperacion;
                    p.tipoUserId = tipoUsuario;
                    p.propiedadId = propiedadId;
                    p.UserId = userId;
                    //p.imagen = imagen;
                    p.tipoMonedaId = tipoMoneda;
                    p.precioPropiedad = precioPropiedad;

                    db.Publicacion.Add(p);
                    db.SaveChanges();
                    return publicacionId;
                }
                catch (Exception ex)
                {

                    throw ex;
                }


            }
        }
        public DTOPublicacion EditarPublicacion(Guid id)
        {
            using (var db = new ApplicationDbContext())
            {
                List<DTOPublicacion> data = (from p in db.Publicacion
                                             join prop in db.Propiedad on p.propiedadId equals prop.propiedadId
                                             where p.publicacionId == id
                                             select new DTOPublicacion
                                             {
                                                 publicacionId = p.publicacionId,
                                                 
                                             }).ToList();


                DTOPublicacion dtoPublicacion = new DTOPublicacion();
                dtoPublicacion.publicacionId = data.First().publicacionId;
                dtoPublicacion.precioPropiedad = data.First().precioPropiedad;
                return dtoPublicacion;
            }

        }

        public List<byte[]> ConvertToBytes(List<HttpPostedFileBase> image)
        {
            List<byte[]> imageBytes = new List<byte[]>();
            foreach (var imagenSubida in image) {
                
                BinaryReader reader = new BinaryReader(imagenSubida.InputStream);
                byte[] imagenLeida = reader.ReadBytes((int)imagenSubida.ContentLength);
                imageBytes.Add(imagenLeida);
                
                
            }
            return imageBytes;
        }

        /*public List<byte[]> ObtenerImagenBD(Guid publicacionId)
        {
            List<byte[]> imagenesDevueltas = new List<byte[]>();
            using (var db = new ApplicationDbContext())
            {
                var q = (from temp in db.Publicacion where temp.publicacionId == publicacionId select temp).FirstOrDefault();
                foreach (var item in q.ImagenPublicacion) {
                    imagenesDevueltas.Add(item.imagen);
                }
                return imagenesDevueltas;
            }

        }*/
        public List<Publicacion> ResultadoBusquedaGenericaPublicaciones()
        {
            
            using (var db = new ApplicationDbContext())
            {
                var data = (from p in db.Publicacion
                           where p.fechaFinPublicacion >= DateTime.Now
                           select p).ToList();

                foreach (var item in data) {
                   
                }

                return data;
            }
        }
    }
}