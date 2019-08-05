namespace SKAuto.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;

    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> repository;

        public UserService(IRepository<ApplicationUser> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> ChangeUserIsAuthorizeSatusByUserIdAsync(string userId, string value)
        {
            var user = await this.repository.All().SingleOrDefaultAsync(x => x.Id == userId);

            bool.TryParse(value, out bool result);

            user.IsAuthorize = result;
            await this.repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool?> GetAuthorizeStatusByUserNameAsync(string userName)
        {
            var user = await this.repository.All().SingleOrDefaultAsync(x => x.UserName == userName);

            if (user == null)
            {
                return null;
            }

            return user.IsAuthorize;
        }

        public async Task<int> GetCountOfUsers()
        {
            int count = await this.repository.All().CountAsync();

            return count;
        }
    }
}
