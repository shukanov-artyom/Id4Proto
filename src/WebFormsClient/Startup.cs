using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Owin;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Linq;
using System.Security.Claims;
using IdentityModel.Client;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security.Notifications;

[assembly: OwinStartup(typeof(WebApp.Startup))]
namespace WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "Cookies",
                ExpireTimeSpan = TimeSpan.FromMinutes(10),
                SlidingExpiration = true
            });

            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "oidc",
                SignInAsAuthenticationType = "Cookies",
                Authority = "http://localhost:5000",
                ClientId = "webforms",
                RedirectUri = "http://localhost:5969/signin-oidc",
                PostLogoutRedirectUri = "http://localhost:5969/signout-callback-oidc",
                ResponseType = "id_token token",
                Scope = "openid profile api1",
                UseTokenLifetime = false,
                Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    SecurityTokenValidated = OnSecurityTokenValidated,
                    RedirectToIdentityProvider = OnRedirectToIdentityProvider
                }
            });
            app.UseStageMarker(PipelineStage.Authenticate);
        }

        private async Task OnSecurityTokenValidated(
            SecurityTokenValidatedNotification<OpenIdConnectMessage,
                OpenIdConnectAuthenticationOptions> n)
        {
            var claimsToExclude = new[]
            {
                "aud", "iss", "nbf", "exp", "nonce", "iat", "at_hash"
            };
            var claimsToKeep =
                n.AuthenticationTicket.Identity.Claims
                    .Where(x => false == claimsToExclude.Contains(x.Type)).ToList();
            claimsToKeep.Add(new Claim("id_token", n.ProtocolMessage.IdToken));

            if (n.ProtocolMessage.AccessToken != null)
            {
                claimsToKeep.Add(new Claim("access_token", n.ProtocolMessage.AccessToken));

                var userInfoClient = 
                    new UserInfoClient(
                        new Uri("http://localhost:5000"), 
                        n.ProtocolMessage.AccessToken);
                var userInfoResponse = await userInfoClient.GetAsync();
                var userInfoClaims = userInfoResponse.Claims
                    .Where(x => x.Item1 != "sub") // filter sub since we're already getting it from id_token
                    .Select(x => new Claim(x.Item1, x.Item2));
                claimsToKeep.AddRange(userInfoClaims);
            }

            var ci = new ClaimsIdentity(
                n.AuthenticationTicket.Identity.AuthenticationType,
                "name", "role");
            ci.AddClaims(claimsToKeep);

            n.AuthenticationTicket = new Microsoft.Owin.Security.AuthenticationTicket(
                ci, n.AuthenticationTicket.Properties);
        }

        private static async Task<int> OnRedirectToIdentityProvider(
            RedirectToIdentityProviderNotification<OpenIdConnectMessage,
                OpenIdConnectAuthenticationOptions> n)
        {
            if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
            {
                var idToken = n.OwinContext.Authentication.User.FindFirst("id_token")?.Value;
                n.ProtocolMessage.IdTokenHint = idToken;
            }
            return 0;
        }
    }
}
