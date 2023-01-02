using LibreriaClases.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using LibreriaClases;
using System.Net.Http;
using LibreriaClases.Transferencia;

namespace LibreriaExperto.Desarrollo
{

    static public class ABMTipoAmbiente
    {
        public static ErrorPropy EliminarTipoAmbiente(string idTipoAmbiente)
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var tipoambiente = clienteHttp.GetAsync("api/TipoAmbiente/obtenerPorID/" + idTipoAmbiente);
            tipoambiente.Wait();
            if (!tipoambiente.Result.IsSuccessStatusCode)
            {
                throw new Exception(tipoambiente.Result.StatusCode.ToString());
            }

            TipoAmbiente tipo = tipoambiente.Result.Content.ReadAsAsync<TipoAmbiente>().Result;
            var tareaeliminarTipoAmbiente = clienteHttp.PostAsJsonAsync<TipoAmbiente>("api/TipoAmbiente/eliminarTipoAmbiente", tipo);
            tareaeliminarTipoAmbiente.Wait();
            if (!tareaeliminarTipoAmbiente.Result.IsSuccessStatusCode)
            {
                error.codigoError = (int)tareaeliminarTipoAmbiente.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaeliminarTipoAmbiente.Result.StatusCode;
                return error;
            }

            return error;

        }

        public static ErrorPropy CrearTipoAmbiente(String nombre)
        {
            ErrorPropy errorPropy = new ErrorPropy();

            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            DTOTipoAmbiente tipoAmbiente = new DTOTipoAmbiente();
            tipoAmbiente.nombreTipoAmbiente = nombre;
            tipoAmbiente.tipoAmbienteId = Guid.NewGuid().ToString();
            var tareaObtenerTipoAmbiente = clienteHttp.PostAsJsonAsync<DTOTipoAmbiente>("api/TipoAmbiente/crearTipoAmbiente", tipoAmbiente);
            tareaObtenerTipoAmbiente.Wait();

            if (!tareaObtenerTipoAmbiente.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaObtenerTipoAmbiente.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaObtenerTipoAmbiente.Result.StatusCode;

            }

            return errorPropy;
        }
        public static (ErrorPropy, List<DTOTipoAmbiente>) TraerTipoAmbiente()
        {
            ErrorPropy errorPropy = new ErrorPropy();
            List<DTOTipoAmbiente> ListaDTOTIposAmbiente = new List<DTOTipoAmbiente>();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();

            var tareaObtenerTipoAmbiente = clienteHttp.GetAsync("api/TipoAmbiente/obtenerTiposAmbientes");
            tareaObtenerTipoAmbiente.Wait();

            if (!tareaObtenerTipoAmbiente.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaObtenerTipoAmbiente.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaObtenerTipoAmbiente.Result.StatusCode;
                ListaDTOTIposAmbiente = null;
            }
            else
            {
                List<TipoAmbiente> tiposAmbientes = tareaObtenerTipoAmbiente.Result.Content.ReadAsAsync<List<TipoAmbiente>>().Result;

                foreach (var tipoAmb in tiposAmbientes)
                {
                    DTOTipoAmbiente dTOTipoAmbiente = new DTOTipoAmbiente();
                    dTOTipoAmbiente.nombreTipoAmbiente = tipoAmb.nombreTipoAmbiente;
                    dTOTipoAmbiente.tipoAmbienteId = tipoAmb.tipoAmbienteId;

                    ListaDTOTIposAmbiente.Add(dTOTipoAmbiente);
                }
            }

            return (errorPropy, ListaDTOTIposAmbiente);
        }
    }
}
   
