using API_Persistencia.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using System;
using System.Threading;
using System.Threading.Tasks;


public class GoogleOAuth2Service
{
    private readonly GoogleAuthorizationCodeFlow flow;
    private readonly string redirectUri;
   
    public GoogleOAuth2Service(string clientId, string clientSecret, string redirectUri)
    {
        this.flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = new ClientSecrets
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
            },
            Scopes = new[] { Oauth2Service.Scope.UserinfoEmail },
        });
        this.redirectUri = redirectUri;
    }

    public string GetAuthorizationUrl()
    {
        var uri = flow.CreateAuthorizationCodeRequest(redirectUri).Build();
        return uri.ToString();
    }

    public async Task<Usuario> AuthorizeAsync(string code)
    {
        var token = await flow.ExchangeCodeForTokenAsync("", code, redirectUri, CancellationToken.None);
        return new Usuario
        {
            AccessToken = token.AccessToken,
            RefreshToken = token.RefreshToken,
            ExpirationTime = token.ExpiresInSeconds.HasValue ? DateTime.Now.AddSeconds(token.ExpiresInSeconds.Value) : default(DateTime?),
        };
    }

    public async Task<Userinfoplus> GetUserInfoAsync(Usuario credentials)
    {
        var credential = new UserCredential(flow, "", new TokenResponse
        {
            AccessToken = credentials.AccessToken,
            RefreshToken = credentials.RefreshToken,
            ExpiresInSeconds = (long)(credentials.ExpirationTime - DateTime.Now)?.TotalSeconds,
        });
        var oauth2Service = new Oauth2Service(new Google.Apis.Services.BaseClientService.Initializer { HttpClientInitializer = credential });
        var userInfo = await oauth2Service.Userinfo.Get().ExecuteAsync();
        return userInfo;
    }
}