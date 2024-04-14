using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                   new Client
                   {
                        ClientId = "bookingClient",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedScopes = { "booking_car_api" }
                   },
                   new Client
                   {
                       ClientId = "appBookingCarMVC",                     
                       AllowedGrantTypes = GrantTypes.Code,
                       ClientSecrets = new List<Secret>
                       {
                           new Secret("secret".Sha256())
                       },
                       RedirectUris = new List<string>()
                       {
                           "https://localhost:44360/signin-oidc"
                       },
                       PostLogoutRedirectUris = new List<string>()
                       {
                           "https://localhost:44360/signout-callback-oidc"
                       },
                       
                       AllowedScopes = new List<string>
                       {
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,                         
                           "booking_car_api"
                       }
                   }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("booking_car_api", "Booking Car API")
           };

        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[]
          {
               new ApiResource("bookingCarAPI", "Booking Car API")
          };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
    }
}
