using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Servicios;
using WebApp.DTO;

namespace WebApp.Servicios
{
    public class RealizarPublicacionServicios
    {
        public DTOVistaNuevaPublicacion PrepararDatosPublicacion() {
            DTOVistaNuevaPublicacion dtoNuevaPublicacion = new DTOVistaNuevaPublicacion();
            using (var db = new ApplicationDbContext()) {
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

                //Empaqueto datos para mostrarlos a la interfaz
                
                foreach (var tipoMoneda in listadoTipoMoneda) {
                    DTOTipoMoneda dtoTipoMoneda = new DTOTipoMoneda();
                    dtoTipoMoneda.tipoMonedaId = tipoMoneda.tipoMonedaId;
                    dtoTipoMoneda.nombreTipoMoneda = tipoMoneda.nombreTipoMoneda;
                    dtoNuevaPublicacion.AddDTOTipoMoneda(dtoTipoMoneda);
                }
                foreach (var tipoOperacion in listadoTipoOperacion) {
                    DTOTipoOperacion dtoTipoOperacion = new DTOTipoOperacion();
                    dtoTipoOperacion.tipoOperacionId = tipoOperacion.tipoPublicacionId;
                    dtoTipoOperacion.nombreTipoOperacion = tipoOperacion.nombreTipoPublicacion;
                    dtoNuevaPublicacion.AddDTOTipoOperacion(dtoTipoOperacion);
                }
                foreach (var tipoConstruccion in listadoTipoConstruccion) {
                    DTOTipoConstruccion dtoTipoconstruccion = new DTOTipoConstruccion();
                    dtoTipoconstruccion.tipoConstruccionId = tipoConstruccion.tipoConstuccionId;
                    dtoTipoconstruccion.nombreTipoConstruccion = tipoConstruccion.nombreTipoConstruccion;
                    dtoNuevaPublicacion.AddDTOTipoConstruccion(dtoTipoconstruccion);
                }
                foreach (var tipoUsuario in listadoTipoUsuario) {
                    DTOTipoUsuario dtoTipousuario = new DTOTipoUsuario();
                    dtoTipousuario.tipoUsuarioId = tipoUsuario.tipoUserId;
                    dtoTipousuario.nombreTipoUsuario = tipoUsuario.nombreTipoUser;
                    dtoNuevaPublicacion.AddDTOTipoUsuario(dtoTipousuario);
                    
                }
                foreach (var tipoPropiedad in listadoTipoPropiedad) {
                    DTOTipoPropiedad dtoTipoPropiedad = new DTOTipoPropiedad();
                    dtoTipoPropiedad.tipoPropiedadId = tipoPropiedad.tipoPropiedadId;
                    dtoTipoPropiedad.nombreTipoPropiedad = tipoPropiedad.nombreTipoPropiedad;
                    dtoNuevaPublicacion.AddDTOTipoPropiedad(dtoTipoPropiedad);
                }
                return dtoNuevaPublicacion;
            }
        }
    }
}