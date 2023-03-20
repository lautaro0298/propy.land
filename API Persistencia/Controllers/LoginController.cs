using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Auth;
using System.Threading.Tasks;
using API_Persistencia.Models;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ConexionDB con;
        private readonly UsuarioController usuario;
        public LoginController(ConexionDB conexion)
        {
            con = conexion;

        }
        [HttpPost]
        [Route("google-login")]
        public async Task<Usuario> GoogleLogin( string token)
        {
            
        var validPayload = await GoogleJsonWebSignature.ValidateAsync(token);
            Usuario usuario1 = new Usuario();
            if (validPayload != null || !validPayload.Equals(null)) { return usuario1 ; }
            else
            {
                String email = (String)validPayload.Email;
                bool emailVerified = validPayload.EmailVerified;
                String name = (String)validPayload.Name;
                String pictureUrl = (String)validPayload.Picture;

                String familyName = (String)validPayload.FamilyName;
                String givenName = (String)validPayload.GivenName;
                usuario1= usuario.ObtenerUsuarioPorEmail(email);
                    if (usuario1.Equals(null) || usuario1==null) {
                    usuario1.email = email;
                    usuario1.apellidoUsuario = familyName;
                    usuario1.nombreUsuario = givenName;
                    usuario1.emailConfirmado = true;
                    usuario1.usuarioId = Guid.NewGuid().ToString();
                     usuario.CrearCuenta(usuario1);
                    }
                return usuario1;
            }
            
        }
    }
}
