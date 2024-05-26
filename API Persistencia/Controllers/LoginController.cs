using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API_Persistencia.Models;
using Microsoft.AspNetCore.Authentication;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private string ClientId = "224298944678-d4rokpd16rm8hjb72c91rs4h7egn2hns.apps.googleusercontent.com";
        private string ClientSecret = "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";
        private ConexionDB con;
        private GoogleOAuth2Service GoogleOAuth2Service;
        private UsuarioController usuario;

        public LoginController(ConexionDB conexion)
        {
            con = conexion;
            GoogleOAuth2Service = new GoogleOAuth2Service(con, ClientId, ClientSecret, "");
            usuario = new UsuarioController(con);
        }

        [HttpGet("auth/google")]
        public IActionResult GoogleAuth()
        {
            var scope = "email profile";
            var googleAuthUrl = "https://accounts.google.com/o/oauth2/auth?" +
                $"client_id={ClientId}&" +
                "response_type=code&" +
                $"redirect_uri={GoogleOAuth2Service.GetAuthorizationUrl()}&" +
                $"scope={scope}";

            return Redirect(googleAuthUrl);
        }

        [HttpGet("callback")]
        public async Task<Usuario> GoogleAuthCallback(string tokenDeId)
        {
            try
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var accessToken = GoogleOAuth2Service.AuthorizeAsync(tokenDeId).Result;
                var validPayload = GoogleOAuth2Service.AuthorizeAsync(tokenDeId).Result;

                if (accessToken != null)
                {
                    var email = accessToken.Email;
                    var token = accessToken.Scope;
                    var nombre = accessToken.GivenName;
                    var apellido = accessToken.FamilyName;

                    Usuario user = await usuario.ObtenerUsuarioPorEmail(email);

                    if (user != null)
                    {
                        user.emailConfirmado = true;
                        user.email = email;
                        user.apellidoUsuario = apellido;
                        user.nombreUsuario = nombre;
                        await usuario.EditarUsuarioGoogle(user);
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
                        await usuario.CrearCuenta(user);
                    }

                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return null;
            }
        }
    }

    public class GoogleOAuth2Service
    {
        private ConexionDB con;
        private string ClientId;
        private string ClientSecret;
        private string RedirectUri;

        public GoogleOAuth2Service(ConexionDB con, string clientId, string clientSecret, string redirectUri)
        {
            this.con = con;
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.RedirectUri = redirectUri;
        }

        public string GetAuthorizationUrl()
        {
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = ClientId,
                    ClientSecret = ClientSecret
                },
                Scopes = new[] { "email", "profile" }
            });

            var result = flow.CreateAuthorizationCodeRequest("state");
            return result.Build().AbsoluteUri;
        }

        public async Task<TokenResponse> AuthorizeAsync(string code)
        {
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = ClientId,
                    ClientSecret = ClientSecret
                },
                Scopes = new[] { "email", "profile" }
            });

            try
            {
                var result = await flow.ExchangeCodeForTokenAsync("", code, RedirectUri, CancellationToken.None);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
