using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using QuickstartIdentityServer.DomainServices;

namespace QuickstartIdentityServer.IdentityServices
{
    public class ProfileService : IProfileService
    {
        private readonly IUserService userService;

        public ProfileService(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            int userId = Int32.Parse(context.Subject.GetSubjectId());
            TestUser user = await userService.FindAsync(userId);
            context.AddRequestedClaims(user.Claims);
            context.IssuedClaims.Add(new Claim("role", "pepe"));
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true; // TODO : some good validation maybe?
            return Task.CompletedTask;
        }
    }
}
