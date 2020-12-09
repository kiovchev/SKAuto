namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> repository;

        public UserService(IRepository<ApplicationUser> repository)
        {
            this.repository = repository;
        }

         public async Task<bool> ChangeUserIsAuthorizeSatusByUserIdAsync(string userId, string value)
        {
            var users = await this.repository.All().ToListAsync();
            var user = users.SingleOrDefault(x => x.Id == userId);

            bool.TryParse(value, out bool result);

            user.IsAuthorize = result;
            await this.repository.SaveAsync();

            return true;
        }

        public async Task<bool?> GetAuthorizeStatusByUserNameAsync(string userName)
        {
            var users = await this.repository.All().ToListAsync();
            var user = users.SingleOrDefault(x => x.UserName == userName);

            if (user == null)
            {
                return null;
            }

            return user.IsAuthorize;
        }

        public async Task<int> GetCountOfUsers()
        {
            var users = await this.repository.All().ToListAsync();
            int count = users.Count();

            return count;
        }
    }
}
