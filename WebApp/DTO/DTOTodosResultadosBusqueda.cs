using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOTodosResultadosBusqueda
    {
        public List<DTOResultadoBusquedaPublicacion> ListaDtoResultadoPublicacion = new List<DTOResultadoBusquedaPublicacion>();
        public List<DTOMarcadorGoogleMaps> ListaDTOCoordProp = new List<DTOMarcadorGoogleMaps>();
        public bool monedaequivalente { get; set; }
        public string tipomonedaseleccionada { get; set; }
        public void AgregarDTOUnResultadoPublicacion(DTOResultadoBusquedaPublicacion dto) {
            ListaDtoResultadoPublicacion.Add(dto);
        }
        public void AgregarDTOCoordenadasPropiedad(DTOMarcadorGoogleMaps dto) {
            ListaDTOCoordProp.Add(dto);
        }
    }
}