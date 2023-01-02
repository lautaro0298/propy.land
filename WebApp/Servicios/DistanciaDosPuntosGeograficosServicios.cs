using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DTO;

namespace WebApp.Servicios
{
    public static class DistanciaDosPuntosGeograficosServicios
    {
        public const double radioTierra = 6371;
        public static double diferenciaLatitudes { get; set; } = 0;
        public static double diferenciaLongitudes { get; set; } = 0;

        public static double CalculoFormulaHaversine(DTOPosicion origen,DTOPosicion destino) {
            diferenciaLatitudes = (destino.latitud - origen.latitud).EnRadianes();
            diferenciaLongitudes = (destino.longitud - origen.longitud).EnRadianes();

            var a = Math.Sin(diferenciaLatitudes / 2)*Math.Sin(diferenciaLatitudes / 2) +
          Math.Cos(origen.latitud.EnRadianes()) *
          Math.Cos(destino.latitud.EnRadianes()) *
          Math.Sin(diferenciaLongitudes / 2) * Math.Sin(diferenciaLongitudes / 2);
            var c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));

            return radioTierra*c;
        }
        public static double EnRadianes(this double valor) {
            return Convert.ToDouble(Math.PI / 180) * valor;
        }
        public static double AlCuadrado(this double valor) {
            return Math.Pow(valor, 2);
        }
    }
}