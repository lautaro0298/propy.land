using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaExperto.Mensajeria
{
    public static class ExpertoMensajeria
    {
        public static async Task EnviarMailAvisoVisita(string email, string ubicacion, string tipoPropiedad) {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("lautaro.0298@gmail.com");
            correo.To.Add(email);
            correo.Subject = "Propy aviso";
            correo.Body = "<h1>¡Una de sus publicaciones ha sido visualizada!</h1>" +
                "<p>¡Genial! Su "+tipoPropiedad+" que se encuentra en "+ubicacion+" ha sido visitada por un nuevo usuario.<p>" +
                "<p>Para más información consulte las estadísticas de su publicación a través del panel de control.<p>" +
                "<p>¡Es un placer tenerlo/a con nosotros!<p>";
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;

            SmtpClient smtp = ConfiguracionMail.Inicializar();
            await smtp.SendMailAsync(correo);
        }
        public static async Task EnviarMailAvisoCreditosAgotados(string email) {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("lautaro.0298@gmail.com");
            correo.To.Add(email);
            correo.Subject = "¡Puntos de crédito agotados!";
            correo.Body = "<h1>¡Se ha quedado sin créditos!</h1>" +
                "<p>Propy le informa que su cuenta no dispone de créditos suficientes como para que sus publicaciones sean visualizadas en la plataforma.<p>" +
                "<p>Para continuar usando los servicios de Propy realice la compra de más créditos a través de su panel de control.<p>"+
                "<p>Saludos cordiales.<p>";
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;

            SmtpClient smtp = ConfiguracionMail.Inicializar();
            await smtp.SendMailAsync(correo);
        }
        public static async Task EnviarMailRecuperarClave(string email,string rutaRecuperarClave) {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("lautaro.0298@gmail.com");
            correo.To.Add(email);
            correo.Subject = "¡Recupere su clave!";
            correo.Body = "<h1>Recuperar Clave</h1>" +
                "<a href='"+RutaConfig.rutaBaseLocal+rutaRecuperarClave + "'>Haga click en este enlace para recuperar su clave</a>";
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;

            SmtpClient smtp = ConfiguracionMail.Inicializar();
            await smtp.SendMailAsync(correo);
        }
        public static async Task ReenvioMailVerificacion(string email,string rutaConfirmarCuenta) {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("lautaro.0298@gmail.com");
            correo.To.Add(email);
            correo.Subject = "Confirme su cuenta en Propy";
            correo.Body = "<h1>Confirme su cuenta en Propy</h1>" +
                "<p>Debe validar su email para poder ingresar a Propy.</p>" +

                "<a href='" + RutaConfig.rutaBaseLocal + rutaConfirmarCuenta + "'>Haga click en este enlace para confirmar su cuenta</a>";
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;

            SmtpClient smtp = ConfiguracionMail.Inicializar();
            await smtp.SendMailAsync(correo);
        }
        public static async Task EnviarEmailBienvenida(string nombreUsuario, string email, string rutaConfirmarCuenta)
        {

            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("lautaro.0298@gmail.com");
            correo.To.Add(email);
            correo.Subject = "¡Bienvenido a Propy!";
            correo.Body = "<h1>Bienvenido " + nombreUsuario + "</h1>" +
                "<p>Es un placer tenerte con nosotros.</p>" +
                "<p>Ya puedes acceder a tu panel de control ingresando a la plataforma.</p>" +
                "<a href='" + RutaConfig.rutaBaseLocal +  rutaConfirmarCuenta + "'>Haga click en este enlace para confirmar su cuenta</a>";
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;

            SmtpClient smtp = ConfiguracionMail.Inicializar();
            await smtp.SendMailAsync(correo);



        }
        public static async Task RestablecerContraseña(string email, string rutaRestablecerContraseña)
        {
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("lautaro.0298@gmail.com");
            correo.To.Add(email);
            correo.Subject = "Recuperar clave";
            correo.Body = "<h1>¡Recupere su clave!</h1>" +
                "<a href='" + RutaConfig.rutaBaseLocal +  rutaRestablecerContraseña + "'>Haga click en este enlace para restablecer su clave</a>";
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;

            SmtpClient smtp = ConfiguracionMail.Inicializar();
            await smtp.SendMailAsync(correo);
        }
        
    }
}
