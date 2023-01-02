using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;

namespace LibreriaExperto.Desarrollo
{
    public static class ABMCaracteristica
    {
        public static ErrorPropy CrearCaracteristica(String nombre)
        {
            ErrorPropy errorPropy = new ErrorPropy();

            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            DTOCaracteristica caracteristica = new DTOCaracteristica();
            caracteristica.nombreCaracteristica = nombre;
            caracteristica.caracteristicaId = Guid.NewGuid().ToString();
            caracteristica.ischeck = true;
            
            var tareaObtenerCarecteristica = clienteHttp.PostAsJsonAsync<DTOCaracteristica>("api/Caracteristica/crearCaracteristica", caracteristica);
            tareaObtenerCarecteristica.Wait();

            if (!tareaObtenerCarecteristica.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaObtenerCarecteristica.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaObtenerCarecteristica.Result.StatusCode;

            }

            return errorPropy;
        }
        public static ErrorPropy EliminarCaracteristica(string idcaracteristica)
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var tareacaracterictica = clienteHttp.GetAsync("api/Caracteristica/obtenerPorID/" + idcaracteristica);
            tareacaracterictica.Wait();
            if (!tareacaracterictica.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareacaracterictica.Result.StatusCode.ToString());
            }
            TransferenciaCaracteristica tipo = tareacaracterictica.Result.Content.ReadAsAsync<TransferenciaCaracteristica>().Result;
            var tareaeliminarCarcteristica = clienteHttp.PostAsJsonAsync<TransferenciaCaracteristica>("api/Caracteristica/eliminarCaracteristica", tipo);
            tareaeliminarCarcteristica.Wait();
            if (!tareaeliminarCarcteristica.Result.IsSuccessStatusCode)
            {
                error.codigoError = (int)tareaeliminarCarcteristica.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaeliminarCarcteristica.Result.StatusCode;
                return error;
            }

            return error;

        }
        public static (ErrorPropy, List<DTOCaracteristica>) TraerCaracteristicas()
        {
            List<DTOCaracteristica> ListaCaracteristica = new List<DTOCaracteristica>();
            ErrorPropy errorPropy = new ErrorPropy();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            var tareaTraerCaracteristica = httpClient.GetAsync("api/Caracteristica/obtenerCaracteristicas");
            tareaTraerCaracteristica.Wait();

            if (!tareaTraerCaracteristica.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaTraerCaracteristica.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraerCaracteristica.Result.StatusCode;
                ListaCaracteristica = null;
            }
            else
            {
                List<TransferenciaCaracteristica> transferenciaCaracteristica = tareaTraerCaracteristica.Result.Content.ReadAsAsync<List<TransferenciaCaracteristica>>().Result;

                foreach(var TC in transferenciaCaracteristica)
                {
                    DTOCaracteristica dTOCaracteristica = new DTOCaracteristica();
                    dTOCaracteristica.caracteristicaId = TC.caracteristicaId;
                    dTOCaracteristica.nombreCaracteristica = TC.nombreCaracteristica;

                    ListaCaracteristica.Add(dTOCaracteristica);
                }
            }


            return (errorPropy, ListaCaracteristica);
        }
    }
}
