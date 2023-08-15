using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;
using LibreriaExperto;
using LibreriaExperto.Usuarios;
using LibreriaMercadoPago.DTO;
using LibreriaMercadoPago.Interface_Compra;
using LibreriaMercadoPago.Plan;
using MercadoPago;
using MercadoPago.DataStructures.Customer;
using MercadoPago.Resources;

namespace LibreriaMercadoPago.Comunicacion_MercadoPago
{
    public class ExpertoContratarPlan<T> : Compra<T> where T : DTOPlan
    {
        private string ClientID = "6676181862368985";
        private string ClientSecret = "rEm8ib9tejXnIWw5qpgBB7OqsDejL4nX";
        private string AccessToken = "APP_USR-6676181862368985-081423-3f6228b8f78a72719143da7fa47cc5b4-172147688";


        #region Acá se arma la vista de compra

        public (ErrorPropy, List<T>) ObtenerPlanes_o_Credito()
        {

            ErrorPropy errorPropy = new ErrorPropy();
            List<T> List = new List<T>();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            var tareaTraerPlanes = httpClient.GetAsync("api/Plan/obtenerPlanes");
            tareaTraerPlanes.Wait();

            if (!tareaTraerPlanes.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaTraerPlanes.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraerPlanes.Result.StatusCode;
                throw new Exception(errorPropy.descripcionError);
            }
            else
            {
                List<TransferenciaPlan> transferenciaPlanes = tareaTraerPlanes.Result.Content.ReadAsAsync<List<TransferenciaPlan>>().Result;

                foreach (var Plan in transferenciaPlanes)
                {
                    DTOPlan dTOPlan = new DTOPlan();
                    dTOPlan.Vencimiento = Plan.Vencimiento;
                    dTOPlan.nombrePlan = Plan.nombrePlan;
                    dTOPlan.precioPlan = Plan.precioPlan;
                    dTOPlan.permiteVideo = Plan.permiteVideo;
                    dTOPlan.accesoEstadisticasAvanzadas = Plan.accesoEstadisticasAvanzadas;
                    dTOPlan.cantidadCreditosIniciales = Plan.cantidadCreditosIniciales;
                    dTOPlan.cantidadMaxImagenesPermitidasPorPublicacion = Plan.cantidadMaxImagenesPermitidasPorPublicacion;
                    dTOPlan.planId = Plan.planId;
                    dTOPlan.tipoMoneda = new DTOTipoMoneda { denominacionMoneda = Plan.TipoMoneda.denominacionMoneda, tipoMonedaId = null };
                    dTOPlan.tipoMoneda.tipoMonedaId = Plan.TipoMoneda.tipoMonedaId;

                    List.Add((T)dTOPlan);
                }
            }
            return (errorPropy, List);
        }

        public (ErrorPropy, DTOCompra) ArmarDTOCompra<R>(string user, List<R> dTO) where R : T
        {
            MercadoPago.SDK.CleanConfiguration();
            MercadoPago.SDK.ClientId = ClientID;
            MercadoPago.SDK.ClientSecret = ClientSecret;
            MercadoPago.SDK.AccessToken = AccessToken;

            HttpClient httpClient = ApiConfiguracion.Inicializar();
            DTOCompra dTOCompra = new DTOCompra();

            (ErrorPropy errorPropy, TransferenciaUsuario TransferenciaUsuario) result = ExpertoUsuarios.ObtenerUsuarioPorID(user, httpClient);
            if (result.errorPropy.codigoError != 0)
            {
                throw new Exception(result.errorPropy.descripcionError);
            }
            else
            {

                foreach (var Plan in dTO)
                {
                    Preference preference = new Preference();
                    MercadoPago.DataStructures.Preference.Item item = new MercadoPago.DataStructures.Preference.Item();
                    MercadoPago.DataStructures.Preference.Payer payer = new MercadoPago.DataStructures.Preference.Payer();

                    switch (Plan.tipoMoneda.denominacionMoneda)
                    {
                        case "ARS":
                            item.CurrencyId = MercadoPago.Common.CurrencyId.ARS;
                            break;
                            //Agregar mas cases en caso de agregar otras monedas del MERCOSUR
                    }

                    item.Id = Plan.planId;
                    item.Title = Plan.nombrePlan;
                    item.UnitPrice = Plan.precioPlan;
                    item.Quantity = 1;

                    payer.Email = result.TransferenciaUsuario.email;
                    payer.Name = result.TransferenciaUsuario.nombreUsuario;
                    payer.Surname = result.TransferenciaUsuario.apellidoUsuario;
                    payer.DateCreated = DateTime.Now;

                    preference.Items.Add(item);
                    preference.Payer = payer;
                    preference.Save();

                    dTOCompra.preferences.Add(preference);
                    dTOCompra.dTOPlanes.Add(Plan);
                }
            }
            return (result.errorPropy, dTOCompra);
        }
        #endregion   
    }
}
