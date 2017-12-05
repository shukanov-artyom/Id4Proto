using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using QuickstartIdentityServer.DomainServices;

namespace QuickstartIdentityServer.IdentityServices
{
    public class ResourceOwnerPasswordValidator :
        IResourceOwnerPasswordValidator
    {
        private readonly IUserService userService;

        public ResourceOwnerPasswordValidator(
            IUserService userService)
        {
            this.userService = userService;
        }

        public async Task ValidateAsync(
            ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user = await userService.FindAsync(context.UserName);
                if (user != null)
                {
                    if (user.Password == context.Password)
                    {
                        context.Result = new GrantValidationResult(
                            subject: user.SubjectId,
                            authenticationMethod: "custom",
                            claims: GetUserClaims(user, context));
                        return;
                    }

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }

        private IEnumerable<Claim> GetUserClaims(
            TestUser user,
            ResourceOwnerPasswordValidationContext context)
        {
            string clientId = context.Request.Client.ClientId;
            return new Claim[]
            {
                new Claim("user_id", user.SubjectId),
                new Claim(user.Username, user.Username),
                new Claim("address", "SomeAddressHere")
            };
        }
    }
}
