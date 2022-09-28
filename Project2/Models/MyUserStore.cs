using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Project2.Models
{
    public class MyUserStore : IUserStore<MyUser>
    {
        private readonly MyUserService myUserService;
        public Task<IdentityResult> CreateAsync(MyUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(MyUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public Task<MyUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.FromResult(new MyUser());
        }

        public Task<MyUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {

            return Task.FromResult(new MyUser());
        }

        public Task<string> GetNormalizedUserNameAsync(MyUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(MyUser user, CancellationToken cancellationToken)
        {
            
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedUserNameAsync(MyUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(MyUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(MyUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
