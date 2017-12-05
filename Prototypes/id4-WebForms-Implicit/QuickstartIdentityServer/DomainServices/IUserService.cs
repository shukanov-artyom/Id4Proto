using System;
using System.Threading.Tasks;
using IdentityServer4.Test;

namespace QuickstartIdentityServer.DomainServices
{
    public interface IUserService
    {
        Task<TestUser> FindAsync(string userName);

        Task<TestUser> FindAsync(int userId);
    }
}
