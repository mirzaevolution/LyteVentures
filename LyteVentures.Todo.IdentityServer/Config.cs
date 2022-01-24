// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace LyteVentures.Todo.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                        new IdentityResources.OpenId(),
                        new IdentityResources.Profile(),
                        new IdentityResources.Email(),
                   };

        public static IEnumerable<ApiScope> ApiScopes(IConfiguration configuration) =>
            new ApiScope[]
            {
                new ApiScope(configuration["TodoApi:Scope"]),
            };

        public static IEnumerable<ApiResource> ApiResources(IConfiguration configuration) =>
            new ApiResource[]
            {
                new ApiResource(configuration["TodoApi:Audience"], "Todo Schedule Api")
                {
                    Scopes =
                    {
                        configuration["TodoApi:Scope"]
                    },
                    UserClaims =
                    {
                        "name",
                        "email"
                    }
                }
            };

        public static IEnumerable<Client> Clients(IConfiguration configuration) =>
            new Client[]
            {
              // swagger client
                new Client
                {
                    ClientId = configuration["TodoApi:ClientId"],
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret(configuration["TodoApi:ClientSecret"].Sha256()) },

                    AllowedScopes =
                    {
                        OidcConstants.StandardScopes.OpenId,
                        OidcConstants.StandardScopes.Profile,
                        OidcConstants.StandardScopes.Email,
                        OidcConstants.StandardScopes.OfflineAccess,
                         configuration["TodoApi:Scope"]
                    },
                    AllowOfflineAccess = true,
                    RedirectUris =
                    {

                        $"{configuration["TodoWebClient:BaseAddress"]}/signin-oidc",
                        $"{configuration["TodoApi:BaseAddress"]}/swagger/oauth2-redirect.html",
                    },
                     PostLogoutRedirectUris =
                    {
                       $"{configuration["TodoWebClient:BaseAddress"]}/signout-callback-oidc"
                    },
                    AllowedCorsOrigins =
                    {
                        configuration["TodoApi:BaseAddress"]
                    }
                },
            };
    }
}