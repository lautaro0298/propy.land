using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOFiltrosBusqueda
    {
        public string ubicacion { get; set; }
        public string tipoPublicacion { get; set; }
        public string tipoPropiedad { get; set; }
        public string tipoConstruccion { get; set; }
        public string tipoPublicante { get; set; }
        public decimal precioDesde { get; set; }
        public decimal precioHasta { get; set; }
        public bool característicasEspecificasHabilitadas { get; set; }
        public int cantidadDormitorios { get; set; }
        public int cantidadAmbientes { get; set; }
        public int cantidadCocheras { get; set; }
        public int cantidadBaños { get; set; }
        public int radioBusqueda { get; set; }
        public double longitud { get; set; }
        public double latitud { get; set; }
        public string denominacionMoneda { get; set; }
        public List<string> extras { get; set; } = new List<string>();
    }
}
