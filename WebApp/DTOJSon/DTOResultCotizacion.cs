using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTOJSon
{
    public class DTOResultCotizacion
    {
        public DateTime updated { get; set; }
        public string source { get; set; }
        public string target { get; set; }
        public float value { get; set; }
        public float quantity { get; set; }
        public decimal amount { get; set; }
    }
}