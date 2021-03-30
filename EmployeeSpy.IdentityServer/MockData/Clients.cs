using System.Collections.Generic;
using IdentityServer4.Models;

namespace EmployeeSpy.IdentityServer.MockData
{
    internal static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "oauthClient",
                    ClientName = "Example client application using client credentials",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = new List<Secret> { new Secret("SuperSecretPassword".Sha256()) }, // change me!
                    AllowedScopes = new List<string> { "api1.read" },
                    RequireClientSecret = false,
                },
            };
        }
    }
}
