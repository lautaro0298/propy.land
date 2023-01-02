using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DTO;

namespace WebApp.Servicios
{
    public class EstadisticasServicios
    {
        public DTOListadoEstadisticas ConsultarEstadisticas(Guid publicacionId) {
            using (var db = new ApplicationDbContext()) {
                var data = (from cp in db.ClickPublicacion
                            where cp.publicacionId.ToString() == publicacionId.ToString()
                            select cp).ToList();
                if (data.Count > 0)
                {
                    DTOListadoEstadisticas dtoListadoEstadisticas = new DTOListadoEstadisticas();
                    foreach (var item in data)
                    {
                        DTOEstadistica dtoEstadistica = new DTOEstadistica();
                        dtoEstadistica.nombreInteresado = item.ApplicationUser.nombre;
                        
                        dtoEstadistica.fechaHoraClickInteresado = item.fechaHoraClickPublicacion;
                        //dtoEstadistica.cantidadVisitas = item.cantidadVisitas;

                        dtoListadoEstadisticas.AgregarDTOEstadistica(dtoEstadistica);
                    }
                    return dtoListadoEstadisticas;
                }
                else {
                    
                    return null;
                }
            }
        }
    }
}