using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOCrearPublicacion
    {
        public List<DTOTipoPublicante> listadoTiposPublicantes { get; set; } = new List<DTOTipoPublicante>();
        public List<DTOTipoPublicacion> listadoTiposPublicaciones { get; set; } = new List<DTOTipoPublicacion>();
        public List<DTOTipoPropiedad> listadoTiposPropiedades { get; set; } = new List<DTOTipoPropiedad>();
        public List<DTOTipoConstruccion> listadoTiposConstruccion { get; set; } = new List<DTOTipoConstruccion>();
        public List<DTOCaracteristica> listadoCaracteristicas { get; set; } = new List<DTOCaracteristica>();
        public List<DTOTipoAmbiente> listadoTiposAmbientes { get; set; } = new List<DTOTipoAmbiente>();
        public List<DTOTipoMoneda> listadoTiposMonedas { get; set; } = new List<DTOTipoMoneda>();
        public int cantidadImagenesDisponibles { get; set; }

    }
}
