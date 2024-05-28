using IdentityServer.Data;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Customs
{
    public class CustomProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            var claims = new List<Claim>
        {
            new Claim("FullName", user.FullName),
            new Claim("UserName", user.UserName),
            new Claim("Id", user.Id),
            //new Claim("OId", user.OId == null ? "" : user.OId.ToString()),
            new Claim("Role", string.Join(",", await _userManager.GetRolesAsync(user)))
        };

            context.IssuedClaims.AddRange(claims);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            // Implement your logic to determine if the user is active
            return Task.CompletedTask;
        }
    }
}
