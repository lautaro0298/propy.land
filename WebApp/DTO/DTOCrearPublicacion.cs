using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOCrearPublicacion
    {
        public string calle { get; set; }
        public int nroCalle { get; set; }
        public string pais { get; set; }
        public string areaAdministrativaNivel1 { get; set; }
        public string areaAdministrativaNivel2 { get; set; }
        public double longitud { get; set; }
        public double latitud { get; set; }
        public string identificadorUbicacionGoogle { get; set; }
        public string observaciones { get; set; }
        public Guid tipoMoneda { get; set; }
        public int antiguedad { get; set; }
        public Guid tipoConstruccion { get; set; }
        public Guid tipoOperacion { get; set; }
        public Guid tipoPropiedad { get; set; }
        public Guid tipoUsuario { get; set; }
        public float superficieCubierta { get; set; }
        public float superficieTerreno { get; set; }
        public int nroPlantas { get; set; }
        public string direccionFormateada { get; set; }
        public List<Guid> tipoAmbiente { get; set; }
        public List<Guid> extras { get; set; }
        public Decimal precioPropiedad { get; set; }
        public List<byte[]> imagenes { get; set; }
        public List<int> cantidadAmbientes { get; set; }
        public string usuarioId { get; set; }
        public List<string> rutasImagenes { get; set; }
    }
}