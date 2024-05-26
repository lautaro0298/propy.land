using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class Actividad
    {
        public string ActividadId { get; set; }
        public DateTime? FechaActividad { get; set; }
        public string DescripcionActividad { get; set; }
        public string UsuarioId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
