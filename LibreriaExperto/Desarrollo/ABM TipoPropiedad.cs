using System;

using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;

namespace LibreriaExperto.Desarrollo
{
    public static class ABM_TipoPropiedad
    {
        public static ErrorPropy CrearTipoPropiedad(string nombreTipoPropiedad,List<string> tiposAmbientesID) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();

            #region Crear instancia de Tipo de Propiedad
            DTOTipoPropiedad tipoPropiedad = new DTOTipoPropiedad();
            tipoPropiedad.tipoPropiedadId = Guid.NewGuid().ToString();
            tipoPropiedad.activo = true;
            tipoPropiedad.nombreTipoPropiedad = nombreTipoPropiedad;
            #endregion
            #region Crear instancia de TipoPropiedadTipoAmbiente
            foreach (var tipoAmbienteID in tiposAmbientesID)
            {
                TransferenciaTipoPropiedadTipoAmbiente tipoPropiedadTipoAmbiente = new TransferenciaTipoPropiedadTipoAmbiente();
                tipoPropiedadTipoAmbiente.tipoPropiedadTipoAmbienteId = Guid.NewGuid().ToString();
                tipoPropiedadTipoAmbiente.activo = true;
                tipoPropiedadTipoAmbiente.tipoPropiedadId = tipoPropiedad.tipoPropiedadId;
                tipoPropiedadTipoAmbiente.tipoAmbienteId = tipoAmbienteID;

                tipoPropiedad.dTOTipoAmbiente.Add(tipoPropiedadTipoAmbiente);
            }
            #endregion
            var tareaCrearTipoPropiedad = clienteHttp.PostAsJsonAsync<DTOTipoPropiedad>("api/TipoPropiedad/crearTipoPropiedad",tipoPropiedad);
            tareaCrearTipoPropiedad.Wait();
            if (!tareaCrearTipoPropiedad.Result.IsSuccessStatusCode) {
                error.codigoError = (int)tareaCrearTipoPropiedad.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaCrearTipoPropiedad.Result.StatusCode;
                return error;
            }
          
            return error;
            
        }

        public static (ErrorPropy, List<DTOTipoPropiedad>) TraerTipoPropiedad()
        {
            ErrorPropy errorPropy = new ErrorPropy();
            List<DTOTipoPropiedad> ListaDTOTIpoPropiedad = new List<DTOTipoPropiedad>();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();

            var tareaObtenerTipoPropiedad = clienteHttp.GetAsync("api/TipoPropiedad/obtenerTiposPropiedades");
            tareaObtenerTipoPropiedad.Wait();

            if (!tareaObtenerTipoPropiedad.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaObtenerTipoPropiedad.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaObtenerTipoPropiedad.Result.StatusCode;
                ListaDTOTIpoPropiedad = null;
            }
            else
            {
                List<DTOTipoPropiedad> tipoPropiedades= tareaObtenerTipoPropiedad.Result.Content.ReadAsAsync<List<DTOTipoPropiedad>>().Result;

                foreach(var tipoProp in tipoPropiedades)
                {
                    DTOTipoPropiedad dTOTipoPropiedad = new DTOTipoPropiedad();
                    dTOTipoPropiedad.nombreTipoPropiedad = tipoProp.nombreTipoPropiedad;
                    dTOTipoPropiedad.tipoPropiedadId = tipoProp.tipoPropiedadId;

                    ListaDTOTIpoPropiedad.Add(dTOTipoPropiedad);
                }
            }
            return (errorPropy, ListaDTOTIpoPropiedad);
        }
          public static ErrorPropy EliminarTipoPropiedad(string idTipoPropiedad)
          {
              ErrorPropy error = new ErrorPropy();
              HttpClient clienteHttp = ApiConfiguracion.Inicializar();
              var tipopropiedad = clienteHttp.GetAsync("api/TipoPropiedad/obtenerPorId/" +idTipoPropiedad);
              tipopropiedad.Wait();
            if (!tipopropiedad.Result.IsSuccessStatusCode)
            {
                throw new Exception(tipopropiedad.Result.StatusCode.ToString());
            }
            DTOTipoPropiedad tipo = tipopropiedad.Result.Content.ReadAsAsync<DTOTipoPropiedad>().Result;
            var tareaeliminarTipoPropiedad = clienteHttp.PostAsJsonAsync<DTOTipoPropiedad>("api/TipoPropiedad/eliminarTipoPropiedad" , tipo);
              tareaeliminarTipoPropiedad.Wait();
              if (!tareaeliminarTipoPropiedad.Result.IsSuccessStatusCode)
              {
                  error.codigoError = (int)tareaeliminarTipoPropiedad.Result.StatusCode;
                  error.descripcionError = "Error: " + error.codigoError + " " + tareaeliminarTipoPropiedad.Result.StatusCode;
                  return error;
              }

              return error;

          }
    }
}
