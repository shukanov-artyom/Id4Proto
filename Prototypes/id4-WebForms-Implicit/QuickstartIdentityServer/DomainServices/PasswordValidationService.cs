using System;

namespace QuickstartIdentityServer.DomainServices
{
    public class PasswordValidationService :
        IPasswordValidationService
    {
        public bool IsPasswordValid(
            string password,
            string savedUserPassword,
            string userGuidString)
        {
            // TODO : hashing here!
            return password == savedUserPassword;
        }
    }
}
