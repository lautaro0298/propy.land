using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.DTOJSon;
using LibreriaExperto.Desarrollo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace LibreriaExperto.Comunicaciones_Externas
{
    public static class APICotizacionToday
    {
        private static string AccessTokenAPICot = "x0EWlvVC8OTNC6ugnXZPtiTeYRkO7KSW";
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

                        string baseCurrency = result.dTOTipoMonedas.ElementAt(cont).denominacionMoneda;
                        string targetCurrency = result.dTOTipoMonedas.ElementAt(cont2).denominacionMoneda;
                        string url = $"https://api.apilayer.com/fixer/latest?base={baseCurrency}&symbols={targetCurrency}";

                        using (HttpClient client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Add("apikey", AccessTokenAPICot);
                            HttpResponseMessage response = client.GetAsync(url).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                string json = response.Content.ReadAsStringAsync().Result;
                                cotizacion = JsonConvert.DeserializeObject<DTORootObjectCotizacion>(json);
                                cotizaciones.Add(cotizacion);
                            }
                        }
                    }
                    else
                    {
                        string baseCurrency = result.dTOTipoMonedas.ElementAt(cont).denominacionMoneda;
                        string targetCurrency = result.dTOTipoMonedas.ElementAt(cont2).denominacionMoneda;
                        string url = $"https://api.apilayer.com/fixer/latest?base={baseCurrency}&symbols={targetCurrency}";

                        using (HttpClient client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Add("apikey", AccessTokenAPICot);
                            HttpResponseMessage response = client.GetAsync(url).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                string json = response.Content.ReadAsStringAsync().Result;
                                cotizacion = JsonConvert.DeserializeObject<DTORootObjectCotizacion>(json);
                                cotizaciones.Add(cotizacion);
                            }
                        }
                    }
                }
            }
            return cotizaciones;
        }
    }
}
