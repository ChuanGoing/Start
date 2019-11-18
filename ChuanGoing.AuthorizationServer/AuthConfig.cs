using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace ChuanGoing.AuthorizationServer
{
    public class AuthConfig
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var res = new ApiResource("WebApi", "ChuanGoingWebApi");
            //res.ApiSecrets.Add(new Secret("ApiSecret"));
            return new List<ApiResource>
            {
                res
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            var List = new List<Client>
            {
                new Client()
                {
                    ClientId = "ClientCredentials",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("ClientSecret".Sha256()) },
                    AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "WebApi"
                }
                },

                new Client()
                {
                    ClientId = "ResourceOwnerPassword",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("ClientSecret".Sha256()) },
                    AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "WebApi"
                }
                },
                new Client()
                {
                    ClientId = "Implicit",
                    ClientName = "ImplicitClient",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    ClientSecrets = { new Secret("ClientSecret".Sha256()) },
                    RedirectUris = { "http://localhost:5020/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5020" },
                    AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "WebApi"
                }
                }
            };
            return List;
        }

        //测试用户
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "admin",
                    Password = "123456",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "admin"),
                        new Claim("website", "https://www.cnblogs.com/chuangoing")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "chuangoing",
                    Password = "123456",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "chuangoing"),
                        new Claim("website", "https://github.com/chuangoing")
                    }
                }
            };
        }
    }
}
