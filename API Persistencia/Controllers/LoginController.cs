using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Auth;
using System.Threading.Tasks;
using API_Persistencia.Models;
using Microsoft.AspNetCore.Authentication;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        string ClientId = "224298944678-d4rokpd16rm8hjb72c91rs4h7egn2hns.apps.googleusercontent.com";
        string ClientSecret = "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";
        private ConexionDB con;
        private GoogleOAuth2Service GoogleOAuth2Service;
        private UsuarioController usuario;
        public LoginController(ConexionDB conexion)
        {
            con = conexion;
            usuario=new UsuarioController(con);
        }
        [HttpGet("auth/google")]
        public IActionResult GoogleAuth()
        {
            var googleAuthUrl = "https://accounts.google.com/o/oauth2/auth?" +
                $"client_id={ClientId}&" +
                "response_type=code&" +
                $"redirect_uri={GoogleOAuth2Service.GetAuthorizationUrl()}&" +
                $"scope={string.Join(" ", "")}";

            return Redirect(googleAuthUrl);
        }

        [HttpGet("callback")]
        public async Task<Usuario> GoogleAuthCallback(string tokenDeId)
        {

            //var tokenDeIdwe = await HttpContext.GetTokenAsync("id_token");
            var oauthService = new GoogleOAuth2Service(con,ClientId, ClientSecret, "");
            var accessToken =  oauthService.AuthorizeAsync(tokenDeId);
            var validPayload = await oauthService.AuthorizeAsync(tokenDeId);
            accessToken.Wait();
            // Aquí puedes almacenar la información del usuario en tu base de datos
            // y crear el token de acceso para la API REST

            var email = accessToken.Result.Email;
            var token = accessToken.Result.Scope;
            
            var nombre = accessToken.Result.GivenName;
            var apellido = accessToken.Result.FamilyName;
       
            Usuario user =usuario.ObtenerUsuarioPorEmail(email);
            
            
            if (user != null)
            {
                user.emailConfirmado = true;
                user.email = email;
                user.apellidoUsuario = apellido;
                user.nombreUsuario = nombre;
                usuario.EditarUsuarioGoogle(user);
            }
            else
            {
                user = new Usuario();
                user.emailConfirmado = true;
                user.email = email;
                user.apellidoUsuario = apellido;
                user.nombreUsuario = nombre;
                // El usuario no existe en la base de datos, crear un nuevo usuario
                user.usuarioId = Guid.NewGuid().ToString();
                usuario.CrearCuenta(user);
            }


            return user;
        }

    }
}
