using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hello.IdentityServer.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //get your user model from db (by username - in my case its email)


                if (context.UserName=="test" && context.Password == "test")
                {
                    context.Result = new GrantValidationResult(
                          subject: context.UserName,
                          authenticationMethod: "custom"
                          ,claims: GetUserClaims()
                          );
                    return;
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                }

                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
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
