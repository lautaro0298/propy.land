using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApp.Servicios
{
    public static class ComunicacionApiServicios
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