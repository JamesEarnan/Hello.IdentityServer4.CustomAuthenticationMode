using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hello.IdentityServer.Services
{
    public class ProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = context.Subject;

            context.IssuedClaims = GetUserClaims().ToList();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = context.Subject;

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
