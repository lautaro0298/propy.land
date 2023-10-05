using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class Publicacion
    {
        public string publicacionId { get; set; }
        public DateTime fechaInicioPublicacion { get; set; }
        public DateTime fechaFinPublicacion { get; set; }
        public string tipoPublicacionId { get; set; }
        public string propiedadId { get; set; }
        public int estado { get; set; }
        public virtual Propiedad Propiedad { get; set; }
        public virtual TipoPublicacion TipoPublicacion { get; set; }
        public virtual ICollection<PublicacionCaracteristica>? Caracteristicas { get; set; }
        [JsonIgnore]
        public virtual ICollection<VisitaInmueble> VisitaInmueble { get; set; }
        public virtual ICollection<VisitaPerfilPublicante> VisitaPerfilPublicante { get; set; }
    }
}
