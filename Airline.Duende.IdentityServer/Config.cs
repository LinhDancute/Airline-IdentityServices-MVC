using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;

namespace Airline.Duende.IdentityServer;

public static class Config
{
    public const string Administrator = "Administrator";
    public const string Manager = "Manager";
    public const string Member = "Member";

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
        new ApiScope(name: "read",   displayName: "Read your data."),
        new ApiScope(name: "write",  displayName: "Write your data."),
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
            ClientId = "airline_webclient",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.Code,
            AllowedScopes = { "airline",
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                JwtClaimTypes.Role
            },
            RedirectUris = { "https://localhost:7000/signin-oidc" },
            PostLogoutRedirectUris = { "https://localhost:7000/signout-callback-oidc" },
            AllowOfflineAccess = true
        }
    };

}
