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

using System.Threading.Tasks;
namespace LibreriaExperto.Comunicaciones_Externas
{
    public static class APICotizacionToday
    {
        private static string AccessTokenAPICot = "x0EWlvVC8OTNC6ugnXZPtiTeYRkO7KSW";
        private static string Url1;
        public static async Task<decimal> ObtenerCotizacion(string denominacionMoneda)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "https://api-dolar-argentina.herokuapp.com/api/dolarblue";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    DTORootObjectCotizacion cotizacion = Newtonsoft.Json.JsonConvert.DeserializeObject<DTORootObjectCotizacion>(json);

                    if (cotizacion.Rates.ContainsKey(denominacionMoneda))
                    {
                        return cotizacion.Rates[denominacionMoneda];
                    }
                    else
                    {
                        throw new Exception("La denominación de moneda especificada no se encontró en la respuesta de la API.");
                    }
                }
                else
                {
                    throw new Exception("Error al llamar a la API de cotizaciones de moneda.");
                }
            }
        }

        public class CotizacionData
        {
            public string last_update { get; set; }
            public MonedaData blue { get; set; }
            // Agrega otras propiedades necesarias según la estructura del JSON
        }

        public class MonedaData
        {
            public decimal value_sell { get; set; }
            // Agrega otras propiedades necesarias según la estructura del JSON
        }
        public static async Task<List<DTORootObjectCotizacion>> GetCotizacionAsync()
        {
            (ErrorPropy errorPropy, List<DTOTipoMoneda> dTOTipoMonedas) result = ABMTipoMoneda.traerTipoMoneda();
            List<DTOTipoMoneda> ListaMonedas = result.dTOTipoMonedas;
            // Agregar las monedas a la lista

            List<DTORootObjectCotizacion> cotizaciones = new List<DTORootObjectCotizacion>();

            using (HttpClient client = new HttpClient())
            {
                string url = "https://api.bluelytics.com.ar/v2/latest";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    CotizacionData cotizacionData = JsonConvert.DeserializeObject<CotizacionData>(json);

                    decimal valorDolar = cotizacionData.blue.value_sell;


                    foreach (DTOTipoMoneda moneda in ListaMonedas)
                    {
                        if (moneda.denominacionMoneda == "ARS")
                        {
                            // La cotización de ARS en relación al dólar es 1 porque es la moneda base
                            DTORootObjectCotizacion objetoCotizacionARS = new DTORootObjectCotizacion
                            {
                                Success = true,
                                Timestamp = cotizacionData.last_update,
                                Base = moneda.denominacionMoneda,
                                Date = DateTime.UtcNow.Date,
                                Rates = new Dictionary<string, decimal>
                    {
                        { moneda.denominacionMoneda, 1/ valorDolar}
                    }
                            };

                            cotizaciones.Add(objetoCotizacionARS);
                        }
                        else if (moneda.denominacionMoneda == "USD")
                        {
                            // La cotización de USD en relación al dólar es 1 porque es la moneda base
                            DTORootObjectCotizacion objetoCotizacionUSD = new DTORootObjectCotizacion
                            {
                                Success = true,
                                Timestamp = cotizacionData.last_update,
                                Base = moneda.denominacionMoneda,
                                Date = DateTime.UtcNow.Date,
                                Rates = new Dictionary<string, decimal>
                    {
                        { moneda.denominacionMoneda, valorDolar }
                    }
                            };

                            cotizaciones.Add(objetoCotizacionUSD);
                        }
                    //    else
                    //    {
                    //        var mon = moneda.denominacionMoneda;
                    //        // Calcular la cotización de la moneda en relación al dólar
                    //        decimal cotizacion =  / cotizacionData[mon].value_sell;

                    //        DTORootObjectCotizacion objetoCotizacion = new DTORootObjectCotizacion
                    //        {
                    //            Success = true,
                    //            Timestamp = cotizacionData.last_update,
                    //            Base = "USD",
                    //            Date = DateTime.UtcNow.Date,
                    //            Rates = new Dictionary<string, decimal>
                    //{
                    //    { moneda.denominacionMoneda, cotizacion }
                    //}
                    //        };

                    //        cotizaciones.Add(objetoCotizacion);
                    //    }
                    }
                }
                else
                {
                    throw new Exception("Error al llamar a la API de cotizaciones de moneda.");
                }
            }
            return cotizaciones;
        }
    }
            // La lista "cotizaciones" contiene los objetos DTORootObjectCotizacion con las cotizaciones de cada moneda en relación al dólar.}
        }
