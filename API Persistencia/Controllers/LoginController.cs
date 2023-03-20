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
        private readonly UsuarioController usuario;
        public LoginController(ConexionDB conexion)
        {
            con = conexion;

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
        public async Task<Usuario> GoogleAuthCallback([FromQuery(Name = "code")] string code)
        {
            var oauthService = new GoogleOAuth2Service(ClientId, ClientSecret, "");
            var accessToken =await oauthService.AuthorizeAsync(code);
            

            // Aquí puedes almacenar la información del usuario en tu base de datos
            // y crear el token de acceso para la API REST

            return accessToken;
        }

    }
}
