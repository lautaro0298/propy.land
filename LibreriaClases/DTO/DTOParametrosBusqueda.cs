using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOParametrosBusqueda
    {
        public List<DTOCaracteristica> caracteristicas { get; set; }
        public List<DTOTipoPropiedad> tiposPropiedades { get; set; }
        public List<DTOTipoAmbiente> tiposAmbientes { get; set; }
        public List<DTOTipoPublicante> tiposPublicantes { get; set; }
        public List<DTOTipoPublicacion> tiposPublicaciones { get; set; }
        public List<DTOTipoConstruccion> tiposConstrucciones { get; set; }
        public List<DTOTipoMoneda> tiposMonedas { get; set; }

        public DTOParametrosBusqueda() {
            caracteristicas = new List<DTOCaracteristica>();
            tiposPropiedades = new List<DTOTipoPropiedad>();
            tiposAmbientes = new List<DTOTipoAmbiente>();
            tiposPublicantes = new List<DTOTipoPublicante>();
            tiposPublicaciones = new List<DTOTipoPublicacion>();
            tiposConstrucciones = new List<DTOTipoConstruccion>();
            tiposMonedas = new List<DTOTipoMoneda>();
        }
    }
}
