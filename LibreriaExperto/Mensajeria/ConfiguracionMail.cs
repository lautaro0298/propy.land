﻿using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace LibreriaExperto.Mensajeria
{
    public static class ConfiguracionMail
    {
        public static SmtpClient Inicializar()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 25;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            string cuentaCorreo = "propy.land.com@gmail.com";
            string passwordCorreo = "Micontrasenaes12";
            smtp.Credentials = new System.Net.NetworkCredential(cuentaCorreo, passwordCorreo);
            return smtp;
        }
    }
}
