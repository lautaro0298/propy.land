using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("Cotizacion")]
    public class Cotizacion
    {
        /// <summary>
        /// El atributo source es la moneda fuente y el atributo target es la moneda destino. Es decir target es la moneda a convertir.
        /// value es el valor que toma la moneda luego de ser convertida.
        /// quantity es la cantidad a convertir.
        /// amount es el resultado de quantity * value
        /// </summary>
        
        [Key]
        public Guid cotizacionId { get; set; } 
        public string source { get; set; }
        public string target { get; set; }
        public float value { get; set; }
        public float quantity { get; set; }
        public decimal amount { get; set; }
    }
}