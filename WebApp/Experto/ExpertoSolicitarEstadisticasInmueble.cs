using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DTO;
using WebApp.Fachada;
using WebApp.Models;

namespace WebApp.Experto
{
    public class ExpertoSolicitarEstadisticasInmueble
    {
        public DTOListadoEstadisticas SolicitarEstadisticasInmueble(Guid publicacionId) {
            int contadorVisitas = 0;
            DTOListadoEstadisticas datosTodasEstadisticas = new DTOListadoEstadisticas();
            try
            {
                List<ClickPublicacion> clickPublicacion = new List<ClickPublicacion>();
                using (var db = new ApplicationDbContext()) {
                    var clicksRealizados = (from clicks in db.ClickPublicacion
                                            where clicks.publicacionId == publicacionId
                                            group clicks by clicks.UserId into g
                                            select g).ToList();
                    
                    foreach (var grupo in clicksRealizados) {
                        contadorVisitas = 0;
                        foreach (var objetoAgrupado in grupo) {
                            contadorVisitas++;
                        }
                        List<ClickPublicacion> clicksRealizadoPorUnUsuario = (from x in db.ClickPublicacion
                                                                              where x.UserId == grupo.Key && x.publicacionId==publicacionId
                                                                              orderby x.fechaHoraClickPublicacion
                                                                              select x).ToList();
                        
                        ClickPublicacion obj = clicksRealizadoPorUnUsuario.Last();
                        TimeZoneInfo zonaHorariaArgentina = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
                        DTOEstadistica datosEstadistica = new DTOEstadistica();
                        datosEstadistica.cantidadVisitas = contadorVisitas;
                        datosEstadistica.fechaHoraClickInteresado = TimeZoneInfo.ConvertTimeFromUtc(obj.fechaHoraClickPublicacion, zonaHorariaArgentina);
                        datosEstadistica.nombreInteresado = obj.ApplicationUser.nombre;
                        datosEstadistica.permiteMostrarDatos = obj.ApplicationUser.permitirSerContactadoPublicante;
                        datosTodasEstadisticas.propiedad = obj.Publicacion.Propiedad.direccionFormateada;
                        datosTodasEstadisticas.AgregarDTOEstadistica(datosEstadistica);
                        
                        ClickContactoPublicante objClickContactoPublicante = (from x in db.ClickContactoPublicante
                                                                              where x.publicacionId == publicacionId && x.UserId == grupo.Key
                                                                              select x).FirstOrDefault();
                        if (objClickContactoPublicante != null) {
                            datosEstadistica.visitaAlPublicante = true;
                        }
                        else {
                            datosEstadistica.visitaAlPublicante = false;
                        }
                    }
                    
                    
                }
                return datosTodasEstadisticas;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}