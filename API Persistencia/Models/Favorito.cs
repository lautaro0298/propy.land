using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class Favorito
    {
        public string favoritoId { get; set; }
        public bool activo { get; set; }
        public string publicacionId { get; set; }
        public string usuarioId { get; set; }
        public virtual Publicacion Publicacion { get; set; }
    }
}
