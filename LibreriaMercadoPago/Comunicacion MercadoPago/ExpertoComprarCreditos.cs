using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;
using LibreriaExperto;
using LibreriaExperto.Usuarios;
using LibreriaMercadoPago.DTO;
using LibreriaMercadoPago.Interface_Compra;
using MercadoPago.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaMercadoPago.Comunicacion_MercadoPago
{
    public class ExpertoComprarCreditos<T> : Compra<T> where T : DTOCredito
    {
        private string ClientID = "310547333208529";
        private string ClientSecret = "bQBIrPmu2I8OacRxbknqGR4wlPnvWw0O";
        private string AccessToken = "TEST-310547333208529-010219-d8cdcce154be5b8148af156c1502b1f1-167342294";


        #region Acá se arma la vista de compra

        public (ErrorPropy, List<T>) ObtenerPlanes_o_Credito()
        {

            ErrorPropy errorPropy = new ErrorPropy();
            List<T> List = new List<T>();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            var tareaTraerCreditos = httpClient.GetAsync("api/Credito/Obtener_Packs_Creditos");
            tareaTraerCreditos.Wait();

            if (!tareaTraerCreditos.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaTraerCreditos.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraerCreditos.Result.StatusCode;
                throw new Exception(errorPropy.descripcionError);
            }
            else
            {
                List<TransferenciaCredito> transferenciaCreditos = tareaTraerCreditos.Result.Content.ReadAsAsync<List<TransferenciaCredito>>().Result;

                foreach (var PackCredito in transferenciaCreditos)
                {
                    DTOCredito dTOCredito = new DTOCredito();
                    dTOCredito.CantidadCreditos = PackCredito.CantidadCreditos;
                    dTOCredito.Precio = PackCredito.Precio;
                    dTOCredito.PaqueteID = PackCredito.PaqueteID;
                    dTOCredito.NombrePack = PackCredito.NombrePack;
                    dTOCredito.TipoMoneda = new DTOTipoMoneda { denominacionMoneda = PackCredito.TipoMoneda.denominacionMoneda, tipoMonedaId = PackCredito.TipoMoneda.tipoMonedaId };

                    List.Add((T)dTOCredito);
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
                foreach (var PackCredito in dTO)
                {
                    Preference preference = new Preference();
                    MercadoPago.DataStructures.Preference.Item item = new MercadoPago.DataStructures.Preference.Item();
                    MercadoPago.DataStructures.Preference.Payer payer = new MercadoPago.DataStructures.Preference.Payer();

                    switch (PackCredito.TipoMoneda.denominacionMoneda)
                    {
                        case "ARS":
                            item.CurrencyId = MercadoPago.Common.CurrencyId.ARS;
                            break;
                            //Agregar mas cases en caso de agregar otras monedas del MERCOSUR
                    }

                    item.Id = PackCredito.PaqueteID;
                    item.Title = PackCredito.NombrePack;
                    item.UnitPrice = PackCredito.Precio;
                    item.Quantity = 1;

                    payer.Email = result.TransferenciaUsuario.email;
                    payer.Name = result.TransferenciaUsuario.nombreUsuario;
                    payer.Surname = result.TransferenciaUsuario.apellidoUsuario;
                    payer.DateCreated = DateTime.Now;

                    preference.Items.Add(item);
                    preference.Payer = payer;
                    preference.Save();

                    dTOCompra.preferences.Add(preference);
                    dTOCompra.dTOCreditos.Add(PackCredito);
                }
            }
            return (result.errorPropy, dTOCompra);
        }
        #endregion
    }
}

