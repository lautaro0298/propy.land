using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LibreriaExperto.Desarrollo
{
    public static class ABMPaquete
    {
        public static ErrorPropy CrearPaquete(string nombre, int cantidad, string tipomoneda , decimal precio )
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();

            #region Crear instancia 
            TransferenciaCredito credito = new TransferenciaCredito();
            credito.PaqueteID = Guid.NewGuid().ToString();
            credito.Activo = true;
            credito.NombrePack = nombre;
            credito.CantidadCreditos = cantidad;
            credito.Precio = precio;
            #endregion
            #region Crear instancia de Tipomoneda
            credito.TipoMonedaID = tipomoneda;
            #endregion
            var tareaCrearTipoPropiedad = clienteHttp.PostAsJsonAsync<TransferenciaCredito>("api/Credito/crearPack", credito);
            tareaCrearTipoPropiedad.Wait();
            if (!tareaCrearTipoPropiedad.Result.IsSuccessStatusCode)
            {
                error.codigoError = (int)tareaCrearTipoPropiedad.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaCrearTipoPropiedad.Result.StatusCode;
                return error;
            }

            return error;

        }
         public static (ErrorPropy, List<DTOCredito>) traerCredito()
        {
            ErrorPropy errorPropy = new ErrorPropy();
            List<DTOCredito> ListaMonedas = new List<DTOCredito>();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            var tareaTraerMonedas = httpClient.GetAsync("api/Credito/obtenerCredito");
            tareaTraerMonedas.Wait();

            if (!tareaTraerMonedas.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaTraerMonedas.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraerMonedas.Result.StatusCode;
                ListaMonedas = null;
            }
            else
            {
                List<TransferenciaCredito> transferenciaCreditos = tareaTraerMonedas.Result.Content.ReadAsAsync<List<TransferenciaCredito>>().Result;
                foreach (var TM in transferenciaCreditos)
                {
                    DTOCredito dTOCredito = new DTOCredito();
                    dTOCredito.NombrePack = TM.NombrePack;
                    dTOCredito.PaqueteID = TM.PaqueteID;
                    dTOCredito.CantidadCreditos = TM.CantidadCreditos;
                    dTOCredito.Precio = TM.Precio;
                    dTOCredito.TipoMonedaID = TM.TipoMonedaID;
                    TransferenciaTipoMoneda Moneda = ABMTipoMoneda.buscarporid(TM.TipoMonedaID);
                    dTOCredito.TipoMoneda = new DTOTipoMoneda();
                    dTOCredito.TipoMoneda.denominacionMoneda = Moneda.denominacionMoneda;
                    dTOCredito.TipoMoneda.tipoMonedaId = Moneda.tipoMonedaId;
                    
                    ListaMonedas.Add(dTOCredito);
                }
            }

            return (errorPropy, ListaMonedas);
        }
        public static ErrorPropy EliminarPaquete(String id)
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            var tareaMoneda = clienteHttp.GetAsync("api/Credito/obtenerPorID/" + id);
            tareaMoneda.Wait();
            if (!tareaMoneda.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaMoneda.Result.StatusCode.ToString());
            }
            TransferenciaCredito tipo = tareaMoneda.Result.Content.ReadAsAsync<TransferenciaCredito>().Result;
            var tareaeliminarCredito = clienteHttp.PostAsJsonAsync<TransferenciaCredito>("api/Credito/eliminarCredito", tipo);
            tareaeliminarCredito.Wait();
            if (!tareaeliminarCredito.Result.IsSuccessStatusCode)
            {
                error.codigoError = (int)tareaeliminarCredito.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaeliminarCredito.Result.StatusCode;
                return error;
            }

            return error;
        }
        public static ErrorPropy EditarCredito(DTOCredito dTOCredito)
        {
            ErrorPropy errorPropy = new ErrorPropy();
            HttpClient httpClient = ApiConfiguracion.Inicializar();
            TransferenciaCredito transferenciaCredito = new TransferenciaCredito();

            transferenciaCredito.PaqueteID = dTOCredito.PaqueteID;
            transferenciaCredito.NombrePack = dTOCredito.NombrePack;
            transferenciaCredito.Precio = dTOCredito.Precio;
            
            transferenciaCredito.TipoMonedaID =dTOCredito.TipoMonedaID;
            transferenciaCredito.CantidadCreditos = dTOCredito.CantidadCreditos;
           
            transferenciaCredito.Activo = true;

            var tareaCrearCredito = httpClient.PostAsJsonAsync<TransferenciaCredito>("api/Credito/EditarCredito", transferenciaCredito);
            tareaCrearCredito.Wait();

            if (!tareaCrearCredito.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaCrearCredito.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaCrearCredito.Result.StatusCode;
            }

            return errorPropy;
        }

    }
}
