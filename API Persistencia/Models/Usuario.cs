using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class Usuario
    {
        public string UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string Email { get; set; }
        public byte[] Contraseña { get; set; }
        public bool EmailConfirmado { get; set; }
        public bool PermitirSerContactadoPorPublicante { get; set; }
        public bool PermitirSerNotificado { get; set; }
        public long Telefono1 { get; set; }
        public long Telefono2 { get; set; }
        public bool Admin { get; set; } = false;
        public byte[] Key { get; set; }
        public byte[] Vector { get; set; }

        public virtual ICollection<PlanUsuario> PlanUsuarios { get; set; }
        public virtual ICollection<Favorito> Favoritos { get; set; }
        public virtual ICollection<Actividad> Actividades { get; set; }
    }
}
