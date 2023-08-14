
using API_Persistencia.Models;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

public class GoogleOAuth2Service
{
    private readonly GoogleAuthorizationCodeFlow flow;
    private readonly string redirectUri;
    public API_Persistencia.ConexionDB con;
    public GoogleOAuth2Service(API_Persistencia.ConexionDB con, string clientId, string clientSecret, string redirectUri)
    {
        this.flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = new ClientSecrets
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
            },
            Scopes = new[] { Oauth2Service.Scope.UserinfoEmail, Oauth2Service.Scope.UserinfoProfile, Oauth2Service.Scope.PlusMe },
        });
        this.redirectUri = "https://localhost:44394/Usuario/Login";
        this.con = con;
    }


    public string GetAuthorizationUrl()
    {
        var uri = flow.CreateAuthorizationCodeRequest(redirectUri).Build();
        return uri.ToString();
    }

    public async Task<Payload>  AuthorizeAsync(string tokenDeId)
    {
        try
        {
            
            var validPayload = await GoogleJsonWebSignature.ValidateAsync(tokenDeId);

            return validPayload;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
        
    }

   
}