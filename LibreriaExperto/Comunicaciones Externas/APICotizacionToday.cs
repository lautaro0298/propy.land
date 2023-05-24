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

        public static async Task<decimal> GetCotizacionAsync(string denominacionMoneda)
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
    }
    }
