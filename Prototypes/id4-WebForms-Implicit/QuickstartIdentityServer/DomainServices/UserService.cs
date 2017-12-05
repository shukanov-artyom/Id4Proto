using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Test;

namespace QuickstartIdentityServer.DomainServices
{
    public class UserService : IUserService
    {
        public async Task<TestUser> FindAsync(string userName)
        {
            var user = GetUsers().FirstOrDefault(u => u.Username == userName);
            return user;
        }

        public async Task<TestUser> FindAsync(int userId)
        {
            var user = GetUsers().FirstOrDefault(u => Int32.Parse(u.SubjectId) == userId);
            return user;
        }

        private IEnumerable<TestUser> GetUsers()
        {
                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "1",
                        Username = "alice",
                        Password = "alice",
                        Claims = new List<Claim>
                        {
                            new Claim("name", "Alice"),
                            new Claim("email", "alice@gmail.com"),
                            new Claim("address", "Minsk"),
                            new Claim("website", "https://alice.com")
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "2",
                        Username = "bob",
                        Password = "bob",

                        Claims = new List<Claim>
                        {
                            new Claim("name", "Bob"),
                            new Claim("email", "bob@gmail.com"),
                            new Claim("address", "Mazyr"),
                            new Claim("website", "https://bob.com")
                        }
                    }
                };
        }
    }
}
