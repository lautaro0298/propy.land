using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class Propiedad
    {
        public string propiedadId { get; set; }
        public string descripcionPropiedad { get; set; }
        public string ubicacion { get; set; }
        public string pais { get; set; }
        public string AreaAdmNivel1 { get; set; }
        public string AreaAdmNivel2 { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public decimal precioPropiedad { get; set; }
        public DateTime fechaRegistro { get; set; }
        public int nroPisos { get; set; }
        public int superficieTerreno { get; set; }
        public int superficieCubierta { get; set; }
        public bool amueblado { get; set; }
        public decimal importeExpensasUltimoMes { get; set; }
        public int añosAntiguedad { get; set; }
        public string tipoPublicanteId { get; set; }
        public string tipoConstruccionId { get; set; }
        public string tipoPropiedadId { get; set; }
        public string usuarioId { get; set; }
        public string tipoMonedaId { get; set; }
        public virtual TipoPublicante TipoPublicante { get; set; }
        public virtual TipoConstruccion TipoConstruccion { get; set; }
        //editar todas las referencias para eliminar conflictos
        
   
        public virtual ICollection<PropiedadTipoAmbiente> PropiedadTipoAmbiente { get; set; }
        public virtual ICollection<ImagenPropiedad> ImagenPropiedad { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual TipoMoneda TipoMoneda { get; set; }
    }
}
