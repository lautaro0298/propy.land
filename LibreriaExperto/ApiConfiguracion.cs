
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
            //configuracion linea https://core.propy.land/
            //configuracion linea http://propyy.somee.com/
            //configuracion local sin ssl http://localhost:5000/
            //configuracion local con ssl https://localhost:5001/
            cliente.BaseAddress = new Uri("http://propyy.somee.com/");
            return cliente;
        }
    }
}
