using LibreriaClases;
using System;
using System.Collections.Generic;
using System.Text;
using LibreriaClases.Transferencia;
using System.Net.Http;
using System.Threading;
using System.Linq;
using LibreriaClases.DTO;

namespace LibreriaExperto.Desarrollo
{
    public static class ABMTipoPropiedadTipoAmbiente
    {
        public static ErrorPropy Crear_y_Asignar_Ambiente_A_Propiedad(string TipoPropiedad, string[] TipoAmbiente)
        {
            ErrorPropy errorPropy = new ErrorPropy();
            HttpClient httpClient = ApiConfiguracion.Inicializar();
            TransferenciaTipoPropiedadTipoAmbiente transferenciaTipoPropiedadTipoAmbiente = new TransferenciaTipoPropiedadTipoAmbiente();
            int count;
            for (count = 0; count < TipoAmbiente.Length; count++)
            {
                transferenciaTipoPropiedadTipoAmbiente.tipoAmbienteId = TipoAmbiente.ElementAt(count);
                transferenciaTipoPropiedadTipoAmbiente.tipoPropiedadId = TipoPropiedad;
                transferenciaTipoPropiedadTipoAmbiente.activo = true;
                transferenciaTipoPropiedadTipoAmbiente.tipoPropiedadTipoAmbienteId = System.Guid.NewGuid().ToString();
                var tareaCrearTipoPropiedadTipoAmbiente = httpClient.PostAsJsonAsync<TransferenciaTipoPropiedadTipoAmbiente>("api/TipoPropiedadTipoAmbiente/crearTipoPropiedadTipoAmbiente", transferenciaTipoPropiedadTipoAmbiente);
                tareaCrearTipoPropiedadTipoAmbiente.Wait();

                if (!tareaCrearTipoPropiedadTipoAmbiente.Result.IsSuccessStatusCode)
                {
                    errorPropy.codigoError = (int)tareaCrearTipoPropiedadTipoAmbiente.Result.StatusCode;
                    errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaCrearTipoPropiedadTipoAmbiente.Result.StatusCode;
                    break;
                }
            }
            return errorPropy;
        }

        public static (ErrorPropy, List<DTOTipoPropiedadTipoAmbiente>) TraerTipoPropiedadTipoAmbiente()
        {
            ErrorPropy errorPropy = new ErrorPropy();
            List<DTOTipoPropiedadTipoAmbiente> ListaTipoPropiedadTipoAmbiente = new List<DTOTipoPropiedadTipoAmbiente>();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            var tareaTraerTP = httpClient.GetAsync("api/TipoPropiedad/obtenerTipoPropiedadesConTipoAmbientes");
            tareaTraerTP.Wait();

            if (!tareaTraerTP.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaTraerTP.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraerTP.Result.StatusCode;
                ListaTipoPropiedadTipoAmbiente = null;
            }
            else
            {
                List<DTOTipoPropiedad> transferenciaTipoPropiedad = tareaTraerTP.Result.Content.ReadAsAsync<List<DTOTipoPropiedad>>().Result;

                foreach (var TP in transferenciaTipoPropiedad)
                {

                    DTOTipoPropiedadTipoAmbiente dTOTipoPropiedadTipoAmbiente = new DTOTipoPropiedadTipoAmbiente();

                    DTOTipoPropiedad dTOTipoPropiedad = new DTOTipoPropiedad();
                    dTOTipoPropiedad.nombreTipoPropiedad = TP.nombreTipoPropiedad;
                    dTOTipoPropiedad.tipoPropiedadId = TP.tipoPropiedadId;

                    dTOTipoPropiedadTipoAmbiente.dTOTipoPropiedades = dTOTipoPropiedad;

                    int count;
                    for (count = 0; count < TP.dTOTipoAmbiente.Count(); count++)
                    {
                        var tareaTraerTPTA = httpClient.GetAsync("api/TipoAmbiente/obtenerPorID/" + TP.dTOTipoAmbiente.ElementAt(count).tipoAmbienteId);
                        tareaTraerTPTA.Wait();

                        if (!tareaTraerTPTA.Result.IsSuccessStatusCode)
                        {
                            errorPropy.codigoError = (int)tareaTraerTPTA.Result.StatusCode;
                            errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraerTPTA.Result.StatusCode;
                            ListaTipoPropiedadTipoAmbiente = null;
                        }
                        else
                        {
                            TipoAmbiente transferenciaTipoAmbiente = tareaTraerTPTA.Result.Content.ReadAsAsync<TipoAmbiente>().Result;

                            DTOTipoAmbiente dTOTipoAmbiente = new DTOTipoAmbiente();
                            dTOTipoAmbiente.tipoAmbienteId = transferenciaTipoAmbiente.tipoAmbienteId;
                            dTOTipoAmbiente.nombreTipoAmbiente = transferenciaTipoAmbiente.nombreTipoAmbiente;

                            dTOTipoPropiedadTipoAmbiente.dTOTipoAmbientes.Add(dTOTipoAmbiente);
                        }
                    }
                    if (count == 0)
                    {
                        dTOTipoPropiedadTipoAmbiente.dTOTipoAmbientes.Add(new DTOTipoAmbiente { nombreTipoAmbiente = "Ninguno", tipoAmbienteId = null });
                    }
                    ListaTipoPropiedadTipoAmbiente.Add(dTOTipoPropiedadTipoAmbiente);
                }
            }
            return (errorPropy,ListaTipoPropiedadTipoAmbiente);
        }
    }
}
