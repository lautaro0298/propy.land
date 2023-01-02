using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LibreriaExperto.Desarrollo
{
    public static class ABMConstruccion
    {
        public static ErrorPropy CrearMoneda(String nombre)
        {
            ErrorPropy errorPropy = new ErrorPropy();

            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            DTOTipoConstruccion TipoConstruccion = new DTOTipoConstruccion();
            TipoConstruccion.nombreTipoConstruccion = nombre;
            TipoConstruccion.tipoConstruccionId = Guid.NewGuid().ToString();

            var tareaObtenerCarecteristica = clienteHttp.PostAsJsonAsync<DTOTipoConstruccion>("api/TipoConstruccion/crearTipoConstruccion", TipoConstruccion);
            tareaObtenerCarecteristica.Wait();

            if (!tareaObtenerCarecteristica.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaObtenerCarecteristica.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaObtenerCarecteristica.Result.StatusCode;

            }

            return errorPropy;
        }
        public static ErrorPropy EliminarTipoConstruccion(String id)
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var tareaMoneda = clienteHttp.GetAsync("api/TipoConstruccion/obtenerPorID/" + id);
            tareaMoneda.Wait();
            if (!tareaMoneda.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaMoneda.Result.StatusCode.ToString());
            }
            TransferenciaTipoConstruccion tipo = tareaMoneda.Result.Content.ReadAsAsync<TransferenciaTipoConstruccion>().Result;
            var tareaeliminarTipoConstruccion = clienteHttp.PostAsJsonAsync<TransferenciaTipoConstruccion>("api/TipoConstruccion/eliminarTipoConstruccion", tipo);
            tareaeliminarTipoConstruccion.Wait();
            if (!tareaeliminarTipoConstruccion.Result.IsSuccessStatusCode)
            {
                error.codigoError = (int)tareaeliminarTipoConstruccion.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaeliminarTipoConstruccion.Result.StatusCode;
                return error;
            }

            return error;
        }

        public static (ErrorPropy, List<DTOTipoConstruccion>) traerTipoConstruccion()
        {
            ErrorPropy errorPropy = new ErrorPropy();
            List<DTOTipoConstruccion> ListaMonedas = new List<DTOTipoConstruccion>();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            var tareaTraerMonedas = httpClient.GetAsync("api/TipoConstruccion/obtenerTiposConstrucciones");
            tareaTraerMonedas.Wait();

            if (!tareaTraerMonedas.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaTraerMonedas.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraerMonedas.Result.StatusCode;
                ListaMonedas = null;
            }
            else
            {
                List<TransferenciaTipoConstruccion> TransferenciaTipoConstruccions = tareaTraerMonedas.Result.Content.ReadAsAsync<List<TransferenciaTipoConstruccion>>().Result;
                foreach (var TM in TransferenciaTipoConstruccions)
                {
                    DTOTipoConstruccion dTOTipoConstruccion = new DTOTipoConstruccion();
                    dTOTipoConstruccion.nombreTipoConstruccion = TM.nombreTipoConstruccion;
                    dTOTipoConstruccion.tipoConstruccionId = TM.tipoConstruccionId;

                    ListaMonedas.Add(dTOTipoConstruccion);
                }
            }

            return (errorPropy, ListaMonedas);
        }
        public static TransferenciaTipoConstruccion buscarporid(string id)
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var tipopropiedad = clienteHttp.GetAsync("api/TipoConstruccion/obtenerPorId/" + id);
            tipopropiedad.Wait();
            if (!tipopropiedad.Result.IsSuccessStatusCode)
            {
                throw new Exception(tipopropiedad.Result.StatusCode.ToString());
            }
            TransferenciaTipoConstruccion tipo = tipopropiedad.Result.Content.ReadAsAsync<TransferenciaTipoConstruccion>().Result;


            return tipo;

        }
    }
}

