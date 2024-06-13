using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

namespace Airline.WebClient;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("airline", "Airline Server"),
            new ApiScope(name: "read", displayName: "Read your data."),
            new ApiScope(name: "write", displayName: "Write your data."),
            new ApiScope(name: "delete", displayName: "Delete your data.")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "service.client",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "api1", "api2.read_only" }
            },
            new Client
            {
                ClientId = "airline",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes =
                {
                    "airline",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email
                },
                RedirectUris = { "https://localhost:7139/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:7139/signout-callback-oidc" }
            }
        };
}

public static class TestUsers
{
    public static List<TestUser> Users =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "alice",
                Password = "password",
                Claims =
                {
                    new System.Security.Claims.Claim("name", "Alice"),
                    new System.Security.Claims.Claim("website", "https://alice.com")
                }
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "bob",
                Password = "password",
                Claims =
                {
                    new System.Security.Claims.Claim("name", "Bob"),
                    new System.Security.Claims.Claim("website", "https://bob.com")
                }
            }
        };
}
