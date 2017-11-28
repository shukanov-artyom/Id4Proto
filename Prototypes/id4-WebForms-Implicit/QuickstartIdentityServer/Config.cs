﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace QuickstartIdentityServer
{
    public class Config
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
                    ClientId = "webforms.owin.implicit.medbullets",
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

                    ClientUri = "https://identityserver.io",

                    RequireConsent = false,
                    AllowRememberConsent = true,
                    FrontChannelLogoutUri = "http://localhost:5969/logout.aspx",
                    RedirectUris = new List<string>
                    {
                        "http://localhost:5969/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:5969/"
                    }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "alice",
                    
                    Claims = new List<Claim>
                    {
                        new Claim("name", "Alice"),
                        new Claim("email", "alice@gmail.com"),
                        new Claim("address", "Minsk"),
                        new Claim("website", "https://alice.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "bob",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Bob"),
                        new Claim("email", "bob@gmail.com"),
                        new Claim("address", "Mazyr"),
                        new Claim("website", "https://bob.com")
                    }
                }
            };
        }
    }
}