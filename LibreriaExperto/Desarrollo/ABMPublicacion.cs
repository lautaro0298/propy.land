using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LibreriaExperto.Desarrollo
{
    public static class ABMTipoPublicacion
    {
        public static ErrorPropy CrearTipoPublicacion(String nombre)
        {
            ErrorPropy errorPropy = new ErrorPropy();

            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            DTOTipoPublicacion TipoPublicacion = new DTOTipoPublicacion();
            TipoPublicacion.nombreTipoPublicacion = nombre;
            TipoPublicacion.tipoPublicacionId = Guid.NewGuid().ToString();

            var tareaObtenerCarecteristica = clienteHttp.PostAsJsonAsync<DTOTipoPublicacion>("api/TipoPublicacion/crearTipoPublicacion", TipoPublicacion);
            tareaObtenerCarecteristica.Wait();

            if (!tareaObtenerCarecteristica.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaObtenerCarecteristica.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaObtenerCarecteristica.Result.StatusCode;

            }

            return errorPropy;
        }
        public static ErrorPropy EliminarTipoPublicacion(String id)
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var tareaMoneda = clienteHttp.GetAsync("api/TipoPublicacion/obtenerPorID/" + id);
            tareaMoneda.Wait();
            if (!tareaMoneda.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaMoneda.Result.StatusCode.ToString());
            }
            TransferenciaTipoPublicacion tipo = tareaMoneda.Result.Content.ReadAsAsync<TransferenciaTipoPublicacion>().Result;
            var tareaeliminarTipoPublicacion = clienteHttp.PostAsJsonAsync<TransferenciaTipoPublicacion>("api/TipoPublicacion/eliminarTipoPublicacion", tipo);
            tareaeliminarTipoPublicacion.Wait();
            if (!tareaeliminarTipoPublicacion.Result.IsSuccessStatusCode)
            {
                error.codigoError = (int)tareaeliminarTipoPublicacion.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaeliminarTipoPublicacion.Result.StatusCode;
                return error;
            }

            return error;
        }

        public static (ErrorPropy, List<DTOTipoPublicacion>) traerTipoPublicacion()
        {
            ErrorPropy errorPropy = new ErrorPropy();
            List<DTOTipoPublicacion> ListaMonedas = new List<DTOTipoPublicacion>();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            var tareaTraerMonedas = httpClient.GetAsync("api/TipoPublicacion/obtenerTiposPublicaciones");
            tareaTraerMonedas.Wait();

            if (!tareaTraerMonedas.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaTraerMonedas.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraerMonedas.Result.StatusCode;
                ListaMonedas = null;
            }
            else
            {
                List<TransferenciaTipoPublicacion> TransferenciaTipoPublicacions = tareaTraerMonedas.Result.Content.ReadAsAsync<List<TransferenciaTipoPublicacion>>().Result;
                foreach (var TM in TransferenciaTipoPublicacions)
                {
                    DTOTipoPublicacion dTOTipoPublicacion = new DTOTipoPublicacion();
                    dTOTipoPublicacion.nombreTipoPublicacion = TM.nombreTipoPublicacion;
                    dTOTipoPublicacion.tipoPublicacionId = TM.tipoPublicacionId;

                    ListaMonedas.Add(dTOTipoPublicacion);
                }
            }

            return (errorPropy, ListaMonedas);
        }
        public static TransferenciaTipoPublicacion buscarporid(string id)
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var tipopropiedad = clienteHttp.GetAsync("api/TipoPublicacion/obtenerPorId/" + id);
            tipopropiedad.Wait();
            if (!tipopropiedad.Result.IsSuccessStatusCode)
            {
                throw new Exception(tipopropiedad.Result.StatusCode.ToString());
            }
            TransferenciaTipoPublicacion tipo = tipopropiedad.Result.Content.ReadAsAsync<TransferenciaTipoPublicacion>().Result;


            return tipo;

        }
    }
}
