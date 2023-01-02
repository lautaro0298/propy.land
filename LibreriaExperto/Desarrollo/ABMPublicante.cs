using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LibreriaExperto.Desarrollo
{
    public static class ABMPublicante
    {
         public static ErrorPropy CrearMoneda(String nombre)
        {
            ErrorPropy errorPropy = new ErrorPropy();

            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            DTOTipoPublicante Publicante = new DTOTipoPublicante();
            Publicante.nombreTipoPublicante = nombre;
            
            Publicante.tipoPublicanteId = Guid.NewGuid().ToString();

            var tareaObtenerCarecteristica = clienteHttp.PostAsJsonAsync<DTOTipoPublicante>("api/TipoPublicante/crearTipoPublicante", Publicante);
            tareaObtenerCarecteristica.Wait();

            if (!tareaObtenerCarecteristica.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaObtenerCarecteristica.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaObtenerCarecteristica.Result.StatusCode;

            }

            return errorPropy;
        }
        public static ErrorPropy EliminarPublicante(String id)
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var tareaMoneda = clienteHttp.GetAsync("api/TipoPublicante/obtenerPorID/" + id);
            tareaMoneda.Wait();
            if (!tareaMoneda.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaMoneda.Result.StatusCode.ToString());
            }
            TransferenciaTipoPublicante tipo = tareaMoneda.Result.Content.ReadAsAsync<TransferenciaTipoPublicante>().Result;
            var tareaeliminarTipoPublicante = clienteHttp.PostAsJsonAsync<TransferenciaTipoPublicante>("api/TipoPublicante/eliminarTipoPublicante", tipo);
            tareaeliminarTipoPublicante.Wait();
            if (!tareaeliminarTipoPublicante.Result.IsSuccessStatusCode)
            {
                error.codigoError = (int)tareaeliminarTipoPublicante.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaeliminarTipoPublicante.Result.StatusCode;
                return error;
            }

            return error;
        }

        public static (ErrorPropy, List<DTOTipoPublicante>) traerPublicante()
        {
            ErrorPropy errorPropy = new ErrorPropy();
            List<DTOTipoPublicante> ListaMonedas = new List<DTOTipoPublicante>();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            var tareaTraerMonedas = httpClient.GetAsync("api/TipoPublicante/obtenerTiposPublicantes");
            tareaTraerMonedas.Wait();

            if (!tareaTraerMonedas.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaTraerMonedas.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraerMonedas.Result.StatusCode;
                ListaMonedas = null;
            }
            else
            {
                List<TransferenciaTipoPublicante> TransferenciaPublicantes = tareaTraerMonedas.Result.Content.ReadAsAsync<List<TransferenciaTipoPublicante>>().Result;
                foreach (var TM in TransferenciaPublicantes)
                {
                    DTOTipoPublicante dTOPublicante = new DTOTipoPublicante();
                    dTOPublicante.nombreTipoPublicante = TM.nombreTipoPublicante;
                    dTOPublicante.tipoPublicanteId = TM.tipoPublicanteId;

                    ListaMonedas.Add(dTOPublicante);
                }
            }

            return (errorPropy, ListaMonedas);
        }
        public static TransferenciaTipoPublicante buscarporid(string id)
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var tipopropiedad = clienteHttp.GetAsync("api/TipoPublicante/obtenerPorId/" + id);
            tipopropiedad.Wait();
            if (!tipopropiedad.Result.IsSuccessStatusCode)
            {
                throw new Exception(tipopropiedad.Result.StatusCode.ToString());
            }
            TransferenciaTipoPublicante tipo = tipopropiedad.Result.Content.ReadAsAsync<TransferenciaTipoPublicante>().Result;


            return tipo;

        }
    }
}
