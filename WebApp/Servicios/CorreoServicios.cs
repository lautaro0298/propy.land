using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using WebApp.Models;
using WebApp.DTO;
using WebApp.Servicios;

namespace WebApp.Servicios
{
    public class CorreoServicios
    {
        

        public async Task EnviarCorreoAvisoNuevaPublicacion(Guid publicacionId) {
            using (var db = new ApplicationDbContext()) {
                
                var dataPublicacion = (from p in db.Publicacion
                                       where p.publicacionId.ToString() == publicacionId.ToString()
                                       select p).FirstOrDefault();
                string tipoOperacion = dataPublicacion.TipoPublicacion.nombreTipoPublicacion;
                string ubicacion = dataPublicacion.Propiedad.calle;
                try
                {
                    MailMessage correo = new MailMessage();
                    correo.From = new MailAddress("jsebastianmartin91@gmail.com");
                    correo.To.Add(dataPublicacion.ApplicationUser.Email);
                    correo.Subject = "Propy aviso";
                    correo.Body = "<h1>¡Gracias por publicar en Propy!</h1>" +
                        "<p>Su operación de <strong>" + tipoOperacion + "</strong> para la Propiedad con dirección <strong>" + ubicacion + "</strong> ha sido <strong>publicada satisfactoriamente</strong></p>" +
                        "<p>¡Ahora su Propiedad puede ser vista por miles de personas!</p>"+
                        "<h4>Para más información respecto a esta propiedad consulte sus estadísticas en su panel de control de Propy</h4>" +
                        "<p>¡Es un placer tenerte con nosotros!</p>";
                    correo.IsBodyHtml = true;
                    correo.Priority = MailPriority.Normal;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 25;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    string cuentaCorreo = "jsebastianmartin91@gmail.com";
                    string passwordCorreo = "moonwalker2";
                    smtp.Credentials = new System.Net.NetworkCredential(cuentaCorreo, passwordCorreo);
                    await smtp.SendMailAsync(correo);
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        public async Task EnviarCorreoAvisoVisualización(string usuarioId,Guid publicacionId) {
            using (var db = new ApplicationDbContext()) {

                var publicacion = (from pub in db.Publicacion
                                   where pub.publicacionId.ToString() == publicacionId.ToString()
                                   select pub).FirstOrDefault();

                //Capturo los datos del usuario que hizo click
                var usuario = (from u in db.Users
                               where u.Id == usuarioId
                               select u).FirstOrDefault();
                string nombreUsuario = usuario.nombre;
                string apellidoUsuario = usuario.apellido;
                string emailUsuario = usuario.Email;
                long telefonoUsuario = usuario.nroTelefono;
                bool permiteSerContactadoPorPublicante = usuario.permitirSerContactadoPublicante;

                //Capturo el email del usuario que realizo la publicacion
                string direccionEmail = publicacion.ApplicationUser.Email;

                //Capturo los datos de la publicación en la que hizo click
                string tipoPropiedad = publicacion.Propiedad.TipoPropiedad.nombreTipoPropiedad;
                DateTime fechaPublicacion = publicacion.fechaInicioPublicacion;
                string direccionPropiedad = publicacion.Propiedad.calle;
                
                string tipoOperacíon = publicacion.TipoPublicacion.nombreTipoPublicacion;

                //Obtengo el pais desde donde hizo el click
                GeolocalizacionServicios serviciosGeolocalizacion = new GeolocalizacionServicios();
                DTOUbicacionIP datosUbicacionIP = new DTOUbicacionIP();
                datosUbicacionIP = serviciosGeolocalizacion.GeolocalizacionIP(null);

                try
                {
                    
                    MailMessage correo = new MailMessage();
                    correo.From = new MailAddress("jsebastianmartin91@gmail.com");
                    correo.To.Add(direccionEmail);
                    correo.Subject = "Propy aviso";
                    if (permiteSerContactadoPorPublicante == true)
                    {
                        correo.Body = "<h1>¡Una de sus publicaciones ha sido Visualizada desde "+datosUbicacionIP.pais+ "</h1>"+
                        "<h4>A continuación Propy le presenta los datos de su publicación:</h4>" +
                        "<ul>" +
                            "<li><strong>Tipo de Propiedad: </strong>" + tipoPropiedad + "</li>" +
                            "<li><strong>Tipo de Operación:</strong>" + tipoOperacíon + "</li>" +
                            "<li><strong>Fecha de Registro de Propiedad:</strong>" + fechaPublicacion + "</li>" +
                            "<li><strong>Dirección de Propiedad:</strong>" + direccionPropiedad + "</li>" +
                        "</ul>" +
                        "<h4>A continuación Propy le presenta los datos de la persona que visualizó su propiedad</h4>" +
                        "<p>¡No dudes en ponerte en contacto con el/ella!</p>" +
                        "<ul><li><strong>Nombre Completo: </strong>" + nombreUsuario + " " + apellidoUsuario + "</li>" +
                            "<li><strong>Email: </strong><a href=mailto:" + emailUsuario + ">" + emailUsuario + "</a></li>" +
                            "<li><strong>Teléfono:</strong> " + telefonoUsuario + "</li>" +
                        "</ul>" +
                        "<h4>Para más información respecto a esta propiedad consulte sus estadísticas en su panel de control de Propy</h4>" +
                        "<p>¡Es un placer tenerte con nosotros!</p>"
                        ;



                        
                    }
                    else {
                        correo.Body = "<h1>¡Una de sus publicaciones ha sido Visualizada!</h1>" +
                        "<h4>A continuación Propy le presenta los datos de su publicación:</h4>" +
                        "<ul>" +
                            "<li><strong>Tipo de Propiedad: </strong>" + tipoPropiedad + "</li>" +
                            "<li><strong>Tipo de Operación:</strong>" + tipoOperacíon + "</li>" +
                            "<li><strong>Fecha de Registro de Propiedad:</strong>" + fechaPublicacion + "</li>" +
                            "<li><strong>Dirección de Propiedad:</strong>" + direccionPropiedad + "</li>" +
                        "</ul>" +
                        "<h4>El usuario que vió esta propiedad, no autorizó la muestra de sus datos</h4>" +
                        "<h4>Para más información respecto a esta propiedad consulte sus estadísticas en su panel de control de Propy</h4>" +
                        "<p>¡Es un placer tenerte con nosotros!</p>"
                        ;
                    }
                    
                    correo.IsBodyHtml = true;
                    correo.Priority = MailPriority.Normal;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 25;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    string cuentaCorreo = "jsebastianmartin91@gmail.com";
                    string passwordCorreo = "moonwalker2";
                    smtp.Credentials = new System.Net.NetworkCredential(cuentaCorreo,passwordCorreo);
                    await smtp.SendMailAsync(correo);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

                
        }
    }
}