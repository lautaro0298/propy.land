using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOCompras
    {
        public List<MercadoPago.Resources.Preference> preferences = new List<MercadoPago.Resources.Preference>();
        public List<DatosCompra> datosCompras = new List<DatosCompra>();

        public void AddPreference(MercadoPago.Resources.Preference preference)
        {
            this.preferences.Add(preference);
        }
        public void AddcontratarPlan(DatosCompra contratarPlan)
        {
            this.datosCompras.Add(contratarPlan);
        }
    }
    public class DatosCompra
    {
        public int cantidadMaxImagenesPermitidasPorPub { get; set; }
        public int creditos { get; set; }
        public decimal precio { get; set; }
        public string nombre { get; set; }
        public string moneda { get; set; }
        public string vencimientoCreditos { get; set; }
        public bool subirVideos { get; set; }
        public bool accesoEstadisticasPremium { get; set; }
        public bool activo { get; set; }
        public string ID { get; set; }
    }
}