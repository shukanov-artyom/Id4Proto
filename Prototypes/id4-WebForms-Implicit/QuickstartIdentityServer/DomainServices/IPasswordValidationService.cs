using System;

namespace QuickstartIdentityServer.DomainServices
{
    public interface IPasswordValidationService
    {
        bool IsPasswordValid(
            string password,
            string savedUserPassword,
            string userGuidString);
    }
}
