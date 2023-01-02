using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOContenedorResultadosBusqueda
    {
        public List<DTOPropiedad> propiedades { get; set; }
        public List<DTOPincheGoogleMaps> pinchesGoogleMaps { get; set; }

        public DTOContenedorResultadosBusqueda() {
            propiedades = new List<DTOPropiedad>();
            pinchesGoogleMaps = new List<DTOPincheGoogleMaps>();
        }
    }
}
