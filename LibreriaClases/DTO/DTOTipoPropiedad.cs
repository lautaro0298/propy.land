using System.Collections.Generic;
using System.Text;
using LibreriaClases.Transferencia;

namespace LibreriaClases.DTO
{
    public class DTOTipoPropiedad
    {
        public string TipoPropiedadId { get; set; }
        public string NombreTipoPropiedad { get; set; }

        public bool Activo { get; set; }

        // Consider using 'IEnumerable' instead of 'ICollection' for read-only collections
        public IEnumerable<DTOTipoPropiedad> SubTipoPropiedads { get; set; }

        // Use a specific type for the list instead of 'object'
        public List<TransferenciaTipoPropiedadTipoAmbiente> TipoAmbientes { get; set; }
    }
}
