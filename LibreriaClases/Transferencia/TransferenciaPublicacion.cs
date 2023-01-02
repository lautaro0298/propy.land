using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaPublicacion
    {
       
        public string publicacionId { get; set; }
        public DateTime fechaInicioPublicacion { get; set; }
        public DateTime fechaFinPublicacion { get; set; }
        public int estado { get; set; }
        public string tipoPublicacionId { get; set; }
        public string propiedadId { get; set; }
        public virtual List<TransferenciaPublicacionCaracteristica> Caracteristicas { get; set; }
        public virtual TransferenciaPropiedad Propiedad { get; set; }
        public virtual TransferenciaTipoPublicacion TipoPublicacion { get; set; }
        public virtual ICollection<TransferenciaVisitaInmueble> VisitaInmueble { get; set; }
        public virtual ICollection<TransferenciaVisitaPerfilPublicante> VisitaPerfilPublicante { get; set; }
    }
}
