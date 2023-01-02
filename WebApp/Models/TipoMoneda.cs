using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("TipoMoneda")]
    public class TipoMoneda
    {
        [Key]
        public Guid tipoMonedaId { get; set; }
        public string nombreTipoMoneda { get; set; }
        public bool activo { get; set; }
    }
}