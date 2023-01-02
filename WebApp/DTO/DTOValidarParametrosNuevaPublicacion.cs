using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOValidarParametrosNuevaPublicacion
    {
        public string direccion { get; set; }
        public Decimal precio { get; set; }
        public float superficieTerreno { get; set; }
        public float superficieCubierta { get; set; }
        public List<HttpPostedFileBase> imagenes { get; set; }
        public string tipoPropiedad { get; set; }
        public string tipoConstruccion { get; set; }
    }
}