using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaClases.DTOJSon
{
    public class DTORootObjectCotizacion
    {
        public bool Success { get; set; }
        public int Timestamp { get; set; }
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}