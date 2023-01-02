using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;

namespace LibreriaExperto.Desarrollo
{
    public static class ABMTipoMoneda   
    {
        public static ErrorPropy CrearMoneda(String  nombre)
        {
                ErrorPropy errorPropy = new ErrorPropy();

                HttpClient clienteHttp = ApiConfiguracion.Inicializar();
                DTOTipoMoneda tipoMoneda = new DTOTipoMoneda();
                tipoMoneda.denominacionMoneda = nombre;
                tipoMoneda.tipoMonedaId = Guid.NewGuid().ToString();

                var tareaObtenerCarecteristica = clienteHttp.PostAsJsonAsync<DTOTipoMoneda>("api/TipoMoneda/crearTipoMoneda", tipoMoneda);
                tareaObtenerCarecteristica.Wait();

                if (!tareaObtenerCarecteristica.Result.IsSuccessStatusCode)
                {
                    errorPropy.codigoError = (int)tareaObtenerCarecteristica.Result.StatusCode;
                    errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaObtenerCarecteristica.Result.StatusCode;

                }

                return errorPropy;
            }
        public static ErrorPropy EliminarTipoMoneda(String id)
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var tareaMoneda = clienteHttp.GetAsync("api/ABMTipoMoneda/obtenerPorID/" + id);
            tareaMoneda.Wait();
            if (!tareaMoneda.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaMoneda.Result.StatusCode.ToString());
            }
            TransferenciaTipoMoneda tipo = tareaMoneda.Result.Content.ReadAsAsync<TransferenciaTipoMoneda>().Result;
            var tareaeliminarTipoMoneda = clienteHttp.PostAsJsonAsync<TransferenciaTipoMoneda>("api/ABMTipoMoneda/eliminarTipoMoneda", tipo);
            tareaeliminarTipoMoneda.Wait();
            if (!tareaeliminarTipoMoneda.Result.IsSuccessStatusCode)
            {
                error.codigoError = (int)tareaeliminarTipoMoneda.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaeliminarTipoMoneda.Result.StatusCode;
                return error;
            }

            return error;
        }

            public static (ErrorPropy, List<DTOTipoMoneda>) traerTipoMoneda()
        {
            ErrorPropy errorPropy = new ErrorPropy();
            List<DTOTipoMoneda> ListaMonedas = new List<DTOTipoMoneda>();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            var tareaTraerMonedas = httpClient.GetAsync("api/TipoMoneda/obtenerTiposMonedas");
            tareaTraerMonedas.Wait();

            if (!tareaTraerMonedas.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaTraerMonedas.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraerMonedas.Result.StatusCode;
                ListaMonedas = null;
            }
            else
            {
                List<TransferenciaTipoMoneda> transferenciaTipoMonedas = tareaTraerMonedas.Result.Content.ReadAsAsync<List<TransferenciaTipoMoneda>>().Result;
                foreach (var TM in transferenciaTipoMonedas)
                {
                    DTOTipoMoneda dTOTipoMoneda = new DTOTipoMoneda();
                    dTOTipoMoneda.denominacionMoneda = TM.denominacionMoneda;
                    dTOTipoMoneda.tipoMonedaId = TM.tipoMonedaId;

                    ListaMonedas.Add(dTOTipoMoneda);
                }
            }

            return (errorPropy, ListaMonedas);
        }
        public static TransferenciaTipoMoneda buscarporid(string id)
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var tipopropiedad = clienteHttp.GetAsync("api/TipoMoneda/obtenerPorId/" + id);
            tipopropiedad.Wait();
            if (!tipopropiedad.Result.IsSuccessStatusCode)
            {
                throw new Exception(tipopropiedad.Result.StatusCode.ToString());
            }
            TransferenciaTipoMoneda tipo = tipopropiedad.Result.Content.ReadAsAsync<TransferenciaTipoMoneda>().Result;
            

            return tipo;

        }
    }
}
