using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace LibreriaExperto.Comunicaciones_Externas
{
    public static class PeticionHttpGet
    {
        public static string GetHTTP(string url)
        {
            WebRequest solicitudWeb = WebRequest.Create(url);
            WebResponse respuestaWeb = solicitudWeb.GetResponse();
            StreamReader sr = new StreamReader(respuestaWeb.GetResponseStream());
            return sr.ReadToEnd();
        }
    }
}
