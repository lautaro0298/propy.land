using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using WebApp.Models;

namespace WebApp.Servicios
{
    public static class ImagenServicios
    {
        
        public static List<string> SubirArchivo(string ruta,List<HttpPostedFileBase> imagenes) {
            string route = @"~/Temp/";
            List<string> rutas = new List<string>();
            foreach (var imagen in imagenes) {
                WebImage img = RedimensionarImagen(imagen);
                img.FileName = imagen.FileName;
                string nombreImagen = AsignarIdImagen(imagen);
                img.Save(route + nombreImagen);
                string rutaFormateada = @"/Temp/";
                rutas.Add(rutaFormateada + nombreImagen);
            }
            return rutas;
        }
        
        public static bool ValidarImagen(HttpPostedFileBase imagen) {
            bool validacionOk=false;
            string extensionArchivo = System.IO.Path.GetExtension(imagen.FileName).ToLower();
            string [] extensionesArchivosPermitidas = { ".gif", ".png", ".jpeg", ".jpg" };
            if (extensionesArchivosPermitidas.Contains(extensionArchivo)) {
                validacionOk = true;
            }

            return validacionOk;
        }
        public static WebImage RedimensionarImagen(HttpPostedFileBase imagen) {
            
            WebImage img = new WebImage(imagen.InputStream);
            if (img.Width > 1024 && img.Height > 768)
            {
                img.Resize(1024, 768,false);
            }
            else {
                img.Resize(1024,768,false);
            }
            return img;
        }
        public static string AsignarIdImagen(HttpPostedFileBase imagen) {
            string idImagen = System.Guid.NewGuid().ToString();
            return idImagen+imagen.FileName;
        }
        public static string ObtenerRutaRelativa2(string rutaAbsoluta) {
            string rutaRelativa = HttpContext.Current.Server.MapPath("~");
            
            string resultadoRuta = "/" + rutaAbsoluta.Substring(rutaRelativa.Length).Replace("\\", "/");
            return resultadoRuta;
        }
        public static string ObtenerRutaRelativa(ImagenPublicacion imagen) {
            string rutaRelativa = HttpContext.Current.Server.MapPath("~");
            string resultadoRuta = "/" +imagen.rutaBD.Substring(rutaRelativa.Length).Replace("\\", "/");
            return resultadoRuta;
            //string resRuta = imagen.rutaBD.Substring(rutaRelativa.Length).Replace("\\", "/");
            
        }
    }
}