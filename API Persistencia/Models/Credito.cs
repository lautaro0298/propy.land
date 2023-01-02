using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{   
    public class Credito
    {
        [Key]
        public string PaqueteID { get; set; }
        public int CantidadCreditos { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
        public string NombrePack { get; set; }
        public string TipoMonedaID { get; set; }
        public virtual TipoMoneda TipoMoneda { get; set; }
    }
}
