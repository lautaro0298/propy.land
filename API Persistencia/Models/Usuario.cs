using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class Usuario
    {
        public string usuarioId { get; set; }
        public string nombreUsuario { get; set; }
        public string apellidoUsuario { get; set; }
        public string email { get; set; }
        public byte[] contraseña { get; set; }
        public bool emailConfirmado { get; set; }
        public bool permiterSerContactadoPorPublicante { get; set; }
        public bool permiteSerNotificado { get; set; }
        public long telefono1 { get; set; }
        public long telefono2 { get; set; }
        public bool admin { get; set; } = false;
        public byte[] Key { get; set; }
        public byte[] Vector { get; set; }
        public virtual ICollection<PlanUsuario> PlanUsuario { get; set; }
        public virtual ICollection<Favorito> Favorito { get; set; }
        public virtual ICollection<Actividad> Actividad { get; set; }
        public string ExternalId { get; }
    }
}
