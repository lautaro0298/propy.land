using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using WebApp.DTO;

namespace WebApp.Servicios
{
    public class GeolocalizacionServicios
    {
        public DTOUbicacionIP GeolocalizacionIP(string ipss) {
            IPHostEntry IPHost = Dns.GetHostEntry(Dns.GetHostName());
            string ip = new WebClient().DownloadString("https://api.ipify.org");

            string host = Dns.GetHostName();
            IPAddress[] ips = Dns.GetHostAddresses(host);

            const string apiKey = "AIzaSyAXxPwQsLSfF4gC7VtJdl9GIIcyAzVdmhk";
            string strQuery = "http://api.ipinfodb.com/v3/ip-city/?" + "ip=" + ip + "&key=" + apiKey + "&format=xml";
            //var url = "http://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}&format=xml";
            var doc = XDocument.Load(string.Format(strQuery, apiKey, ip));

            DTOUbicacionIP ubicacion = new DTOUbicacionIP();
            ubicacion.pais = doc.Descendants("countryName").First().Value;

            return ubicacion;
        }
    }
}