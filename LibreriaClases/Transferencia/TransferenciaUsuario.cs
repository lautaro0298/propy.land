using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaUsuario
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
        public virtual ICollection<TransferenciaPlanUsuario> PlanUsuario { get; set; }
        public virtual ICollection<TransferenciaFavorito> Favorito { get; set; }
        public virtual ICollection<TransferenciaActividad> Actividad { get; set; }
    }
}
