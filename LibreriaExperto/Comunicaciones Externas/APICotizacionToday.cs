using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.DTOJSon;
using LibreriaExperto.Desarrollo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaExperto.Comunicaciones_Externas
{
    public static class APICotizacionToday
    {
        private static string AccessTokenAPICot = "3042|DTORowxhqstN44wcx~UBUZfhp9C8iCQQ";
        private static string Url1;

        public static List<DTORootObjectCotizacion> GetCotizacion()
        {
            List<DTORootObjectCotizacion> cotizaciones = new List<DTORootObjectCotizacion>();
            DTORootObjectCotizacion cotizacion;

            (ErrorPropy errorPropy, List<DTOTipoMoneda> dTOTipoMonedas) result = ABMTipoMoneda.traerTipoMoneda();

            for (int cont = 0; cont < result.dTOTipoMonedas.Count; cont++)
            {
                for (int cont2 = 0; cont2 < result.dTOTipoMonedas.Count; cont2++)
                {
                    if (result.dTOTipoMonedas.ElementAt(cont).denominacionMoneda == result.dTOTipoMonedas.ElementAt(cont2).denominacionMoneda)
                    {
                        cont2++;
                        if (cont2 >= result.dTOTipoMonedas.Count()) break;

                        Url1 = "https://api.cambio.today/v1/quotes/" + result.dTOTipoMonedas.ElementAt(cont).denominacionMoneda + "/" + result.dTOTipoMonedas.ElementAt(cont2).denominacionMoneda + "/json?quantity=1&key=" + AccessTokenAPICot;
                        cotizacion = JsonConvert.DeserializeObject<DTORootObjectCotizacion>(PeticionHttpGet.GetHTTP(Url1));
                        cotizaciones.Add(cotizacion);
                    }
                    else
                    {
                        Url1 = "https://api.cambio.today/v1/quotes/" + result.dTOTipoMonedas.ElementAt(cont).denominacionMoneda + "/" + result.dTOTipoMonedas.ElementAt(cont2).denominacionMoneda + "/json?quantity=1&key=" + AccessTokenAPICot;
                        cotizacion = JsonConvert.DeserializeObject<DTORootObjectCotizacion>(PeticionHttpGet.GetHTTP(Url1));
                        cotizaciones.Add(cotizacion);
                    }
                }
            }
            return cotizaciones;
        }
    }
}
