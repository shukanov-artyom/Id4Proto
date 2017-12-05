using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace QuickstartIdentityServer
{
    public static class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                // currently nonfunctional but still present for sample
                new ApiResource("api1", "My API")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Medbullets",
                    ClientId = "implicit.medbullets",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    // TODO : investigate and get rid of passing access tokens to front channel
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email
                    },

                    ClientUri = "https://medbullets.io",

                    RequireConsent = false,
                    AllowRememberConsent = true,
                    FrontChannelLogoutUri = "http://localhost:5969/logout.aspx",
                    RedirectUris = new List<string>
                    {
                        "http://localhost:5969/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:5969/"
                    }
                },
                new Client
                {
                    ClientName = "Orthobullets",
                    ClientId = "implicit.orthobullets",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    // TODO : investigate and get rid of passing access tokens to front channel
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address
                    },

                    ClientUri = "https://orthobullets.io",

                    RequireConsent = false,
                    AllowRememberConsent = true,
                    FrontChannelLogoutUri = "http://localhost:5970/logout.aspx",
                    RedirectUris = new List<string>
                    {
                        "http://localhost:5970/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:5970/"
                    }
                },
                new Client
                {
                    ClientName = "Consolidated Platform",
                    ClientId = "implicit.cp",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    // TODO : investigate and get rid of passing access tokens to front channel
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address
                    },

                    ClientUri = "https://cp.io",

                    RequireConsent = false,
                    AllowRememberConsent = true,
                    FrontChannelLogoutUri = "http://local.orthobullets.com/logout.aspx",
                    RedirectUris = new List<string>
                    {
                        "http://local.orthobullets.com/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://local.orthobullets.com/"
                    }
                }
            };
        }
    }
}