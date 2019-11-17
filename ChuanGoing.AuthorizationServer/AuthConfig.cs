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
            return new List<ApiResource>
            {
                new ApiResource("WebApi", "ChuanGoingWebApi")
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
            return new List<Client>
        {
            new Client()
            {
                ClientId="ClientId",
                AllowedGrantTypes=GrantTypes.Implicit,
                ClientSecrets={ new Secret("ClientSecret".Sha256())},
                AllowedScopes=
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "WebApi"
                }
            }
        };
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
