using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LibreriaExperto.Desarrollo
{
    public static class ABMPlan
    {
        public static ErrorPropy CrearPlan(DTOPlan dTOPlan)
        {
            ErrorPropy errorPropy = new ErrorPropy();
            HttpClient httpClient = ApiConfiguracion.Inicializar();
            TransferenciaPlan transferenciaPlan = new TransferenciaPlan();

            transferenciaPlan.planId = System.Guid.NewGuid().ToString();
            transferenciaPlan.nombrePlan = dTOPlan.nombrePlan;
            transferenciaPlan.precioPlan = dTOPlan.precioPlan;
            transferenciaPlan.TipoMonedaID = dTOPlan.tipoMonedaId;
            transferenciaPlan.cantidadCreditosIniciales = dTOPlan.cantidadCreditosIniciales;
            transferenciaPlan.cantidadMaxImagenesPermitidasPorPublicacion = dTOPlan.cantidadMaxImagenesPermitidasPorPublicacion;
            transferenciaPlan.permiteVideo = dTOPlan.permiteVideo;
            transferenciaPlan.accesoEstadisticasAvanzadas = dTOPlan.accesoEstadisticasAvanzadas;
            transferenciaPlan.activo = true;

            var tareaCrearPLan = httpClient.PostAsJsonAsync<TransferenciaPlan>("api/Plan/crearPlan", transferenciaPlan);
            tareaCrearPLan.Wait();

            if (!tareaCrearPLan.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaCrearPLan.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaCrearPLan.Result.StatusCode;
            }

            return errorPropy;
        }
        public static (ErrorPropy, List<DTOPlan>) traerPlan()
        {
            ErrorPropy errorPropy = new ErrorPropy();
            List<DTOPlan> ListaPlanes = new List<DTOPlan>();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            var tareaTraerPlan = httpClient.GetAsync("api/Plan/obtenerPlanes");
            tareaTraerPlan.Wait();


            if (!tareaTraerPlan.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaTraerPlan.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraerPlan.Result.StatusCode;
                ListaPlanes = null;
            }
            else
            {
                ListaPlanes = tareaTraerPlan.Result.Content.ReadAsAsync<List<DTOPlan>>().Result;
                
                
            }
            return (errorPropy, ListaPlanes);
        }
        public static ErrorPropy EditarPlan(DTOPlan dTOPlan)
        {
            ErrorPropy errorPropy = new ErrorPropy();
            HttpClient httpClient = ApiConfiguracion.Inicializar();
            TransferenciaPlan transferenciaPlan = new TransferenciaPlan();

            transferenciaPlan.planId = dTOPlan.planId;
            transferenciaPlan.nombrePlan = dTOPlan.nombrePlan;
            transferenciaPlan.precioPlan = dTOPlan.precioPlan;
            transferenciaPlan.TipoMonedaID = dTOPlan.tipoMonedaId;
            transferenciaPlan.cantidadCreditosIniciales = dTOPlan.cantidadCreditosIniciales;
            transferenciaPlan.cantidadMaxImagenesPermitidasPorPublicacion = dTOPlan.cantidadMaxImagenesPermitidasPorPublicacion;
            transferenciaPlan.permiteVideo = dTOPlan.permiteVideo;
            transferenciaPlan.accesoEstadisticasAvanzadas = dTOPlan.accesoEstadisticasAvanzadas;
            transferenciaPlan.activo = true;

            var tareaCrearPLan = httpClient.PostAsJsonAsync<TransferenciaPlan>("api/Plan/EditarPlan", transferenciaPlan);
            tareaCrearPLan.Wait();

            if (!tareaCrearPLan.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaCrearPLan.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaCrearPLan.Result.StatusCode;
            }

            return errorPropy;
        }
        public static ErrorPropy EliminarPlan(string idPlan)
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var Plan = clienteHttp.GetAsync("api/Plan/obtenerPorID/" + idPlan);
            Plan.Wait();
            if (!Plan.Result.IsSuccessStatusCode)
            {
                throw new Exception(Plan.Result.StatusCode.ToString());
            }

            TransferenciaPlan tipo = Plan.Result.Content.ReadAsAsync<TransferenciaPlan>().Result;
            var tareaeliminarPlan = clienteHttp.PostAsJsonAsync<TransferenciaPlan>("api/Plan/eliminarPlan", tipo);
            tareaeliminarPlan.Wait();
            if (!tareaeliminarPlan.Result.IsSuccessStatusCode)
            {
                error.codigoError = (int)tareaeliminarPlan.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaeliminarPlan.Result.StatusCode;
                return error;
            }

            return error;

        }
    }
}
