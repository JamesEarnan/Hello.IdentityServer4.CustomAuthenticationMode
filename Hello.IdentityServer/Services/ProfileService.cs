using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hello.IdentityServer.Services
{
    public class ProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims = GetUserClaims().ToList();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }

        public Claim[] GetUserClaims()
        {
            return new Claim[]
            {
            new Claim("user_id", "test"),
            new Claim(JwtClaimTypes.Name, "Name"),
            new Claim(JwtClaimTypes.Email, "Email"  ?? ""),

            //roles
            new Claim(JwtClaimTypes.Role, "Role")
            };
        }
    }
}
