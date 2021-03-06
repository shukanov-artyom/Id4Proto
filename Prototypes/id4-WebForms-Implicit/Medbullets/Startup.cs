﻿using System;
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

[assembly: OwinStartup(typeof(WebApp.Startup))]

namespace WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string userInfoEndpoint = 
                "https://localhost:44336/connect/userinfo";

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "CookiesMedbullets",
                ExpireTimeSpan = TimeSpan.FromMinutes(10),
                SlidingExpiration = true
            });

            JwtSecurityTokenHandler.InboundClaimTypeMap.Clear();

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "oidc",
                SignInAsAuthenticationType = "CookiesMedbullets",
                Authority = "https://localhost:44336/",
                ClientId = "implicit.medbullets",
                RedirectUri = "http://localhost:5969/signin-oidc",
                PostLogoutRedirectUri = "http://localhost:5969/",
                ResponseType = "id_token token",
                Scope = "openid profile email profile.agent",
                UseTokenLifetime = false,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var claimsToExclude = new[]
                        {
                            "aud", "iss", "nbf", "exp", "nonce", "iat", "at_hash"
                        };

                        var claimsToKeep =
                            n.AuthenticationTicket.Identity.Claims
                            .Where(x => !claimsToExclude.Contains(x.Type))
                            .ToList();
                        claimsToKeep.Add(new Claim("id_token", n.ProtocolMessage.IdToken));

                        if (n.ProtocolMessage.AccessToken != null)
                        {
                            claimsToKeep.Add(new Claim("access_token", n.ProtocolMessage.AccessToken));

                            var userInfoClient = new UserInfoClient(
                                new Uri(userInfoEndpoint),
                                n.ProtocolMessage.AccessToken); // Leverage Implicit flow we request User claims from endpoint usina access token!
                            // But access token came through front channel!
                            var userInfoResponse = await userInfoClient.GetAsync();
                            var userInfoClaims = userInfoResponse.Claims
                                .Where(x => x.Item1 != "sub") // filter sub since we're already getting it from id_token
                                .Select(x => new Claim(x.Item1, x.Item2));
                            claimsToKeep.AddRange(userInfoClaims);
                        }

                        var claimsIdentity = new ClaimsIdentity(
                            n.AuthenticationTicket.Identity.AuthenticationType,
                            "name", "role");
                        claimsIdentity.AddClaims(claimsToKeep);

                        n.AuthenticationTicket = new Microsoft.Owin.Security.AuthenticationTicket(
                            claimsIdentity, n.AuthenticationTicket.Properties
                        );
                    },
                    RedirectToIdentityProvider = n =>
                    {
                        if (n.ProtocolMessage.RequestType ==
                            OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idToken = n.OwinContext.Authentication
                                .User.FindFirst("id_token")?.Value;
                            n.ProtocolMessage.IdTokenHint = idToken;
                        }
                        return Task.FromResult(0);
                    }
                }
            });
            app.UseStageMarker(PipelineStage.Authenticate);
        }
    }
}
