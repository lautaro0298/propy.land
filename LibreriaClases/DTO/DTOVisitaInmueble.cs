using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOVisitaInmueble
    {
        public string publicacionId { get; set; }
        public string precioPropiedad { get; set; }
        public string tipoMoneda { get; set; }
        public string ubicacionPropiedad { get; set; }
        public string tipoConstruccion { get; set; }
        public List<string> tipoPropiedad { get; set; }
        public string tipoPublicacion { get; set; }
        public string tipoPublicante { get; set; }
        public string reseña { get; set; }
        public int cantidadBaños { get; set; }
        public int cantidadAmbientes { get; set; }
        public int cantidadDormitorios { get; set; }
        public int cantidadCocheras { get; set; }
        public int superficieTerreno { get; set; }
        public int superficieCubierta { get; set; }
        public bool inmueblePagaExpensas { get; set; }
        public string importeExpensasUltimoMes { get; set; }
        public bool amueblado { get; set; }
        public List<string> extras { get; set; }
        public List<string> imagenes { get; set; }

        public DTOVisitaInmueble() {
            extras = new List<string>();
            imagenes = new List<string>();
        }
    }
}
