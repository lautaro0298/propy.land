using LibreriaClases.Transferencia;
using LibreriaClases.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LibreriaExperto.ConsultaAsincrona
{
    public class ExpertoConsultaAsincrona
    {
        private int CreditosActuales { get; set; }
       // se agrega este codigo para poder obtener caracterisiticas en todo los controllers necesarios
        public List<DTOCaracteristica> ConsultaCaracteristica(string tipoPropiedad)
        {
            HttpClient httpClient = ApiConfiguracion.Inicializar();
            
            var tareaObtenercaracteristicas = httpClient.GetAsync($"api/TipoPropiedadCaracteristica/ObtenerPorIDdePropiedad/{tipoPropiedad}");
            tareaObtenercaracteristicas.Wait();
            var carac = tareaObtenercaracteristicas.Result.Content.ReadAsAsync<List<DTOCaracteristica>>().Result;
            
            return carac;
        }
        public int Consulta_Creditos_Async(string User)
        {
            CreditosActuales = 0;

            HttpClient httpClient = ApiConfiguracion.Inicializar();
            var tareaConsultarCreditos = httpClient.GetAsync("api/PlanUsuario/ObtenerPorID/" + User);
            tareaConsultarCreditos.Wait();

            if (!tareaConsultarCreditos.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaConsultarCreditos.Result.StatusCode.ToString());
            }
            else
            {
                TransferenciaPlanUsuario transferenciaPlanUsuario = tareaConsultarCreditos.Result.Content.ReadAsAsync<TransferenciaPlanUsuario>().Result;
                if (transferenciaPlanUsuario != null)
                {
                    CreditosActuales = transferenciaPlanUsuario.cantidadCreditosActivos;
                }
                else { CreditosActuales = 0; }
            }
            return CreditosActuales;
        }
    }
}
