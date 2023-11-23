using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaPropiedad
    {
        public string propiedadId { get; set; }
        public string descripcionPropiedad { get; set; }
        public string ubicacion { get; set; }
        public string pais { get; set; }
        public string AreaAdmNivel1 { get; set; }
        public string AreaAdmNivel2 { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public int nroPisos { get; set; }
        public decimal precioPropiedad { get; set; }
        public DateTime fechaRegistro { get; set; }
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
        public virtual TransferenciaTipoPublicante TipoPublicante { get; set; }
        public virtual TransferenciaTipoConstruccion TipoConstruccion { get; set; }
        // se quita por nuevo modelado
        //public virtual ICollection<TransferenciaPropiedadCaracteristica> PropiedadCaracteristica { get; set; }
        public virtual List<TransferenciaTipoPropiedad> TipoPropiedad { get; set; }
        public virtual ICollection<TransferenciaPropiedadTipoAmbiente> PropiedadTipoAmbiente { get; set; }
        public virtual ICollection<TransferenciaImagenPropiedad> ImagenPropiedad { get; set; }
         public virtual ICollection<TransferenciaImagenPropiedad> imagenPropiedad { get; set; }
        public virtual TransferenciaUsuario Usuario { get; set; }
        public virtual TransferenciaTipoMoneda TipoMoneda { get; set; }
    }
}
