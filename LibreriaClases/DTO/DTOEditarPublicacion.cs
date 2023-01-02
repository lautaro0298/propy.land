using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOEditarPublicacion
    {
        public string publicacionId { get; set; }
        #region datos seleccionados previamente por el usuario
        public string ubicacionAnterior { get; set; }
        public bool amueblado { get; set; }
        public string tipoPublicacionElegidaAnteriormente { get; set; }
        public string reseñaAnterior { get; set; }
        public string tipoPropiedadElegidaAnteriormente { get; set; }
        public string tipoPublicanteElegidoAnteriormente { get; set; }
        public string tipoConstruccionElegidaAnteriormente { get; set; }
        public int añosAntiguedadElegidosAnteriormente { get; set; }
        public int cantidadBañosElegidosAnteriormente { get; set; }
        public int cantidadAmbientesElegidosAnteriormente { get; set; }
        public int cantidadDormitorioesElegidosAnteriormente { get; set; }
        public int cantidadCocherasElegidasAnteriormente { get; set; }
        public Int64 precioPropiedadAnterior { get; set; }
        public string tipoMonedaElegidaAnteriormente { get; set; }
        public int supTerrenoElegidaAnteriormente { get; set; }
        public int supCubiertaElegidaAnteriormente { get; set; }
        public int nroPisosElegidosAnteriormente { get; set; }
        public decimal importeExpensasAnterior { get; set; }
        public List<string> extrasElegidosAnteriormente { get; set; }
        
        public List<DTOImagenAnterior> imagenesAnteriores { get; set; }
        #endregion

        #region Datos disponibles de la bd
        public List<DTOTipoPublicante> listadoTiposPublicantes { get; set; } = new List<DTOTipoPublicante>();
        public List<DTOTipoPublicacion> listadoTiposPublicaciones { get; set; } = new List<DTOTipoPublicacion>();
        public List<DTOTipoPropiedad> listadoTiposPropiedades { get; set; } = new List<DTOTipoPropiedad>();
        public List<DTOTipoConstruccion> listadoTiposConstruccion { get; set; } = new List<DTOTipoConstruccion>();
        public List<DTOCaracteristica> listadoCaracteristicas { get; set; } = new List<DTOCaracteristica>();
        public List<DTOTipoAmbiente> listadoTiposAmbientes { get; set; } = new List<DTOTipoAmbiente>();
        public List<DTOTipoMoneda> listadoTiposMonedas { get; set; } = new List<DTOTipoMoneda>();
        #endregion

    }
}
