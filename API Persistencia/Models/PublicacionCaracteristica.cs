using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class PublicacionCaracteristica
    {
        public string PublicacionCaracteristicaId { get; set; }
        public string CaracteristicaId { get; set; }
        public string PublicacionId { get; set; }
        public virtual Publicacion Publicacion { get; set; }
        public virtual Caracteristica Caracteristica { get; set; }
    }
}
