
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LibreriaExperto
{
    public static class ApiConfiguracion
    {
      
        public static HttpClient Inicializar()
        {
            var cliente = new HttpClient();
            //http://jsebastianmartin-001-site1.itempurl.com/
            //Notebook casa Seba https://localhost:44312/0
            //PC Casa Seba https://localhost:44371/
            //configuracion local http://propyy.somee.com/
            //configuracion local http://localhost:33763/
            //en linea https://core.propy.land/
            cliente.BaseAddress = new Uri("http://localhost:33763/");
            return cliente;
        }
    }
}
